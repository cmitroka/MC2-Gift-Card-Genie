//
//  IAP.m
//  GCG
//
//  Created by Chris on 10/16/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//


#import "IAP.h"
#import "SFHFKeychainUtils.h"
#import "StaticData.h"
#import "CJMUtilities.h"
#import "TVCAppDelegate.h"
#import "TVCMasterViewController.h"
#import "WebAccess.h"
#import "MyGCs.h"
#import "SplashScreen00.h"
#import "GCGSpecific.h"
#import "WatchAd.h"
#import "ViewInterstitialAd.h"
@interface IAP()
-(void)disableExtend;
-(void)disablePurchase;
-(void)LogIt;
-(void)LogConsideredBuying:(NSString *)LogType;
@end
NSString *LAWD;
BOOL DeActivate;
@implementation IAP
StaticData *sd;
@synthesize request, pPurchaseType, verified;

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization
    }
    return self;
}
-(IBAction)PurchaseApp:(id)sender
{    
    NSLog(@"PurchaseApp");  
    pPurchaseType=@"GiftCardGenie001";
    [self LogConsideredBuying:[NSString stringWithFormat:@"%@", @"Unlimited"]];
    [self processsPurchaseRequest];
}
-(IBAction)WatchIntAd:(id)sender
{
    //TVCAppDelegate *appDelegate = (TVCAppDelegate *)[[UIApplication sharedApplication] delegate];
    //[appDelegate useNavController:[WatchAd class]];
    
    [self GoToWatchAd];
}
-(IBAction)WatchAd:(id)sender
{
    //TVCAppDelegate *appDelegate = (TVCAppDelegate *)[[UIApplication sharedApplication] delegate];
    //[appDelegate useNavController:[WatchAd class]];
    WatchAd *pWatchAd=[[WatchAd alloc] init];
    [self.navigationController pushViewController:pWatchAd animated:YES];
}
-(IBAction)doGoToMain:(id)sender
{
    //TVCAppDelegate *appDelegate = (TVCAppDelegate *)[[UIApplication sharedApplication] delegate];
    //[appDelegate useNavController:[WatchAd class]];
    TVCAppDelegate *appDelegate = (TVCAppDelegate *)[[UIApplication sharedApplication] delegate];
    [appDelegate useNavController:[TVCMasterViewController class]];
}
-(IBAction)Restart:(id)sender
{    
    exit(0);
}


-(IBAction)Purchase10Lookups:(id)sender
{    
    NSLog(@"Purchase10Lookups");  
    NSLog(sd.pAmntForADollar,NULL);
    pPurchaseType=@"15for1";
    
    //pPurchaseType=@"SetOfLookups";
    
    //tfStatus.text=[NSString stringWithFormat:@"%@ \r\n %@", @"Purchase 10 sent...", tfStatus.text];
    [self LogConsideredBuying:[NSString stringWithFormat:@"%@ %@", @"15", @" for .99"]];
    [self processsPurchaseRequest];
}

-(void)processsPurchaseRequest
{
    if ([SKPaymentQueue canMakePayments]) {  
        request = [[SKProductsRequest alloc] initWithProductIdentifiers:[NSSet setWithObject:pPurchaseType]]; //GiftCardGenie001  com.mc2techservices.gcg.02
        request.delegate = self;   
        [request start];   
    } else {  
        UIAlertView *tmp = [[UIAlertView alloc]  
                            initWithTitle:@"Prohibited"  
                            message:@"Parental Control is enabled, cannot make a purchase!"  
                            delegate:self  
                            cancelButtonTitle:nil  
                            otherButtonTitles:@"Ok", nil];  
        [tmp show];  
    }  
}

-(IBAction)ExtendTrial:(id)sender
{
    sd.pDemoAcknowledged=@"Y";
    //[SFHFKeychainUtils pmUpdateSettingName:@"AppStatus" SettingValue:@"Extension"];
    [self.navigationController popViewControllerAnimated:YES];

}
-(IBAction)ReReg:(id)sender
{
    [[SKPaymentQueue defaultQueue] addTransactionObserver:self];
    [[SKPaymentQueue defaultQueue] restoreCompletedTransactions];
}
- (void)didReceiveMemoryWarning
{
    // Releases the view if it doesn't have a superview.
    [super didReceiveMemoryWarning];
    
    // Release any cached data, images, etc that aren't in use.
}
-(void)YesPickedExec
{
    
}  
#pragma mark - View lifecycle

