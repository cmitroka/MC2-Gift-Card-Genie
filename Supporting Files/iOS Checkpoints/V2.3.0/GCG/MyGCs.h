//
//  MyGCs.h
//  GCG
//
//  Created by Chris on 9/18/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>
@class Settings;
@interface MyGCs : UIViewController
{
    IBOutlet UITableView *tableView;
    IBOutlet UIToolbar *toolbar;
    IBOutlet Settings *pSettings;
    IBOutlet MyGCs *pMyGCs;
    IBOutlet UIView *NoCardsView;
}
@property(nonatomic, retain) IBOutlet UIBarButtonItem *cmdMid;
@property(nonatomic, retain) IBOutlet UIBarButtonItem *cmdRight;
@property(nonatomic, retain) IBOutlet UIBarButtonItem *cmdLeft;
@property (nonatomic, retain) Settings *pSettings;
@property (nonatomic, retain) MyGCs *pMyGCs;
@property(nonatomic, retain) IBOutlet UIToolbar *toolbar;
@property(nonatomic, retain) IBOutlet UITableView *tableView;
@end
