//
//  TVCAppDelegate.h
//  NAVCON00
//
//  Created by Chris on 2/1/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>

@class SplashScreen00;
@class TVCMasterViewController;
@class Feedback;
@class SwitchViewController;
@interface TVCAppDelegate : NSObject <UIApplicationDelegate>
{
    IBOutlet SwitchViewController *switchViewController;
}

@property (nonatomic,retain) UIWindow *window;
@property (nonatomic,retain) SwitchViewController *switchViewController;
@property (nonatomic,retain) UINavigationController *navigationController;
@property (nonatomic,retain) UIViewController *viewController;
@property (nonatomic,retain) SplashScreen00 *splash;
@property (nonatomic,retain) TVCMasterViewController *main;
@property (nonatomic,retain) Feedback *feed;

-(void)changeDisplay:(int)viewnumber;
-(void)loadNavController;
-(void)loadNavigationController;
-(void)loadViewController;
-(void)loadVCFeedback;
-(void)useViewController:(Class)classType;
-(void)useNavController:(Class)classType;
-(void)pushView:(NSString *)xibName;
-(void)useSubview;
-(void)usePhotoController;
@end
