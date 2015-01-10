//
//  GCGSpecific.m
//  GCG
//
//  Created by Chris on 9/29/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "GCGSpecific.h"
#import "CJMUtilities.h"
#import "RespNeedsMoreInfo.h"
#import "RespNeedsCAPTCHA.h"
#import "MyGCs.h"
#import "StaticData.h"
#import "DataAccess.h"
#import "SFHFKeychainUtils.h"
#import "IAP.h"
#import "WebAccess.h"
@interface GCGSpecific()
{
}
@end

@implementation GCGSpecific
+(void)pmHandleResponse:(NSString *)rs PassNavView:(UINavigationController *)ViewToSwitchTo
{
    StaticData *sd=[StaticData sd];
    NSString *goBack=@"";    
    NSString *rsType=@"";    
    NSString *rsValue=@"";    
    NSMutableArray *tempArray=[CJMUtilities ConvertNSStringToNSMutableArray:rs delimiter:gcgLINEDEL];
    rsType=[tempArray objectAtIndex:0];
    rsValue=[tempArray objectAtIndex:1];
    if ([rsType isEqualToString:gcgGCNEEDSMORRINFO])
    {
        sd.pPopAmount=sd.pPopAmount+1;
        RespNeedsMoreInfo *rnmi=[[RespNeedsMoreInfo alloc]initWithParamString:rsValue];
        [ViewToSwitchTo pushViewController:rnmi animated:YES];
    } else if ([rsType isEqualToString:gcgGCCAPTCHA])
    {
        sd.pPopAmount=sd.pPopAmount+1;
        RespNeedsCAPTCHA *rnc =[[RespNeedsCAPTCHA alloc]initWithParamString:rsValue];
        [ViewToSwitchTo pushViewController:rnc animated:YES];
    } else if ([rsType isEqualToString:gcgGCBALANCE])
    {
        sd.pGoBack=@"1";
        NSString *Balance=@"";
        Balance=rsValue;
        NSString *temp=[CJMUtilities GetCurrentDate];
        //xxx
        DataAccess *da = [DataAccess da];
        [da pmUpdateMyCardBalanceInfo:sd.pLoadGCKey lastbalknown:Balance lastbaldate:temp];
        [CJMUtilities ShowAlert:@"Your Balance Is..." Message:Balance ButtonText:@"Thanks!"];
        sd.pDemoAcknowledged=@"";
        int AmntOfLookupsRemaining=[CJMUtilities ConvertNSStringToInt:sd.pAmntOfLookupsRemaining]-1;
        sd.pAmntOfLookupsRemaining=[CJMUtilities ConvertIntToNSString:AmntOfLookupsRemaining];        
    } else if ([rsType isEqualToString:gcgGCBALANCEERR]||[rsType isEqualToString:gcgGCERR])
    {
        sd.pGoBack=@"1";
        [CJMUtilities ShowAlert:@"Hmmm..." Message:@"We couldn't get a balance, which means 1 of 3 thing....\r\n\r\n1 - You balance may be $0.00\r\n2 - Some piece of the data sent was incorrectly entered\r\n3 - The vendors balance lookup webpage is offline\r\n\r\nTry the 'Go To Website' button and see if that works." ButtonText:@"OK"];
    } else if ([rsType isEqualToString:gcgWSBLOCKEDIP])
    {
        sd.pGoBack=@"1";
        [CJMUtilities ShowAlert:@"Not Good" Message:@"This IP has been blocked.  Please send us an email so we can figure out what happened." ButtonText:@"OK"];
    } else if ([rsType isEqualToString:gcgGCCUSTOM])
    {
        sd.pGoBack=@"1";
        NSString *Response=@"";
        Response=rsValue;
        [CJMUtilities ShowAlert:@"Server Response" Message:Response ButtonText:@"OK"];
    } else if ([rsType isEqualToString:gcgJEXEMISSING])
    {
        sd.pGoBack=@"1";
        NSString *Response=@"";
        Response=rsValue;
        [CJMUtilities ShowAlert:@"Back Soon" Message:@"We're doing some work on this merchant, we'll try to have the lookup back online within 48hrs." ButtonText:@"OK"];
    } else
    {
        sd.pGoBack=@"1";
        [CJMUtilities ShowAlert:@"Our Bad..." Message:@"Somethings wacked out on our end, we'll fix it.  Check back a little later." ButtonText:@"OK"];
    }
    
}
+(NSString *)pmGetChecksum: (NSString *)sessionIdIn
{
    NSString *temp=[CJMUtilities GetLastChars:sessionIdIn theamount:4];
    int temp2=[CJMUtilities ConvertNSStringToInt:temp];
    int temp3=temp2*316;
    temp=[CJMUtilities ConvertIntToNSString:temp3];
    temp=[CJMUtilities GetLastChars:temp theamount:2];
    return temp;
}
@end
