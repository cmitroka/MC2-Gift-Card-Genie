//
//  StaticData.m
//  GCG
//
//  Created by Chris on 9/11/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "StaticData.h"
#import "CJMUtilities.h"

@implementation StaticData
@synthesize merchantsAllData,merchantsNames,pSystemMessage;
//@synthesize pLastDBUpdate=_pLastDBUpdate,pCAPTCHAURLInfo=_pCAPTCHAURLInfo,pAmntOfLookupsRemaining=_ppAmntOfLookupsRemaining,pLoadGCKey=_pLoadGCKey,pUUID=_pUUID,pSessionID=_pSessionID,pChecksum=_pChecksum,pMode=_pMode,pDemoAcknowledged=_pDemoAcknowledged,pModeAcknowledged=_pModeAcknowledged,pPopAmount=_pPopAmount,pVersionNumeric=_pVersionNumeric,pVersion=_pVersion,pGoBack=_pGoBack,pDrawn=_pDrawn,pAlertMessage=_pAlertMessage,pPurchased=_pPurchased,pAmntForADollar=_pAmntForADollar,pCostForApp=_pCostForApp,pCallback=_pCallback;
static NSMutableArray *_merchantsAllData;
static NSMutableArray *_merchantsNames;
static NSString *_pCallback=@"";
static NSString *_pLoadGCKey=@"";
static NSString *_pUUID=@"";
static NSString *_pChecksum=@"";
static NSString *_pSessionID=@"";
static NSString *_pAmntOfBalanceReturns=@"";
static NSString *_pLastDBUpdate=@"";
static NSString *_pSystemMessage;
static NSString *_pCAPTCHAURLInfo=@"";
static NSString *_pMode=@"";
static NSString *_pDemoAcknowledged=@"";
static NSString *_pModeAcknowledged=@"";
static NSString *_pVersion=@"";
static NSString *_pGoBack=@"";
static NSString *_pDrawn=@"";
static NSString *_pAlertMessage=@"";
static NSString *_pPurchased=@"";
static NSString *_pAmntForADollar=@"";
static NSString *_pCostForApp=@"";
static NSString *_pAdUnitID=@"";
static int _pVersionNumeric=0;
static int _pPopAmount=0;
static StaticData *_sd;

+ (StaticData *)sd{
    if (_sd == nil) {
        _sd = [[StaticData alloc] init];
        _merchantsAllData = [[NSMutableArray alloc] init];
        _merchantsNames = [[NSMutableArray alloc] init];
        
    }
    return _sd;
}
//synthesis normally handles this... when in the header we have (readwrite, retain)
//other way to do it..
- (NSString *)pSystemMessage
{
    return _pSystemMessage;
}
- (void) setPSystemMessage:(NSString *)ptempSystemMessage
{
    _pSystemMessage=ptempSystemMessage;
}



/*
- (id)init {
    return self;
}
*/
-(int)pmInsertToMerchantsDatasource:(NSMutableArray *)zmerchantsAllData
{
    [_merchantsAllData addObject:(zmerchantsAllData)];
    NSString *merchName=[zmerchantsAllData objectAtIndex:(0)] ;
    [_merchantsNames addObject:(merchName)];
    return 1;
}
- (NSMutableArray *)pmGetMerchantAllDataFromName:(NSString *)zmerchantName
{    
    zmerchantName=[zmerchantName uppercaseString];
    NSString *temp;
    int pos=-1;
    int max=_merchantsNames.count;
    for (int x=0; x<max; x++) 
    {
        //NSString *strx=[CJMUtilities ConvertIntToNSString:x];
        temp=[_merchantsNames objectAtIndex:x];
        temp=[temp uppercaseString];
        if ([temp isEqualToString:zmerchantName]) {
            pos=x;
            break;
        }
    }
    if (pos==-1) {
        return NULL;
    }
    NSMutableArray *nsma=[_merchantsAllData objectAtIndex:pos];
    return nsma;
}


@end
