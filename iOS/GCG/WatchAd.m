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
#import "ViewBannerAd.h"
#import "ViewInterstitialAd.h"

@interface WatchAd ()

@end
StaticData *sd;

@implementation WatchAd

- (void)viewDidLoad {
    viewBannerAdV1.layer.borderColor=[[UIColor blackColor] CGColor];
    viewBannerAdV1.layer.borderWidth=1.0f;
    _viewInterstitialAd.layer.borderColor=[[UIColor blackColor] CGColor];
    _viewInterstitialAd.layer.borderWidth=1.0f;
}
-(IBAction)doViewBannerAd:(id)sender
{
    ViewBannerAd *v = [[ViewBannerAd alloc] init];
    [self.navigationController pushViewController:v animated:YES];
}
-(IBAction)doViewInterstitialAd:(id)sender
{
    ViewInterstitialAd *v = [[ViewInterstitialAd alloc] init];
    [self.navigationController pushViewController:v animated:YES];
}
- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}


@end
