//
//  TestCenter.m
//  NAVCON01
//
//  Created by Chris on 12/2/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "TestCenter.h"
#import "CJMUtilities.h"
#import "SFHFKeychainUtils.h"
#import "TVCAppDelegate.h"
#import "GiftCard.h"
#import "DataAccess.h"
#import "Feedback.h"
#import "AddModGC.h"
#import "About.h"
@implementation TestCenter

-(IBAction)doTest1:(id)sender
{
    NSError *error = nil;
    NSString *msg;
    NSString *Purchased = [SFHFKeychainUtils pmGetValueForSetting:@"AppStatus"];;
    if ([Purchased isEqualToString:@"Purchased"])
    {
        [SFHFKeychainUtils pmDeleteSetting:@"AppStatus"];
        msg=@"Unpurchased";
    }
    else
    {
        [SFHFKeychainUtils pmUpdateSettingName:@"AppStatus" SettingValue:@"Purchased"];
        msg=@"Purchased";            
    }
    [CJMUtilities ShowAlert:@"" Message:msg ButtonText:@"OK"];

}
-(IBAction)doTest2:(id)sender
{
    TVCAppDelegate *appDelegate = (TVCAppDelegate *)[[UIApplication sharedApplication] delegate];
    [appDelegate useViewController:[Feedback class]];
}
-(IBAction)doTest3:(id)sender
{
    DataAccess *da=[DataAccess da];
    MerchantInfo *mi = [[MerchantInfo alloc] init];
    mi = [da pmGetMerchantInfoByName:@"5 Guys"];
    NSLog(mi.p_name);
    NSLog([CJMUtilities ConvertIntToNSString:mi.p_maxCardLen]);
    NSLog([CJMUtilities ConvertIntToNSString:mi.p_minCardLen]);
    NSLog([CJMUtilities ConvertIntToNSString:mi.p_maxPINLen]);
    NSLog([CJMUtilities ConvertIntToNSString:mi.p_minPINLen]);
    NSLog(mi.p_note);
}
-(IBAction)doTest4:(id)sender
{
    NSString *merchName=@"5 Guys";
    TVCAppDelegate *appDelegate = (TVCAppDelegate *)[[UIApplication sharedApplication] delegate];
    [appDelegate useViewController:[AddModGC class]];
}
-(IBAction)doTest5:(id)sender
{
    About *about=[[About alloc]init];
    [self.navigationController pushViewController:about animated:YES];    
}
-(IBAction)doTest6:(id)sender
{
    
}
-(IBAction)doTest7:(id)sender
{
    
}

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization
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
