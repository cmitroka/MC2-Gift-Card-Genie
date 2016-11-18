//
//  LoadGC.m
//  GCG
//
//  Created by Chris on 8/2/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "LoadGC.h"
#import "OutOfLookups.h"
#import "DataAccess.h"
#import "CJMUtilities.h"
#import "GiftCard.h"
#import "StaticData.h"
#import "WebView.h"
#import "AddModGC.h"
#import "WebAccess.h"
#import "WatchIAd.h"
#import "Feedback.h"
#import "GCGSpecific.h"
#import "MyGCs.h"
#import "SFHFKeychainUtils.h"
#import "IAP.h"
#import "TVCAppDelegate.h"
#import "AdjustBalance.h"
#import "ManualLookupWebview.h"
#import "ManualLookupWebviewHelper.h"
@interface LoadGC()
-(void)monitorResp:(NSTimer*)theTimer;
-(void)rsDone;
@end
NSString *pIDFileName;
NSString *pSessionID;
NSString *pChecksum;
NSString *pCaller;
StaticData *sd;
@implementation LoadGC
@synthesize tfID,timer,pMyGC,pLoadedGC,lookupletter,mygcname;
static int timeout;

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    return self;
}
-(void)RefreshAmntOfLookupsRemaining
{
    NSString *retRS=@"";
    WebAccess *wa=[[WebAccess alloc]init];
    retRS=wa.pmDoAppStartup;
    retRS=retRS;
    NSMutableArray *temp=[CJMUtilities ConvertNSStringToNSMutableArray:retRS delimiter:gcgPIECEDEL];
    if (temp.count>0)
    {
        sd.pAmntOfLookupsRemaining=[temp objectAtIndex:2];
    }
}

-(IBAction)DoModCard:(id)sender
{
    
    AddModGC *pAddModGC=[[AddModGC alloc] initWithMerchantName:pMyGC.p_gctype myGCName:pMyGC.p_mygcname];
    [self.navigationController pushViewController:pAddModGC animated:YES];
}
-(IBAction)DoAdjustBalance:(id)sender
{
    AdjustBalance *pAdjustBalance=[[AdjustBalance alloc] init];
    [self.navigationController pushViewController:pAdjustBalance animated:YES];
}
-(IBAction)DoSendCAPTCHA:(id)sender
{
    WebAccess *wa=[[WebAccess alloc]init];
    NSString *SessionIDAndAdInfo =[wa pmGetSessionIDAndAdInfo:pMyGC.p_gctype];
    NSMutableArray *SessionIDAndAdInfoPieces=[CJMUtilities ConvertNSStringToNSMutableArray:SessionIDAndAdInfo delimiter:gcgPIECEDEL];
    pSessionID=[SessionIDAndAdInfoPieces objectAtIndex:0];
    pChecksum=[GCGSpecific pmGetChecksum:pSessionID];


    
    spinner.hidden=FALSE;
    [spinner startAnimating];
    btnLookup.enabled=FALSE;
    btnLookup.alpha=.5;
    [self performSelector:@selector(rqSendCAPTCHA:) withObject:nil afterDelay:.1];
}
-(void)rqSendCAPTCHA:(NSTimer*)theTimer
{
    WebAccess *wa=[[WebAccess alloc]init];
    NSString *rs = [wa pmContinueRequest:@"UDID" IDFileName:pIDFileName Answer:_uiCAPTCHAAnswer.text];
    NSLog(rs);
    [spinner stopAnimating];
    spinner.hidden=TRUE;
    btnLookup.enabled=TRUE;
    btnLookup.alpha=1;
    //NSMutableArray *tempArray=[CJMUtilities ConvertNSStringToNSMutableArray:rs delimiter:gcgLINEDEL];
    //NSString *rsType=[tempArray objectAtIndex:0];
    //NSString *rsValue=[tempArray objectAtIndex:1];
    //NSLog(rsValue);
    //[GCGSpecific pmHandleResponse:rs PassNavView:nil];
    [self HandleResponse:rs];
}

-(IBAction)DoLookup:(id)sender
{
        if (sd.pMode==@"Offline")
        {
            [CJMUtilities ShowOfflineAlert];
            return;
        }
        int iAmntOfLookupsRemaining=[CJMUtilities ConvertNSStringToInt:sd.pAmntOfLookupsRemaining];
        if (iAmntOfLookupsRemaining<=0)
        {
            //OutOfLookups *v = [[OutOfLookups alloc] init];
            sd.pAlertMessage=@"OOL";
            IAP *v=[[IAP alloc]init];
            [self.navigationController pushViewController:v animated:YES];
            
            //[CJMUtilities ShowOOLAlert];
            return;
        }
    NSString *appStatus=[SFHFKeychainUtils pmGetValueForSetting:@"AppStatus"];
    if (![appStatus isEqualToString:@"Purchased"])
        {
            /*
            pCaller=@"ShowAd";
            UIAlertView *showAlert=[[UIAlertView alloc]initWithTitle:@"We’re About to do your Lookup" message:@"While we do this, you may be shown an ad.  You can check it out (the process will continue in the background) or close it and wait for the process to finish." delegate:self cancelButtonTitle:@"Close" otherButtonTitles:nil];
            [showAlert show];
            */
            WatchIAd *wia =[WatchIAd wia];
            if ([wia.pWatchAdStatus isEqualToString:@"AdCompleted"]) {
                [self PrepNewRequest];
            }
            else
            {
                //[self PrepNewRequest];
                [self PrepAd];
            }
        }
}

