//
//  AddModGC.m
//  NAVCON01
//
//  Created by Chris on 7/12/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "AddModGC.h"
#import "CJMUtilities.h"
#import "GiftCard.h"
#import "StaticData.h"
#import "WebAccess.h"
#import "GCGSpecific.h"
#import "SFHFKeychainUtils.h"
#import "TVCAppDelegate.h"
#import "MyGCs.h"

#import "IAP.h"


@interface AddModGC()
-(void)rsDone;
-(void)loadCurrCard;
-(void)MakeGCInfoVisible;
-(void)MakeGCInfoInvisible;
-(void)ShowSend;
-(void)ShowSaveDel;
-(void)doScreenConfig;
-(void)goBack;
- (void)PossiblyRemoveiAd;
@end
@implementation AddModGC
static int timeout;
static int buttontag;
int showAL;
int revShowAL;
@synthesize txtGCType, txtCredLogin, txtCredPass, txtGCNum, txtGCPIN, txtMyOldGCName;
@synthesize lblCredLogin, lblCredPass,lblGCNum,lblGCPIN,lblGCType,lblCredInfo,lblGCInfo,lblSave,lblDelete,timer,lblAutoLookup;
@synthesize cmdSave, cmdDelete, cmdSendUnsupported,cmdDone,cmdBack,nbNavBar,cmdAutoLookup;
@synthesize merchantIn,myGCNameIn,currentlySelected;
static NSString *isDirty;

-(void)loadCurrCard
{
    currCard=[[MyCard alloc] initWithName:myGCNameIn];
    txtGCType.text=merchantIn;
    txtGCType.text=currCard.p_gctype;
    txtGCNum.text=currCard.p_gcnum;
    txtGCPIN.text=currCard.p_gcpin;
    txtCredLogin.text=currCard.p_credlogin;
    txtCredPass.text=currCard.p_credpass;
    txtMyOldGCName.text=currCard.p_mygcname;
    if ([txtGCType.text isEqualToString:@"(null)"]) {txtGCType.text=@"";}
    if ([txtGCNum.text isEqualToString:@"(null)"]) {txtGCNum.text=@"";}
    if ([txtGCPIN.text isEqualToString:@"(null)"]) {txtGCPIN.text=@"";}
    if ([txtCredLogin.text isEqualToString:@"(null)"]) {txtCredLogin.text=@"";}
    if ([txtCredPass.text isEqualToString:@"(null)"]) {txtCredPass.text=@"";}    
}
-(void)loadMerchantInfo
{
    merchantInfo=[[MerchantInfo alloc] initWithName:myGCNameIn];
    
}

-(void)MakeGCInfoInvisible
{
    [lblGCInfo setHidden:TRUE];
    [lblGCType setHidden:TRUE];
    [txtGCType setHidden:TRUE];
    [lblGCNum setHidden:TRUE];
    [txtGCNum setHidden:TRUE];
    [lblGCPIN setHidden:TRUE];
    [txtGCPIN setHidden:TRUE];
}
-(void)MakeCredInfoInvisible
{
    [lblCredInfo setHidden:TRUE];
    [lblCredLogin setHidden:TRUE];
    [txtCredLogin setHidden:TRUE];
    [lblCredPass setHidden:TRUE];
    [txtCredPass setHidden:TRUE];
}

