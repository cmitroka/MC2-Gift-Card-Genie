//
//  GCGSpecific.m
//  GCG
//
//  Created by Chris on 9/29/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "GCGSpecific.h"
#import "CJMUtilities.h"
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
    if ([rsType isEqualToString:gcgGCBALANCE])
    {
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
    }

    else
    {
        UIAlertView *av=[[UIAlertView alloc] initWithTitle:@"Try Alternate Lookup?" message:[NSString stringWithFormat:@"%@%@",@"We couldn't automatically get the balance; ",@"would you like to use the alternate lookup method?"] delegate:self cancelButtonTitle:@"Yeah, let's try that" otherButtonTitles:@"No thanks", nil];
        [av show];
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
