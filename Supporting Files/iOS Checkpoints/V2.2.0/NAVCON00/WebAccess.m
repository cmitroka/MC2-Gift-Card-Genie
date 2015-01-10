//
//  WebAccess.m
//  NAVCON01
//
//  Created by Chris on 8/7/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "WebAccess.h"
#import "DataAccess.h"
#import "Communicator.h"
#import "Communicator4.h"
#import "CJMUtilities.h"
#import "StaticData.h"
#import "SFHFKeychainUtils.h"

@implementation WebAccess
- (id)init {
    return self;
}

-(BOOL)pmIsConnectedToInternet
{
    NSString *connected = [NSString stringWithContentsOfURL:[NSURL URLWithString:@"http://www.google.com"]];
    if (connected == NULL) 
    {
        NSLog(@"Not connected");
        return NO;
    }
    else
    {
        NSLog(@"Connected - %@",connected);
        return YES;
    }
}
-(NSString *)doLogUser
{
    
    NSString *IP=@"1.1.1.2";//[CJMUtilities getIPAddress];
    Communicator *c=[[Communicator alloc] init];
    [c DoGenericRequest:@"GetUserIPAddress" bodyData:@"" secTimeout:3 retryAmnt:0];
    IP=c.currentXMLResp;
    StaticData *sd=[StaticData sd];
    NSString *uuid=sd.pUUID;
    NSString *bodyData=[NSString stringWithFormat:@
                        "<UUID>%@</UUID>\n"
                        "<IP>%@</IP>\n"
                        ,uuid,IP];
    [c DoGenericRequest:@"LogUser" bodyData:bodyData secTimeout:0 retryAmnt:0];
    do {
        [[NSRunLoop currentRunLoop] runUntilDate:[NSDate date]];
    } while (c.currentXMLResp==@"");
    return c.currentXMLResp;
}
-(NSString *)pmDoLogConsideredBuying:(NSString *)pPurchaseType
{    
    Communicator *c=[[Communicator alloc] init];
    StaticData *sd=[StaticData sd];
    NSString *uuid=sd.pUUID;
    NSString *bodyData=[NSString stringWithFormat:@
                        "<UUID>%@</UUID>\n"
                        "<Option>%@</Option>\n"
                        ,uuid,pPurchaseType];
    [c DoGenericRequest:@"LogConsideredBuying" bodyData:bodyData secTimeout:0 retryAmnt:0];
    return c.currentXMLResp;
}

-(NSString *)pmBackupData:(NSString *)pAllData
{
    StaticData *sd=[StaticData sd];
    Communicator *c=[[Communicator alloc] init];
    NSString *uuid=sd.pUUID;
    NSString *bodyData=[NSString stringWithFormat:@
                        "<UUID>%@</UUID>\n"
                        "<AllData>%@</AllData>\n"
                        ,uuid,pAllData];
    [c DoGenericRequest:@"BackupData" bodyData:bodyData  secTimeout:10 retryAmnt:0];
    return c.currentXMLResp;    
}
-(NSString *)pmRetrieveData:(NSString *)pRetrievalKey
{
    StaticData *sd=[StaticData sd];
    Communicator *c=[[Communicator alloc] init];
    NSString *uuid=sd.pUUID;
    NSString *bodyData=[NSString stringWithFormat:@
                        "<UUID>%@</UUID>\n"
                        "<RetrievalKey>%@</RetrievalKey>\n"
                        ,uuid,pRetrievalKey];
    [c DoGenericRequest:@"RetrieveData" bodyData:bodyData secTimeout:10 retryAmnt:0];
    return c.currentXMLResp;    
}
-(NSString *)pmDoAppStartup
{
    Communicator *c=[[Communicator alloc] init];
    StaticData *sd=[StaticData sd];
    NSString *uuid=sd.pUUID;
    NSString *vers=sd.pVersion;
    NSString *bodyData=[NSString stringWithFormat:@
                        "<UUID>%@</UUID>\n"
                        "<Version>%@</Version>\n"
                        ,uuid,vers];
    [c DoGenericRequest:@"DoAppStartup2" bodyData:bodyData secTimeout:10 retryAmnt:0];
    return c.currentXMLResp;
}
-(NSString *)pmGetSessionIDAndAdInfo:(NSString *)CardType
{
    Communicator *c=[[Communicator alloc] init];
    StaticData *sd=[StaticData sd];
    NSString *uuid=sd.pUUID;
    NSString *bodyData=[NSString stringWithFormat:@
                        "<UUID>%@</UUID>\n"
                        "<CardType>%@</CardType>\n"
                        ,uuid,CardType];
    [c DoGenericRequest:@"GetSessionIDAndAdInfo" bodyData:bodyData secTimeout:7 retryAmnt:0];
    return c.currentXMLResp;
}
-(NSString *)pmNewRequestSessionID:(NSString *)pSessionID CheckSum:(NSString *)pCheckSum CardType:(NSString *)pCardType CardNumber:(NSString *)pCardNumber PIN:(NSString *)pPIN Login:(NSString *)pLogin Password:(NSString *)pPassword
{
    Communicator *c=[[Communicator alloc] init];    
    StaticData *sd=[StaticData sd];
    NSString *uuid=sd.pUUID;
    NSString *bodyData=[NSString stringWithFormat:@
                        "<UUID>%@</UUID>\n"
                        "<SessionID>%@</SessionID>\n"
                        "<CheckSum>%@</CheckSum>\n"
                        "<CardType>%@</CardType>\n"
                        "<CardNumber>%@</CardNumber>\n"
                        "<PIN>%@</PIN>\n"
                        "<Login>%@</Login>\n"
                        "<Password>%@</Password>\n"
                        ,uuid,pSessionID,pCheckSum,pCardType,pCardNumber,pPIN,pLogin,pPassword];
   [c DoGenericRequest:@"NewRequest" bodyData:bodyData secTimeout:30 retryAmnt:0];
    return c.currentXMLResp;
}

