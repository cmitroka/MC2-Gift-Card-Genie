//
//  SwitchViewController.h
//  NAVCON01
//
//  Created by Chris on 12/9/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>
@class Settings;
@class MyGCs;
@interface SwitchViewController : UIViewController {
    Settings *pSettings;
    MyGCs *pMyGCs;
}
@property (retain, nonatomic) Settings *pSettings;
@property (retain, nonatomic) MyGCs *pMyGCs;
@end