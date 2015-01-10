//
//  DataAccess.m
//  GCG
//
//  Created by Chris on 8/7/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "DataAccess.h"
#import "GiftCard.h"
#import "CJMUtilities.h"
#import "ResourceHandler.h"
@implementation DataAccess
@synthesize merchantsAutoLookup, merchantsAll=_merchantsAll,cardNumbsForMerchants,databasePath,databaseName,myCards;
static DataAccess *_da;

+ (DataAccess *)da{
    if (_da == nil) {
        _da = [[DataAccess alloc] init];
    }
    return _da;
}

- (id)init {
	databaseName = @"merchants.sqlite";
	NSArray *documentPaths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
	NSString *documentsDir = [documentPaths objectAtIndex:0];
	databasePath = [documentsDir stringByAppendingPathComponent:databaseName];
    ResourceHandler *pResourceHandler=[[ResourceHandler alloc] initForDB:databasePath databaseName:databaseName];
    [pResourceHandler pmCheckAndCreateDatabase];
    return self;
}
-(NSString *)doStrRead:(int)colInt statement:(sqlite3_stmt*)statement
{
    NSString *retVal;
    @try {
        retVal = [NSString stringWithUTF8String:(char *)sqlite3_column_text(statement, colInt)];
    }
    @catch (NSException *exception) {
        NSLog(exception.reason);
        if ([CJMUtilities Contains:exception.reason.uppercaseString SearchForString:@"NULL"])
        {
            retVal=@"";
        }
            else
            {
                retVal=@"SQLV1ERROR";            
            }
    }
    @finally {        
    }
    return retVal;
}
-(int)doIntRead:(int)colInt statement:(sqlite3_stmt*)statement
{
    int retVal;
    @try {
        retVal = [NSString stringWithUTF8String:(char *)sqlite3_column_int(statement, colInt)];
    }
    @catch (NSException *exception) {
        NSLog(exception.reason);
        if ([CJMUtilities Contains:exception.reason.uppercaseString SearchForString:@"NULL"])
        {
            retVal=@"";
        }
        else
        {
            retVal=@"SQLV1ERROR";            
        }
    }
    @finally {        
    }
    return retVal;
}
-(bool)doInsertOrDelete:(NSString *)SQLIn
{
    bool retVal;
	sqlite3 *database;
    sqlite3_stmt *statement;
	if(sqlite3_open([databasePath UTF8String], &database) == SQLITE_OK) {
        const char *sqlStatement = [SQLIn UTF8String];
		if(sqlite3_prepare_v2(database, sqlStatement, -1, &statement, NULL) == SQLITE_OK)
        {
            if (sqlite3_step(statement) == SQLITE_DONE)
            {
                retVal=TRUE;
            } else {
                //NSAssert1(0, @"Error while updating. '%s'", sqlite3_errmsg(database));
                NSLog(@"Error1 in DoInsertOrDelete. '%s'", sqlite3_errmsg(database));
                retVal=FALSE;
            }
		}
        else
        {
            NSLog(@"Error2 in DoInsertOrDelete. '%s'", sqlite3_errmsg(database));
            retVal=FALSE;
        }
		sqlite3_finalize(statement);
	}
    else
    {
        NSLog(@"Error3 in DoInsertOrDelete. '%s'", sqlite3_errmsg(database));
        retVal=FALSE;
    }
	sqlite3_close(database);
    return retVal;
}
-(NSString *)pmGetSetting:(NSString *)setting
{
    NSString *retVal=@"UNDEFINED";
    sqlite3 *database;
    sqlite3_stmt *statement;
    NSString *SQL = [NSString stringWithFormat: @"SELECT svalue FROM config WHERE setting=\"%@\"",setting];
    if(sqlite3_open([databasePath UTF8String], &database) == SQLITE_OK) 
    {
        const char *sqlStatement = [SQL UTF8String];
        if(sqlite3_prepare_v2(database, sqlStatement, -1, &statement, NULL) == SQLITE_OK) {
            while(sqlite3_step(statement) == SQLITE_ROW) {
                retVal= [self doStrRead:0 statement:statement];
            }
        }
    }
    sqlite3_close(database);
    return retVal;
}
-(int)pmUpdateSetting:(NSString *)setting svalue:(NSString *)svalue
{
    NSString *SQL=@"";
    bool retval=FALSE;
    SQL = [NSString stringWithFormat: @"DELETE FROM config WHERE setting=\"%@\"", setting];
    retval=[self doInsertOrDelete:SQL];    
    
    SQL = [NSString stringWithFormat: @"INSERT INTO config (setting, svalue) VALUES (\"%@\", \"%@\")",setting,svalue];
    retval=[self doInsertOrDelete:SQL];    
    if (retval==TRUE) 
    {
        return 1;
    }
    return 0;
}


