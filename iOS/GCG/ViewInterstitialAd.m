//
//  ViewInterstitialAd.m
//  GCG
//
//  Created by Chris Mitroka on 7/6/16.
//
//
#import <GoogleMobileAds/GoogleMobileAds.h>
#import "ViewInterstitialAd.h"
#import "StaticData.h"
#import "WebAccess.h"
#import "GCGSpecific.h"
#import "CJMUtilities.h"

@interface ViewInterstitialAd () <GADInterstitialDelegate>
@property(nonatomic, strong) GADInterstitial *interstitial;
@property(nonatomic, strong) UITextField *uiLog;
@end

@implementation ViewInterstitialAd
static NSString *isClicked;
StaticData *sd;
- (void)viewDidLoad {
    [super viewDidLoad];
    isClicked=@"";
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
    //[[GADInterstitial alloc] initWithAdUnitID:@"ca-app-pub-3940256099942544/4411468910"];
    self.interstitial.adUnitID=@"ca-app-pub-2250341510214691/3397200761";
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
    [self ShowExit:0];
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
    if ([isClicked isEqualToString:@"1"]) {
        [self ShowExit:1];
    }
}

/// Called just before the app will background or terminate because the user clicked on an
/// ad that will launch another app (such as the App Store).
- (void)interstitialWillLeaveApplication:(GADInterstitial *)ad {
    NSLog(@"adViewDidLeaveApplication");
    isClicked=@"1";
    [self LogIt];
    NSLog(@"Out of here");
}
-(IBAction)Exit:(id)sender
{
    exit(0);
}
-(void)LogIt
{
    WebAccess *wa=[[WebAccess alloc]init];
    NSString *SessionIDAndAdInfo =[wa pmGetSessionIDAndAdInfo:@""];
    NSMutableArray *SessionIDAndAdInfoPieces=[CJMUtilities ConvertNSStringToNSMutableArray:SessionIDAndAdInfo delimiter:gcgPIECEDEL];
    NSString *SessionID=[SessionIDAndAdInfoPieces objectAtIndex:0];
    NSString *Checksum=[GCGSpecific pmGetChecksum:SessionID];
    [wa pmLogPurchase:SessionID CheckSum:Checksum PurchaseType:@"5"];

}

-(void)ShowExit:(int)MessageType
{
    if (MessageType==0) {
        [_uiExit setTitle:@"Sorry, the ad couldn't load - probably blocked by the WiFi your on.  Try again later." forState:NULL];
    }
    else if (MessageType==1) {
        [_uiExit setTitle:@"Thanks!  Click to exit, then relauch the app and you'll have more lookups!" forState:NULL];
    }
    
    _uiExit.hidden=NO;
    [_uiExit.titleLabel setTextAlignment: NSTextAlignmentCenter];
    _uiExit.layer.borderWidth=1.0f;
    _uiExit.layer.borderColor=[[UIColor blackColor] CGColor];
}
@end