-(void)ShowPINandNoCreds
{
    [lblGCInfo setHidden:FALSE];
    [lblGCType setHidden:FALSE];
    [txtGCType setHidden:FALSE];
    [lblGCNum setHidden:FALSE];
    [txtGCNum setHidden:FALSE];
    [lblGCPIN setHidden:FALSE];
    [txtGCPIN setHidden:FALSE];
    
    [lblCredInfo setHidden:TRUE];
    [lblCredLogin setHidden:TRUE];
    [txtCredLogin setHidden:TRUE];
    [lblCredPass setHidden:TRUE];
    [txtCredPass setHidden:TRUE];
    [self ShowSaveDel];
}
-(void)ShowPINandCreds
{
    [lblGCInfo setHidden:FALSE];
    [lblGCType setHidden:FALSE];
    [txtGCType setHidden:FALSE];
    [lblGCNum setHidden:FALSE];
    [txtGCNum setHidden:FALSE];
    [lblGCPIN setHidden:FALSE];
    [txtGCPIN setHidden:FALSE];
    
    [lblCredInfo setHidden:FALSE];
    [lblCredLogin setHidden:FALSE];
    [txtCredLogin setHidden:FALSE];
    [lblCredPass setHidden:FALSE];
    [txtCredPass setHidden:FALSE];
    [self ShowSaveDel];
}
-(void)ShowNoPINandCreds //Show type and card # and Login/Pass info
{
    [lblGCInfo setHidden:FALSE];
    [lblGCType setHidden:FALSE];
    [txtGCType setHidden:FALSE];
    [lblGCNum setHidden:FALSE];
    [txtGCNum setHidden:FALSE];
    [lblGCPIN setHidden:TRUE];
    [txtGCPIN setHidden:TRUE];
    
    [lblCredInfo setHidden:FALSE];
    [lblCredLogin setHidden:FALSE];
    [txtCredLogin setHidden:FALSE];
    [lblCredPass setHidden:FALSE];
    [txtCredPass setHidden:FALSE];
    [self ShowSaveDel];
}
-(void)ShowNoPINandNoCreds //Just show type and card #
{
    [lblGCInfo setHidden:FALSE];
    [lblGCType setHidden:FALSE];
    [txtGCType setHidden:FALSE];
    [lblGCNum setHidden:FALSE];
    [txtGCNum setHidden:FALSE];
    [lblGCPIN setHidden:TRUE];
    [txtGCPIN setHidden:TRUE];
    
    [lblCredInfo setHidden:TRUE];
    [lblCredLogin setHidden:TRUE];
    [txtCredLogin setHidden:TRUE];
    [lblCredPass setHidden:TRUE];
    [txtCredPass setHidden:TRUE];
    [self ShowSaveDel];
}
-(IBAction)SelectOther:(id)sender
{
    currentlySelected=@"";
}
-(IBAction)SelectNumber:(id)sender
{
    currentlySelected=@"Number";    
}
-(IBAction)SelectPIN:(id)sender
{
    currentlySelected=@"PIN";
}

-(void)ShowSend
{
    nbNavBar.topItem.title=@"Submit a Card";
    [cmdSave setHidden:YES];
    [cmdDelete setHidden:YES];
    [cmdAutoLookup setHidden:YES];
    [lblAutoLookup setHidden:YES];
    [lblSave setHidden:YES];
    [lblDelete setHidden:YES];
    [spinner setHidden:YES];
    [cmdSendUnsupported setHidden:NO];
}
-(void)ShowSaveDel
{
    nbNavBar.topItem.title=@"Add/Modify Card";
    [cmdSave setHidden:NO];
    [cmdDelete setHidden:NO];
    [cmdAutoLookup setHidden:NO];
    [lblAutoLookup setHidden:NO];
    [lblSave setHidden:NO];
    [lblDelete setHidden:NO];
    [spinner setHidden:YES];
    [cmdSendUnsupported setHidden:YES];
}