-(int)pmDoesMyCardExist:(NSString *)mygcname
{
    int retVal=0;
    sqlite3 *database;
    sqlite3_stmt *statement;
    NSString *SQL = [NSString stringWithFormat: @"SELECT * FROM mycards WHERE mygcname=\"%@\"",mygcname];
    if(sqlite3_open([databasePath UTF8String], &database) == SQLITE_OK) 
    {
        const char *sqlStatement = [SQL UTF8String];
        if(sqlite3_prepare_v2(database, sqlStatement, -1, &statement, NULL) == SQLITE_OK) {
            while(sqlite3_step(statement) == SQLITE_ROW) {
                retVal=1;
            }
        }
    }
    sqlite3_close(database);
    return retVal;
}
-(NSMutableArray *)pmGetMerchantsAutoLookup
{
    NSMutableArray *temp = [[NSMutableArray alloc] initWithObjects:nil];
	sqlite3 *database;
    sqlite3_stmt *statement;
    NSString *SQL = @"";
    NSString *tempname=@"";
    const char *sqlStatement = "";
    SQL=[NSString stringWithFormat: @"SELECT name FROM merchants where showCardNum='True' or showCardPIN='True' or showCreds='True'"];
    if(sqlite3_open([databasePath UTF8String], &database) == SQLITE_OK) 
    {
        sqlStatement = [SQL UTF8String];
        if(sqlite3_prepare_v2(database, sqlStatement, -1, &statement, NULL) == SQLITE_OK)
        {
            while(sqlite3_step(statement) == SQLITE_ROW) {
                tempname= [self doStrRead:0 statement:statement];
                [temp addObject:tempname];
            }
        }
        else
        {
            NSLog(@"pmGetMerchantsAutoLookup Error. %s", sqlite3_errmsg(database));
        }
    }
    merchantsAutoLookup=temp;
    return temp;
}
-(NSMutableArray *)pmGetCardNumbsForMerchants:(NSString *)merchantName
{
    NSMutableArray *temp = [[NSMutableArray alloc] initWithObjects:nil];
	sqlite3 *database;
    sqlite3_stmt *statement;
    NSString *SQL = @"";
    NSString *tempname=@"";
    const char *sqlStatement = "";
    SQL=[NSString stringWithFormat: @"SELECT gcnumb FROM mycards where gctype=\"%@\"",merchantName];
    if(sqlite3_open([databasePath UTF8String], &database) == SQLITE_OK) 
    {
        sqlStatement = [SQL UTF8String];
        if(sqlite3_prepare_v2(database, sqlStatement, -1, &statement, NULL) == SQLITE_OK)
        {
            while(sqlite3_step(statement) == SQLITE_ROW) {
                tempname= [self doStrRead:0 statement:statement];
                [temp addObject:tempname];
            }
        }
        else
        {
            NSLog(@"pmGetCardNumbsForMerchants Error. %s", sqlite3_errmsg(database));
        }
    }
    cardNumbsForMerchants=temp;
    return temp;
}

-(NSMutableArray *)pmGetMerchantsAll {
	sqlite3 *database;
    sqlite3_stmt *statement;
	NSMutableArray *temp = [[NSMutableArray alloc] init];
    [temp addObject:@"Unlisted - Manually Enter"];
    const char *sqlStatement = "SELECT * FROM merchants";
    if (sqlite3_open([databasePath UTF8String], &database) == SQLITE_OK)
    {
        if(sqlite3_prepare_v2(database, sqlStatement, -1, &statement, NULL) == SQLITE_OK) {
            while(sqlite3_step(statement) == SQLITE_ROW) {
                NSString *aName = [self doStrRead:0 statement:statement];
                [temp addObject:aName];        
            }    
        }
    }
    return temp;
}

