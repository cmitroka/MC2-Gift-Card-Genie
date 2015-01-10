//
//  Communicator4.m
//  NAVCON01
//
//  Created by Chris on 9/27/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "Communicator4.h"
#import "CJMUtilities.h"
#import "DataAccess.h"
@interface Communicator4()
-(void)sendRequest:(NSString *)requestToSend operationToDo:(NSString *)operation;
@end

@implementation Communicator4
NSMutableData *webData;
NSString *requestOperation;
NSXMLParser *xmlParser;
NSMutableString *soapResults;
bool elementFound;
-(NSString *)pmDoGenericRequest:(NSString *)operation bodyData:(NSString *)bodyData
{
    requestOperation=[NSString stringWithFormat:@"%@Result", operation];
    DataAccess *da=[DataAccess da];
    [da pmUpdateSetting:requestOperation svalue:@""];
	NSString *requestXML = @"<?xml version=\"1.0\" encoding=\"utf-8\"?>\n"
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
    [self sendRequest:requestXML operationToDo:operation];
    return requestOperation;
}

-(void)sendRequest:(NSString *)requestToSend operationToDo:(NSString *)operation
{
    NSURL *url = [NSURL URLWithString:@"https://gcg.mc2techservices.com/WebService.asmx"];
	NSMutableURLRequest *theRequest = [NSMutableURLRequest requestWithURL:url];
	NSString *msgLength = [NSString stringWithFormat:@"%d", [requestToSend length]];
    NSString *temp1=[NSString stringWithFormat:@"%@/%@", @"gcg.mc2techservices.com", operation];
	//NSLog(temp1,nil);
	[theRequest addValue: @"text/xml; charset=utf-8" forHTTPHeaderField:@"Content-Type"];
	[theRequest addValue: temp1 forHTTPHeaderField:@"SOAPAction"];
	[theRequest addValue: msgLength forHTTPHeaderField:@"Content-Length"];
	[theRequest setHTTPMethod:@"POST"];
	[theRequest setHTTPBody: [requestToSend dataUsingEncoding:NSUTF8StringEncoding]]; //
    NSURLConnection *theConnection = [[NSURLConnection alloc] initWithRequest:theRequest delegate:self];
    
    if( theConnection )
    {
        webData = [NSMutableData data];
    }
    else
    {
        NSLog(@"theConnection is NULL");
    }
}
-(void)connection:(NSURLConnection *)connection didReceiveResponse:(NSURLResponse *)response
{
    [webData setLength: 0];
}
-(void)connection:(NSURLConnection *)connection didReceiveData:(NSData *)data
{
    [webData appendData:data];
}
-(void)connection:(NSURLConnection *)connection didFailWithError:(NSError *)error
{
    NSLog(@"ERROR with theConenction");
}
-(void)connectionDidFinishLoading:(NSURLConnection *)connection
{
    NSLog(@"DONE. Received Bytes: %d", [webData length]);
    NSString *theXML = [[NSString alloc] initWithBytes: [webData mutableBytes] length:[webData length] encoding:NSUTF8StringEncoding];
    NSLog(@"%@",theXML);
    if(xmlParser)
    {
    }
    xmlParser = [[NSXMLParser alloc] initWithData: webData];
    [xmlParser setDelegate:self];
    [xmlParser setShouldResolveExternalEntities:YES];
    [xmlParser parse];
}
//---when the start of an element is found---
-(void) parser:(NSXMLParser *) parser didStartElement:(NSString *) elementName namespaceURI:(NSString *) namespaceURI qualifiedName:(NSString *) qName attributes:(NSDictionary *)attributeDict {
    
    if( [elementName isEqualToString:requestOperation])
    {
        if (!soapResults)
        {
            soapResults = [[NSMutableString alloc] init];
        }
        elementFound = YES;
    }
}
-(void)parser:(NSXMLParser *) parser foundCharacters:(NSString *)string
{
    if (elementFound)
    {
        [soapResults appendString: string];
    }
}
//---when the end of element is found---

-(void)parser:(NSXMLParser *)parser
didEndElement:(NSString *)elementName
 namespaceURI:(NSString *)namespaceURI
qualifiedName:(NSString *)qName
{
    if ([elementName isEqualToString:requestOperation])
    {
        NSLog(@"%@",soapResults);
        /*
        UIAlertView *alert = [[UIAlertView alloc]
                              initWithTitle:@"Fahrenheit Value!"
                              message:soapResults
                              delegate:self
                              cancelButtonTitle:@"OK"
                              
                              otherButtonTitles:nil];
        
        [alert show];
        */
        DataAccess *da=[DataAccess da];
        NSString *temp=[CJMUtilities ConvertNSMutableStringToNSString:soapResults];
        //NSLog(temp);
        [da pmUpdateSetting:requestOperation svalue:temp];
        [soapResults setString:@""];
        elementFound = FALSE;
    }
    
}
@end
