//
//  ResourceHandler.m
//  GCG
//
//  Created by Chris on 8/12/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "ResourceHandler.h"
#import "DataAccess.h"

@implementation ResourceHandler

- (id)init {

    databasePath=[DataAccess da].databasePath;
    databaseName=[DataAccess da].databaseName;
    return self;
}
- (id)initForDB:(NSString *)pDatabasePath databaseName:(NSString *)pDatabaseName
{    
    databasePath=pDatabasePath;
    databaseName=pDatabaseName;
    return self;
}

-(BOOL)checkDatabase
{
	BOOL retVal;
	NSFileManager *fileManager = [NSFileManager defaultManager];
	retVal = [fileManager fileExistsAtPath:databasePath];
    return retVal;
}
-(BOOL)pmCreateDatabase
{
	BOOL retVal;
	NSString *databasePathFromApp=@"";
    NSFileManager *fileManager = [NSFileManager defaultManager];
	databasePathFromApp = [[[NSBundle mainBundle] resourcePath] stringByAppendingPathComponent:databaseName];
    [fileManager removeItemAtPath:databasePath error:NULL];
    retVal=[fileManager copyItemAtPath:databasePathFromApp toPath:databasePath error:nil];    
    return retVal;
}

-(BOOL)pmCheckAndCreateDatabase
{
	BOOL retVal;
    retVal=[self checkDatabase];
    if (retVal==NO) {
        retVal=[self pmCreateDatabase];
    }
    return retVal;
}

@end