-(MerchantInfo *)pmGetMerchantInfoByName:(NSString *)merchantName
{
    MerchantInfo *m=[[MerchantInfo alloc] init];
	sqlite3 *database;
    sqlite3_stmt *statement;
    NSString *SQL = @"";
    SQL=[NSString stringWithFormat: @"SELECT name,phone,url,showCardNum,showCardPIN,showCreds,reqReg,minCardLen,maxCardLen,minPINLen,maxPINLen,note FROM merchants WHERE name=\"%@\"", merchantName];
    if(sqlite3_open([databasePath UTF8String], &database) == SQLITE_OK) 
    {
        const char *sqlStatement = [SQL UTF8String];
        if(sqlite3_prepare_v2(database, sqlStatement, -1, &statement, NULL) == SQLITE_OK)
        {
            while(sqlite3_step(statement) == SQLITE_ROW) {
                NSString *aName = [self doStrRead:0 statement:statement];
                NSString *aPhone = [self doStrRead:1 statement:statement];
                NSString *aURL = [self doStrRead:2 statement:statement];
                NSString *ashowCardNum = [self doStrRead:3 statement:statement];
                NSString *ashowCardPIN = [self doStrRead:4 statement:statement];
                NSString *ashowCreds = [self doStrRead:5 statement:statement];
                NSString *areqReg = [self doStrRead:6 statement:statement];
                NSString *minCardLen=[self doStrRead:7 statement:statement];
                NSString *maxCardLen=[self doStrRead:8 statement:statement];
                NSString *minPINLen=[self doStrRead:9 statement:statement];
                NSString *maxPINLen=[self doStrRead:10 statement:statement];
                NSString *note=[self doStrRead:11 statement:statement];
                m.p_name=aName;
                m.p_phone=aPhone;
                m.p_url=aURL;
                m.p_showCardNum=ashowCardNum.boolValue;
                m.p_showCardPIN=ashowCardPIN.boolValue;
                m.p_showCreds=ashowCreds.boolValue;
                m.p_reqReg=areqReg.boolValue;
                m.p_maxCardLen=maxCardLen.integerValue;
                m.p_minCardLen=minCardLen.integerValue;
                m.p_maxPINLen=maxPINLen.integerValue;
                m.p_minPINLen=minPINLen.integerValue;
                m.p_note=note;
            }    
        }
        sqlite3_finalize(statement);
	}
    sqlite3_close(database);
    return m;
}
-(int)pmInsertFavorite:(NSString *)merchantName
{
    NSString *SQL = [NSString stringWithFormat: @"INSERT INTO favorites (name) VALUES (\"%@\")", merchantName];
    bool retval=[self doInsertOrDelete:SQL];    
    return retval;
}
-(int)pmUpdateMyCard:(NSString *)gctype gcnum:(NSString *)gcnum gcpin:(NSString *)gcpin credlogin:(NSString *)credlogin credpass:(NSString *)credpass mygcname:(NSString *)mygcname myoldgcname:(NSString *)myoldgcname
{
    NSString *SQL=@"";
    bool retval=FALSE;
    SQL = [NSString stringWithFormat: @"DELETE FROM mycards WHERE mygcname=\"%@\"", myoldgcname];
    retval=[self doInsertOrDelete:SQL];    
    
    SQL = [NSString stringWithFormat: @"INSERT INTO mycards (gctype, gcnumb, gcpin, credlogin, credpass, mygcname) VALUES (\"%@\", \"%@\", \"%@\", \"%@\", \"%@\", \"%@\")",gctype, gcnum, gcpin, credlogin, credpass, mygcname];
    retval=[self doInsertOrDelete:SQL];    
    if (retval==TRUE) return 1;
    return 0;
}
-(int)pmUpdateMyCardBalanceInfo:(NSString *)mygcname lastbalknown:(NSString *)lastbalknown lastbaldate:(NSString *)lastbaldate
{
    NSString *SQL=@"";
    bool retval=FALSE;
    //INSERT INTO mycards VALUES ('0','1','2','3','4','ID5','6','7')
    //UPDATE mycards SET lastbalknown='%@', lastbaldate='%@' WHERE mygcname='%@'
    SQL = [NSString stringWithFormat: @"UPDATE mycards SET lastbalknown='%@', lastbaldate='%@' WHERE mygcname=\"%@\"", lastbalknown, lastbaldate, mygcname];
    retval=[self doInsertOrDelete:SQL];    
    if (retval==TRUE)
    {
        return 1;
    }
    else
    {
        return 0;
    }
}


-(int)pmDeleteMyCard:(NSString *)mygcname
{
    NSString *SQL=@"";
    bool retval=FALSE;
    SQL = [NSString stringWithFormat: @"DELETE FROM mycards WHERE mygcname=\"%@\"", mygcname];
    retval=[self doInsertOrDelete:SQL];    
    if (retval==TRUE)
    {
        return 1;
    }
    else
    {
        return 0;
    }
}

