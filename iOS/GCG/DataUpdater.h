//
//  DataUpdater.h
//  GCG
//
//  Created by Chris on 8/10/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "DataAccess.h"
@interface DataUpdater : UIViewController
{
    IBOutlet UILabel *lblmessage;
    IBOutlet UIProgressView *progressbar;
    float zFltProgress;
    int zIntCurrentCount;
    int zIntMerchantCount;
    DataAccess *da;
    NSTimer *timer;
    NSString *zStrRemMerchData;
    NSString *zStrTemp;
}
@end
