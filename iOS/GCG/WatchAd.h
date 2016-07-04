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
    IBOutlet UITextField *uiLogA;
}

@property(nonatomic, weak) IBOutlet GADBannerView *bannerView;
@property(nonatomic, weak) IBOutlet UITextView *uiLogB;
@end
