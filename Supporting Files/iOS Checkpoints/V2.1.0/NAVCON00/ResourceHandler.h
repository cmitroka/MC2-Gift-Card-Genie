//
//  ResourceHandler.h
//  NAVCON01
//
//  Created by Chris on 8/12/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface ResourceHandler : NSObject
{
    NSString *databasePath;
    NSString *databaseName;
}
-(BOOL)pmCheckAndCreateDatabase;
-(BOOL)pmCreateDatabase;
- (id)initForDB:(NSString *)pDatabasePath databaseName:(NSString *)pDatabaseName;
@end