-(int)pmDeleteMerchants
{
    NSString *SQL=@"";
    bool retval=FALSE;
    SQL = [NSString stringWithFormat: @"DELETE FROM merchants"];
    retval=[self doInsertOrDelete:SQL];    
    if (retval==TRUE) return 1;
    return 1;
}
-(int)pmDeleteMyCards
{
    NSString *SQL=@"";
    bool retval=FALSE;
    SQL = [NSString stringWithFormat: @"DELETE FROM mycards"];
    retval=[self doInsertOrDelete:SQL];    
    if (retval==TRUE) return 1;
    return 1;
    
}
-(int)pmInsertMerchant:(NSString *)pName url:(NSString *)pURL phone:(NSString *)pPhone showCardNum:(NSString *)pshowCardNum showCardPIN:(NSString *)pshowCardPIN showCreds:(NSString *)pshowCreds reqReg:(NSString *)preqReg minCardLen:(int)pminCardLen maxCardLen:(int)pmaxCardLen minPINLen:(int)pminPINLen maxPINLen:(int)pmaxPINLen note:(NSString *)pNote
{
    NSString *SQL=@"";
    bool retval=FALSE;
    SQL = [NSString stringWithFormat: @"INSERT INTO merchants (name, url, phone, showCardNum, showCardPIN, showCreds, reqReg, minCardLen, maxCardLen, minPINLen, maxPINLen, note) VALUES (\"%@\",\"%@\",\"%@\",\"%@\",\"%@\",\"%@\",\"%@\",%i,%i,%i,%i,\"%@\")", pName,pURL,pPhone,pshowCardNum,pshowCardPIN,pshowCreds,preqReg,pminCardLen, pmaxCardLen, pminPINLen,pmaxCardLen,pNote];
    retval=[self doInsertOrDelete:SQL];    

    if (retval==TRUE) return 1;
    return 1;
}
-(int)pmEraseFavorites {
    NSString *SQL=@"";
    bool retval=FALSE;
    SQL = [NSString stringWithFormat: @"DELETE FROM favorites"];
    retval=[self doInsertOrDelete:SQL];    
    if (retval==TRUE) return 1;
    return 0;
}
-(NSMutableArray *)pmGetMyCards
{
    sqlite3 *database;
    sqlite3_stmt *statement;
    NSMutableArray *temp = [[NSMutableArray alloc] initWithObjects:nil];
    NSString *SQL = @"";
    SQL=[NSString stringWithFormat: @"SELECT * FROM mycards ORDER BY mygcname"];
    if(sqlite3_open([databasePath UTF8String], &database) == SQLITE_OK) 
    {
        const char *sqlStatement = [SQL UTF8String];
        if(sqlite3_prepare_v2(database, sqlStatement, -1, &statement, NULL) == SQLITE_OK)
        {
            while(sqlite3_step(statement) == SQLITE_ROW)
            {
            NSString *gctype = [self doStrRead:0 statement:statement];
            NSString *gcnum = [self doStrRead:1 statement:statement];
            NSString *gcpin = [self doStrRead:2 statement:statement];
            NSString *credlogin=[self doStrRead:3 statement:statement];
            NSString *credpass=[self doStrRead:4 statement:statement];
            NSString *mygcname=[self doStrRead:5 statement:statement];
            NSString *lastknownbal=[self doStrRead:6 statement:statement];
            NSString *lastbaldate=[self doStrRead:7 statement:statement];
                
            MyCard *myCard = [[MyCard alloc] init];
            myCard.p_gctype=gctype;
            myCard.p_gcnum=gcnum;
            myCard.p_gcpin=gcpin;
            myCard.p_credlogin=credlogin;
            myCard.p_credpass=credpass;
            myCard.p_mygcname=mygcname;
            myCard.p_lastknownbal=lastknownbal;
            myCard.p_lastbaldate=lastbaldate;
            [temp addObject:myCard];        
            }    
            myCards=temp;
        }
    }
    return temp;
}
-(MyCard *)pmGetMyCard:(NSString *)mygcname
{
    sqlite3 *database;
    sqlite3_stmt *statement;
    MyCard *myCard = [[MyCard alloc] init];
    NSString *SQL = [NSString stringWithFormat: @"SELECT * FROM mycards WHERE mygcname=\"%@\"",mygcname];
    if(sqlite3_open([databasePath UTF8String], &database) == SQLITE_OK) 
    {
        const char *sqlStatement = [SQL UTF8String];
        if(sqlite3_prepare_v2(database, sqlStatement, -1, &statement, NULL) == SQLITE_OK) {
            while(sqlite3_step(statement) == SQLITE_ROW) {
                NSString *gctype = [self doStrRead:0 statement:statement];
                NSString *gcnum = [self doStrRead:1 statement:statement];
                NSString *gcpin = [self doStrRead:2 statement:statement];
                NSString *credlogin=[self doStrRead:3 statement:statement];
                NSString *credpass=[self doStrRead:4 statement:statement];
                NSString *mygcname=[self doStrRead:5 statement:statement];
                NSString *lastknownbal=[self doStrRead:6 statement:statement];
                NSString *lastbaldate=[self doStrRead:7 statement:statement];
                if (lastknownbal.length<1) {
                    lastknownbal=@"";
                }
                if (lastbaldate.length<1) {
                    lastbaldate=@"";
                }
                
                myCard.p_gctype=gctype;
                myCard.p_gcnum=gcnum;
                myCard.p_gcpin=gcpin;
                myCard.p_credlogin=credlogin;
                myCard.p_credpass=credpass;
                myCard.p_mygcname=mygcname;
                //myCard.p_lastknownbal=lastknownbal;
                //myCard.p_lastbaldate=lastbaldate;
            }
            return myCard;
        }
    }
    return myCard;
}
-(NSMutableArray *)pmGetMyCardArray:(NSString *)mygcname
{
    sqlite3 *database;
    sqlite3_stmt *statement;
    NSMutableArray *temp = [[NSMutableArray alloc] initWithObjects:nil];
    NSString *SQL = [NSString stringWithFormat: @"SELECT * FROM mycards WHERE mygcname=\"%@\"",mygcname];
    if(sqlite3_open([databasePath UTF8String], &database) == SQLITE_OK) 
    {
        const char *sqlStatement = [SQL UTF8String];
        if(sqlite3_prepare_v2(database, sqlStatement, -1, &statement, NULL) == SQLITE_OK) {
            while(sqlite3_step(statement) == SQLITE_ROW) {
                NSString *gctype = [self doStrRead:0 statement:statement];
                NSString *gcnum = [self doStrRead:1 statement:statement];
                NSString *gcpin = [self doStrRead:2 statement:statement];
                NSString *credlogin=[self doStrRead:3 statement:statement];
                NSString *credpass=[self doStrRead:4 statement:statement];
                NSString *mygcname=[self doStrRead:5 statement:statement];
                NSString *lastknownbal=[self doStrRead:6 statement:statement];
                NSString *lastbaldate=[self doStrRead:7 statement:statement];
                if (lastknownbal.length<1) {
                    lastknownbal=@"";
                }
                if (lastbaldate.length<1) {
                    lastbaldate=@"";
                }
                [temp addObject:gctype];
                [temp addObject:gcnum];
                [temp addObject:gcpin];
                [temp addObject:credlogin];
                [temp addObject:credpass];
                [temp addObject:mygcname];
                [temp addObject:lastknownbal];
                [temp addObject:lastbaldate];
            }
        }
    }
    return temp;
}
-(int)pmIsManual:(NSString *)gctype
{
    int retVal=0;
    sqlite3 *database;
    sqlite3_stmt *statement;
    NSString *SQL = [NSString stringWithFormat: @"SELECT * FROM merchants WHERE name=\"%@\"",gctype];
    if(sqlite3_open([databasePath UTF8String], &database) == SQLITE_OK) 
    {
        const char *sqlStatement = [SQL UTF8String];
        if(sqlite3_prepare_v2(database, sqlStatement, -1, &statement, NULL) == SQLITE_OK) {
            while(sqlite3_step(statement) == SQLITE_ROW) {
                retVal=1;
            }
        }
    }
    sqlite3_close(database);
    return retVal;
}
-(int)pmConvDB
{
    NSString *SQL=@"";
    bool retval=FALSE;
    //INSERT INTO mycards VALUES ('0','1','2','3','4','ID5','6','7')
    //UPDATE mycards SET lastbalknown='%@', lastbaldate='%@' WHERE mygcname='%@'
    SQL = @"ALTER TABLE \"merchants\" ADD COLUMN \"minCardLen\" INTEGER";
    retval=[self doInsertOrDelete:SQL];   
    SQL = @"ALTER TABLE \"merchants\" ADD COLUMN \"maxCardLen\" INTEGER";
    retval=[self doInsertOrDelete:SQL];
    SQL = @"ALTER TABLE \"merchants\" ADD COLUMN \"minPINLen\" INTEGER";
    retval=[self doInsertOrDelete:SQL];
    SQL = @"ALTER TABLE \"merchants\" ADD COLUMN \"maxPINLen\" INTEGER";
    retval=[self doInsertOrDelete:SQL];
    SQL = @"ALTER TABLE \"merchants\" ADD COLUMN \"note\" TEXT";
    retval=[self doInsertOrDelete:SQL];
    if (retval==TRUE)
    {
        return 1;
    }
    else
    {
        return 0;
    }
}
@end