-(NSString *)pmC4NewRequest:(NSString *)pUDID SessionID:(NSString *)pSessionID CheckSum:(NSString *)pCheckSum CardType:(NSString *)pCardType CardNumber:(NSString *)pCardNumber PIN:(NSString *)pPIN Login:(NSString *)pLogin Password:(NSString *)pPassword
{
    Communicator4 *c=[[Communicator4 alloc] init];    
    StaticData *sd=[StaticData sd];
    NSString *uuid=sd.pUUID;
    NSString *bodyData=[NSString stringWithFormat:@
                        "<UUID>%@</UUID>\n"
                        "<SessionID>%@</SessionID>\n"
                        "<CheckSum>%@</CheckSum>\n"
                        "<CardType>%@</CardType>\n"
                        "<CardNumber>%@</CardNumber>\n"
                        "<PIN>%@</PIN>\n"
                        "<Login>%@</Login>\n"
                        "<Password>%@</Password>\n"
                        ,uuid,pSessionID,pCheckSum,pCardType,pCardNumber,pPIN,pLogin,pPassword];
    NSString *retVal= [c pmDoGenericRequest:@"NewRequest" bodyData:bodyData];
    return retVal;
}
-(NSString *)pmC4ContinueRequest:(NSString *)pUDID IDFileName:(NSString *)pIDFileName Answer:(NSString *)pAnswer
{
    Communicator4 *c=[[Communicator4 alloc] init];    
    StaticData *sd=[StaticData sd];
    NSString *uuid=sd.pUUID;

    NSString *bodyData=[NSString stringWithFormat:@
                        "<pUUID>%@</pUUID>\n"
                        "<pIDFileName>%@</pIDFileName>\n"
                        "<pAnswer>%@</pAnswer>\n"
                        ,uuid,pIDFileName,pAnswer];
    NSString *retVal= [c pmDoGenericRequest:@"ContinueRequest" bodyData:bodyData];
    return retVal;
}
-(void)pmAddMerchantRequest:(NSString *)pCardType CardNumber:(NSString *)pCardNumber PIN:(NSString *)pPIN
{
    Communicator *c=[Communicator alloc];
    StaticData *sd=[StaticData sd];
    NSString *uuid=sd.pUUID;
    NSString *bodyData=[NSString stringWithFormat:@
                        "<pMerchant>%@</pMerchant>\n"
                        "<pURL>%@</pURL>\n"
                        "<pCardNum>%@</pCardNum>\n"
                        "<pCardPIN>%@</pCardPIN>\n"
                        "<pOtherInfo>%@</pOtherInfo>\n"
                        ,pCardType,@"",pCardNumber,pPIN,uuid];
    [c DoGenericRequest:@"AddMerchantRequest" bodyData:bodyData secTimeout:1 retryAmnt:0];
}
-(void)pmRecordFeedback:(NSString *)pName ContactInfo:(NSString *)pContactInfo Feedback:(NSString *)pFeedback
{
    Communicator *c=[Communicator alloc];
    StaticData *sd=[StaticData sd];
    NSString *uuid=sd.pUUID;
    NSString *bodyData=[NSString stringWithFormat:@
                        "<UUID>%@</UUID>\n"
                        "<Name>%@</Name>\n"
                        "<ContactInfo>%@</ContactInfo>\n"
                        "<Feedback>%@</Feedback>\n"
                        ,uuid,pName,pContactInfo,pFeedback];
    [c DoGenericRequest:@"RecordFeedback" bodyData:bodyData secTimeout:1 retryAmnt:0];    
}
-(void)pmLogPurchase:(NSString *)pSessionID CheckSum:(NSString *)pCheckSum PurchaseType:(NSString *)pPurchaseType
{
    //Communicator4 *c=[[Communicator4 alloc] init];    
    Communicator *c=[Communicator alloc];
    StaticData *sd=[StaticData sd];
    NSString *uuid=sd.pUUID;
    NSString *bodyData=[NSString stringWithFormat:@
                        "<SessionID>%@</SessionID>\n"
                        "<CheckSum>%@</CheckSum>\n"
                        "<UUID>%@</UUID>\n"
                        "<PurchaseType>%@</PurchaseType>\n"
                        ,pSessionID,pCheckSum,uuid,pPurchaseType];
    //NSString *temp=[c pmDoGenericRequest:@"LogPurchase" bodyData:bodyData];
    [c DoGenericRequest:@"LogPurchase" bodyData:bodyData secTimeout:10 retryAmnt:0];    
    NSLog(c.currentXMLResp);
}
-(int)pmGetMerchantCount
{
    /*
    int Bypass=10;
    return Bypass;
    */
    Communicator *c=[Communicator alloc];
    [c DoGenericRequest:@"GetMerchantCount" bodyData:@"" secTimeout:10 retryAmnt:0];
    int retVal=[CJMUtilities ConvertNSStringToInt:c.currentXMLResp];
    return retVal;
}
-(NSString *)pmDownloadAllData
{
    /*    
    NSString *Bypass=@"5 Guys~_~~_~http://www.fiveguyscard.com/b2c/balance_inquiry.aspx~_~True~_~False~_~False~_~False~_~8~_~10~_~4~_~6~_~5 Guys Note^)(Abercrombie and Fitch~_~~_~~_~True~_~True~_~False~_~False~_~12~_~12~_~4~_~4~_~AF Note^)(AC Moore~_~~_~~_~True~_~True~_~False~_~False~_~16~_~20~_~5~_~5~_~AC Moore Note Not Supported^)(Aerie~_~~_~http://www.ae.com/web/checkout/giftcards/giftCard.jsp~_~True~_~True~_~False~_~False~_~1~_~50~_~0~_~0~_~Aerie Note^)(Albertsons~_~~_~https://www.supervalugift.com/b2c/balance_inquiry.aspx?id=559&amp;AspxAutoDetectCookieSupport=1~_~True~_~False~_~False~_~False~_~~_~~_~~_~~_~^)(AMC Theatres~_~~_~https://www.amcnationalsales.com/standard/balanceinquiry.aspx?AspxAutoDetectCookieSupport=1~_~True~_~False~_~False~_~False~_~~_~~_~~_~~_~^)(American Apparel~_~~_~~_~True~_~True~_~False~_~False~_~~_~~_~~_~~_~^)(American Eagle~_~~_~http://www.ae.com/web/checkout/giftcards/giftCard.jsp~_~True~_~True~_~False~_~False~_~~_~~_~~_~~_~^)(Ann Taylor~_~~_~https://www.anntaylor.com/ann/giftCard.jsp~_~True~_~True~_~False~_~False~_~~_~~_~~_~~_~^)(Applebees~_~~_~https://wbiprod.storedvalue.com/WBI/clientPages/apple_en_Lookup.jsp?host=applebees.com~_~True~_~False~_~False~_~False~_~~_~~_~~_~~_~^)(";
    return Bypass;
    */
    
    //*
    StaticData *sd=[StaticData sd];
    int iVersion=[CJMUtilities ConvertNSStringToFloat:sd.pVersion];
    Communicator *c=[[Communicator alloc] init];
    [c DoGenericRequest:@"DownloadAllData" bodyData:@"" secTimeout:10 retryAmnt:0];
    NSLog(@"DownloadAllData - %@",c.currentXMLResp);
    return c.currentXMLResp;
    //*/
}

-(NSString *)pmDeleteOldBackup:(NSString *)pUUID
{    
    Communicator *c=[[Communicator alloc] init];
    NSString *bodyData=[NSString stringWithFormat:@
                        "<UUID>%@</UUID>\n"
                        ,pUUID];
    [c DoGenericRequest:@"DeleteOldBackup" bodyData:bodyData secTimeout:0 retryAmnt:0];
    return c.currentXMLResp;
}

@end
