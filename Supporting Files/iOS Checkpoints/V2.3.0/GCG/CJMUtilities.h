//
//  CJMUtilities.h
//  GCG
//
//  Created by Chris on 2/2/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface CJMUtilities : NSObject

+(BOOL)NSDate1GreaterThanNSDate2:(NSDate *)date1 date2:(NSDate *)date2;
+(NSDate *)ConvertNSStringDateToNSDate:(NSString *)NSStringIn;
+(NSString *)GetGeneratedUUID;
+(NSString *)ConvertIntToNSString:(int)myInt;
+(NSString *)ConvertFloatToNSString:(float)myFloat;
+(NSString *)ConvertNSMutableStringToNSString: (NSMutableString *)textIn;
+(NSMutableString *)ConvertNSStringToNSMutableString: (NSString *)textIn;
+(NSMutableArray *)ConvertNSStringToNSMutableArray: (NSString *)textIn delimiter:(NSString *)delimiter;
+(NSString *)GetFirstChars: (NSString *)textIn theamount:(int)theamount;
+(NSString *)GetLastChars: (NSString *)textIn theamount:(int)theamount;
+(NSString *)ReverseNSString: (NSString *)textIn;
+(NSString *)GetIPAddress;
+(NSString *)GetValueBetweenTags:(NSString *)AllContent StartTag:(NSString *)StartTag EndTag:(NSString *)EndTag;
+(NSString *)GetPieceUsingDelimiter:(NSString *)lineIn Delimiter:(NSString *)Delimiter Piece:(int)Piece;
+(NSString *)GetCurrentDate;
+(NSString *)GetLastChars: (NSString *)textIn theamount:(int)theamount;
+(NSString *)StripAllButNumbers:(NSString *)originalString;
+(int)ConvertNSStringToInt:(NSString *)myNSString;
+(float)ConvertNSStringToFloat:(NSString *)myNSString;
+(NSMutableArray *)GetPiecesUsingDelimiter:(NSString *)lineIn Delimiter:(NSString *)Delimiter;
+(BOOL)Contains:(NSString *)EntireString SearchForString:(NSString *)SearchForString;
+(void)ShowAlert:(NSString *)titleText Message:(NSString *)messageText ButtonText:(NSString *)buttonText;
+(void)TestLoop:(int)myInt;
+(void)ShowOfflineAlert;
+(NSString *)TrueTextboxValue:(UITextField *)textField;
@end