- (void)viewDidLoad
{
    [super viewDidLoad];
    sd=[StaticData sd];
    int iRemAmnt=sd.pAmntOfLookupsRemaining.integerValue;
    
    
    NSString *temp=[CJMUtilities GetTodaysDate];
    LAWD=[SFHFKeychainUtils pmGetValueForSetting:@"AdWatchDate"];
    if ([temp isEqualToString:LAWD]) {
        DeActivate=true;
    }
    
    NSString *sRemAmnt=[CJMUtilities ConvertIntToNSString:iRemAmnt];
    if (iRemAmnt<0)
    {
        sRemAmnt=@"0";
    }
    else if (iRemAmnt>50)
    {
        sRemAmnt=@"unlimited";
    }
    if (sd.pMode==@"Offline")
    {
        sRemAmnt=@"? (your offline)";        
    }
    
    if ([sd.pAlertMessage isEqualToString:@"OOL"]) {
        [CJMUtilities ShowAlert:@"Uh Oh!" Message:@"You're out of lookups!  Don't worry, it's easy to get more - take a look at the options we offer." ButtonText:@"OK"];
        sd.pAlertMessage=@"";
    }
    
    NSString *temp1=[NSString stringWithFormat:@"%@ %@ lookups remaining.", @"You have", sRemAmnt];
    [lblLookupInfo setText:temp1];    
    
    NSString *temp2=[NSString stringWithFormat:@"I'll take %@ more lookups please", sd.pAmntForADollar];
    //[btnPurchaseQuantity setTitle:temp2 forState:NULL];

//    NSString *temp3=[NSString stringWithFormat:@"Look up as many realtime balances as you'd like for as long as GCG's around%@.", sd.pCostForApp];
//    [lblPurchase setText:temp3];
    
    //This will let you look up as many realtime balances as you'd like for as long as GCG's around.
    NSString *temp4=[NSString stringWithFormat:@"As the button indicates, this purchase option will give you %@ successful lookups for .99", sd.pAmntForADollar];
    [lblPurchaseQuantity setText:temp4];

    //For testing
    //pPurchaseType=@"GiftCardGenie001";
    //pPurchaseType=@"15for1";
    //[self LogIt];
    
    //You get 5 successful lookups.  So far you've received X, you have Y remaining.

}
-(void)disablePurchase
{
    lblPurchase.alpha=.5;
    btnPurchase.enabled=NO;
    btnPurchase.alpha=.5;
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
- (void) failedTransaction:(SKPaymentTransaction *) transaction
{
        NSLog(@"failedTransaction");  
    if (transaction.error.code != SKErrorPaymentCancelled) {
        NSMutableString *messageToBeShown = [[NSMutableString alloc] init];
        [messageToBeShown setString:[NSString stringWithFormat:@"%@ %@", NSLocalizedString(@"Reason:", @"Reason Text in alert when payment transaction failed"), [transaction.error localizedFailureReason]]];
        NSLog(messageToBeShown);
    }
}
-(void)paymentQueue:(SKPaymentQueue *)queue updatedTransactions:(NSArray *)transactions {
    NSLog(@"paymentQueue");  
    NSString *ChangeScreen=@"";
    for (SKPaymentTransaction *transaction in transactions) {
        switch (transaction.transactionState) {
            case SKPaymentTransactionStatePurchasing:
                NSLog(@"SKPaymentTransactionStatePurchasing");
                break;
            case SKPaymentTransactionStatePurchased:
                NSLog(@"SKPaymentTransactionStatePurchased");
                [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
                [SFHFKeychainUtils pmUpdateSettingName:@"AppStatus" SettingValue:@"Purchased"];
                [self LogIt];
                [CJMUtilities ShowAlert:@"Thanks!" Message:@"We really appreciate you purchasing the app!  Please restart GCG so it can be updated." ButtonText:@"Sure"];
                ChangeScreen=@"1";
                break;
            case SKPaymentTransactionStateRestored:
                NSLog(@"SKPaymentTransactionStateRestored");
                [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
                break;
                
            case SKPaymentTransactionStateFailed:
                NSLog(@"SKPaymentTransactionStateFailed");
                if (transaction.error.code != SKErrorPaymentCancelled) {
                    NSMutableString *messageToBeShown = [[NSMutableString alloc] init];
                    [messageToBeShown setString:[NSString stringWithFormat:@"%@ %@", NSLocalizedString(@"PaymentTransactionStateFailed: ", @"Reason Text in alert when payment transaction failed"), [transaction.error localizedFailureReason]]];
                    NSLog(messageToBeShown);
                    [CJMUtilities ShowAlert:@"Error" Message:messageToBeShown ButtonText:@"OK"];
                }
                [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
                 break;
                
            default:
                break;
        }
    }
    if (ChangeScreen==@"1")
    {
        //TVCAppDelegate *appDelegate = (TVCAppDelegate *)[[UIApplication sharedApplication] delegate];
        //[appDelegate useNavController:[SplashScreen00 class]];        
        subscreen.hidden=FALSE;
    }
}

-(void)GoToWatchAd
{
    if (DeActivate==true) {
        [CJMUtilities ShowAlert:@"Sorry" Message:[NSString stringWithFormat:@"%@%@",@"You're only able to do this ",@"once a day."] ButtonText:@"Oh well, maybe tomorrow..."];
        return;
    }

    UIAlertView *av=[[UIAlertView alloc] initWithTitle:@"Reminder" message:[NSString stringWithFormat:@"%@%@",@"You're going to have to actually click the ad for it to count; ",@"still good with this?"] delegate:self cancelButtonTitle:@"Forget It" otherButtonTitles:@"Yeah, I've Got It", nil];
    [av show];
}
- (void)alertView:(UIAlertView *)alertView didDismissWithButtonIndex:(NSInteger)buttonIndex {
    
    if(buttonIndex==0)
    {
    }
    else
    {
        TVCAppDelegate *appDelegate = (TVCAppDelegate *)[[UIApplication sharedApplication] delegate];
        [appDelegate useNavController:[ViewInterstitialAd class]];
    }
}

-(void)LogIt
{
    WebAccess *wa=[[WebAccess alloc]init];
    NSString *SessionIDAndAdInfo =[wa pmGetSessionIDAndAdInfo:@""];
    NSMutableArray *SessionIDAndAdInfoPieces=[CJMUtilities ConvertNSStringToNSMutableArray:SessionIDAndAdInfo delimiter:gcgPIECEDEL];    
    NSString *SessionID=[SessionIDAndAdInfoPieces objectAtIndex:0];
    NSString *Checksum=[GCGSpecific pmGetChecksum:SessionID]; 
    NSString *Amnt=@"0";
    if ([pPurchaseType isEqualToString:@"GiftCardGenie001"]) {
        Amnt=@"999";
    }
    else if ([pPurchaseType isEqualToString:@"15for1"]) {
        Amnt=@"15";
    }
    [wa pmLogPurchase:SessionID CheckSum:Checksum PurchaseType:Amnt];
}
-(void)LogConsideredBuying:(NSString *)LogType
{
    WebAccess *wa=[[WebAccess alloc]init];
    [wa pmDoLogConsideredBuying:LogType];
}
- (void)viewWillAppear:(BOOL)animated
{
    [super viewWillAppear:animated];
    self.navigationController.navigationBar.hidden = NO;
    
    //btnPurchase.layer.borderWidth=1.0f;
    //btnPurchase.layer.borderColor=[[UIColor blackColor] CGColor];
    //btnPurchase.layer.borderWidth=1.0f;
    //btnPurchaseQuantity.layer.borderColor=[[UIColor blackColor] CGColor];
    //btnPurchaseQuantity.layer.borderWidth=1.0f;
    //btnWatchAd.layer.borderColor=[[UIColor blackColor] CGColor];
    //btnWatchAd.layer.borderWidth=1.0f;
    //btnDemoMode.layer.borderColor=[[UIColor blackColor] CGColor];
    //btnDemoMode.layer.borderWidth=1.0f;
    //btnWatchIntAd.layer.borderColor=[[UIColor blackColor] CGColor];
    //btnWatchIntAd.layer.borderWidth=1.0f;
    int iAmntOfLookupsRemaining=[CJMUtilities ConvertNSStringToInt:sd.pAmntOfLookupsRemaining];
    if (iAmntOfLookupsRemaining<=0) {
        [btnDemoMode setTitle:@"I'll use the app without lookups" forState:nil];
    }
    else
    {
        [btnDemoMode setTitle:@"Continue, I've got a few lookups left" forState:nil];
    }
}

-(void)productsRequest:(SKProductsRequest *)request didReceiveResponse:(SKProductsResponse *)response
{      
    NSLog(@"productsRequest");  
    // remove wait view here
    tfStatus.text = @"";
    
    SKProduct *validProduct = nil;
    int count = [response.products count];
    
    if (count>0) {
        validProduct = [response.products objectAtIndex:0];
        
        
        NSLog(pPurchaseType);
        SKPayment *payment = [SKPayment paymentWithProductIdentifier:pPurchaseType];
        [[SKPaymentQueue defaultQueue] addTransactionObserver:self];
        [[SKPaymentQueue defaultQueue] addPayment:payment];
        
        
    } else {
        UIAlertView *tmp = [[UIAlertView alloc] 
                            initWithTitle:@"Not Available" 
                            message:@"No products to purchase"
                            delegate:self 
                            cancelButtonTitle:nil 
                            otherButtonTitles:@"Ok", nil]; 
        [tmp show];
    }
    
    
}  

-(void)requestDidFinish:(SKRequest *)request  
{  
    NSLog(@"SKRequest requestDidFinish");
}  

-(void)request:(SKRequest *)request didFailWithError:(NSError *)error  
{  
    //NSMutableString *messageToBeShown = [[NSMutableString alloc] init];
    NSString *err=@"";
    err=[NSString stringWithFormat:@"%@ %@", @"Failed to connect with error: ", [error localizedDescription]];
    //[messageToBeShown setString:[NSString stringWithFormat:@"%@ %@", NSLocalizedString(@"PaymentTransactionStateFailed: ", @"Reason Text in alert when payment transaction failed"), [transaction.error localizedFailureReason]]];
    [CJMUtilities ShowAlert:@"Error" Message:err ButtonText:@"OK"];
    NSLog(err);  
}  

@end
