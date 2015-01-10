//
//  WebAccess.h
//  NAVCON01
//
//  Created by Chris on 8/7/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface WebAccess : NSObject
@property(nonatomic, retain) NSString *currentXMLResp;
- (id)init;
-(NSString *)pmDoAppStartup;
-(int)pmGetMerchantCount;
-(NSString *)pmDownloadAllData;
-(NSString *)pmNewRequest:(NSString *)pUDID SessionID:(NSString *)pSessionID CheckSum:(NSString *)pCheckSum CardType:(NSString *)pCardType CardNumber:(NSString *)pCardNumber PIN:(NSString *)pPIN Login:(NSString *)pLogin Password:(NSString *)pPassword;
-(NSString *)pmC4NewRequest:(NSString *)pUDID SessionID:(NSString *)pSessionID CheckSum:(NSString *)pCheckSum CardType:(NSString *)pCardType CardNumber:(NSString *)pCardNumber PIN:(NSString *)pPIN Login:(NSString *)pLogin Password:(NSString *)pPassword;

-(NSString *)pmContinueRequest:(NSString *)pUDID IDFileName:(NSString *)pIDFileName Answer:(NSString *)pAnswer;
-(NSString *)pmC4ContinueRequest:(NSString *)pUDID IDFileName:(NSString *)pIDFileName Answer:(NSString *)pAnswer;
-(NSString *)pmGetSessionIDAndAdInfo:(NSString *)CardType;
-(void)pmAddMerchantRequest:(NSString *)pCardType CardNumber:(NSString *)pCardNumber PIN:(NSString *)pPIN;
-(void)pmRecordFeedback:(NSString *)pName ContactInfo:(NSString *)pContactInfo Feedback:(NSString *)pFeedback;
-(void)pmLogPurchase:(NSString *)pSessionID CheckSum:(NSString *)pCheckSum PurchaseType:(NSString *)pPurchaseType;
-(NSString *)pmBackupData:(NSString *)pAllData;
-(NSString *)pmRetrieveData:(NSString *)pRetrievalKey;
-(BOOL)pmIsConnectedToInternet;
-(NSString *)pmDoLogConsideredBuying:(NSString *)pPurchaseType;
-(NSString *)pmDeleteOldBackup:(NSString *)pUUID;
@end
