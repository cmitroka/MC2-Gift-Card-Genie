//
//  IAP.h
//  GCG
//
//  Created by Chris on 10/16/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//
#import <Storekit/StoreKit.h>
#import <UIKit/UIKit.h>

@interface IAP : UIViewController<SKProductsRequestDelegate,SKPaymentTransactionObserver,UIAlertViewDelegate>
{
    IBOutlet UITextField *tfStatus;
    IBOutlet UIButton *btnPurchase;
    IBOutlet UIButton *btnPurchaseQuantity;
    IBOutlet UIButton *btnWatchAd;
    IBOutlet UIButton *btnWatchIntAd;
    IBOutlet UIButton *btnReReg;
    IBOutlet UILabel *lblPurchase;
    IBOutlet UILabel *lblPurchaseQuantity;
    IBOutlet UILabel *lblLookupInfo;
    IBOutlet UIView *subscreen;
    IBOutlet UIButton *btnDemoMode;
}

@property BOOL verified;
@property (nonatomic, retain) NSString *pPurchaseType;
@property (strong, nonatomic) SKProductsRequest *request; //Store the request as a property
-(void)processsPurchaseRequest;
@end
