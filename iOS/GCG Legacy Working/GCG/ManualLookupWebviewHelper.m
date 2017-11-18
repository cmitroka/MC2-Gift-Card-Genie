//
//  ManualLookupWebviewHelper.m
//  GCG
//
//  Created by Chris Mitroka on 10/3/16.
//
//

#import "ManualLookupWebviewHelper.h"

@implementation ManualLookupWebviewHelper
static ManualLookupWebviewHelper *_mlwh;

+ (ManualLookupWebviewHelper *)mlwh{
    if (_mlwh == nil) {
        _mlwh = [[ManualLookupWebviewHelper alloc] init];
        
    }
    return _mlwh;
}

@end
