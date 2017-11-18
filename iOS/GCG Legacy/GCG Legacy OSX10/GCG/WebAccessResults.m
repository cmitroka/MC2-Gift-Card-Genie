//
//  WebAccessResults.m
//  GCG
//
//  Created by Chris on 11/10/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "WebAccessResults.h"

@implementation WebAccessResults
@synthesize pResult=_pResult, pStatus=_pStatus;
static NSString *_pResult=@"";
static NSString *_pStatus=@"";
static WebAccessResults *_war;
+ (WebAccessResults *)war{
    if (_war == nil) {
        _war = [[WebAccessResults alloc] init];        
    }
    return _war;
}
@end
