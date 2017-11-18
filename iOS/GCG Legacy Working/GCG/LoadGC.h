//
//  LoadGC.h
//  GCG
//
//  Created by Chris on 8/25/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "GiftCard.h"
@interface LoadGC : UIViewController
{
    IBOutlet UIActivityIndicatorView *spinner;
    IBOutlet UITextField *tfMerchant;
    IBOutlet UITextField *tfGCNumber;
    IBOutlet UIButton *btnLookup;
    IBOutlet UILabel *lookuptype;
}
@property(nonatomic, retain) IBOutlet UITextField *tfID;
@property(nonatomic, retain) IBOutlet UIView *uiCAPTCHAView;
@property(nonatomic, retain) IBOutlet UITextField *uiCAPTCHAAnswer;
@property(nonatomic, retain) IBOutlet UIImageView *uiCAPTCHAImage;
@property(nonatomic, retain) MerchantInfo *pLoadedGC;
@property(nonatomic, retain) MyCard *pMyGC;
@property(nonatomic, retain) NSString *lookupletter;
@property(nonatomic, retain) NSString *mygcname;
@property(nonatomic, retain) NSTimer *timer;
@end