-(void)Save
{
    NSString *p_mygcname=[NSString stringWithFormat:@"%@-%@", txtGCType.text, txtGCNum.text];
    int test=[da pmDoesMyCardExist:p_mygcname];
    if (test==1)  //It's an update in this case
    {
        txtMyOldGCName.text=p_mygcname;
    }
    NSString *temp=txtMyOldGCName.text;
    int retStatus=[da pmUpdateMyCard:txtGCType.text gcnum:txtGCNum.text gcpin:txtGCPIN.text credlogin:txtCredLogin.text credpass:txtCredPass.text mygcname:p_mygcname myoldgcname:txtMyOldGCName.text];
    if (temp==Nil)
    {
        [da pmUpdateMyCardBalanceInfo:p_mygcname lastbalknown:@" " lastbaldate:@" "];    
    }    
    //NSString *retStatus1=[CJMUtilities ConvertIntToNSString:retStatus];
    txtMyOldGCName.text=p_mygcname;
    StaticData *sd=[StaticData sd];
    sd.pLoadGCKey=p_mygcname;
    if (retStatus==1) {
        [CJMUtilities ShowAlert:@"Data Saved" Message:@"Your card data is saved." ButtonText:@"OK"];
    } 
}
-(int)pmLoadByMyCard:(MyCard *)mygc
{
    currCard=mygc;
    return 1;
}
-(void)getScreenConfig
{
    merchantInfo=[[MerchantInfo alloc] initWithName:merchantIn];
    if (merchantInfo.p_showCardPIN==TRUE) {
        dispmode=dispmode+1;
    }
    if (merchantInfo.p_showCreds==TRUE) {
        dispmode=dispmode+2;
    }
    if (merchantInfo.p_reqReg==TRUE)
    {
        NSString *message = [NSString stringWithFormat: @"%@ needs you to register with them before using this.  If you haven't, go to their website and do so.",merchantIn];
        [CJMUtilities ShowAlert:@"FYI..." Message:message ButtonText:@"OK"];
    }
}
-(void)doScreenConfig
{
    switch (dispmode) {
        case 0:
            [self ShowNoPINandNoCreds];
            break;
        case 1:
            [self ShowPINandNoCreds];
            break;
        case 2:
            [self ShowNoPINandCreds];
            break;
        case 3:
            [self ShowPINandCreds];
            break;            
        case 4:
            [self ShowPINandNoCreds];
            [self ShowSaveDel];
            break;            
        default:
            break;
    }
}
- (void)alertView:(UIAlertView *)alertView didDismissWithButtonIndex:(NSInteger)buttonIndex {
    
    if (buttontag==1) {
        if(buttonIndex==0)
        {
            WebAccess *wa=[[WebAccess alloc]init];
            [wa pmAddMerchantRequest:txtGCType.text CardNumber:txtGCNum.text PIN:txtGCPIN.text];                            
        }
        else
        {
        }
    }
    
    if (buttontag==2)
    {
        int retVal=0;    
        if(buttonIndex==0) {
            
        }
        else
        {
            da=[DataAccess da];
            retVal=[da pmDeleteMyCard:txtMyOldGCName.text];
            TVCAppDelegate *appDelegate = (TVCAppDelegate *)[[UIApplication sharedApplication] delegate];
            [appDelegate useNavController:[MyGCs class]];
        }
        if (retVal!=1) {
            //[CJMUtilities ShowAlert:@"Oops" Message:@"Couldn't Delete" ButtonText:@"Aw Snap"];
        }
        else
        {
            txtMyOldGCName.text=@"";
            txtCredLogin.text=@"";
            txtCredPass.text=@"";
            txtGCNum.text=@"";
            txtGCPIN.text=@"";
            [self.navigationController popViewControllerAnimated:YES];
        }
        
    }
}

-(IBAction)Cancel:(id)sender
{
    UIAlertView *av=[[UIAlertView alloc] initWithTitle:@"Confirm" message:[NSString stringWithFormat:@"%@%@",@"This will clear the value here; ",@"OK?"] delegate:self cancelButtonTitle:@"No, My Mistake" otherButtonTitles:@"Yeah, I'm Sure", nil];
    [av show];
}

-(int)pmLoadByGCName:(NSString *)mygcname
{
    currCard=[[MyCard alloc] initWithName:mygcname];
    return 1;
}