-(void)PrepAd
{
    
    WatchIAd *wia =[WatchIAd wia];
    wia.pWatchAdStatus=@"AS";
    wia.pWatchAdNagDesc=@"ND";
    wia.pWatchAdDoneFrom=@"PreLookup";
    wia.pWatchAdGoToPage=@"LoadGC";
    wia.pWatchAdIDtoLog=sd.pUUID;
    //TVCAppDelegate *appDelegate = (TVCAppDelegate *)[[UIApplication sharedApplication] delegate];
    //[appDelegate useViewControllerS:@"WatchIAd"];

    WatchIAd *myWatchIAd = [[WatchIAd alloc]init];
    [self.navigationController pushViewController:myWatchIAd animated:YES];

    //[self PrepNewRequest];
}


-(void)PrepNewRequest
{
    WebAccess *wa=[[WebAccess alloc]init];
    NSString *SessionIDAndAdInfo =[wa pmGetSessionIDAndAdInfo:pMyGC.p_gctype];
    NSMutableArray *SessionIDAndAdInfoPieces=[CJMUtilities ConvertNSStringToNSMutableArray:SessionIDAndAdInfo delimiter:gcgPIECEDEL];
    pSessionID=[SessionIDAndAdInfoPieces objectAtIndex:0];
    pChecksum=[GCGSpecific pmGetChecksum:pSessionID];
    spinner.hidden=FALSE;
    [spinner startAnimating];
    btnLookup.enabled=FALSE;
    btnLookup.alpha=.5;
    [self performSelector:@selector(rqNewRequest:) withObject:nil afterDelay:.1];
}

-(void)rqNewRequest:(NSTimer*)theTimer
{
    WebAccess *wa=[[WebAccess alloc]init];
    if ([lookupletter isEqualToString:@"M"]) {
        [self DoManualRequest];
    }
    else
    {
        NSString *rs;
        rs=[wa pmNewRequest:@"UDID" SessionID:pSessionID CheckSum:pChecksum CardType:pMyGC.p_gctype CardNumber:pMyGC.p_gcnum PIN:pMyGC.p_gcpin Login:pMyGC.p_credlogin Password:pMyGC.p_credpass];
        NSLog(@"rs: %@",rs);
        [spinner stopAnimating];
        spinner.hidden=TRUE;
        btnLookup.enabled=TRUE;
        btnLookup.alpha=1;
        [self HandleResponse:rs];
    }
}
-(void)HandleResponse:(NSString *)rsIn
{
    NSMutableArray *tempArray=[CJMUtilities ConvertNSStringToNSMutableArray:rsIn delimiter:gcgLINEDEL];
    NSString *rsType=[tempArray objectAtIndex:0];
    NSString *rsValue=[tempArray objectAtIndex:1];
    if ([rsType isEqualToString:gcgGCCAPTCHA])
    {
        pIDFileName=rsValue;
        _uiCAPTCHAView.hidden=NO;
        NSString *pCAPTCHAURL=[NSString stringWithFormat:@"%@%@%@", sd.pCAPTCHAURLInfo,rsValue,@".bmp"];
        NSURL *imageURL = [NSURL URLWithString:pCAPTCHAURL];
        NSData *imageData = [NSData dataWithContentsOfURL:imageURL];
        UIImage * image = [UIImage imageWithData:imageData];
        _uiCAPTCHAImage.image = image;
    }
    else if ([rsType isEqualToString:gcgGCBALANCE])
    {
        NSString *Balance=@"";
        Balance=rsValue;
        NSString *temp=[CJMUtilities GetCurrentDate];
        DataAccess *da = [DataAccess da];
        [da pmUpdateMyCardBalanceInfo:sd.pLoadGCKey lastbalknown:Balance lastbaldate:temp];
        [CJMUtilities ShowAlert:@"Your Balance Is..." Message:Balance ButtonText:@"Thanks!"];
        sd.pDemoAcknowledged=@"";
        int AmntOfLookupsRemaining=[CJMUtilities ConvertNSStringToInt:sd.pAmntOfLookupsRemaining]-1;
        sd.pAmntOfLookupsRemaining=[CJMUtilities ConvertIntToNSString:AmntOfLookupsRemaining];
        [self GoBackToMyCards];
   }
    else
    {
        pCaller=@"DoManualRequest";
        UIAlertView *av=[[UIAlertView alloc] initWithTitle:@"Try Alternate Lookup?" message:[NSString stringWithFormat:@"%@%@",@"We couldn't automatically get the balance; ",@"would you like to use the alternate lookup method?"] delegate:self cancelButtonTitle:@"No thanks" otherButtonTitles:@"Yeah, let's try that", nil];
        [av show];
    }
}
-(void)GoBackToMyCards
{
    NSMutableArray *allViewControllers = [NSMutableArray arrayWithArray:[self.navigationController viewControllers]];
    for (UIViewController *aViewController in allViewControllers) {
        if ([aViewController isKindOfClass:[MyGCs class]]) {
            [self.navigationController popToViewController:aViewController animated:NO];
        }
    }
}
-(void)DoManualRequest
{
    WebAccess *wa=[[WebAccess alloc]init];
    NSString *rs;
    rs=[wa pmNewRequest:@"UDID" SessionID:pSessionID CheckSum:pChecksum CardType:pMyGC.p_gctype CardNumber:pMyGC.p_gcnum PIN:pMyGC.p_gcpin Login:pMyGC.p_credlogin Password:@"MANUAL"];
    NSLog(@"rs: %@",rs);

    //Have to finish this...
    [wa pmNewManualRequest:pMyGC.p_gctype CardNumber:pMyGC.p_gcnum PIN:pMyGC.p_gcpin];
    //Put an option to see them on the webpage
    
    ManualLookupWebviewHelper *pMLWH=[ManualLookupWebviewHelper mlwh];
    pMLWH.pGCNum=pMyGC.p_gcnum;
    pMLWH.pGCType=pMyGC.p_gctype;
    pMLWH.pPIN=pMyGC.p_gcpin;
    pMLWH.pURL=pLoadedGC.p_url;
    pMLWH.pID=sd.pLoadGCKey;
    NSLog(@"pMLWH.pGCNum: %@",pMLWH.pGCNum);
    NSLog(@"pMLWH.pGCType: %@",pMLWH.pGCType);
    NSLog(@"pMLWH.pPIN: %@",pMLWH.pPIN);
    NSLog(@"pMLWH.pURL: %@",pMLWH.pURL);
    NSLog(@"pMLWH.pID: %@",pMLWH.pID);
    NSLog(@"Launching MLW%@",@"");
    ManualLookupWebview *pMLW=[[ManualLookupWebview alloc] init];
    [self.navigationController pushViewController:pMLW animated:YES];
}
- (void)didReceiveMemoryWarning
{
    // Releases the view if it doesn't have a superview.
    [super didReceiveMemoryWarning];
    
    // Release any cached data, images, etc that aren't in use.
}

