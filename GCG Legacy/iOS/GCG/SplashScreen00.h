//
//  SplashScreen00.h
//  GCG
//
//  Created by Chris Mitroka on 3/3/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//


#import <UIKit/UIKit.h>

@interface SplashScreen00 : UIViewController<UIAlertViewDelegate>
{
}
@property(nonatomic, retain) IBOutlet UIProgressView *progressbar;
@property(nonatomic, retain) IBOutlet UILabel *lblVersion;

@property(nonatomic, retain) NSTimer *doLoadTimer;
@property(nonatomic, retain) NSTimer *timer;
-(void)processData:(NSString *)processData maxRecs:(int)maxRecs;
-(void)changeScreens;

@end

