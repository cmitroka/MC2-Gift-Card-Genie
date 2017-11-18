//
//  Communicator4.h
//  GCG
//
//  Created by Chris on 9/27/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface Communicator4 : NSObject <NSXMLParserDelegate>
{

}
-(NSString *)pmDoGenericRequest:(NSString *)operation bodyData:(NSString *)bodyData;
@end