#pragma mark - View lifecycle

-(void)ShowViewSelected
{
    btnLookup.alpha=1;
}
-(void)ShowViewUnselected
{
    btnLookup.alpha=.5;
}
-(void)ShowViewNotAvail
{
    btnLookup.alpha=.5;
}

- (void)viewWillAppear:(BOOL)animated
{
    [self RefreshAmntOfLookupsRemaining];
    self.navigationController.navigationBarHidden=NO;
    //self.title=@"Card Actions";
    [super viewWillAppear:animated];
    sd=[StaticData sd];
}
- (void)viewWillDisappear:(BOOL)animated
{
    //self.title=@"";
}

- (void)viewDidAppear:(BOOL)animated
{
    [super viewDidAppear:animated];
    mygcname=sd.pLoadGCKey;
    pMyGC=[[MyCard alloc] initWithName:mygcname];
    pLoadedGC=[[MerchantInfo alloc]initWithName:pMyGC.p_gctype];
    //xxx=[da pmGetMyCardArray:test];
    //NSString *name=[xxx objectAtIndex:0];
    //NSString *num=[xxx objectAtIndex:1];
    tfMerchant.text=pMyGC.p_gctype;
    tfGCNumber.text=pMyGC.p_gcnum;
    tfID.text=mygcname;
    
    DataAccess *da = [DataAccess da];
    lookupletter=[da pmGetLookupLetter:pMyGC.p_gctype];
    [btnLookup setTitle:lookupletter forState:nil];
    if ([lookupletter isEqualToString:@"X"]) {
        btnLookup.enabled=NO;
        btnLookup.alpha=.5;
        lookuptype.alpha=.5;
    }
    //if ([sd.pPromptForAltLookup isEqualToString:@"1"]) {
    //    NSLog(@"1");
    //}
}
- (void)viewDidLoad
{
    [super viewDidLoad];
    spinner.hidden=TRUE;
    
}

- (void)viewDidUnload
{
    [super viewDidUnload];
}
- (void)alertView:(UIAlertView *)alertView didDismissWithButtonIndex:(NSInteger)buttonIndex {
    if ([pCaller isEqualToString:@"ShowAd"]) {
        [self PrepAd];
    }
    else
    {
        if(buttonIndex==1)
        {
            [self DoManualRequest];
        }
        else
        {
            [self GoBackToMyCards];
        }
    }
}

@end
