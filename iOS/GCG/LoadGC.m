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
#import "RespNeedsCAPTCHA.h"
#import "Feedback.h"
#import "GCGSpecific.h"
#import "MyGCs.h"
#import "SFHFKeychainUtils.h"
#import "IAP.h"
#import "TVCAppDelegate.h"
#import "AdjustBalance.h"
@interface LoadGC()
-(void)monitorResp:(NSTimer*)theTimer;
-(void)rsDone;
@end
NSString *pIDFileName;
NSString *pSessionID;
NSString *pChecksum;
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
    StaticData *sd=[StaticData sd];

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
    NSMutableArray *tempArray=[CJMUtilities ConvertNSStringToNSMutableArray:rs delimiter:gcgLINEDEL];
    NSString *rsType=[tempArray objectAtIndex:0];
    NSString *rsValue=[tempArray objectAtIndex:1];
    NSLog(rsValue);
    [GCGSpecific pmHandleResponse:rs PassNavView:nil];
    NSMutableArray *allViewControllers = [NSMutableArray arrayWithArray:[self.navigationController viewControllers]];
    for (UIViewController *aViewController in allViewControllers) {
        if ([aViewController isKindOfClass:[MyGCs class]]) {
            [self.navigationController popToViewController:aViewController animated:NO];
        }
    }
}

-(IBAction)DoLookup:(id)sender
{
    
    if ([lookupletter isEqualToString:@"M"]) {
        
        WebAccess *wa=[[WebAccess alloc]init];
        [wa pmNewManualRequest:pMyGC.p_gctype CardNumber:pMyGC.p_gcnum PIN:pMyGC.p_gcpin];
        //7-Elevel=6050 6566 9828 2801
        //AMC=6006 4966 9520 8294 719
        
        UIPasteboard *pasteboard = [UIPasteboard generalPasteboard];
        pasteboard.string = tfGCNumber.text;
        pasteboard.string = tfGCNumber.text;
        
        [CJMUtilities ShowAlert:@"Redirecting" Message:@"The card number has been copied to the clipboard, just paste it in the card number and get your balance." ButtonText:@"OK"];
        WebView *pWV=[[WebView alloc] initWithURL:pLoadedGC.p_url];
        [self.navigationController pushViewController:pWV animated:YES];
    }
    else
    {
        StaticData *sd=[StaticData sd];
        if (sd.pMode==@"Offline")
        {
            [CJMUtilities ShowOfflineAlert];
            return;
        }
        int iAmntOfLookupsRemaining=[CJMUtilities ConvertNSStringToInt:sd.pAmntOfLookupsRemaining];
        if (iAmntOfLookupsRemaining<=0)
        {
            [CJMUtilities ShowOOLAlert];
            return;
        }
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
}

-(void)rqNewRequest:(NSTimer*)theTimer
{
    WebAccess *wa=[[WebAccess alloc]init];
    NSString *rs=[wa pmNewRequest:@"UDID" SessionID:pSessionID CheckSum:pChecksum CardType:pMyGC.p_gctype CardNumber:pMyGC.p_gcnum PIN:pMyGC.p_gcpin Login:pMyGC.p_credlogin Password:pMyGC.p_credpass];
    NSLog(rs);
    [spinner stopAnimating];
    spinner.hidden=TRUE;
    btnLookup.enabled=TRUE;
    btnLookup.alpha=1;
    NSMutableArray *tempArray=[CJMUtilities ConvertNSStringToNSMutableArray:rs delimiter:gcgLINEDEL];
    NSString *rsType=[tempArray objectAtIndex:0];
    NSString *rsValue=[tempArray objectAtIndex:1];
    
    
    if ([rsType isEqualToString:gcgGCCAPTCHA])
    {
        pIDFileName=rsValue;
        _uiCAPTCHAView.hidden=NO;
        StaticData *sd=[StaticData sd];
        NSString *pCAPTCHAURL=[NSString stringWithFormat:@"%@%@%@", sd.pCAPTCHAURLInfo,rsValue,@".bmp"];
        NSURL *imageURL = [NSURL URLWithString:pCAPTCHAURL];
        NSData *imageData = [NSData dataWithContentsOfURL:imageURL];
        UIImage * image = [UIImage imageWithData:imageData];
        _uiCAPTCHAImage.image = image;
    }
    else
    {
        [GCGSpecific pmHandleResponse:rs PassNavView:nil];
        NSMutableArray *allViewControllers = [NSMutableArray arrayWithArray:[self.navigationController viewControllers]];
        for (UIViewController *aViewController in allViewControllers) {
            if ([aViewController isKindOfClass:[MyGCs class]]) {
                [self.navigationController popToViewController:aViewController animated:NO];
            }
        }

    }
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
}
- (void)viewWillDisappear:(BOOL)animated
{
    //self.title=@"";
}

- (void)viewDidAppear:(BOOL)animated
{
    [super viewDidAppear:animated];
    StaticData *sd=[StaticData sd];
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

@end
