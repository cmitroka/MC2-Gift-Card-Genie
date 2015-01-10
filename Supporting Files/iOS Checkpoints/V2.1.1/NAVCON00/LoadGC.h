//
//  LoadGC.h
//  NAVCON01
//
//  Created by Chris on 8/25/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "GiftCard.h"
#import "iAd/ADBannerView.h"
@interface LoadGC : UIViewController<ADBannerViewDelegate>
{
    IBOutlet ADBannerView *_ADBannerView;
    IBOutlet UIButton *btnGoToAd;
    IBOutlet UIImageView *AdImage;
    IBOutlet UIActivityIndicatorView *spinner;
    IBOutlet UITextField *tfMerchant;
    IBOutlet UITextField *tfGCNumber;
    IBOutlet UIButton *btnGoToWebpage;
    IBOutlet UIButton *btnAutoLookup;
    IBOutlet UIButton *btnAdjustBalance;
    IBOutlet UIButton *btnAutoLookupMod;
    NSString* mygcname;
    MerchantInfo *pLoadedGC;
    MyCard *pMyGC;
}
@property(nonatomic, retain) NSString* mygcname;
@property(nonatomic, retain) MyCard *pMyGC;
@property(nonatomic, retain) MerchantInfo *pLoadedGC;
@property(nonatomic, retain) NSTimer *timer;
@property(nonatomic, retain) IBOutlet UITextField *tfID;
@end
