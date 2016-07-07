//
//  ViewBannerAd.m
//  GCG
//
//  Created by Chris Mitroka on 7/6/16.
//
//

#import "ViewBannerAd.h"
#import <GoogleMobileAds/GoogleMobileAds.h>
#import <GoogleMobileAds/GADBannerView.h>
#import <GoogleMobileAds/GADBannerViewDelegate.h>
#import "StaticData.h"
#import "WebAccess.h"
#import "GCGSpecific.h"
#import "CJMUtilities.h"
@interface ViewBannerAd ()

@end
StaticData *sd;

@implementation ViewBannerAd

- (void)viewDidLoad {
    [super viewDidLoad];
    sd=[StaticData sd];
    NSLog(@"Google Mobile Ads SDK version: %@", [GADRequest sdkVersion]);
    self.bannerView.adUnitID = @"ca-app-pub-2250341510214691/7256076768";
    self.bannerView.rootViewController = self;
    
    GADRequest *request = [GADRequest request];
    // Requests test ads on devices you specify. Your test device ID is printed to the console when
    // an ad request is made. GADBannerView automatically returns test ads when running on a
    // simulator.
    request.testDevices = @[ @"2499dc0434bdbd92fdf5ba7a7ca5dbad"];  //3GS
    request.testDevices = @[ kGADSimulatorID ];
    [self.bannerView loadRequest:request];
    
    // Do any additional setup after loading the view from its nib.
    _uiLogB.layer.borderColor=[[UIColor blackColor] CGColor];
    _uiLogB.layer.borderWidth=1.0f;
    _bannerView.layer.borderColor=[[UIColor blackColor] CGColor];
    _bannerView.layer.borderWidth=1.0f;
    

}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (void)adViewWillLeaveApplication:(GADBannerView *)adView {
    NSLog(@"adViewDidLeaveApplication");
    WebAccess *wa=[[WebAccess alloc]init];
    NSString *SessionIDAndAdInfo =[wa pmGetSessionIDAndAdInfo:@""];
    NSMutableArray *SessionIDAndAdInfoPieces=[CJMUtilities ConvertNSStringToNSMutableArray:SessionIDAndAdInfo delimiter:gcgPIECEDEL];
    NSString *SessionID=[SessionIDAndAdInfoPieces objectAtIndex:0];
    NSString *Checksum=[GCGSpecific pmGetChecksum:SessionID];
    [wa pmLogPurchase:SessionID CheckSum:Checksum PurchaseType:@"05"];
    NSLog(@"Out of here");
}
/// Called when an ad request loaded an ad.
- (void)adViewDidReceiveAd:(GADBannerView *)adView {
    NSLog(@"adViewDidReceiveAd");
    _uiLogA.text=[NSString stringWithFormat:@"%@", @"adViewDidReceiveAd"];
    _uiLogB.text=[NSString stringWithFormat:@"%@/%@", _uiLogB.text, @"adViewDidReceiveAd"];
}

/// Called when an ad request failed.
- (void)adView:(GADBannerView *)adView didFailToReceiveAdWithError:(GADRequestError *)error {
    NSLog(@"adViewDidFailToReceiveAdWithError: %@", [error localizedDescription]);
    _uiLogB.text=[NSString stringWithFormat:@"%@ERROR: %@/%@", _uiLogB.text, @"adViewDidFailToReceiveAdWithError", [error localizedDescription]];
    _uiLogA.text=[NSString stringWithFormat:@"%@/%@", @"adViewDidReceiveAd", [error localizedDescription]];

}

/// Called just before presenting the user a full screen view, such as
/// a browser, in response to clicking on an ad.
- (void)adViewWillPresentScreen:(GADBannerView *)adView {
    NSLog(@"adViewWillPresentScreen");
    _uiLogB.text=[NSString stringWithFormat:@"%@/%@", _uiLogB.text, @"adViewWillPresentScreen"];
}

/// Called just before dismissing a full screen view.
- (void)adViewWillDismissScreen:(GADBannerView *)adView {
    NSLog(@"adViewWillDismissScreen");
    _uiLogB.text=[NSString stringWithFormat:@"%@/%@", _uiLogB.text, @"adViewWillDismissScreen"];
}

/// Called just after dismissing a full screen view.
- (void)adViewDidDismissScreen:(GADBannerView *)adView {
    NSLog(@"adViewDidDismissScreen");
    _uiLogB.text=[NSString stringWithFormat:@"%@/%@", _uiLogB.text, @"adViewDidDismissScreen"];
}

@end
