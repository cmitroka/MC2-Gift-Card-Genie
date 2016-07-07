//
//  WatchAd.h
//  GCG
//
//  Created by Chris Mitroka on 6/19/16.
//
//
#import <GoogleMobileAds/GoogleMobileAds.h>
#import <UIKit/UIKit.h>

@interface WatchAd : UIViewController
{
    //Things that dont need to be syntehesized
    IBOutlet UITextField *uiLogA;
    IBOutlet UIButton *viewBannerAdV1;
}
//Things that dont you'd access with an _ as the leading character.
@property(nonatomic, weak) IBOutlet UIButton *viewInterstitialAd;
@property(nonatomic, weak) IBOutlet GADBannerView *bannerView;
@property(nonatomic, weak) IBOutlet UITextView *uiLogB;
@end
