//
//  ViewBannerAd.h
//  GCG
//
//  Created by Chris Mitroka on 7/6/16.
//
//
#import <GoogleMobileAds/GoogleMobileAds.h>
#import <UIKit/UIKit.h>

@interface ViewBannerAd : UIViewController
@property(nonatomic, weak) IBOutlet GADBannerView *bannerView;
@property(nonatomic, weak) IBOutlet UITextView *uiLogB;
@property(nonatomic, weak) IBOutlet UITextField *uiLogA;
@end
