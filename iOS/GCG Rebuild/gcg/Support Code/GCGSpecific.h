//
//  GCGSpecific.h
//  GCG
//
//  Created by Chris on 9/29/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>
#ifdef DEBUG
#define TEST @"1"
#else
#define TEST @"2"
#endif
#define gcgLINEDEL @"^)("
#define gcgPIECEDEL @"~_~"
#define gcgGCBALANCE @"GCBALANCE"
#define gcgGCCAPTCHA @"GCCAPTCHA"
#define gcgGCNEEDSMORRINFO @"GCNEEDSMOREINFO"
#define gcgGCBALANCEERR @"GCBALANCEERR"
#define gcgGCERR @"GCERR"
#define gcgWSBLOCKEDIP @"WSBLOCKEDIP"
#define gcgGCCUSTOM @"GCCUSTOM"
#define gcgJEXEMISSING @"JEXEMISSING"



@interface GCGSpecific : NSObject
{
}
//+(void)pmHandleResponse:(NSString *)rs PassNavView:(UINavigationController *)ViewToSwitchTo;
+(NSString *)pmGetChecksum: (NSString *)sessionIdIn;
+(void)pmDoAutoLookup: (NSString *)sessionIdIn;
@end
