//
//  Backup.m
//  GCG
//
//  Created by Chris on 11/11/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "Backup.h"
#import "DataAccess.h"
#import "WebAccess.h"
#import "WebAccessResults.h"
#import "CJMUtilities.h"
#import "GCGSpecific.h"
#import "SFHFKeychainUtils.h"
@interface Backup()
-(void)doBackupMyCards;
@end

@implementation Backup
-(IBAction)BackupMyCards:(id)sender
{
    [self doBackupMyCards];
}
- (void)doBackupMyCards
{
    [spinner setHidden:NO];
    [spinner startAnimating];    
	NSMutableString *temp3 = [[NSMutableString alloc] init];
    DataAccess *da=[DataAccess da];
    //[da pmGetMyCards]
    NSMutableArray *temp = [[NSMutableArray alloc] initWithObjects:nil];
    temp=[da pmGetMyCards];
    
    for (MyCard *tempx in temp)
    {
        NSString *temp1=@"";
        //temp1=[NSString stringWithFormat:@"%@%@%@%@%@%@%@%@%@%@%@%@", tempx.p_gctype,gcgPIECEDEL, tempx.p_gcnum, gcgPIECEDEL,tempx.p_gcpin,gcgPIECEDEL,tempx.p_credlogin,gcgPIECEDEL,tempx.p_credpass,gcgPIECEDEL,tempx.p_mygcname,gcgLINEDEL];

        temp1=[NSString stringWithFormat:@"%@%@%@%@%@%@%@%@%@%@%@%@%@%@%@%@", tempx.p_gctype,gcgPIECEDEL, tempx.p_gcnum, gcgPIECEDEL,tempx.p_gcpin,gcgPIECEDEL,tempx.p_credlogin,gcgPIECEDEL,tempx.p_credpass,gcgPIECEDEL,tempx.p_mygcname,gcgPIECEDEL,tempx.p_lastknownbal,gcgPIECEDEL,tempx.p_lastbaldate,gcgLINEDEL];
        [temp3 appendFormat:temp1];
    }
    WebAccess *wa=[[WebAccess alloc]init];
    /*
    [wa pmBackupData:temp3];
    NSLog(temp3);
    WebAccessResults *war=[WebAccessResults war];
    do {
        [[NSRunLoop currentRunLoop] runUntilDate:[NSDate date]];
        if (war.pStatus.length>0)break;
    } while (1==1);
    NSLog(war.pResult);
    [spinner stopAnimating];
    [spinner setHidden:YES];
    */
    //[da pmUpdateSetting:@"LastRetrievalCode" svalue:war.pResult];
    NSString *RetrievalCode =[wa pmBackupData:temp3];
    NSString *AlertMsg=@"";
    NSString *AlertTitle=@"";    
    if (RetrievalCode.length==0)
    {
        AlertMsg=@"There was a problem doing the backup, please try again.";
        AlertTitle=@"Offline?";
    }
    else
    {
        [SFHFKeychainUtils pmUpdateSettingName:@"LastRetrievalCode" SettingValue:RetrievalCode];
        AlertMsg=[NSString stringWithFormat:@"You're retrieval code is\n\n%@\n\nIf you lose it, you wont be able to retrieve your backed up data.", RetrievalCode];
        AlertTitle=@"Dont lose this!";
    }
    [CJMUtilities ShowAlert:AlertTitle Message:AlertMsg ButtonText:@"OK"];
    [spinner stopAnimating];
    [spinner setHidden:YES];
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
