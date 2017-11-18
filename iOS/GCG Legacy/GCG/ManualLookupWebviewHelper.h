//
//  ManualLookupWebviewHelper.h
//  GCG
//
//  Created by Chris Mitroka on 10/3/16.
//
//

#import <Foundation/Foundation.h>

@interface ManualLookupWebviewHelper : NSObject
+ (ManualLookupWebviewHelper *)mlwh;

@property (nonatomic, retain) NSString *pGCNum;
@property (nonatomic, retain) NSString *pGCType;
@property (nonatomic, retain) NSString *pPIN;
@property (nonatomic, retain) NSString *pURL;
@property (nonatomic, retain) NSString *pID;

@end
