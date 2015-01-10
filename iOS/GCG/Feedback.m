//
//  Feedback.m
//  GCG
//
//  Created by Chris on 6/12/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "Feedback.h"
#import "TVCAppDelegate.h"
#import "WebAccess.h"
#import "CJMUtilities.h"
#import <QuartzCore/QuartzCore.h>

@implementation Feedback
-(IBAction)hideFeedback:(id)sender
{
    //NSLog(@"hideFeedback");        
    TVCAppDelegate *appDelegate = (TVCAppDelegate *)[[UIApplication sharedApplication] delegate];
    [appDelegate loadNavController];
}
-(IBAction)logFeedback:(id)sender
{
    WebAccess *wa=[[WebAccess alloc]init];
    [wa pmRecordFeedback:tfName.text ContactInfo:tfContactInfo.text Feedback:tvFeedback.text];
    [CJMUtilities ShowAlert:@"Feedback Sent" Message:@"We'll take a look at your comments and see what we can do to make the app better." ButtonText:@"OK"];
    TVCAppDelegate *appDelegate = (TVCAppDelegate *)[[UIApplication sharedApplication] delegate];
    [appDelegate loadNavController];
}
-(IBAction)hideKeyboard:(id)sender
{
    [tvFeedback resignFirstResponder];
    [tfName resignFirstResponder];
    [tfContactInfo resignFirstResponder];
    [hideFeedbackButton setHidden:YES];
    tvFeedback.frame=CGRectMake(20,100,280,260);
}
- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *) nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization
    }
    return self;
}
- (BOOL)textViewShouldBeginEditing:(UITextView *)textView
{
    hideFeedbackButton.hidden=NO;
    tvFeedback.frame=CGRectMake(20,100,280,60);
    return YES;
}
- (BOOL)textFieldShouldBeginEditing:(UITextField *)textField
{
    hideFeedbackButton.hidden=NO;
    tvFeedback.frame=CGRectMake(20,100,280,60);
    return YES;    
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
    [super viewDidLoad];
    // Do any additional setup after loading the view from its nib.
    tvFeedback.layer.borderWidth = 1;
    tvFeedback.layer.borderColor = [[UIColor blackColor] CGColor];
    tvFeedback.layer.cornerRadius = 8;
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
