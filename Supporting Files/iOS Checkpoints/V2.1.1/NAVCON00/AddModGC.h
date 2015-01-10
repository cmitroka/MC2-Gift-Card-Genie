//
//  AddModGC.h
//  NAVCON01
//
//  Created by Chris on 7/12/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//
#import "GiftCard.h"
#import "DataAccess.h"
#import <UIKit/UIKit.h>
#import "iAd/ADBannerView.h"
@interface AddModGC : UIViewController<ADBannerViewDelegate>
{
    IBOutlet ADBannerView *_ADBannerView;
    DataAccess *da;
    MyCard *currCard;
    MerchantInfo *merchantInfo;
    int dispmode;
    IBOutlet UIView *pModDataView;
    IBOutlet UITextField *pModDataText;
    IBOutlet UILabel *pModDataLabel;
    
    
    IBOutlet UIActivityIndicatorView *spinner;
}
-(int)pmLoadByMyCard:(MyCard *)mygc;
-(int)pmLoadByGCName:(NSString *)mygcname;
-(NSString*)IsSelectionVaild;
- (id)initWithMerchantName:(NSString *)merchantNameIn myGCName:(NSString *)myGCName;
@property(nonatomic, retain) NSTimer *timer;
@property(nonatomic, retain) NSString *merchantIn;
@property(nonatomic, retain) NSString *myGCNameIn;
@property(nonatomic, retain) NSString *currentlySelected;


@property(nonatomic, retain) IBOutlet UIButton *cmdDone;
@property(nonatomic, retain) IBOutlet UIButton *cmdSendUnsupported;
@property(nonatomic, retain) IBOutlet UIButton *cmdSave;
@property(nonatomic, retain) IBOutlet UIButton *cmdDelete;
@property(nonatomic, retain) IBOutlet UIButton *cmdAutoLookup;


@property(nonatomic, retain) IBOutlet UINavigationBar *nbNavBar;

@property(nonatomic, retain) IBOutlet UITextField *txtGCType;
@property(nonatomic, retain) IBOutlet UITextField *txtGCNum;
@property(nonatomic, retain) IBOutlet UITextField *txtGCPIN;
@property(nonatomic, retain) IBOutlet UITextField *txtCredLogin;
@property(nonatomic, retain) IBOutlet UITextField *txtCredPass;

@property(nonatomic, retain) IBOutlet UIBarButtonItem *cmdBack;

@property(nonatomic, retain) IBOutlet UITextField *txtMyOldGCName;

@property(nonatomic, retain) IBOutlet UILabel *lblGCType;
@property(nonatomic, retain) IBOutlet UILabel *lblGCNum;
@property(nonatomic, retain) IBOutlet UILabel *lblGCPIN;
@property(nonatomic, retain) IBOutlet UILabel *lblCredLogin;
@property(nonatomic, retain) IBOutlet UILabel *lblCredPass;
@property(nonatomic, retain) IBOutlet UILabel *lblCredInfo;
@property(nonatomic, retain) IBOutlet UILabel *lblGCInfo;
@property(nonatomic, retain) IBOutlet UILabel *lblAutoLookup;

@property(nonatomic, retain) IBOutlet UILabel *lblSave;
@property(nonatomic, retain) IBOutlet UILabel *lblDelete;

@end
