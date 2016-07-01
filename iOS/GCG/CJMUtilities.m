//
//  CJMUtilities.m
//  GCG
//
//  Created by Chris on 2/2/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//
#import "SFHFKeychainUtils.h"
#import "CJMUtilities.h"

//subString2=[subString2 stringByReplacingOccurrencesOfString:@"%" withString:@""];
//int asciiCode = [strData1 characterAtIndex:1627]; // 65
//NSString *stringConv = [NSString stringWithFormat:@"%c", asciiCode];


@implementation CJMUtilities
+(void)ShowOfflineAlert
{
    [self ShowAlert:@"In Offline Mode" Message:@"This feature requires the app to be in online mode.  You may want to restart the app." ButtonText:@"OK"];
}
+(void)ShowOOLAlert
{
    [self ShowAlert:@"Out of Lookups" Message:@"You're out of lookups! Please consider getting some more with a purchase or by watching an ad." ButtonText:@"OK"];
}

+(NSString *)GetGeneratedUUID
{
    CFUUIDRef uuid = CFUUIDCreate(NULL);
    NSString *uuidStr = (__bridge_transfer NSString *)CFUUIDCreateString(NULL, uuid);
    CFRelease(uuid);
    return uuidStr;
}
+(NSString *)GetAppStatus
{
    NSString *appStatus=nil;
    appStatus=[SFHFKeychainUtils pmGetValueForSetting:@"AppStatus"];
    if ([appStatus isEqualToString:@"Purchased"])
    {
        appStatus=@"Purchased";
    }
    else
    {
        appStatus=nil;
    }
    return appStatus;
}

+(NSString *)GetUniqueGlobalDeviceIdentifier
{
//    UIDevice *device = [UIDevice currentDevice];
//    NSString *uniqueIdentifier = [device uniqueIdentifier];
//    return uniqueIdentifier;
}
+(NSString *)GetCurrentDate
{
    NSDateFormatter *formatter = [[NSDateFormatter alloc] init];
    //uncomment to get the time only
    //[formatter setDateFormat:@"hh:mm a"];
    //[formatter setDateFormat:@"MMM dd, YYYY"];
    [formatter setDateStyle:NSDateFormatterMediumStyle];
    //get the date today
    NSString *dateToday = [formatter stringFromDate:[NSDate date]];
    return dateToday;
}

+(int)ConvertNSStringToInt:(NSString *)myNSString
{
    int retval=-999;
    @try {
        retval = [myNSString intValue];
    }
    @catch (NSException *exception) {
    }
    @finally {
    }
    return retval;
}
+(float)ConvertNSStringToFloat:(NSString *)myNSString
{
    float retval=-999;
    @try {
        retval = [myNSString floatValue];
    }
    @catch (NSException *exception) {
    }
    @finally {
    }
    return retval;
}
+(BOOL)Contains:(NSString *)EntireString SearchForString:(NSString *)SearchForString
{
    BOOL retval=NO;
    NSRange Exists=[EntireString rangeOfString:SearchForString options:NSCaseInsensitiveSearch];
    //NSString *ex=NSStringFromRange(Exists);
    if (Exists.location==0||Exists.length==0)
    {
        retval=NO;
    }
    else
    {
        retval=YES;
    }
    return retval;
}
+(NSString *)StripAllButNumbers:(NSString *)originalString
{
    //originalString = @"(123) 123123 abc";
    NSMutableString *strippedString = [NSMutableString 
                                       stringWithCapacity:originalString.length];
    
    NSScanner *scanner = [NSScanner scannerWithString:originalString];
    NSCharacterSet *numbers = [NSCharacterSet 
                               characterSetWithCharactersInString:@"0123456789"];
    
    while ([scanner isAtEnd] == NO) {
        NSString *buffer;
        if ([scanner scanCharactersFromSet:numbers intoString:&buffer]) {
            [strippedString appendString:buffer];
            
        } else {
            [scanner setScanLocation:([scanner scanLocation] + 1)];
        }
    }
    
    NSLog(@"%@", strippedString); // "123123123"
    return strippedString;
}
+(NSString*)GetIPAddress {
    NSString* ip=@"http://www.whatismyip.org/";
    NSURL *url = [[NSURL alloc] initWithString:ip]; 
    NSError* error = nil;
    NSString* IP = [NSString stringWithContentsOfURL:url encoding:NSASCIIStringEncoding error:&error];
    return IP;
}
+(NSString *)ConvertIntToNSString:(int)myInt;
{
    NSString *retval=nil;
    @try {
        retval = [NSString stringWithFormat:@"%d", myInt];
    }
    @catch (NSException *exception) {
    }
    @finally {
    }
    return retval;
}
+(NSString *)ConvertFloatToNSString:(float)myFloat;
{
    NSString *retval=nil;
    @try {
        retval = [NSString stringWithFormat:@"%f",myFloat];
    }
    @catch (NSException *exception) {
    }
    @finally {
    }
    return retval;
}
+(void)ShowAlert:(NSString *)titleText Message:(NSString *)messageText ButtonText:(NSString *)buttonText
{
    UIAlertView *errorAlert = [[UIAlertView alloc]
                               initWithTitle:titleText
                               message:messageText
                               delegate:nil
                               cancelButtonTitle:buttonText
                               otherButtonTitles:nil];
    [errorAlert show];
}
+(NSString *)GetValueBetweenTags:(NSString *)AllContent StartTag:(NSString *)StartTag EndTag:(NSString *)EndTag
{
	NSMutableString *ss = [[NSMutableString alloc] init];
	NSString *string2;
	int max=[AllContent length];
	int i;
	for (i=0; i<max; i++) {
		string2 = [AllContent substringWithRange: NSMakeRange (i, 1)];
		[ss appendFormat:string2];
	}
	NSString *immutableString = [NSString stringWithString:ss];
	NSRange match1;
	NSRange match2;
	match1 = [immutableString rangeOfString: StartTag];
	match2 = [immutableString rangeOfString: EndTag];
	@try {
		string2 = [ss substringWithRange: NSMakeRange (match1.location+StartTag.length, match2.location-(match1.location+StartTag.length))];
	}
	@catch (NSException * e) {
		string2 = @"<TagNotFound>";
	}
	@finally {
		
	}
	return string2;
}

