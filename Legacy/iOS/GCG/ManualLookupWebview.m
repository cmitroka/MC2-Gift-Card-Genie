//
//  ManualLookupWebview.m
//  GCG
//
//  Created by Chris Mitroka on 10/3/16.
//
//

#import "ManualLookupWebview.h"
#import "ManualLookupWebviewHelper.h"
#import "CJMUtilities.h"
#import "DataAccess.h"
@interface ManualLookupWebview ()

@end
ManualLookupWebviewHelper *pMLWH;
@implementation ManualLookupWebview

- (void)viewDidLoad
{
    [super viewDidLoad];
    pMLWH=[ManualLookupWebviewHelper mlwh];
    NSLog(@"Navigating to URL: %@",pMLWH.pURL);
    [webView loadRequest:[NSURLRequest requestWithURL:[NSURL URLWithString:pMLWH.pURL]]];
    NSLog(@"Finshed loading");
    txtCardNumb.text=pMLWH.pGCNum;
    txtPIN.text=pMLWH.pPIN;
}

- (void)webViewDidStartLoad:(UIWebView *)webViewX {
    [spinner startAnimating];
    //NSLog(@"Started loading");
    webView.hidden=true;
}

- (void)webView:(UIWebView *)wv didFailLoadWithError:(NSError *)error {
    //NSLog(@"webView:didFailLoadWithError:");
    [UIApplication sharedApplication].networkActivityIndicatorVisible = NO;
    //[UtilsA ShowAlert:@"Connection?" Message:@"Seems you don't have an internet connection right now; check and try again." ButtonText:@"OK"];
    NSString *errorString = [error localizedDescription];
    NSString *errorTitle = [NSString stringWithFormat:@"Error"];
    
    UIAlertView *errorView = [[UIAlertView alloc] initWithTitle:errorTitle message:errorString delegate:self cancelButtonTitle:nil otherButtonTitles:@"OK", nil];
    [errorView show];
}
- (void)webViewDidFinishLoad:(UIWebView *)webViewX {
    [spinner stopAnimating];
    webView.hidden=false;
    //NSLog(@"Finshed loading");
    spinner.hidden=YES;
}
- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        self.title = NSLocalizedString(@"Get Balance", @"");
    }
    return self;
}

-(void)PromptForNewBalance
{
 //       UIAlertView *av=[[UIAlertView alloc] initWithTitle:@"Reminder" message:[NSString stringWithFormat:@"%@%@",@"You're going to have to actually click the ad for it to count; ",@"still good with this?"] delegate:self cancelButtonTitle:@"Forget It" otherButtonTitles:@"Yeah, I've Got It", nil];
    
    UIAlertView * alert = [[UIAlertView alloc] initWithTitle:@"Update Balance" message:@"Please enter new balance:" delegate:self cancelButtonTitle:@"Update" otherButtonTitles:@"Cancel",nil];
    alert.alertViewStyle = UIAlertViewStylePlainTextInput;
    UITextField * alertTextField = [alert textFieldAtIndex:0];
    alertTextField.keyboardType = UIKeyboardTypeNumberPad;
    alertTextField.placeholder = @"New Balance";
    [alert show];
}
- (void)alertView:(UIAlertView *)alertView clickedButtonAtIndex:(NSInteger)buttonIndex{
    NSLog(@"Entered: %@",[[alertView textFieldAtIndex:0] text]);
}
-(IBAction)UpdateBalance:(id)sender
{
    [self PromptForNewBalance];
}
- (void)alertView:(UIAlertView *)alertView didDismissWithButtonIndex:(NSInteger)buttonIndex {
    
    if(buttonIndex==0)
    {
        NSString *NewBal=[[alertView textFieldAtIndex:0] text];
        DataAccess *da = [DataAccess da];
        NSString *NewDate=[CJMUtilities GetCurrentDate];
        int test=[da pmUpdateMyCardBalanceInfo:pMLWH.pID lastbalknown:NewBal lastbaldate:NewDate];
        [CJMUtilities ShowAlert:@"Done!" Message:@"Your balance has been updated." ButtonText:@"OK"];
    }
}
@end
