//
//  GiftCard.m
//  GCG
//
//  Created by Chris on 8/20/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "GiftCard.h"
#import "DataAccess.h"

@interface MyCard()
-(void)pmGetCard:(NSString *)mygcname;
@end


@implementation MyCard
@synthesize p_gctype, p_gcnum, p_gcpin, p_credlogin, p_credpass, p_mygcname;
@synthesize p_lastbaldate,p_lastknownbal;
-(id)initWithName:(NSString *)n
{
    [self pmGetCard:n];
	return self;
}
-(void)pmGetCard:(NSString *)mygcname
{
    DataAccess *da = [DataAccess da];
    MyCard *m=[da pmGetMyCard:mygcname];
    self.p_gctype=m.p_gctype;
    self.p_gcnum=m.p_gcnum;
    self.p_gcpin=m.p_gcpin;
    self.p_credlogin=m.p_credlogin;
    self.p_credpass=m.p_credpass;
    self.p_mygcname=m.p_mygcname;
    self.p_lastbaldate=m.p_lastbaldate;
    self.p_lastknownbal=m.p_lastknownbal;
}
@end
/*
@interface CardAbilities()
-(void)pmGetCard:(NSString *)mygcname;
@end

@implementation CardAbilities
@synthesize p_hasgcpin,p_hasgcnum,p_hasgccreds;
-(id)initWithName:(NSString *)n
{
    [self pmGetCard:n];
	return self;
}
-(void)pmGetCard:(NSString *)mygcname
{
    DataAccess *da = [DataAccess da];
    MyCard *m=[da pmGetMyCard:mygcname];
    self.p_hasgcnum=m.p_gctype;
    self.p_hasgcpin=m.p_gcnum;
    self.p_hasgccreds=m.p_gcnum;
}
@end
@implementation Merchant00
@synthesize favorite,showCreds,showCardNum,showCardPIN,name,url,phone,isAutoLookup;
- (id)init {
    return self;
}
@end
*/
@interface MerchantInfo()
-(void)pmGetCard:(NSString *)mygcname;
@end

@implementation MerchantInfo
@synthesize p_url,p_name,p_phone,p_showCreds,p_showCardNum,p_showCardPIN,p_reqReg,p_maxCardLen,p_maxPINLen,p_minCardLen,p_minPINLen,p_note;
-(id)initWithName:(NSString *)n
{
    [self pmGetCard:n];
	return self;
}
-(void)pmGetCard:(NSString *)mygcname
{
    DataAccess *da = [DataAccess da];
    MerchantInfo *mi=[da pmGetMerchantInfoByName:mygcname];
    self.p_showCreds=mi.p_showCreds;
    self.p_showCardNum=mi.p_showCardNum;
    self.p_showCardPIN=mi.p_showCardPIN;
    self.p_name=mi.p_name;
    self.p_url=mi.p_url;
    self.p_phone=mi.p_phone;
    self.p_reqReg=mi.p_reqReg;
    self.p_maxCardLen=mi.p_maxCardLen;
    self.p_minCardLen=mi.p_minCardLen;
    self.p_maxPINLen=mi.p_maxPINLen;
    self.p_minPINLen=mi.p_minPINLen;
    self.p_note=mi.p_note;
}
@end
