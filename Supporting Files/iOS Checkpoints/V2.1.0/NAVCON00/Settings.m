//
//  Settings.m
//  NAVCON01
//
//  Created by Chris on 8/12/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "Settings.h"
#import "ResourceHandler.h"
#import "CJMUtilities.h"
#import "DataUpdater.h"
#import "SFHFKeychainUtils.h"
#import "IAP.h"
#import "WebAccess.h"
#import "WebAccessResults.h"
#import "GCGSpecific.h"
#import "DataAccess.h"
#import "GiftCard.h"
#import "Backup.h"
#import "Restore.h"
#import "TVCAppDelegate.h"
#import "About.h"
#import "Feedback.h"
#import "MyGCs.h"
#import "StaticData.h"
@interface Settings()

-(void)provideContent:(NSString *)productIdentifier;
- (void) restoreTransaction: (SKPaymentTransaction *)transaction;
-(void)RestoreMyCards2:(NSString *)retrievalCode;
- (void)BackupMyCards2;
@end

StaticData *sd;


@implementation Settings
int alertGlobal;
NSMutableArray *purchasedItemIDs;
static NSString *pSessionID;
static NSString *pChecksum;
static NSString *pAlert;

#pragma mark - View lifecycle

- (void)viewDidLoad
{
    //self.title = NSLocalizedString(@"Settings", @"Settings");        
    [super viewDidLoad];
    sd=[StaticData sd];
    NSString *appStatus=nil;
    appStatus=[SFHFKeychainUtils pmGetValueForSetting:@"AppStatus"];
    NSString *boolAlwaysUpdate=[SFHFKeychainUtils pmGetValueForSetting:@"AlwaysUpdate"];

    if ([appStatus isEqualToString:@"Purchased"])
    {
        locks1.hidden=YES;
        locks2.hidden=YES;
        if (boolAlwaysUpdate.boolValue==TRUE)
        {
            [AlwaysUpdate setOn:TRUE];
        }
        else
        {
            [AlwaysUpdate setOn:FALSE];            
        }
        lInfo.hidden=YES;
    }
    else
    {
        locks1.hidden=NO;
        locks2.hidden=NO;
        lockedFeature1.enabled=NO;
        lockedFeature1.alpha=.5;
        lockedFeature2.enabled=NO;
        lockedFeature2.alpha=.5;
        AlwaysUpdate.enabled=NO;
        lAlwaysUpdate.alpha=.5;
    }

    //[self.navigationController.navigationBar=myNavigationBar];
}
- (void)viewWillAppear:(BOOL)animated
{
    self.navigationController.navigationBarHidden=YES;
    [super viewWillAppear:animated];
}
- (void)viewWillDisappear:(BOOL)animated
{
    self.navigationController.navigationBarHidden=NO;
	[super viewWillDisappear:animated];
}

- (void)viewDidDisappear:(BOOL)animated
{
	[super viewDidDisappear:animated];
}
-(IBAction)showMyGCs:(id)sender
{
    TVCAppDelegate *appDelegate = (TVCAppDelegate *)[[UIApplication sharedApplication] delegate];
    [appDelegate useNavController:[MyGCs class]];
}
-(IBAction)showAbout:(id)sender
{
    About *about=[[About alloc]init];
    [self.navigationController pushViewController:about animated:YES];        
}
-(IBAction)toggleAlwaysUpdate:(id)sender
{
    if (AlwaysUpdate.isOn)
    {
        [SFHFKeychainUtils pmUpdateSettingName:@"AlwaysUpdate" SettingValue:@"True"];
    }
    else
    {
        [SFHFKeychainUtils pmUpdateSettingName:@"AlwaysUpdate" SettingValue:@""];        
    }
}
-(IBAction)showFeedback:(id)sender
{
    
    //Feedback *feedback = [[Feedback alloc]init];
    //[self.navigationController pushViewController:feedback animated:YES];
    // Email Subject

    NSString *title=[CJMUtilities GetFirstChars:sd.pUUID theamount:8];    
    NSString *emailTitle = [NSString stringWithFormat:@"GCG Feedback from %@",title];
    // Email Content
    NSString *messageBody = @"";
    // To address
    NSArray *toRecipents = [NSArray arrayWithObject:@"service@mc2techservices.com"];
    
    MFMailComposeViewController *mc = [[MFMailComposeViewController alloc] init];
    mc.mailComposeDelegate = self;
    [mc setSubject:emailTitle];
    [mc setMessageBody:messageBody isHTML:NO];
    [mc setToRecipients:toRecipents];
    
    // Present mail view controller on screen
    [self presentViewController:mc animated:YES completion:NULL];
    
}

