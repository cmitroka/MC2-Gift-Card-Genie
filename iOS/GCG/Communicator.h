//
//  Communicator.h
//  GCG
//
//  Created by Chris Mitroka on 4/1/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>
@interface Communicator : NSObject
{
    NSString *requestXML;
    NSString *requestOperation;
}
@property(nonatomic, retain) NSString *currentXMLResp;
@property(nonatomic, retain) NSString *currentXMLStatus;
@property(nonatomic, retain) NSMutableData *webDataReceived;
- (id)init;
-(void)DoGenericRequest:(NSString *)operation bodyData:(NSString *)bodyData secTimeout:(int)secTimeout retryAmnt:(int)retryAmnt;
- (NSString *)xmlHTMLUnencodeString:(NSString *)NSStringIn;
- (NSString *)xmlHTMLEncodeString:(NSString *)NSStringIn;
@end
