//
//  WatchAd.m
//  GCG
//
//  Created by Chris Mitroka on 6/19/16.
//
//

#import "WatchAd.h"
#import <GoogleMobileAds/GoogleMobileAds.h>
#import <GoogleMobileAds/GADBannerView.h>
#import <GoogleMobileAds/GADBannerViewDelegate.h>
#import "StaticData.h"
#import "WebAccess.h"
#import "GCGSpecific.h"
#import "CJMUtilities.h"
@interface WatchAd ()

@end
StaticData *sd;

@implementation WatchAd
@synthesize bannerView;
- (void)viewDidLoad {
    [super viewDidLoad];
    sd=[StaticData sd];
    NSLog(@"Google Mobile Ads SDK version: %@", [GADRequest sdkVersion]);
    [_lblMsg setText:@"Attempting to load ad..."];
    self.bannerView.adUnitID = @"ca-app-pub-2250341510214691/7256076768";
    self.bannerView.rootViewController = self;
    
    GADRequest *request = [GADRequest request];
    // Requests test ads on devices you specify. Your test device ID is printed to the console when
    // an ad request is made. GADBannerView automatically returns test ads when running on a
    // simulator.
    //request.testDevices = @[ @"2499dc0434bdbd92fdf5ba7a7ca5dbad"];  //3GS
    request.testDevices = @[ kGADSimulatorID ];
    [self.bannerView loadRequest:request];
    
    // Do any additional setup after loading the view from its nib.
    //For testing
    //[self LogIt];

}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (void)adViewWillLeaveApplication:(GADBannerView *)adView {
    NSLog(@"adViewDidLeaveApplication");
    [self LogIt];
    NSLog(@"Out of here");
}
- (void)adView:(GADBannerView *)adView didFailToReceiveAdWithError:(GADRequestError *)error {
    NSLog(@"didFailToReceiveAdWithError");
    [_lblMsg setText:@"Sorry, the ad couldn't load - probably blocked by the WiFi your on.  Try again later."];
    bannerView.alpha=0;
}

-(void)LogIt
{
    WebAccess *wa=[[WebAccess alloc]init];
    NSString *SessionIDAndAdInfo =[wa pmGetSessionIDAndAdInfo:@""];
    NSMutableArray *SessionIDAndAdInfoPieces=[CJMUtilities ConvertNSStringToNSMutableArray:SessionIDAndAdInfo delimiter:gcgPIECEDEL];
    NSString *SessionID=[SessionIDAndAdInfoPieces objectAtIndex:0];
    NSString *Checksum=[GCGSpecific pmGetChecksum:SessionID];
    [wa pmLogPurchase:SessionID CheckSum:Checksum PurchaseType:@"3"];
    
}

@end