+(NSString *)ReverseNSString: (NSString *)textIn
{
	NSMutableString *reversedString = [NSMutableString string];
	NSInteger charIndex = [textIn length];
	while(charIndex >= 0) {
		charIndex--;
		if (charIndex==-1){break;}
		NSRange subStrRange = NSMakeRange(charIndex, 1);
		[reversedString appendString:[textIn substringWithRange:subStrRange]];
	}
	return reversedString;
}

+(NSString *)GetLastChars: (NSString *)textIn theamount:(int)theamount
{
	int count;
	int len;
	count=0;
	NSString *temp1;
	NSString *temp2;	
	NSMutableString *temp3 = [[NSMutableString alloc] init];
	temp1=textIn;
	temp2=@"";
	len=[temp1 length];
	if (len<theamount)
	{
		theamount=len;
	}
	do {
		count++;
		if (count>theamount){break;}
		temp2=[temp1 substringWithRange: NSMakeRange(len-count,1)];
		[temp3 appendFormat:temp2];
	} while (count<=999);
	////reverse temp3
    NSString *temp4=[self ConvertNSMutableStringToNSString:temp3];
    
	NSString *temp5=[self ReverseNSString:temp4];
	return temp5;
}
+(NSString *)GetFirstChars: (NSString *)textIn theamount:(int)theamount
{
	int count;
	int len;
	count=0;
	NSString *temp1;
	NSString *temp2;	
	NSMutableString *temp3 = [[NSMutableString alloc] init];
	temp1=textIn;
	temp2=@"";
	len=[temp1 length];
	if (len<=theamount){theamount=len;}
	do {
		if (count>=theamount){break;}
		temp2=[temp1 substringWithRange: NSMakeRange(count,1)];
		count++;
		[temp3 appendFormat:temp2];
	} while (count<9999);
	//reverse temp3
	return temp3;
}
+(NSMutableArray *)ConvertNSStringToNSMutableArray: (NSString *)textIn delimiter:(NSString *)delimiter
{
	NSMutableArray *temp = [[NSMutableArray alloc] init];
    NSMutableArray *pieceArray = [[NSMutableArray alloc] initWithObjects:nil];
    NSString *currentData=[[NSString alloc] init];
    NSString *remainingData=[[NSString alloc] init];
    int amnt=0;
    remainingData=textIn;
    do {

        pieceArray=[CJMUtilities GetPiecesUsingDelimiter:remainingData Delimiter:delimiter];
        if (pieceArray.count==3) {
            if (amnt>0) {
                [temp addObject:remainingData];
            }
            break;
        }
        currentData=[pieceArray objectAtIndex:2];
        [temp addObject:currentData];
        amnt++;
        remainingData=[pieceArray objectAtIndex:3];
    } while (1==1);
    return temp;
}
+(NSMutableString *)ConvertNSStringToNSMutableString: (NSString *)textIn
{
	NSMutableString *temp = [[NSMutableString alloc] init];
	[temp appendFormat:textIn];
	return temp;
}
+(NSDate *)ConvertNSStringDateToNSDate:(NSString *)NSStringIn
{
    NSDateFormatter *dateFormatter = [[NSDateFormatter alloc] init];
    [dateFormatter setDateFormat:@"yyyy-MM-dd"];
    NSDate *dateFromString = [[NSDate alloc] init];
    dateFromString = [dateFormatter dateFromString:NSStringIn];
    return dateFromString;
}
+(NSString *)TrueTextboxValue:(UITextField *)textField
{
    NSString *retVal=@"";
    if (textField.text.length>0)
    {
        if ([textField.text.uppercaseString isEqualToString:@"(NULL)"])
        {
            retVal=@"";            
        }
        else
        {
            retVal=textField.text;
        }
    }
    return retVal;
}
+(BOOL)NSDate1GreaterThanNSDate2:(NSDate *)date1 date2:(NSDate *)date2
{
    BOOL retVal=false;
    if ([date1 compare:date2] == NSOrderedDescending) {
        //NSLog(@"date1 is later than date2");
        retVal=true;
    } else if ([date1 compare:date2] == NSOrderedAscending) {
        //NSLog(@"date1 is earlier than date2");
    } else {
        //NSLog(@"dates are the same");
    }
    return retVal;
}


