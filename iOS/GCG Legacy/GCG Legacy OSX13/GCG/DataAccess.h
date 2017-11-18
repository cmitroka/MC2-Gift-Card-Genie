//
//  DataAccess.h
//  GCG
//
//  Created by Chris on 8/7/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "sqlite3.h"
#import "GiftCard.h"
@interface DataAccess : NSObject
+ (DataAccess *)da;
@property (nonatomic, retain) NSMutableArray *myCards;
@property (nonatomic, retain) NSMutableArray *merchantsAutoLookup;
@property (nonatomic, retain) NSMutableArray *merchantsAll;
@property (nonatomic, retain) NSMutableArray *cardNumbsForMerchants;
@property (nonatomic, retain) NSString *databasePath;
@property (nonatomic, retain) NSString *databaseName;
-(int)pmTEST;
-(NSMutableArray *)pmGetMerchantsAutoLookup;
-(NSMutableArray *)pmGetMerchantsAll;
-(NSMutableArray *)pmGetCardNumbsForMerchants:(NSString *)merchantName;
-(NSString *)pmGetSetting:(NSString *)setting;
-(int)pmUpdateSetting:(NSString *)setting svalue:(NSString *)svalue;
-(MerchantInfo *)pmGetMerchantInfoByName:(NSString *)merchantName;
-(NSMutableArray *)pmGetMyCardArray:(NSString *)mygcname;
-(int)pmDeleteMyCard:(NSString *)mygcname;
-(int)pmDeleteMyCards;
-(int)pmUpdateMyCard:(NSString *)gctype gcnum:(NSString *)gcnum gcpin:(NSString *)gcpin credlogin:(NSString *)credlogin credpass:(NSString *)credpass mygcname:(NSString *)mygcname myoldgcname:(NSString *)myoldgcname;
-(int)pmUpdateMyCardBalanceInfo:(NSString *)mygcname lastbalknown:(NSString *)lastbalknown lastbaldate:(NSString *)lastbaldate;
-(int)pmDoesMyCardExist:(NSString *)mygcname;
-(int)pmDeleteMerchants;

-(int)pmInsertMerchant:(NSString *)pName url:(NSString *)pURL showCardNum:(NSString *)pshowCardNum showCardPIN:(NSString *)pshowCardPIN minCardLen:(int)pminCardLen maxCardLen:(int)pmaxCardLen minPINLen:(int)pminPINLen maxPINLen:(int)pmaxPINLen isLookupManual:(NSString *)pisLookupManual note:(NSString *)pNote;

-(NSMutableArray *)pmGetMyCards;
-(MyCard *)pmGetMyCard:(NSString *)mygcname;
-(NSString *)pmGetLookupLetter:(NSString *)gctype;
-(int)pmConvDB;
@end
