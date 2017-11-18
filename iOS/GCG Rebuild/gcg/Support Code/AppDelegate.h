//
//  AppDelegate.h
//  gcg
//
//  Created by Chris Mitroka on 11/17/17.
//  Copyright © 2017 Chris Mitroka. All rights reserved.
//

#import <UIKit/UIKit.h>

@class SplashScreen00;
@class TVCMasterViewController;
@class Feedback;
@class SwitchViewController;
@interface AppDelegate : NSObject <UIApplicationDelegate>
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
-(void)useViewControllerS:(NSString *)className;


@end

