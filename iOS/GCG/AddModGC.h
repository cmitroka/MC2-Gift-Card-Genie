//
//  AddModGC.h
//  GCG
//
//  Created by Chris on 7/12/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//
#import "GiftCard.h"
#import "DataAccess.h"
#import <UIKit/UIKit.h>
@interface AddModGC : UIViewController
{
    DataAccess *da;
    MyCard *currCard;
    MerchantInfo *merchantInfo;
    int dispmode;
    IBOutlet UIView *pModDataView;
    IBOutlet UITextField *pModDataText;
    IBOutlet UILabel *pModDataLabel;
}
-(int)pmLoadByMyCard:(MyCard *)mygc;
-(int)pmLoadByGCName:(NSString *)mygcname;
- (id)initWithMerchantName:(NSString *)merchantNameIn myGCName:(NSString *)myGCName;
@property(nonatomic, retain) NSString *merchantIn;
@property(nonatomic, retain) NSString *myGCNameIn;
@property(nonatomic, retain) NSString *currentlySelected;
@property(nonatomic, weak) UIBarButtonItem *back;

@property(nonatomic, retain) IBOutlet UIButton *cmdSave;
@property(nonatomic, retain) IBOutlet UIButton *cmdDelete;

@property(nonatomic, retain) IBOutlet UITextField *txtGCType;
@property(nonatomic, retain) IBOutlet UITextField *txtGCNum;
@property(nonatomic, retain) IBOutlet UITextField *txtGCPIN;
@property(nonatomic, retain) IBOutlet UITextField *txtMyOldGCName;

@property(nonatomic, retain) IBOutlet UILabel *lblGCType;
@property(nonatomic, retain) IBOutlet UILabel *lblGCNum;
@property(nonatomic, retain) IBOutlet UILabel *lblGCPIN;
@property(nonatomic, retain) IBOutlet UILabel *lblGCInfo;

@property(nonatomic, retain) IBOutlet UILabel *lblSave;
@property(nonatomic, retain) IBOutlet UILabel *lblDelete;

@end
