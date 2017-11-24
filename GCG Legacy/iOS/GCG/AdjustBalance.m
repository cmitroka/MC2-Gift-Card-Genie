//
//  AdjustBalance.m
//  GCG
//
//  Created by Chris on 12/9/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "AdjustBalance.h"
#import "DataAccess.h"
#import "StaticData.h"
#import "CJMUtilities.h"
@implementation AdjustBalance
@synthesize tfBal,tfDate;
- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization
    }
    return self;
}
-(IBAction)SetDate:(id)sender
{
    NSString *temp=[CJMUtilities GetCurrentDate];
    tfDate.text=temp;
}

-(IBAction)Update:(id)sender
{
    StaticData *sd=[StaticData sd];
    DataAccess *da = [DataAccess da];
    NSLog(@"%@",sd.pLoadGCKey);
    int test=[da pmUpdateMyCardBalanceInfo:sd.pLoadGCKey lastbalknown:tfBal.text lastbaldate:tfDate.text];
    if (test!=1)
    {
        [CJMUtilities ShowAlert:@"Oops" Message:@"Couldn't Update, must be a problem with what your trying to do.  Maybe go back and try again." ButtonText:@"OK"];        
    }
    else
    {
        [CJMUtilities ShowAlert:@"Updated!" Message:@"You're balance and/or date have been updated." ButtonText:@"OK"];        
    }
}

- (void)didReceiveMemoryWarning
{
    // Releases the view if it doesn't have a superview.
    [super didReceiveMemoryWarning];
    
    // Release any cached data, images, etc that aren't in use.
}

#pragma mark - View lifecycle

- (void)viewDidLoad
{
    tfBal.keyboardType=UIKeyboardTypeNumbersAndPunctuation;
    tfDate.keyboardType=UIKeyboardTypeNumbersAndPunctuation;
    [super viewDidLoad];
    // Do any additional setup after loading the view from its nib.
}

- (void)viewDidUnload
{
    [super viewDidUnload];
    // Release any retained subviews of the main view.
    // e.g. self.myOutlet = nil;
}

- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)interfaceOrientation
{
    // Return YES for supported orientations
    return (interfaceOrientation == UIInterfaceOrientationPortrait);
}

@end
