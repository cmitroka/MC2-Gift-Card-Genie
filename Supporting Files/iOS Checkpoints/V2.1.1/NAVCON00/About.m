//
//  About.m
//  NAVCON01
//
//  Created by Chris Mitroka on 5/8/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "About.h"

@implementation About
-(IBAction)GoToWebpage:(id)sender
{
    [[UIApplication sharedApplication] 
       openURL:[NSURL URLWithString:@"http://gcg.mc2techservices.com"]];
}
-(IBAction)GoToEmail:(id)sender
{
    [[UIApplication sharedApplication] openURL:[NSURL URLWithString:@"mailto://service@mc2techservices.com"]];
}

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        self.title = NSLocalizedString(@"About Gift Card Genie", @"About Gift Card Genie");        
    }
    return self;
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
