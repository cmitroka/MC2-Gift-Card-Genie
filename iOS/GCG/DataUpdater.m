//
//  DataUpdater.m
//  GCG
//
//  Created by Chris on 8/10/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "DataUpdater.h"

#import "Communicator.h"
#import "WebAccess.h"
#import "DataAccess.h"
#import "CJMUtilities.h"
@implementation DataUpdater
- (void)viewDidLoad
{
    
    [super viewDidLoad];
	// Do any additional setup after loading the view, typically from a nib.
    //timer=[NSTimer scheduledTimerWithTimeInterval:.1 target:self selector:@selector(DoUpdateDB) userInfo:nil repeats:YES];  

}
//static int ddadCurrentCount,ddadMerchantCount;
-(void)DownloadAllData
{
    Communicator *c=[Communicator alloc];
    zIntCurrentCount=0;
    [c DoGenericRequest:@"GetMerchantCount" bodyData:@"" secTimeout:1 retryAmnt:0];
    NSString *allMerchantCount=c.currentXMLResp;
    zIntMerchantCount=[CJMUtilities ConvertNSStringToInt:allMerchantCount];
    //zIntMerchantCount=11;
    zStrTemp=[CJMUtilities ConvertIntToNSString:zIntMerchantCount];
    //NSLog(zStrTemp);
    [c DoGenericRequest:@"DownloadAllData" bodyData:@"" secTimeout:1 retryAmnt:0];
    NSString *remainingMerchantData=c.currentXMLResp;
    //remainingMerchantData=@"1-800-PetSupplies@#@1-800-738-7877@#@http://www.petsupplies.com/CS/GiftCard.aspx@#@True@#@False@#@False~-~1800Flowers@#@1-800-242-5353@#@@#@False@#@True@#@False~-~24 Hour Fitness@#@1-888-951-2597@#@@#@False@#@False@#@True~-~5 & Diner@#@1-480-962-7104@#@@#@True@#@True@#@False~-~7-Eleven@#@@#@http://www.7-eleven.com/category.aspx?categoryid=6001005@#@False@#@True@#@True~-~76 Gas@#@1-877-526-6626@#@https://wbiprod.storedvalue.com/WBI/lookupservlet?language=en&brand_id=conoco@#@True@#@True@#@True~-~77kids@#@1-877-274-3004@#@http://www.ae.com/77kids/checkout/giftcards/giftCard.jsp@#@False@#@False@#@False~-~A Pea in the Pod@#@1-877-273-2763@#@@#@False@#@False@#@False~-~A&W@#@@#@@#@False@#@False@#@False~-~Abercrombie & Fitch@#@1-877-529-6991@#@https://www.abercrombie.com/webapp/wcs/stores/servlet/GCLookup?catalogId=10901&langId=-1&storeId=10051&krypto=LMo8TRMueknDw16exMLeKA==&ddkey=http:OrderItemDisplayView@#@False@#@False@#@False~-~";
    zStrRemMerchData=remainingMerchantData;
    //NSLog(remainingMerchantData);
    [da pmDeleteMerchants];    
    timer=[NSTimer scheduledTimerWithTimeInterval:.001 target:self selector:@selector(DoDownloadAllData) userInfo:nil repeats:NO];
}
- (void)viewWillAppear:(BOOL)animated
{
    [self.navigationController setNavigationBarHidden:YES];
}
- (id)init {
    //da = [[DataAccess alloc]init];    
    da = [DataAccess da];
    [self DownloadAllData];
    return self;
}
-(void)DoDownloadAllData
{
    do {
    NSString *status=@"1";
    NSString *merchantName, *merchantPhone, *merchantURL, *showPIN, *showCreds, *showCardNum;
    NSString *specificdata;
    NSMutableArray *pieceArray = [[NSMutableArray alloc] initWithObjects:nil];
    NSMutableArray *pieceArray2 = [[NSMutableArray alloc] initWithObjects:nil];
    
    zIntCurrentCount++;
    float m_amnt_remaining=zIntMerchantCount-zIntCurrentCount;
    if ((m_amnt_remaining)==0){
        [da pmGetMerchantsAll];
        //[timer invalidate];
    }
    float test1=m_amnt_remaining/zIntMerchantCount;
    zFltProgress=1-test1;
    progressbar.progress=zFltProgress;
    pieceArray=[CJMUtilities GetPiecesUsingDelimiter:zStrRemMerchData Delimiter:@"~-~"];
    status=[pieceArray objectAtIndex:1];
    specificdata=[pieceArray objectAtIndex:2];
    if ([pieceArray count]<4) {
        self.navigationController.navigationBarHidden=NO;
        lblmessage.text=@"Thanks for waiting, you can resume using the app now.";
        return;
    }
    zStrRemMerchData=[pieceArray objectAtIndex:3];
    
    //merchantName = [merchantName stringByReplacingOccurrencesOfString:@"&amp;" withString:@"&"];
    pieceArray2=[CJMUtilities GetPiecesUsingDelimiter:specificdata Delimiter:@"@#@"];
    merchantName=[pieceArray2 objectAtIndex:2];
    specificdata=[pieceArray2 objectAtIndex:3];
    
    pieceArray2=[CJMUtilities GetPiecesUsingDelimiter:specificdata Delimiter:@"@#@"];
    merchantPhone=[pieceArray2 objectAtIndex:2];
    specificdata=[pieceArray2 objectAtIndex:3];
    
    pieceArray2=[CJMUtilities GetPiecesUsingDelimiter:specificdata Delimiter:@"@#@"];
    merchantURL=[pieceArray2 objectAtIndex:2];
    specificdata=[pieceArray2 objectAtIndex:3];
    
    pieceArray2=[CJMUtilities GetPiecesUsingDelimiter:specificdata Delimiter:@"@#@"];
    showCardNum=[pieceArray2 objectAtIndex:2];
    specificdata=[pieceArray2 objectAtIndex:3];
    
    pieceArray2=[CJMUtilities GetPiecesUsingDelimiter:specificdata Delimiter:@"@#@"];
    showPIN=[pieceArray2 objectAtIndex:2];
    specificdata=[pieceArray2 objectAtIndex:3];
    
    pieceArray2=[CJMUtilities GetPiecesUsingDelimiter:specificdata Delimiter:@"@#@"];
    showCreds=[pieceArray2 objectAtIndex:2];
        int temp=0;
    //[da pmInsertMerchant:merchantName url:merchantURL phone:merchantPhone showCardNum:showCardNum showCardPIN:showPIN showCreds:showCreds reqReg:@"" minCardLen:temp];
    [[NSRunLoop currentRunLoop] runUntilDate:[NSDate date]];
    } while (1==1);
}
@end
