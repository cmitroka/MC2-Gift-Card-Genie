//
//  TVCAppDelegate.m
//  NAVCON00
//
//  Created by Chris on 2/1/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "TVCAppDelegate.h"
#import "TVCMasterViewController.h"
#import "SplashScreen00.h"
#import "Communicator.h"
#import "CJMUtilities.h"
#import "Feedback.h"
#import "StaticData.h"
#import "DataAccess.h"
#import "MyGCs.h"
#import "ViewGCs.h"
#import "AddModGC.h"
#import "SwitchViewController.h"
#import "TestCenter.h"
#import "SFHFKeychainUtils.h"
#import "OutOfLookups.h"
#import "WatchAd.h"
/*
@interface TVCAppDelegate()
{
}
@end
*/
@implementation TVCAppDelegate
@synthesize window;
@synthesize navigationController;
@synthesize viewController;
@synthesize switchViewController;
- (void)applicationDidFinishLaunching:(UIApplication *)application { 
    self.window = [[UIWindow alloc] initWithFrame:[[UIScreen mainScreen] bounds]];
    StaticData *sd=[StaticData sd];

    NSString *convNumStr=[SFHFKeychainUtils pmGetValueForSetting:@"Conv"];
    int convNum=[CJMUtilities ConvertNSStringToInt:convNumStr];
    //convNum=0;  //force a conversion
    if (convNum==0)
    {
        DataAccess *da = [DataAccess da];
        [da pmConvDB];
        NSString *LastRetrievalCode=[da pmGetSetting:@"LastRetrievalCode"];
        [SFHFKeychainUtils pmUpdateSettingName:@"LastRetrievalCode" SettingValue:LastRetrievalCode];
        [SFHFKeychainUtils pmUpdateSettingName:@"Conv" SettingValue:@"1"];
    }
    NSString *appStatus=[SFHFKeychainUtils pmGetValueForSetting:@"AppStatus"];
    if ([appStatus isEqualToString:@"Purchased"])
    {
        sd.pPurchased=@"1";
    }
    //NSString *version = [[[NSBundle mainBundle] infoDictionary] objectForKey:(NSString*)kCFBundleVersionKey];
    NSString *version = [[NSBundle mainBundle] objectForInfoDictionaryKey: @"CFBundleShortVersionString"];
    NSString *appversion=[NSString stringWithFormat:@"V%@", version];
    sd.pVersion=appversion;
    NSString *versionStripped=[CJMUtilities StripAllButNumbers:version];
    int iversionStripped=[CJMUtilities ConvertNSStringToInt:versionStripped];
    sd.pVersionNumeric=iversionStripped;
    
    [self useViewController:[SplashScreen00 class]];
    
}

-(void)useNavController:(Class)classType
{
    NSString *className = NSStringFromClass(classType);
    viewController = [[classType alloc] initWithNibName:className bundle:nil];
    navigationController = [[UINavigationController alloc] initWithRootViewController:viewController];
    self.window.rootViewController = navigationController;
    [self.window makeKeyAndVisible];
}
-(void)useSubview
{
    switchViewController=[[SwitchViewController alloc] init];
    [self.window addSubview:switchViewController.view];
    [self.window makeKeyAndVisible];
}
-(void)useViewController:(Class)classType {
    NSString *className = NSStringFromClass(classType);
    //viewController = [[classType alloc] initWithMerchantName:@"5 Guys" myGCName:@"5 Guys"];
    viewController = [[classType alloc] initWithNibName:className bundle:nil];
    self.window.rootViewController = viewController;
    [self.window makeKeyAndVisible];
}

-(void)useViewControllerS:(NSString *)className {
    UIViewController *tempUIViewController;
    Class classFromString = NSClassFromString(className);
    tempUIViewController= [[classFromString alloc] initWithNibName:className bundle:nil];
    self.window.rootViewController = tempUIViewController;
    [self.window makeKeyAndVisible];
}