-(IBAction)DoSave:(id)sender
{
    if (merchantIn == @"Unlisted - Manually Enter")
    {
        //UIAlertView *av=[[UIAlertView alloc] initWithTitle:@"Share?" message:[NSString stringWithFormat:@"%@%@",@"If you share this data with us, we can TRY making as autolookup.", @"  Share it with us?  No credit cards please..."] delegate:self cancelButtonTitle:@"Yeah, I'll Share" otherButtonTitles:@"No, Keep Private", nil];
        //[av show];
        WebAccess *wa=[[WebAccess alloc]init];
        [wa pmAddMerchantRequest:txtGCType.text CardNumber:txtGCNum.text PIN:txtGCPIN.text];                           
        [self Save];
    }
    else
    {
        [self Save];
    }
    
    buttontag=1;
    isDirty=@"";
}
-(IBAction)DoDelete:(id)sender
{
    UIAlertView *av=[[UIAlertView alloc] initWithTitle:@"Confirm" message:[NSString stringWithFormat:@"%@%@",@"Are you sure you want to ",@"delete this?"] delegate:self cancelButtonTitle:@"No, My Mistake" otherButtonTitles:@"Yeah, I'm Sure", nil];
    [av show];
    buttontag=2;
    isDirty=@"";
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

    if (txtGCNum.isHidden==FALSE)
    {
        if (txtGCNum.text.length==0)
        {
            [CJMUtilities ShowAlert:@"" Message:@"Need a Card Number" ButtonText:@"OK"];
            return;
        }
    }
    if (txtGCPIN.isHidden==FALSE)
    {
        if (txtGCPIN.text.length==0)
        {
            [CJMUtilities ShowAlert:@"" Message:@"Need a PIN" ButtonText:@"OK"];
            return;
        }        
    }
    if (txtCredLogin.isHidden==FALSE)
    {
        if (txtCredLogin.text.length==0)
        {
            [CJMUtilities ShowAlert:@"" Message:@"Need a Login" ButtonText:@"OK"];
            return;
        }    
    }
    if (txtCredPass.isHidden==FALSE)
    {
        if (txtCredPass.text.length==0)
        {
            [CJMUtilities ShowAlert:@"" Message:@"Need a Password" ButtonText:@"OK"];
            return;
        }        
    }
    
    
    sd.pPopAmount=0;
    WebAccess *wa=[[WebAccess alloc]init];
    NSString *SessionIDAndAdInfo =[wa pmGetSessionIDAndAdInfo:txtGCType.text];
    NSMutableArray *SessionIDAndAdInfoPieces=[CJMUtilities ConvertNSStringToNSMutableArray:SessionIDAndAdInfo delimiter:gcgPIECEDEL];    
    NSString *SessionID=[SessionIDAndAdInfoPieces objectAtIndex:0];
    NSString *Checksum=[GCGSpecific pmGetChecksum:SessionID]; 
    NSString *rs =@"";
    rs = [wa pmC4NewRequest:@"UDID" SessionID:SessionID CheckSum:Checksum CardType:txtGCType.text CardNumber:txtGCNum.text PIN:txtGCPIN.text Login:txtCredLogin.text Password:txtCredPass.text];
    spinner.hidden=NO;
    [spinner startAnimating];
    timeout=0;
    timer=[NSTimer scheduledTimerWithTimeInterval:.1 target:self selector:@selector(monitorResp:) userInfo:rs repeats:YES];
    [cmdAutoLookup setEnabled:NO];
    
}

-(IBAction)DoSendUnsupported:(id)sender
{
    //NSString *unsuppvendor =[NSString stringWithFormat:@"UNSUPPORTED - %@",txtGCType.text];
    WebAccess *wa=[[WebAccess alloc]init];
    [wa pmAddMerchantRequest:txtGCType.text CardNumber:txtGCNum.text PIN:txtGCPIN.text];
    //[CJMUtilities ShowAlert:@"Thanks!" Message:@"We'll take a look at what you sent us, then build the interface if possible" ButtonText:@"OK"];
    isDirty=@"";
}

- (void)HandleTextDone
{
    [txtCredLogin resignFirstResponder];
    [txtCredPass resignFirstResponder];    
    [txtGCNum resignFirstResponder];    
    [txtGCPIN resignFirstResponder];    
    [txtGCType resignFirstResponder];    
    [txtMyOldGCName resignFirstResponder];
    [pModDataText resignFirstResponder];
    [self doScreenConfig];
    [cmdDone setHidden:TRUE];
    NSString *TestNum=txtGCNum.text;
    NSString *TestPIN=txtGCPIN.text;
    NSString *TestLogin=txtCredLogin.text;
    NSString *TestPass=txtCredPass.text;
    
    TestNum=[CJMUtilities TrueTextboxValue:txtGCNum];
    TestPIN=[CJMUtilities TrueTextboxValue:txtGCPIN];
    TestLogin=[CJMUtilities TrueTextboxValue:txtCredLogin];
    TestPass=[CJMUtilities TrueTextboxValue:txtCredPass];
    
    if ([pModDataLabel.text isEqualToString:@"Card Vendor"]) {txtGCType.text=pModDataText.text;}
    if ([pModDataLabel.text isEqualToString:@"Card Number"]) {txtGCNum.text=pModDataText.text;}
    if ([pModDataLabel.text isEqualToString:@"Card PIN"]) {txtGCPIN.text=pModDataText.text;}
    if ([pModDataLabel.text isEqualToString:@"Login"]) {txtCredLogin.text=pModDataText.text;}
    if ([pModDataLabel.text isEqualToString:@"Password"]) {txtCredPass.text=pModDataText.text;}
    isDirty=@"";
    NSString *TestNum2=txtGCNum.text;
    NSString *TestPIN2=txtGCPIN.text;
    NSString *TestLogin2=txtCredLogin.text;
    NSString *TestPass2=txtCredPass.text;
    
    TestNum2=[CJMUtilities TrueTextboxValue:txtGCNum];
    TestPIN2=[CJMUtilities TrueTextboxValue:txtGCPIN];
    TestLogin2=[CJMUtilities TrueTextboxValue:txtCredLogin];
    TestPass2=[CJMUtilities TrueTextboxValue:txtCredPass];
    
    if ([TestNum isEqualToString:TestNum2]==NO)
    {
        isDirty=@"Y";
    }
    if ([TestPIN isEqualToString:TestPIN2]==NO){isDirty=@"Y";}
    if ([TestLogin isEqualToString:TestLogin2]==NO){isDirty=@"Y";}
    if ([TestPass isEqualToString:TestPass2]==NO){isDirty=@"Y";}
}
-(IBAction)DoBack:(id)sender
{
    NSString *test=isDirty;
    NSLog(@"%i", isDirty.length);
    if (isDirty.length>0)
    {
        [CJMUtilities ShowAlert:@"Before you go..." Message:@"Make sure you save first, otherwise tap this button again to go back." ButtonText:@"OK"];
        isDirty=@"";
    }
    else
    {
        [self goBack];        
    }
    
}
-(void)goBack
{
    [self.navigationController popViewControllerAnimated:YES];  
}

