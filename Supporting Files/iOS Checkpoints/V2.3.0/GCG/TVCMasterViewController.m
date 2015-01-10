//
//  TVCMasterViewController.m
//  NAVCON00
//
//  Created by Chris on 2/1/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "TVCMasterViewController.h"
#import "TVCAppDelegate.h"
#import "MyGCs.h"
#import "About.h"
#import "Feedback.h"
#import "Settings.h"
#import "ViewGCs.h"
#import "AddModGC.h"
#import "GCGSpecific.h"
#import "StaticData.h"
#import "SFHFKeychainUtils.h"
#import "CJMUtilities.h"
#import "DataAccess.h"
#import "TestCenter.h"
@interface TVCMasterViewController()
- (void)PossiblyRemoveiAd;
@end


@implementation TVCMasterViewController

-(IBAction)doTest:(id)sender
{
    static int cntr;
    cntr++;
    if (cntr>2)
    {
        TVCAppDelegate *appDelegate = (TVCAppDelegate *)[[UIApplication sharedApplication] delegate];
        [appDelegate useViewController:[TestCenter class]];
        cntr=0;
    }
    //DataAccess *da=[DataAccess da];
    //[da pmUpdateMyCardBalanceInfo:@"ID5" lastbalknown:@"111" lastbaldate:@"1/1/2012"];

}

-(IBAction)showFeedback:(id)sender
{
    Feedback *feedback = [[Feedback alloc]init];
    [self.navigationController pushViewController:feedback animated:YES];
}
- (void)alertView:(UIAlertView *)alertView didDismissWithButtonIndex:(NSInteger)buttonIndex {
    if(buttonIndex==0) {
        MyGCs *pMyGCs = [[MyGCs alloc]init];
        [self.navigationController pushViewController:pMyGCs animated:YES];
        //AddModGC *pAddModGC=[[AddModGC alloc] init];
        //[self.navigationController pushViewController:pAddModGC animated:YES];
    }
    else
    {
        ViewGCs *pViewGCs=[[ViewGCs alloc] init];
        [self.navigationController pushViewController:pViewGCs animated:YES];
    }
}

-(IBAction)showMyCards:(id)sender
{
    /*
     UIAlertView *av=[[UIAlertView alloc] initWithTitle:@"Message" message:[NSString stringWithFormat:@"%@%@",@"A",@"D"] delegate:self cancelButtonTitle:@"Add Card" otherButtonTitles:@"View Cards", nil];
     [av show];
     */
    MyGCs *myGCs = [[MyGCs alloc]init];
    [self.navigationController pushViewController:myGCs animated:YES];
    
}

-(IBAction)showViewGCs:(id)sender
{
    ViewGCs *pViewGCs=[[ViewGCs alloc] init];
    [self.navigationController pushViewController:pViewGCs animated:YES];    
}
-(IBAction)showSettings:(id)sender
{
    Settings *s = [[Settings alloc] init];
    [self.navigationController pushViewController:s animated:YES];    
}

-(IBAction)showAbout:(id)sender
{
    About *about=[[About alloc]init];
    [self.navigationController pushViewController:about animated:YES];    

}

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        self.title = NSLocalizedString(@"Main", @"Main");        
        //Hide Title
        //UIView *fakeTitleView = [[UIView alloc] init];
        //fakeTitleView.hidden = YES;
        //[self.navigationItem setTitleView:fakeTitleView];
    }
    return self;
}
- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Release any cached data, images, etc that aren't in use.
}

- (void)viewDidLoad
{
    [super viewDidLoad];
	// Do any additional setup after loading the view, typically from a nib.
}
- (void)viewDidUnload
{
    [super viewDidUnload];
    // Release any retained subviews of the main view.
    // e.g. self.myOutlet = nil;
}

- (void)viewWillAppear:(BOOL)animated
{
    [super viewWillAppear:animated];
    self.navigationController.navigationBarHidden=NO;
    StaticData *sd=[StaticData sd];
    if (sd.pMode==@"Offline") {
        if (sd.pModeAcknowledged.length==0)
        {
            [CJMUtilities ShowAlert:@"Communications Issue" Message:@"GCG is having trouble communicating with the server. You'll still be able to use the app, but functionality may be limited." ButtonText:@"OK"];
            sd.pModeAcknowledged=@"Y";
            
        }
    }
    [self PossiblyRemoveiAd];
}

- (void)viewDidAppear:(BOOL)animated
{
    [super viewDidAppear:animated];
}

- (void)viewWillDisappear:(BOOL)animated
{
	[super viewWillDisappear:animated];
}

- (void)viewDidDisappear:(BOOL)animated
{
	[super viewDidDisappear:animated];
}

- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)interfaceOrientation
{
    // Return YES for supported orientations
    return (interfaceOrientation == UIInterfaceOrientationPortrait);
}
- (void)bannerView:(ADBannerView *)banner didFailToReceiveAdWithError:(NSError *)error
{
        NSLog(@"bannerview did not receive any banner due to %@", error);
}

- (void)bannerViewActionDidFinish:(ADBannerView *)banner
{
    NSLog(@"bannerview was selected");
}

- (BOOL)bannerViewActionShouldBegin:(ADBannerView *)banner willLeaveApplication:(BOOL)willLeave
{
    NSLog(@"Banner was clicked on; will%sleave application", willLeave ? " " : " not ");
        return willLeave;
}

- (void)bannerViewDidLoadAd:(ADBannerView *)banner 
{
    NSLog(@"banner was loaded");
}
- (void)PossiblyRemoveiAd
{
    NSString *appStatus=nil;
    appStatus=[SFHFKeychainUtils pmGetValueForSetting:@"AppStatus"];
    if ([appStatus isEqualToString:@"Purchased"])
    {
        [_ADBannerView setHidden:YES];
        [_ADBannerView removeFromSuperview];
    }
}
@end
