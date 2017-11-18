//
//  Communicator.m
//  GCG
//
//  Created by Chris Mitroka on 4/1/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "Communicator.h"
#import "DataAccess.h"

@interface Communicator()
-(void)waitForResp:(int)secTimeout retryAmnt:(int)retryAmnt;
-(void)sendRequest:(NSString *)requestToSend operationToDo:(NSString *)operation;
-(NSString *)GetValueBetweenTags:(NSString *)AllContent StartTag:(NSString *)StartTag EndTag:(NSString *)EndTag;
@end
@implementation Communicator
@synthesize currentXMLResp,currentXMLStatus,webDataReceived;
DataAccess *da;
                                                                                 
-(void)DoGenericRequest:(NSString *)operation bodyData:(NSString *)bodyData secTimeout:(int)secTimeout retryAmnt:(int)retryAmnt
{
    currentXMLResp=@"";
	requestXML = @"<?xml version=\"1.0\" encoding=\"utf-8\"?>\n"
    "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">\n"
    "<soap:Body>\n"
    "<";
	requestXML = [requestXML stringByAppendingString:operation];
    requestXML = [requestXML stringByAppendingString:@" xmlns=\"gcg.mc2techservices.com\">\n"];
	requestXML = [requestXML stringByAppendingString:bodyData];
    requestXML = [requestXML stringByAppendingString:@"</"];
    requestXML = [requestXML stringByAppendingString:operation];
    requestXML = [requestXML stringByAppendingString:@">\n"
                   "</soap:Body>\n"
                  "</soap:Envelope>\n"];
    requestOperation=operation;
    [self sendRequest:requestXML operationToDo:requestOperation];
    [self waitForResp:secTimeout retryAmnt:retryAmnt];
    NSLog(@"Communicator DoGenericRequest Finished");
}



-(void)waitForResp:(int)secTimeout retryAmnt:(int)retryAmnt
{
    int secCount=0;
    int retryCount=0;
    do {
       if (secCount==secTimeout) {
           if (retryCount==retryAmnt)
           {
               //things just aren't working...
               break;
           }
           secCount=0;
           retryCount++;
           [self sendRequest:requestXML operationToDo:requestOperation];
       }
       NSDate *MyTargetDateObject = [NSDate dateWithTimeIntervalSinceNow:0];  //add 0 seconds
       NSDate *now = [NSDate date];
       NSDateComponents *dc = [[NSDateComponents alloc] init];
       [dc setSecond:1];
       MyTargetDateObject = [[NSCalendar currentCalendar] dateByAddingComponents:dc toDate:now options:1];
        //[self performSelector:@selector(waitForResp2) withObject:self afterDelay:5.0];
        //[self performSelector:@selector(waitForResp2:retryAmnt:) withObject:nil afterDelay:5.0];
        //sleep(2);
        if (currentXMLStatus == @"Error")
        {
            break;
        }
        else
        {
            if (currentXMLStatus != @"Error") [[NSRunLoop currentRunLoop] runUntilDate:MyTargetDateObject];
        }
        if (currentXMLResp!=@"")
       {
           break;
       }
       else
       {
           secCount++;
       }
       NSLog(@"Waiting for response...");
    } while (currentXMLResp==@"");
    //NSLog(currentXMLResp,NULL);
    NSLog(@"waitForResp Done");
}

