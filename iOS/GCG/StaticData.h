//
//  StaticData.h
//  GCG
//
//  Created by Chris on 9/11/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface StaticData : NSObject
+ (StaticData *)sd;

@property (nonatomic, retain) NSMutableArray *merchantsAllData;
@property (nonatomic, retain) NSMutableArray *merchantsNames;
@property (nonatomic, retain) NSString *pCAPTCHAURLInfo;
@property (nonatomic, retain) NSString *pSessionID;
@property (nonatomic, retain) NSString *pChecksum;
@property (nonatomic, retain) NSString *pUUID;
@property (nonatomic, retain) NSString *pSystemMessage;
@property (nonatomic, retain) NSString *pAmntOfLookupsRemaining;
@property (readwrite, retain) NSString *pLastDBUpdate;
@property (readwrite, retain) NSString *pLoadGCKey;
@property (readwrite, retain) NSString *pMode;
@property (readwrite, retain) NSString *pModeAcknowledged;
@property (readwrite, retain) NSString *pDemoAcknowledged;
@property (readwrite, retain) NSString *pVersion;
@property (readwrite, retain) NSString *pGoBack;
@property (readwrite, retain) NSString *pDrawn;
@property (readwrite, retain) NSString *pAlertMessage;
@property (readwrite, retain) NSString *pPurchased;
@property (readwrite, retain) NSString *pAmntForADollar;
@property (readwrite, retain) NSString *pCostForApp;
@property (readwrite, retain) NSString *pCallback;
@property (readwrite, retain) NSString *pAdUnitID;
@property int pPopAmount;
@property int pVersionNumeric;

-(int)pmInsertToMerchantsDatasource:(NSMutableArray *)merchantsAllData;
- (NSMutableArray *)pmGetMerchantAllDataFromName:(NSString *)zmerchantName;
-(void)AddressWarnings;
@end
