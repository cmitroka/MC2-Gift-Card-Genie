//
//  LoadGC.m
//  GCG
//
//  Created by Chris on 8/2/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "LoadGC.h"
#import "DataAccess.h"
#import "CJMUtilities.h"
#import "GiftCard.h"
#import "StaticData.h"
#import "WebView.h"
#import "AddModGC.h"
#import "WebAccess.h"
#import "RespNeedsMoreInfo.h"
#import "RespNeedsCAPTCHA.h"
#import "Feedback.h"
#import "GCGSpecific.h"
#import "SFHFKeychainUtils.h"
#import "IAP.h"
#import "TVCAppDelegate.h"
#import "AdjustBalance.h"
@interface LoadGC()
-(void)monitorResp:(NSTimer*)theTimer;
-(void)rsDone;
-(void)loadAd:(NSString *)ImageURL InfoURL:(NSString *)InfoURL;
-(void)PossiblyRemoveiAd;
@end

@implementation LoadGC
@synthesize tfID,timer,pMyGC,pLoadedGC,mygcname;
static int timeout;
static NSString *AdURL;
- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    self.title=@"Card Actions";
    return self;
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

-(IBAction)DoGoToWebpage:(id)sender
{
    NSLog(pLoadedGC.p_url,NULL);
    WebView *wv=[[WebView alloc]initWithURL:pLoadedGC.p_url];
    [self.navigationController pushViewController:wv animated:YES];
}
-(IBAction)DoGoToAd:(id)sender
{
    [[UIApplication sharedApplication] openURL:[NSURL URLWithString:AdURL]];
}


-(IBAction)DoAutoLookup:(id)sender
{
    StaticData *sd=[StaticData sd];
    if (sd.pMode==@"Offline")
    {
        [CJMUtilities ShowOfflineAlert];
        return;
    }
    int iAmntOfLookupsRemaining=[CJMUtilities ConvertNSStringToInt:sd.pAmntOfLookupsRemaining];
    if (iAmntOfLookupsRemaining<=2)
    {
            if (sd.pDemoAcknowledged!=@"Y")
            {
                IAP *pIAP=[[IAP alloc] init];
                [self.navigationController pushViewController:pIAP animated:YES];  
                return;
            }
    }
    spinner.hidden=FALSE;
    sd.pPopAmount=0;
    WebAccess *wa=[[WebAccess alloc]init];
    NSString *SessionIDAndAdInfo =[wa pmGetSessionIDAndAdInfo:pMyGC.p_gctype];
    NSMutableArray *SessionIDAndAdInfoPieces=[CJMUtilities ConvertNSStringToNSMutableArray:SessionIDAndAdInfo delimiter:gcgPIECEDEL];    
    NSString *SessionID=[SessionIDAndAdInfoPieces objectAtIndex:0];
    NSString *ImageURL=[SessionIDAndAdInfoPieces objectAtIndex:1];
    NSString *InfoURL=[SessionIDAndAdInfoPieces objectAtIndex:2];
    [self loadAd:ImageURL InfoURL:InfoURL];
    NSLog(SessionID);
    NSString *Checksum=[GCGSpecific pmGetChecksum:SessionID]; 
    NSString *rs =@"";
    rs = [wa pmC4NewRequest:@"UDID" SessionID:SessionID CheckSum:Checksum CardType:pMyGC.p_gctype CardNumber:pMyGC.p_gcnum PIN:pMyGC.p_gcpin Login:pMyGC.p_credlogin Password:pMyGC.p_credpass];
    [spinner startAnimating];
    timer=[NSTimer scheduledTimerWithTimeInterval:.1 target:self selector:@selector(monitorResp:) userInfo:rs repeats:YES];
    [btnAutoLookup setEnabled:NO];
}