-(void)MakeFocusView:(UITextField *)textField
{
    currentlySelected=@"";
    [pModDataText setText:textField.text];
    [pModDataLabel setText:textField.placeholder];
    if ([pModDataLabel.text isEqualToString:@"Card Vendor"]) {
        pModDataText.keyboardType=UIKeyboardTypeDefault;}
    if ([pModDataLabel.text isEqualToString:@"Card Number"]) 
    {
        pModDataText.keyboardType=UIKeyboardTypeNumberPad;
        currentlySelected=@"Number";
    }
    if ([pModDataLabel.text isEqualToString:@"Card PIN"])
    {
        pModDataText.keyboardType=UIKeyboardTypeNumberPad;
        currentlySelected=@"PIN";
    }
    if ([pModDataLabel.text isEqualToString:@"Login"]) {
        pModDataText.keyboardType=UIKeyboardTypeDefault;}
    if ([pModDataLabel.text isEqualToString:@"Password"]) {
        pModDataText.keyboardType=UIKeyboardTypeDefault;}
    [pModDataView setHidden:NO];    
    [pModDataText becomeFirstResponder];
    //[self HandleTextDone];    
}
-(IBAction)DoDismiss:(id)sender
{
    NSString* OK=@"";
    OK=[self IsSelectionVaild];    
    if (OK==@"")
    {
        [self HandleTextDone];
        [pModDataView setHidden:YES];
    }
    else
    {
        [CJMUtilities ShowAlert:@"Cannot Save" Message:OK ButtonText:@"OK"];
    }
}
- (NSString*)IsSelectionVaild
{
    NSString *retVal=@"";
    int temp=0;
    NSString *currLenStr=[CJMUtilities ConvertIntToNSString:pModDataText.text.length];
    if ([currentlySelected isEqualToString:@""])
    {
        retVal=@"";
    }
    else if ([currentlySelected isEqualToString:@"PIN"]) {
        temp=pModDataText.text.length;
        if (temp<merchantInfo.p_minPINLen)
        {
            retVal=[NSString stringWithFormat:@"This %@ has to be at least %i long; you've entered %@.", currentlySelected, merchantInfo.p_minPINLen, currLenStr];
        }
        if (temp>merchantInfo.p_maxPINLen)
        {
            retVal=[NSString stringWithFormat:@"This %@ can't be over %i long; you've entered %@.", currentlySelected, merchantInfo.p_maxPINLen, currLenStr];
        }
    }    
    else if ([currentlySelected isEqualToString:@"Number"]) {
        if (pModDataText.text.length<merchantInfo.p_minCardLen)                
        {
            retVal=[NSString stringWithFormat:@"This %@ has to be at least %i long; you've entered %@.", currentlySelected, merchantInfo.p_minCardLen, currLenStr];
        }
        if (pModDataText.text.length>merchantInfo.p_maxCardLen)
        {
            retVal=[NSString stringWithFormat:@"This %@ can't be over %i long; you've entered %@.", currentlySelected, merchantInfo.p_maxCardLen, currLenStr];
        }
    }
    else
    {
        retVal=@"";
    }
    if ([retVal length]>0)
    {
        retVal=[NSString stringWithFormat:@"%@ %@", retVal, @"\r\nTap 'Back' to cancel"];
    }
    
    if (merchantIn == @"Unlisted - Manually Enter")
    {
        retVal=@"";
    }
    
    return retVal;
}
- (BOOL)textFieldShouldBeginEditing:(UITextField *)textField
{
    NSString *test=textField.placeholder;
    if ([test isEqualToString:@"Enter Info"])
    {
        return YES;
    }
    if (textField.tag==1) {
        [self doScreenConfig];
        [self MakeGCInfoInvisible];
    }
    else if (textField.tag==0) {
        [self doScreenConfig];
        [self MakeCredInfoInvisible];
    }    
    [self MakeFocusView:textField];
    [cmdDone setHidden:FALSE];
    return NO;
}
- (void)viewWillDisappear:(BOOL)animated {
    [super viewWillDisappear:animated];
    StaticData *sd=[StaticData sd];
    sd.pLoadGCKey=txtMyOldGCName.text;
}
- (BOOL)textFieldShouldReturn:(UITextField *)textField {
    [self HandleTextDone];
    [pModDataView setHidden:YES];
    return NO;
}
- (void)viewWillAppear:(BOOL)animated
{
    [self PossiblyRemoveiAd];
}
/*
 -(BOOL)textFieldShouldReturn:(UITextField*)textField;
 {
 NSInteger nextTag = textField.tag + 1;
 // Try to find next responder
 UIResponder* nextResponder = [textField.superview viewWithTag:nextTag];
 if (nextResponder) {
 // Found next responder, so set it.
 [nextResponder becomeFirstResponder];
 } else {
 // Not found, so remove keyboard.
 [textField resignFirstResponder];
 }
 return NO; // We do not want UITextField to insert line-breaks.
 }
 */


