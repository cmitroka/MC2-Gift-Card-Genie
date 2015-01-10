//
//  Settings.h
//  GCG
//
//  Created by Chris on 8/12/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <Storekit/StoreKit.h>
#import <UIKit/UIKit.h>
#import <MessageUI/MessageUI.h>

@interface Settings : UIViewController<SKProductsRequestDelegate,SKPaymentTransactionObserver,UIAlertViewDelegate,MFMailComposeViewControllerDelegate>
{
    IBOutlet UIButton *btnPurchase;
    IBOutlet UIButton *btnExtend;
    IBOutlet UIButton *btnTest;
    IBOutlet UINavigationBar *myNavigationBar;
    IBOutlet UIImageView *locks1;
    IBOutlet UIImageView *locks2;
    IBOutlet UIButton *lockedFeature1;
    IBOutlet UIButton *lockedFeature2;
    IBOutlet UISwitch *AlwaysUpdate;
    IBOutlet UILabel *lAlwaysUpdate;
    IBOutlet UILabel *lInfo;    
}
@property (strong, nonatomic) SKProductsRequest *request; //Store the request as a property
@end