-(void)sendRequest:(NSString *)requestToSend operationToDo:(NSString *)operation
{
	//NSLog(requestToSend,nil);
    //NSURL *url = [NSURL URLWithString:@"http://192.168.0.186/AdminSite/WebService.asmx"];
	NSURL *url = [NSURL URLWithString:@"https://gcg.mc2techservices.com/WebService.asmx"];
	NSMutableURLRequest *theRequest = [NSMutableURLRequest requestWithURL:url];
	NSString *msgLength = [NSString stringWithFormat:@"%d", [requestToSend length]];
    NSString *temp1=[NSString stringWithFormat:@"%@/%@", @"gcg.mc2techservices.com", operation];
	//NSLog(temp1,nil);f
	[theRequest addValue: @"text/xml; charset=utf-8" forHTTPHeaderField:@"Content-Type"];
	[theRequest addValue: temp1 forHTTPHeaderField:@"SOAPAction"];
	[theRequest addValue: msgLength forHTTPHeaderField:@"Content-Length"];
	[theRequest setHTTPMethod:@"POST"];
	[theRequest setHTTPBody: [requestToSend dataUsingEncoding:NSUTF8StringEncoding]]; //NSUTF8StringEncoding NSASCIIStringEncoding
    NSURLConnection *theConnection = [[NSURLConnection alloc] initWithRequest:theRequest delegate:self];
    theConnection.accessibilityHint=@"";
	/*
	if( theConnection )
	{
		pWebData = [NSMutableData data];
	}
	else
	{
		NSLog(@"theConnection is NULL");
	}
    */
}
- (BOOL)connection:(NSURLConnection *)connection canAuthenticateAgainstProtectionSpace:(NSURLProtectionSpace *)protectionSpace {
    return [protectionSpace.authenticationMethod isEqualToString:NSURLAuthenticationMethodServerTrust];
}

- (void)connection:(NSURLConnection *)connection didReceiveAuthenticationChallenge:(NSURLAuthenticationChallenge *)challenge 
    {
        NSArray *trustedHosts=[NSArray arrayWithObject:@"gcg.mc2techservices.com"];
        if ([challenge.protectionSpace.authenticationMethod isEqualToString:NSURLAuthenticationMethodServerTrust])
            if ([trustedHosts containsObject:challenge.protectionSpace.host])
                [challenge.sender useCredential:[NSURLCredential credentialForTrust:challenge.protectionSpace.serverTrust] forAuthenticationChallenge:challenge];
        
        [challenge.sender continueWithoutCredentialForAuthenticationChallenge:challenge];
}
-(void)connection:(NSURLConnection *)connection didReceiveResponse:(NSURLResponse *)response
{
	NSLog(@"Connection didReceiveResponse");
    webDataReceived = [[NSMutableData alloc] initWithLength:0];
	[webDataReceived setLength: 0];
}
-(void)connection:(NSURLConnection *)connection didReceiveData:(NSData *)data
{
    NSLog(@"Connection didReceiveData");
	[webDataReceived appendData:data];
}
-(void)connection:(NSURLConnection *)connection didFailWithError:(NSError *)error
{
    NSLog(@"Connection didFailWithError");
    NSLog([error localizedDescription],NULL);
    NSLog([error localizedFailureReason],NULL);
    currentXMLStatus = @"Error";
    currentXMLResp=@"";
}
-(void)connectionDidFinishLoading:(NSURLConnection *)connection
{
	//NSLog(@"connectionDidFinishLoading");
	//NSLog(@"DONE. Received Bytes: %d", [webDataReceived length]);
	//NSString *theXML = [[NSString alloc] initWithBytes: [webDataReceived mutableBytes] length:[webDataReceived length] encoding:NSUTF8StringEncoding];
    NSString *strData1= [[NSString alloc] initWithData:webDataReceived encoding:NSASCIIStringEncoding];
    int test=strData1.length;
	NSString *theXML = [[NSString alloc] initWithBytes:[webDataReceived mutableBytes] length:[webDataReceived length] encoding:NSASCIIStringEncoding];
    da=[DataAccess da];
    NSString *StartTag=[NSString stringWithFormat:@"<%@Result>", requestOperation];
    NSString *EndTag=[NSString stringWithFormat:@"</%@Result>", requestOperation];
    NSString *retVal=[self GetValueBetweenTags:theXML StartTag:StartTag EndTag:EndTag];
    NSLog([NSString stringWithFormat:@"Communicator Done - XML Response for %@: %@", requestOperation, currentXMLResp],nil);
}