- (void)didReceiveMemoryWarning
{
    // Releases the view if it doesn't have a superview.
    [super didReceiveMemoryWarning];
    
    // Release any cached data, images, etc that aren't in use.
}

#pragma mark - View lifecycle
- (void)viewDidLoad
{
    //self.navigationController.toolbarHidden=YES;
    da = [DataAccess da];
    NSLog(@"merchantIn: %@",merchantIn);
    NSLog(@"myGCNameIn: %@",myGCNameIn);
    //merchantIn=@"5 Guys";
    //myGCNameIn=@"5 Guys";
    [self getScreenConfig];
    
    
    self.navigationController.navigationBarHidden=YES;
    [super viewDidLoad];
    isDirty=@"";
    txtCredPass.secureTextEntry=YES;
    self.title=@"";
    [self doScreenConfig];
    [self loadCurrCard];
    //self.navigationController.navigationBarHidden=YES;
    
    txtGCType.text=merchantIn;
    showAL=[da pmIsManual:merchantIn];
    cmdAutoLookup.enabled=showAL;
    if (showAL==0)
    {
        revShowAL=1;
    }
    else
    {
        revShowAL=0;
    }
    txtGCType.enabled=revShowAL;
    if (showAL == 0)
    {
        if (merchantIn == @"Unlisted - Manually Enter")
        {
            [txtGCType setText:@""];
        }
        //[txtGCType setEnabled:YES];
        dispmode=4;
        [self doScreenConfig];
        [self loadCurrCard];
        //[CJMUtilities ShowAlert:@"Whats this now?" Message:@"What this does is sends the info to our server so I can later build a lookup for it.  This helps us expand with your help." ButtonText:@"Got it"];
    }
    if (myGCNameIn==@"") {
        cmdDelete.enabled=false;
        //cmdDelete.alpha=.5;
    }
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
- (id)init {
    return self;
}
- (id)initWithMerchantName:(NSString *)merchantNameIn myGCName:(NSString *)myGCName
{
    da = [DataAccess da];
    merchantIn=merchantNameIn;
    myGCNameIn=myGCName;
    //[self getScreenConfig];
    return self;
}
- (void)PossiblyRemoveiAd
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
    [cmdAutoLookup setEnabled:YES];
    timer=nil;
    [spinner stopAnimating];
    spinner.hidden=YES;
}
@end