-(void)loadAd:(NSString *)ImageURL InfoURL:(NSString *)InfoURL;
{
    if ([ImageURL isEqualToString:@""]) return;
    NSURL *imageURL;
    NSData *imageData;
    [super viewDidLoad];
    // Do any additional setup after loading the view from its nib.
    imageURL = [NSURL URLWithString:ImageURL];
    imageURL = [NSURL URLWithString:@"http://25.media.tumblr.com/tumblr_ltoxjmEiql1qft3eko1_400.jpg"];
    NSURLRequest *request2 = [NSURLRequest requestWithURL: imageURL 
                                                 cachePolicy: NSURLRequestReloadIgnoringCacheData 
                                             timeoutInterval:5.0];
    NSData *responseData = [NSURLConnection 
                                sendSynchronousRequest:request2 returningResponse:nil error:nil];
    if (responseData.length>0)
    {
        UIImage * image = [UIImage imageWithData:responseData];
        AdImage.image = image;
        AdURL=InfoURL;
        btnGoToAd.enabled=YES;
    }
}

-(void)monitorResp:(NSTimer*)theTimer
{
    timeout++;
    NSString *retRS = @"";
    NSString *MonitorThis=(NSString*)[theTimer userInfo];
    DataAccess *da=[DataAccess da];
    retRS=[da pmGetSetting:MonitorThis];
    //retRS = [NSString stringWithFormat:@"%@%@%@%@%@",@"NEEDSMOREINFO",gcgLINEDEL,@"123456789",gcgPIECEDEL,@"ZIP"];
    
    if ([retRS isEqualToString:@""])
    {
        if (timeout>=300)
        {
            [CJMUtilities ShowAlert:@"Timed Out" Message:@"Seems your request timed out.  The data may be wrong, or we're experiencing too much traffic." ButtonText:@"OK"];
            [self rsDone];
        }
    }
    else
    {
        [self rsDone];
        [GCGSpecific pmHandleResponse:retRS PassNavView:self.navigationController];
    }
}
-(void)rsDone
{
    timeout=0;
    [timer invalidate];
    [btnAutoLookup setEnabled:YES];
    timer=nil;
    [spinner stopAnimating];
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
    btnAutoLookup.alpha=1;
    btnAutoLookupMod.alpha=1;    
}
-(void)ShowViewUnselected
{
    btnAutoLookup.alpha=.5;
    btnAutoLookupMod.alpha=.5;
}
-(void)ShowViewNotAvail
{
    btnAutoLookup.alpha=.5;
    btnAutoLookupMod.alpha=.5;
}
- (void)viewWillAppear:(BOOL)animated
{
    self.navigationController.navigationBarHidden=NO;
    [super viewWillAppear:animated];
    [self PossiblyRemoveiAd];
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
    if (pLoadedGC.p_url.length==0)
    {
        btnGoToWebpage.enabled=FALSE;
    }
    DataAccess *da = [DataAccess da];
    int showAL=[da pmIsManual:pMyGC.p_gctype];
    btnGoToWebpage.enabled=showAL;
    btnAutoLookup.enabled=showAL;
    /*
    if (showAL==0)
    {
        revShowAL=1;
    }
    else
    {
        revShowAL=0;
    }
    txtGCType.enabled=revShowAL;
    */
}
- (void)viewDidLoad
{
    [super viewDidLoad];
    // Do any additional setup after loading the view from its nib.
    //DataAccess *da = [DataAccess da];
    //arrayNo=da.pmGetMerchantsAutoLookup;
    //StaticResources *sr = [StaticResources sr];
    //arrayNo=sr.pmGetAutoLookupMerchants;
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
- (void)bannerView:(ADBannerView *)banner didFailToReceiveAdWithError:(NSError *)error
{
    NSLog(@"bannerview did not receive any banner due to %@", error);
}

- (void)bannerViewActionDidFinish:(ADBannerView *)banner
{
    NSLog(@"bannerview was selected");
}

- (BOOL)bannerViewActionShouldBegin:(ADBannerView *)banner willLeaveApplication:(BOOL)willLeave
{
    NSLog(@"Banner was clicked on; will%sleave application", willLeave ? " " : " not ");
    return willLeave;
}

- (void)bannerViewDidLoadAd:(ADBannerView *)banner 
{
    NSLog(@"banner was loaded");
}
-(void)PossiblyRemoveiAd
{
    NSString *appStatus=nil;
    appStatus=[SFHFKeychainUtils pmGetValueForSetting:@"AppStatus"];
    if ([appStatus isEqualToString:@"Purchased"])
    {
        [_ADBannerView setHidden:YES];
        [_ADBannerView removeFromSuperview];
    }
    else
    {
    }
}
@end
