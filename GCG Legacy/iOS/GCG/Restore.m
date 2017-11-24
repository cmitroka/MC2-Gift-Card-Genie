//
//  Restore.m
//  GCG
//
//  Created by Chris on 11/11/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "Restore.h"
#import "DataAccess.h"
#import "WebAccess.h"
#import "WebAccessResults.h"
#import "SFHFKeychainUtils.h"
#import "CJMUtilities.h"
#import "GCGSpecific.h"
#import "StaticData.h"
@interface Restore()
-(void)doRestoreMyCards:(NSString *)retrievalCode;
@end

@implementation Restore
NSString *YNQuestion;
StaticData *sd;
-(IBAction)RestoreMyCards:(id)sender
{
    if (txtRetrievalCode.text.length>0)
    {
        [self doRestoreMyCards:txtRetrievalCode.text];
        [txtRetrievalCode resignFirstResponder];
    }
    else
    {
        [CJMUtilities ShowAlert:@"Oops" Message:@"You need to a provide a restore code." ButtonText:@"OK"];
    }
}
-(IBAction)UseLastCode:(id)sender
{

    NSString *LastRetrievalCodeStr=[SFHFKeychainUtils pmGetValueForSetting:@"LastRetrievalCode"];
    if ([LastRetrievalCodeStr isEqualToString:@"UNDEFINED"]){LastRetrievalCodeStr=@"";}
    if (LastRetrievalCodeStr.length==0)
    {
        LastRetrievalCodeStr=@"";
    }
    txtRetrievalCode.text=LastRetrievalCodeStr;
    if (txtRetrievalCode.text.length>0)
    {
    }
    else
    {
        [CJMUtilities ShowAlert:@"None Available" Message:@"Sorry, this phone doesn't have a saved code.  You can still manually enter one." ButtonText:@"OK"];
    }
}

-(void)doRestoreMyCards:(NSString *)retrievalCode;
{    
    int record=0;
    DataAccess *da=[DataAccess da];
    NSString *pcardtype,*pcardnum,*pcardpin,*pcredname,*pcredpass,*pid,*lastbal,*lastbaldate;
    WebAccess *wa=[[WebAccess alloc]init];    
    NSString *restoredData=@"";
    restoredData=[wa pmRetrieveData:retrievalCode];
    if (restoredData.length==3)
    {
        [CJMUtilities ShowAlert:@"Sorry" Message:@"The code you entered doesn't have any backed up data associated with it." ButtonText:@"OK"];
        return;
    }
    
    NSLog(restoredData);
    //ss=wa.pmDownloadAllData;
    NSMutableArray *pieceArray = [[NSMutableArray alloc] initWithObjects:nil];
    pieceArray=[CJMUtilities ConvertNSStringToNSMutableArray:restoredData delimiter:gcgLINEDEL];
    for (NSString *tempx in pieceArray) 
    {
        if (record==0)
        {
            NSLog(tempx);
            if (tempx==@"") {
                break;
            }
            [da pmDeleteMyCards];
            NSError *error = nil;
            //[SFHFKeychainUtils storeUsername:@"UUID" andPassword:tempx forServiceName:@"com.mc2techservices.gcg" updateExisting:YES error:&error];
        }
        else
        {
            NSMutableArray *temp=[CJMUtilities ConvertNSStringToNSMutableArray:tempx delimiter:gcgPIECEDEL];
            if (temp.count<6)break;
            pcardtype=[temp objectAtIndex:0];
            pcardnum=[temp objectAtIndex:1];
            pcardpin=[temp objectAtIndex:2];
            pcredname=[temp objectAtIndex:3];
            pcredpass=[temp objectAtIndex:4];
            pid=[temp objectAtIndex:5];
            lastbal=[temp objectAtIndex:6];
            lastbaldate=[temp objectAtIndex:7];
            [da pmUpdateMyCard:pcardtype gcnum:pcardnum gcpin:pcardpin credlogin:pcredname credpass:pcredpass mygcname:pid myoldgcname:pid];
            [da pmUpdateMyCardBalanceInfo:pid lastbalknown:lastbal lastbaldate:lastbaldate];
        }
        record++;
    }
    record=1;
    if (record>0)
    {
        YNQuestion=@"DeleteOldData";
        UIAlertView *av=[[UIAlertView alloc] initWithTitle:@"Restore Complete!" message:[NSString stringWithFormat:@"The restore has completed.  Delete the data from the the GCG backup server?"] delegate:self cancelButtonTitle:@"No" otherButtonTitles:@"Yes", nil];
        [av show];
    }
    else
    {
        [CJMUtilities ShowAlert:@"Restore Failed!" Message:@"The server didn't have any backup data for the retrieval code you provided." ButtonText:@"OK"];
    }
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
    self.title = @"Restore";   
    [super viewDidLoad];
    sd=[StaticData sd];
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
- (BOOL)textFieldShouldReturn:(UITextField *)textField {
    [textField resignFirstResponder];
    //    [self doScreenConfig];
    return NO;
}
- (void)alertView:(UIAlertView *)alertView didDismissWithButtonIndex:(NSInteger)buttonIndex {
    
    if ([YNQuestion isEqualToString:@"DeleteOldData"]) {
        if(buttonIndex==0)
        {
        }
        else
        {
            WebAccess *wa=[[WebAccess alloc]init];
            [wa pmDeleteOldBackup:txtRetrievalCode.text];
            txtRetrievalCode.text=@"";
        }
    }
}

@end