/*
- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions
{
    //[self loadNavigationController];
    //[self loadViewController];
    return YES;
}
*/
-(void)loadViewController
{
    self.viewController = [[Feedback alloc] initWithNibName:@"S01" bundle:nil]; 
    //NSString *IP=[CJMUtilities getIPAddress];
    self.window.rootViewController = self.viewController;
    [self.window makeKeyAndVisible];    
}
-(void)loadNavController
{
    self.window = [[UIWindow alloc] initWithFrame:[[UIScreen mainScreen] bounds]];
    TVCMasterViewController *masterViewController = [[TVCMasterViewController alloc] initWithNibName:@"TVCMasterViewController" bundle:nil];
    self.navigationController = [[UINavigationController alloc] initWithRootViewController:masterViewController];
    self.window.rootViewController = self.navigationController;        
    [self.window makeKeyAndVisible];
}
+ (id)loadController:(Class)classType {
    NSString *className = NSStringFromClass(classType);
    UIViewController *controller = [[classType alloc] initWithNibName:className bundle:nil];
    return controller;
}
-(void)loadVCFeedback
{
    self.window = [[UIWindow alloc] initWithFrame:[[UIScreen mainScreen] bounds]];
    self.feed = [[Feedback alloc] initWithNibName:@"Feedback" bundle:nil];    
    self.window.rootViewController = self.feed;
    [self.window makeKeyAndVisible];
}

-(void)loadNavigationController
{
    self.window = [[UIWindow alloc] initWithFrame:[[UIScreen mainScreen] bounds]];
    TVCMasterViewController *masterViewController = [[TVCMasterViewController alloc] initWithNibName:@"TVCMasterViewController" bundle:nil];
    self.navigationController = [[UINavigationController alloc] initWithRootViewController:masterViewController];
    
    self.viewController = [[UIViewController alloc] initWithNibName:@"TVCMasterViewController" bundle:nil];
    
    self.window.rootViewController = self.navigationController;
    [self.window makeKeyAndVisible];      
}
- (void)changeDisplay:(int)viewnumber
{
    self.window = [[UIWindow alloc] initWithFrame:[[UIScreen mainScreen] bounds]];
    TVCMasterViewController *masterViewController = [[TVCMasterViewController alloc] initWithNibName:@"TVCMasterViewController" bundle:nil];
    self.navigationController = [[UINavigationController alloc] initWithRootViewController:masterViewController];
    self.viewController = [[UIViewController alloc] initWithNibName:@"TVCMasterViewController" bundle:nil];
    if (viewnumber==0) {
        self.window.rootViewController = self.viewController;
    }
    else
    {
        self.window.rootViewController = self.navigationController;        
    }
    [self.window makeKeyAndVisible];
}

- (void)applicationWillResignActive:(UIApplication *)application
{
    /*
     Sent when the application is about to move from active to inactive state. This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) or when the user quits the application and it begins the transition to the background state.
     Use this method to pause ongoing tasks, disable timers, and throttle down OpenGL ES frame rates. Games should use this method to pause the game.
     */
}

- (void)applicationDidEnterBackground:(UIApplication *)application
{
    /*
     Use this method to release shared resources, save user data, invalidate timers, and store enough application state information to restore your application to its current state in case it is terminated later. 
     If your application supports background execution, this method is called instead of applicationWillTerminate: when the user quits.
     */
}

- (void)applicationWillEnterForeground:(UIApplication *)application
{
    /*
     Called as part of the transition from the background to the inactive state; here you can undo many of the changes made on entering the background.
     */
}

- (void)applicationDidBecomeActive:(UIApplication *)application
{
    /*
     Restart any tasks that were paused (or not yet started) while the application was inactive. If the application was previously in the background, optionally refresh the user interface.
     */
}

- (void)applicationWillTerminate:(UIApplication *)application
{
    /*
     Called when the application is about to terminate.
     Save data if appropriate.
     See also applicationDidEnterBackground:.
     */
}

@end
