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
#import "IAP.h"
@interface TVCMasterViewController()
@end


@implementation TVCMasterViewController
StaticData *sd;
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
-(IBAction)showGetMore:(id)sender
{
    IAP *iap=[[IAP alloc]init];
    [self.navigationController pushViewController:iap animated:YES];
    
}


- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        //Hide Title
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
    self.navigationController.navigationBar.hidden = YES;
    StaticData *sd=[StaticData sd];
    if (sd.pMode==@"Offline") {
        if (sd.pModeAcknowledged.length==0)
        {
            [CJMUtilities ShowAlert:@"Communications Issue" Message:@"GCG is having trouble communicating with the server. You'll still be able to use the app, but functionality may be limited." ButtonText:@"OK"];
            sd.pModeAcknowledged=@"Y";
            
        }
//        [[UIApplication sharedApplication] setStatusBarHidden:YES withAnimation:UIStatusBarAnimationNone];
//        self.navigationController.navigationBar.frame = CGRectMake(0, 0, 320, 44);
    }
}

- (void)viewDidAppear:(BOOL)animated
{
    sd=[StaticData sd];
    int iRemAmnt=sd.pAmntOfLookupsRemaining.integerValue;
    NSString *sRemAmnt=[CJMUtilities ConvertIntToNSString:iRemAmnt];
    if (iRemAmnt<0)
    {
        sRemAmnt=@"0";
    }
    if (sd.pMode==@"Offline")
    {
        sRemAmnt=@"? (your offline)";
    }
    
    
    NSString *temp1=[NSString stringWithFormat:@"%@ %@ lookups remaining.", @"You have", sRemAmnt];
    [lblLookupInfo setText:temp1];
    
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
@end
