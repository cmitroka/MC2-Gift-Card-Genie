//
//  GiftCard.h
//  GCG
//
//  Created by Chris on 8/20/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>
//@class MyCardX,CardAbilitiesX;
@interface MyCard : NSObject
@property (nonatomic, retain) NSString *p_gctype;
@property (nonatomic, retain) NSString *p_gcnum;
@property (nonatomic, retain) NSString *p_gcpin;
@property (nonatomic, retain) NSString *p_credlogin;
@property (nonatomic, retain) NSString *p_credpass;
@property (nonatomic, retain) NSString *p_mygcname;
@property (nonatomic, retain) NSString *p_lastknownbal;
@property (nonatomic, retain) NSString *p_lastbaldate;

-(id)initWithName:(NSString *)n;
@end

/*
@interface CardAbilities : NSObject
@property (nonatomic, retain) NSString *p_hasgcnum;
@property (nonatomic, retain) NSString *p_hasgcpin;
@property (nonatomic, retain) NSString *p_hasgccreds;
@property (nonatomic, retain) NSString *p_hasgccreds;
-(id)initWithName:(NSString *)n;
@end
@interface Merchant00 : NSObject
@property (nonatomic, readwrite) BOOL favorite;
@property (nonatomic, readwrite) BOOL isAutoLookup;
@property (nonatomic, readwrite) BOOL showCardNum;
@property (nonatomic, readwrite) BOOL showCardPIN;
@property (nonatomic, readwrite) BOOL showCreds;
@property (nonatomic, retain) NSString *name;
@property (nonatomic, retain) NSString *url;
@property (nonatomic, retain) NSString *phone;
@end
 */

@interface MerchantInfo : NSObject
@property (nonatomic, retain) NSString *p_name;
@property (nonatomic, retain) NSString *p_url;
@property (nonatomic, retain) NSString *p_phone;
@property (nonatomic, readwrite) BOOL p_showCardNum;
@property (nonatomic, readwrite) BOOL p_showCardPIN;
@property (nonatomic, readwrite) BOOL p_showCreds;
@property (nonatomic, readwrite) BOOL p_reqReg;
@property int p_maxCardLen;
@property int p_minCardLen;
@property int p_maxPINLen;
@property int p_minPINLen;
@property (nonatomic, retain) NSString *p_note;
-(id)initWithName:(NSString *)n;
@end