-(NSString *)GetValueBetweenTags:(NSString *)AllContent StartTag:(NSString *)StartTag EndTag:(NSString *)EndTag
{
    NSString *retVal;
    //AllContent=[da pmGetSetting:@"TempResp"];
    //NSLog(AllContent);

    NSRange startMarker = [AllContent rangeOfString:StartTag];
    NSRange endMarker = [AllContent rangeOfString:EndTag];
    NSLog(AllContent);
	NSMutableString *immutableString = [[NSMutableString alloc] initWithFormat:AllContent];
	@try {
        retVal = [AllContent substringWithRange: NSMakeRange (startMarker.location+StartTag.length, endMarker.location-(startMarker.location+StartTag.length))];
		retVal = [immutableString substringWithRange: NSMakeRange (startMarker.location+StartTag.length, endMarker.location-(startMarker.location+StartTag.length))];
		currentXMLStatus = @"OK";
        currentXMLResp=retVal;
	}
	@catch (NSException * e) {
        NSString *errmg=@"Error";
        NSLog(@"uncaughtExceptionHnadler -- Exception %@", [e description]);
		currentXMLStatus = errmg;
        currentXMLResp=@"";
	}
	return currentXMLResp;
}
- (NSString *)xmlHTMLUnencodeString:(NSString *)NSStringIn
{
    NSMutableString *escapeStr = [NSMutableString stringWithString:NSStringIn];    
    [escapeStr replaceOccurrencesOfString:@"&amp;"  withString:@"&"  options:NSLiteralSearch range:NSMakeRange(0, [escapeStr length])];
    [escapeStr replaceOccurrencesOfString:@"&quot;" withString:@"\"" options:NSLiteralSearch range:NSMakeRange(0, [escapeStr length])];
    [escapeStr replaceOccurrencesOfString:@"&#x27;" withString:@"'"  options:NSLiteralSearch range:NSMakeRange(0, [escapeStr length])];
    [escapeStr replaceOccurrencesOfString:@"&#x39;" withString:@"'"  options:NSLiteralSearch range:NSMakeRange(0, [escapeStr length])];
    [escapeStr replaceOccurrencesOfString:@"&#x92;" withString:@"'"  options:NSLiteralSearch range:NSMakeRange(0, [escapeStr length])];
    [escapeStr replaceOccurrencesOfString:@"&#x96;" withString:@"'"  options:NSLiteralSearch range:NSMakeRange(0, [escapeStr length])];
    [escapeStr replaceOccurrencesOfString:@"&gt;"   withString:@">"  options:NSLiteralSearch range:NSMakeRange(0, [escapeStr length])];
    [escapeStr replaceOccurrencesOfString:@"&lt;"   withString:@"<"  options:NSLiteralSearch range:NSMakeRange(0, [escapeStr length])];
    NSString *retVal = [NSString stringWithString:escapeStr];
    return retVal;}

- (NSString *)xmlHTMLEncodeString:(NSString *)NSStringIn
{
    NSMutableString *escapeStr = [NSMutableString stringWithString:NSStringIn];    
    [escapeStr replaceOccurrencesOfString:@"&"  withString:@"&amp;"  options:NSLiteralSearch range:NSMakeRange(0, [escapeStr length])];
    [escapeStr replaceOccurrencesOfString:@"\"" withString:@"&quot;" options:NSLiteralSearch range:NSMakeRange(0, [escapeStr length])];
    [escapeStr replaceOccurrencesOfString:@"'"  withString:@"&#x27;" options:NSLiteralSearch range:NSMakeRange(0, [escapeStr length])];
    [escapeStr replaceOccurrencesOfString:@">"  withString:@"&gt;"   options:NSLiteralSearch range:NSMakeRange(0, [escapeStr length])];
    [escapeStr replaceOccurrencesOfString:@"<"  withString:@"&lt;"   options:NSLiteralSearch range:NSMakeRange(0, [escapeStr length])];
    NSString *retVal = [NSString stringWithString:escapeStr];
    return retVal;
}
- (id)init {
    return self;
}
@end
