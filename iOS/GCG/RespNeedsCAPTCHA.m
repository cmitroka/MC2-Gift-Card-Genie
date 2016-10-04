//
//  RespNeedsCAPTCHA.m
//  GCG
//
//  Created by Chris on 9/29/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "RespNeedsCAPTCHA.h"
#import "CJMUtilities.h"
#import "GCGSpecific.h"
#import "StaticData.h"
#import "WebAccess.h"
#import "DataAccess.h"
#import "ManualLookupWebviewHelper.h"
#import "ManualLookupWebview.h"
@implementation RespNeedsCAPTCHA
@synthesize timer;
static NSString *pRespID;
static NSString *pCAPTCHAURL;
-(void)TurnButtonOn
{
    btnSubmit.alpha=1;
    btnSubmit.alpha=1;    
    [btnSubmit setEnabled:YES];
}
-(void)TurnButtonOff
{
    btnSubmit.alpha=.5;
    btnSubmit.alpha=.5;
    [btnSubmit setEnabled:NO];
}
- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization
    }
    return self;
}
-(IBAction)DoContinueRequest:(id)sender
{
    
    //[CJMUtilities ShowAlert:@"Coming Soon" Message:@"This feature will be available (to some extent)in the next version." ButtonText:@"OK"];
    spinner.hidden=FALSE;
    [txtAnswer resignFirstResponder];
    txtAnswer.enabled=false;
    WebAccess *wa=[[WebAccess alloc]init];
    NSString *rs =@"";
    rs = [wa pmContinueRequest:@"UDID" IDFileName:pRespID Answer:txtAnswer.text];
    [spinner startAnimating];
    //timer=[NSTimer scheduledTimerWithTimeInterval:.1 target:self selector:@selector(kickoffWebrequest:) userInfo:rs repeats:YES];
    [btnSubmit setEnabled:NO];
    [self performSelector:@selector(kickoffWebrequest:) withObject:nil afterDelay:.1];
}
-(void)monitorResp:(NSTimer*)theTimer
{
    NSString *retRS = @"";
    NSString *MonitorThis=(NSString*)[theTimer userInfo];
    DataAccess *da=[DataAccess da];
    retRS=[da pmGetSetting:MonitorThis];
    //retRS = [NSString stringWithFormat:@"%@%@%@%@%@",@"NEEDSMOREINFO",gcgLINEDEL,@"123456789",gcgPIECEDEL,@"ZIP"];
    
    if ([retRS isEqualToString:@""])
    {
    }
    else
    {
        [timer invalidate];
        [self TurnButtonOn];
        timer=nil;
        [spinner stopAnimating];
        //[GCGSpecific pmHandleResponse:retRS PassNavView:self.navigationController];
        NSMutableArray *tempArray=[CJMUtilities ConvertNSStringToNSMutableArray:retRS delimiter:gcgLINEDEL];
        NSString *rsType=[tempArray objectAtIndex:0];
        NSString *rsValue=[tempArray objectAtIndex:1];

        if ([rsType isEqualToString:gcgGCBALANCE])
        {
            NSString *Balance=@"";
            Balance=rsValue;
            NSString *temp=[CJMUtilities GetCurrentDate];
            StaticData *sd=[StaticData sd];
            DataAccess *da = [DataAccess da];
            ManualLookupWebviewHelper *pMLWH=[ManualLookupWebviewHelper mlwh];
            [da pmUpdateMyCardBalanceInfo:pMLWH.pID lastbalknown:Balance lastbaldate:temp];
            [CJMUtilities ShowAlert:@"Your Balance Is..." Message:Balance ButtonText:@"Thanks!"];
            int AmntOfLookupsRemaining=[CJMUtilities ConvertNSStringToInt:sd.pAmntOfLookupsRemaining]-1;
            sd.pAmntOfLookupsRemaining=[CJMUtilities ConvertIntToNSString:AmntOfLookupsRemaining];
        }
        else
        {
            UIAlertView *av=[[UIAlertView alloc] initWithTitle:@"Try Alternate Lookup?" message:[NSString stringWithFormat:@"%@%@",@"We couldn't automatically get the balance; ",@"would you like to use the alternate lookup method?"] delegate:self cancelButtonTitle:@"No thanks" otherButtonTitles:@"Yeah, let's try that", nil];
            [av show];
        }
        
        UINavigationController *navController = self.navigationController;
        NSMutableArray *controllers = [self.navigationController.viewControllers mutableCopy];
        //[controllers removeLastObject];
        navController.viewControllers = controllers;
        [navController popViewControllerAnimated:YES];

    }
}
-(void)kickoffWebrequest
{
    
}
-(id)initWithParamString:(NSString *)pString
{
    StaticData *sd=[StaticData sd];
    pRespID=pString;
    pCAPTCHAURL=[NSString stringWithFormat:@"%@%@%@", sd.pCAPTCHAURLInfo,pString,@".bmp"];
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
    NSURL *imageURL;
    NSData *imageData;
    [super viewDidLoad];
    // Do any additional setup after loading the view from its nib.
    do {
        [[NSRunLoop currentRunLoop] runUntilDate:[NSDate date]];
        imageURL = [NSURL URLWithString:pCAPTCHAURL];
        imageData = [NSData dataWithContentsOfURL:imageURL];
        int x=imageData.length;
        if (x>0)break;
    } while (1==1);
    UIImage * image = [UIImage imageWithData:imageData];
    CAPTCHAImage.image = image;

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
-(void)DoManualRequest
{
    WebAccess *wa=[[WebAccess alloc]init];
    ManualLookupWebviewHelper *pMLWH=[ManualLookupWebviewHelper mlwh];
    [wa pmNewManualRequest:pMLWH.pGCType CardNumber:pMLWH.pGCNum PIN:pMLWH.pPIN];
    NSLog(@"pMLWH.pGCNum: %@",pMLWH.pGCNum);
    NSLog(@"pMLWH.pPIN: %@",pMLWH.pPIN);
    NSLog(@"pMLWH.pURL: %@",pMLWH.pURL);
    ManualLookupWebview *pMLW=[[ManualLookupWebview alloc] init];
    [self.navigationController pushViewController:pMLW animated:YES];
}
- (void)alertView:(UIAlertView *)alertView didDismissWithButtonIndex:(NSInteger)buttonIndex {
    
    if(buttonIndex==1)
    {
        [self DoManualRequest];
    }
}

@end
