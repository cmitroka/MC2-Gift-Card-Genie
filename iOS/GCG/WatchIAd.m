//
//  WatchIAd.m
//  YMCA Check In
//
//  Created by Chris Mitroka on 9/5/16.
//  Copyright (c) 2016 MC2 Tech Services. All rights reserved.
//

#import "WatchIAd.h"
#import "LoadGC.h"
#import "Communicator.h"
#import "TVCAppDelegate.h"
#import <GoogleMobileAds/GoogleMobileAds.h>

@interface WatchIAd () <GADInterstitialDelegate>
@property(nonatomic, strong) GADInterstitial *interstitial;
@property(nonatomic, strong) UITextField *uiLog;
@end

@implementation WatchIAd
static WatchIAd *_wia;
+ (WatchIAd *)wia{
    if (_wia == nil) {
        _wia = [[WatchIAd alloc] init];
        //NSString *version = [[[NSBundle mainBundle] infoDictionary] objectForKey:(NSString*)kCFBundleVersionKey];
        //NSString *appversion=[NSString stringWithFormat:@"V%@", version];
    }
    return _wia;
}
WatchIAd *wia;

static NSString *isClicked;
- (void)viewDidLoad {
    [super viewDidLoad];
    isClicked=@"";
    wia=[WatchIAd wia];
    [self createAndLoadInterstitial];
    // Do any additional setup after loading the view from its nib.
    
    //For testing
    //[self LogIt];
    
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}
- (void)createAndLoadInterstitial {
    self.interstitial = [[GADInterstitial alloc] init];
    //self.interstitial =
    self.interstitial.adUnitID=@"ca-app-pub-2250341510214691/5332333962";  //YMCA Check In FREE | iOS
    self.interstitial.delegate=self;
    GADRequest *request = [GADRequest request];
    // Request test ads on devices you specify. Your test device ID is printed to the console when
    // an ad request is made.
    request.testDevices = @[ kGADSimulatorID, @"2077ef9a63d2b398840261c8221a0c9b" ];
    [self.interstitial loadRequest:request];
}

/// Called when an interstitial ad request succeeded.
- (void)interstitialDidReceiveAd:(GADInterstitial *)ad {
    NSLog(@"interstitialDidReceiveAd");
    [self.interstitial presentFromRootViewController:self];
}

/// Called when an interstitial ad request failed.
- (void)interstitial:(GADInterstitial *)ad
didFailToReceiveAdWithError:(GADRequestError *)error {
    NSLog(@"interstitial:didFailToReceiveAdWithError: %@", [error localizedDescription]);
    wia.pWatchAdStatus=@"AdCompleted";
    uiDesc.text=@"Ad done; please go back and the lookup will work.  BTW - purchasers never see ads.";
    uiSpinner.alpha=0;
}

/// Called just before presenting an interstitial.
- (void)interstitialWillPresentScreen:(GADInterstitial *)ad {
    NSLog(@"interstitialWillPresentScreen");
}

/// Called before the interstitial is to be animated off the screen.
- (void)interstitialWillDismissScreen:(GADInterstitial *)ad {
    NSLog(@"interstitialWillDismissScreen");
}

/// Called just after dismissing an interstitial and it has animated off the screen.
- (void)interstitialDidDismissScreen:(GADInterstitial *)ad {
    NSLog(@"interstitialDidDismissScreen");
    wia.pWatchAdStatus=@"AdCompleted";
    uiDesc.text=@"Ad done; please go back and the lookup will work.  BTW - purchasers never see ads.";
    uiSpinner.alpha=0;
    TVCAppDelegate *appDelegate = (TVCAppDelegate *)[[UIApplication sharedApplication] delegate];
    //[appDelegate useViewControllerS:@"WaitScreen"];
    //[appDelegate useViewControllerS:wia.pWatchAdGoToPage];

}

/// Called just before the app will background or terminate because the user clicked on an
/// ad that will launch another app (such as the App Store).
- (void)interstitialWillLeaveApplication:(GADInterstitial *)ad {
    NSLog(@"adViewDidLeaveApplication");
    uiDesc.text=@"Ad done; please go back and the lookup will work.  BTW - purchasers never see ads.";
    uiSpinner.alpha=0;
    [self LogIt];
    wia.pWatchAdStatus=@"AdCompleted";
    //TVCAppDelegate *appDelegate = (TVCAppDelegate *)[[UIApplication sharedApplication] delegate];
    //[appDelegate useViewControllerS:wia.pWatchAdGoToPage];
}
-(IBAction)Restart:(id)sender
{
    TVCAppDelegate *appDelegate = (TVCAppDelegate *)[[UIApplication sharedApplication] delegate];
    //[appDelegate useViewControllerS:@"Splashscreen"];
}
-(IBAction)Exit:(id)sender
{
    exit(0);
}
-(void)LogIt
{
    //NSLog(@"app.pBCBID: %@", app.pBCBID);
    //NSLog(@"pWatchAdDoneFrom: %@", app.pWatchAdDoneFrom);
    //NSLog(@"pWatchAdGoToPage: %@", app.pWatchAdGoToPage);
    NSString *postData = [NSString stringWithFormat:@"UUID=%@&pSource=%@", wia.pWatchAdIDtoLog, wia.pWatchAdDoneFrom];
    [self sendRequest:postData operationToDo:@"LogAdClick"];
}
-(void)sendRequest:(NSString *)post operationToDo:(NSString *)pOperation
{
    NSURLConnection *connection;
    NSMutableData *receivedData;
    NSString *BaseURL=@"https://gcg.mc2techservices.com/WebService.asmx";
    NSString *Operation=pOperation;
    NSData *postData = [post dataUsingEncoding:NSASCIIStringEncoding allowLossyConversion:YES];
    NSString *postLength = [NSString stringWithFormat:@"%d", [postData length]];
    
    NSString *strurl=@"";
    if (Operation.length==0) {
        strurl=[NSString stringWithFormat:@"%@", Operation];
    }
    else
    {
        strurl=[NSString stringWithFormat:@"%@%@%@", BaseURL, @"/", Operation];
        
    }
    NSURL *url = [NSURL URLWithString:strurl];
    NSMutableURLRequest *theRequest = [NSMutableURLRequest requestWithURL:url];
    [theRequest setHTTPMethod:@"POST"];
    [theRequest setValue:postLength forHTTPHeaderField:@"Content-Length"];
    [theRequest setHTTPBody:postData];
    [theRequest setValue:@"application/x-www-form-urlencoded" forHTTPHeaderField:@"Content-Type"];
    connection = [[NSURLConnection alloc] initWithRequest:theRequest delegate:self];
    connection.accessibilityHint=@"";
    if(connection)
    {
        NSLog(@"Connection: %@ with params of %@ Sent", strurl, post);
    }
    else
    {
        NSLog(@"Connection: %@ with params of %@ Failed", strurl, post);
    }
}

@end