- (void) mailComposeController:(MFMailComposeViewController *)controller didFinishWithResult:(MFMailComposeResult)result error:(NSError *)error
{
    switch (result)
    {
        case MFMailComposeResultCancelled:
            NSLog(@"Mail cancelled");
            break;
        case MFMailComposeResultSaved:
            NSLog(@"Mail saved");
            break;
        case MFMailComposeResultSent:
            NSLog(@"Mail sent");
            break;
        case MFMailComposeResultFailed:
            NSLog(@"Mail sent failure: %@", [error localizedDescription]);
            break;
        default:
            break;
    }
    
    // Close the Mail Interface
    [self dismissViewControllerAnimated:YES completion:NULL];
}

-(IBAction)PurchaseApp:(id)sender
{
    WebAccess *wa=[[WebAccess alloc]init];
    [wa pmDoLogConsideredBuying:@"Viewing"];
    IAP *iap = [[IAP alloc]init];
    [self.navigationController pushViewController:iap animated:YES];                
}
-(IBAction)BackupMyCards:(id)sender
{
    Backup *b = [[Backup alloc]init];
    [self.navigationController pushViewController:b animated:YES];
}
-(IBAction)RestoreMyCards:(id)sender
{
    Restore *r = [[Restore alloc]init];
    [self.navigationController pushViewController:r animated:YES];
}
-(IBAction)ResetDB:(id)sender
{
    alertGlobal=1;
    UIAlertView *av=[[UIAlertView alloc] initWithTitle:@"Confirm" message:[NSString stringWithFormat:@"%@%@",@"Are you sure you want to totally reset ",@"the database?"] delegate:self cancelButtonTitle:@"No, My Mistake" otherButtonTitles:@"Yeah, I'm Sure", nil];
    [av show];
}
- (void)alertView:(UIAlertView *)alertView didDismissWithButtonIndex:(NSInteger)buttonIndex {
    if (alertGlobal==1) 
    {
        if(buttonIndex==0)
        {
        }
        else
        {
            ResourceHandler *rh =[[ResourceHandler alloc]init];
            BOOL result=[rh pmCreateDatabase];
            if (result==YES) {
                [SFHFKeychainUtils pmUpdateSettingName:@"Conv" SettingValue:@"0"];
                [CJMUtilities ShowAlert:@"Done!" Message:@"Reset Complete!" ButtonText:@"Thanks"];
            }
            else
            {
                [CJMUtilities ShowAlert:@"Oops!" Message:@"There was a problem with this, it's best to delete and reinstall this app." ButtonText:@"OK I Guess"];
            }
        }
    }
    if (alertGlobal==2)
    {
        if(buttonIndex==0)
        {
        }
        else
        {
        }
    }
}

-(IBAction)UpdateDB:(id)sender
{
    DataUpdater *du = [[DataUpdater alloc]init];
    [self.navigationController pushViewController:du animated:YES];            
}
-(IBAction)EraseLastUpdate:(id)sender
{
    DataAccess *da = [DataAccess da];
    int result=[da pmUpdateSetting:@"LastUpdate" svalue:@"1900-01-01"];
    if (result==1) {
        [CJMUtilities ShowAlert:@"Done!" Message:@"Vendors will be updated once the app is restarted." ButtonText:@"Thanks"];
    }
    else
    {
        [CJMUtilities ShowAlert:@"Oops!" Message:@"There was a problem with this, it's best to delete and reinstall this app." ButtonText:@"OK I Guess"];
    }
}
- (void)alertView:(UIAlertView *)alertView clickedButtonAtIndex:(NSInteger)buttonIndex{ 
    if (pAlert==@"1")
    {
        pAlert=@"0";
        NSLog(@"Entered: %@",[[alertView textFieldAtIndex:0] text]);
    }
    else if(pAlert==@"Backup")
    {
        if (buttonIndex==0) {
        }
        else
        {
            [self BackupMyCards2];
        }
    }
    else if(pAlert==@"Restore")
    {
        if (buttonIndex==0) {
        }
        else
        {
            [self RestoreMyCards2:[[alertView textFieldAtIndex:0] text]];
        }
    }
}
@end