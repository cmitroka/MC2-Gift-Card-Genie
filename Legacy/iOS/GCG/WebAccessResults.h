//
//  WebAccessResults.h
//  GCG
//
//  Created by Chris on 11/10/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface WebAccessResults : NSObject
+ (WebAccessResults *)war;
@property (readwrite, retain) NSString *pResult;
@property (readwrite, retain) NSString *pStatus;
@end
