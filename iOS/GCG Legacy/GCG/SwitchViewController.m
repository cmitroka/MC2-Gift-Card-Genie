//
//  SwitchViewController.m
//  GCG
//
//  Created by Chris on 12/9/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "SwitchViewController.h"
#import "MyGCs.h"
#import "Settings.h"
@implementation SwitchViewController
@synthesize pMyGCs,pSettings;
- (void)viewDidLoad{
    /*
    Settings *y=[[Settings alloc] init];
    self.pSettings=y;
    [self.view insertSubview:y.view atIndex:0];
    */
    MyGCs *z = [[MyGCs alloc] init];
    self.pMyGCs = z;
    [self.view insertSubview:z.view atIndex:0];
}
-(IBAction)switchViews:(id)sender;
{
    [UIView beginAnimations:@"View Flip" context:nil];
    [UIView setAnimationDuration:1.25];
    [UIView setAnimationCurve:UIViewAnimationCurveEaseInOut];
    //blue view will appear by flipping from right
    if(pMyGCs.view.superview == nil)
    {
        [UIView setAnimationTransition: UIViewAnimationTransitionFlipFromRight 
                               forView:self.view cache:YES];
        
        //[blueViewController viewWillAppear:YES];
        //[yellowViewController viewWillDisappear:YES];
        
        [pMyGCs.view removeFromSuperview];
        [self.view insertSubview:self.pSettings.view atIndex:0];
        
        //[yellowViewController viewDidDisappear:YES];
        //[blueViewController viewDidAppear:YES];
    }
    [UIView commitAnimations];   
}
@end
