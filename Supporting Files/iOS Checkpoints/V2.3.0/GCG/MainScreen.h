//
//  MainScreen.h
//  GCG
//
//  Created by Chris on 12/6/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface MainScreen : UIViewController
{
    IBOutlet UITableView *tableView;
    IBOutlet UIToolbar *toolbar;
}
@property(nonatomic, retain) IBOutlet UIToolbar *toolbar;
@end