+(NSString *)ConvertNSMutableStringToNSString: (NSMutableString *)textIn
{
	NSString *temp = [[NSString alloc] init];
	temp=textIn;
	return temp;
}
+(NSMutableArray *)GetPiecesUsingDelimiter:(NSString *)lineIn Delimiter:(NSString *)Delimiter
{
    //returns status(1), parsedpiece(2), remainingpiece(3)
    NSMutableArray *pieceArray = [[NSMutableArray alloc] initWithObjects:nil];
    int count;
	int len;
	int loc;
	count=0;
	NSString *temp1;
	NSString *temp2;
	NSString *temp3;
	NSRange loc1;
	temp1=lineIn;
	
    len=[temp1 length];
    loc1=[temp1 rangeOfString:Delimiter];
    if (loc1.length==0)
    {
        [pieceArray addObject:@""];
        [pieceArray insertObject:@"0" atIndex:1];
        [pieceArray insertObject:lineIn atIndex:2];
        return pieceArray;
    }
    else
    {
        loc = loc1.location;
    }
    @try {
        temp2= [temp1 substringWithRange: NSMakeRange(0,loc)];
        int num1=loc+Delimiter.length;
        int num2=len-num1;
        temp3= [temp1 substringWithRange: NSMakeRange(num1,num2)];
    }
    @catch (NSException * e) {
        //temp3 = e.reason;
        temp3=@"Delimiter not found";
        [pieceArray addObject:@""];
        [pieceArray insertObject:temp3 atIndex:1];
    }
    [pieceArray addObject:@""];        
    [pieceArray insertObject:@"1" atIndex:1];
    [pieceArray insertObject:temp2 atIndex:2];
    [pieceArray insertObject:temp3 atIndex:3];
	return pieceArray;
}
+(NSString *)GetPieceUsingDelimiter:(NSString *)lineIn Delimiter:(NSString *)Delimiter Piece:(int)Piece
{
	int count;
	int len;
	int loc;
	count=0;
	NSString *temp1;
	NSString *temp2;
	NSString *temp3;
	NSRange loc1;
	temp1=lineIn;
	
	do {
		count++;
		len=[temp1 length];
//		loc1=[temp1 rangeOfString:@"\t"];
		loc1=[temp1 rangeOfString:Delimiter];
		loc=loc1.location+1;
		
        @try {
            if (loc<0)
            {
                temp3=temp1;
            }
            else
            {
                temp2=[temp1 substringWithRange: NSMakeRange(loc,len-loc)];
                temp3=[temp1 substringWithRange: NSMakeRange(0,loc-Delimiter.length)];
            }
        }
        @catch (NSException * e) {
            //temp3 = e.reason;
            temp3=@"Delimiter not found";
            return temp3;
        }
		temp1=temp2;
	} while (count<Piece);
	return temp3;
}
+(void)TestLoop:(int)myInt
{
    float currcnt,currprogress,amntremaining;
    int max=myInt;
    for (int x=0; x<max; x++)
    {
        currcnt++;
        amntremaining=max-currcnt;
        currprogress=(1-(amntremaining/max));
        [[NSRunLoop currentRunLoop] runUntilDate:[NSDate date]];
    }
}

@end
