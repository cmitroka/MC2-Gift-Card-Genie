//
//  OutOfLookups.m
//  GCG
//
//  Created by Chris Mitroka on 6/11/16.
//
//
#import "TVCAppDelegate.h"
#import "WatchAd.h"
#import "IAP.h"
#import "OutOfLookups.h"

@interface OutOfLookups ()

@end

@implementation OutOfLookups

- (void)viewDidLoad
{
    [super viewDidLoad];
    [btnPurchase.titleLabel setTextAlignment: NSTextAlignmentCenter];
    [btnWatchAd.titleLabel setTextAlignment: NSTextAlignmentCenter];
    [btnExit.titleLabel setTextAlignment: NSTextAlignmentCenter];
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

-(IBAction)Exit:(id)sender
{
    exit(0);
}
-(IBAction)GoToIAP:(id)sender
{
    TVCAppDelegate *appDelegate = (TVCAppDelegate *)[[UIApplication sharedApplication] delegate];
    [appDelegate useNavController:[IAP class]];
    return;
}
-(IBAction)GoToWatchAd:(id)sender
{
    TVCAppDelegate *appDelegate = (TVCAppDelegate *)[[UIApplication sharedApplication] delegate];
    [appDelegate useNavController:[WatchAd class]];
    return;
}


/*
#pragma mark - Navigation

// In a storyboard-based application, you will often want to do a little preparation before navigation
- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender {
    // Get the new view controller using [segue destinationViewController].
    // Pass the selected object to the new view controller.
}
*/

@end
