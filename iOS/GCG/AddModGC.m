//
//  AddModGC.m
//  GCG
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
-(void)loadCurrCard;
-(void)MakeGCInfoVisible;
-(void)MakeGCInfoInvisible;
-(void)ShowSaveDel;
@end
@implementation AddModGC
static NSString *isDirty;
static int buttontag;
@synthesize txtGCType, txtGCNum, txtGCPIN, txtMyOldGCName;
@synthesize lblGCNum,lblGCPIN,lblGCType,lblGCInfo,lblSave,lblDelete;
@synthesize cmdSave, cmdDelete;
@synthesize merchantIn,myGCNameIn,currentlySelected;


-(void)loadCurrCard
{
    currCard=[[MyCard alloc] initWithName:myGCNameIn];
    currCard.p_gctype=merchantIn;
    txtGCType.text=currCard.p_gctype;
    txtGCNum.text=currCard.p_gcnum;
    txtGCPIN.text=currCard.p_gcpin;
    txtMyOldGCName.text=currCard.p_mygcname;
    if ([txtGCType.text isEqualToString:@"(null)"]) {txtGCType.text=@"";}
    if ([txtGCNum.text isEqualToString:@"(null)"]) {txtGCNum.text=@"";}
    if ([txtGCPIN.text isEqualToString:@"(null)"]) {txtGCPIN.text=@"";}
}
-(void)loadMerchantInfo
{
    //merchantInfo=[[MerchantInfo alloc] initWithName:myGCNameIn];
    merchantInfo=[[MerchantInfo alloc] initWithName:merchantIn];
    if (merchantInfo.p_maxCardLen==0) merchantInfo.p_maxCardLen=999;
    if (merchantInfo.p_maxPINLen==0) merchantInfo.p_maxPINLen=999;
    if (merchantInfo.p_showCardPIN==TRUE || merchantInfo.p_url.length==0) {
        txtGCPIN.hidden=NO;
        lblGCPIN.hidden=NO;
    }
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
-(void)MakeGCInfoVisible
{
    [lblGCInfo setHidden:FALSE];
    [lblGCType setHidden:FALSE];
    [txtGCType setHidden:FALSE];
    [lblGCNum setHidden:FALSE];
    [txtGCNum setHidden:FALSE];
    if (merchantInfo.p_showCardPIN==TRUE) {
        txtGCPIN.hidden=NO;
        lblGCPIN.hidden=NO;
    }
}

-(void)ShowSaveDel
{
    [cmdSave setHidden:NO];
    [cmdDelete setHidden:NO];
    [lblSave setHidden:NO];
    [lblDelete setHidden:NO];
}

-(void)Save
{
    NSString *p_isValid=[self IsSelectionVaild];
    if (p_isValid.length>1) {
        [CJMUtilities ShowAlert:@"Cannot Save; either the card and/or PIN has an issue." Message:@"Can't Save" ButtonText:@"OK"];
        return;
    }
    
    
    NSString *p_mygcname=[NSString stringWithFormat:@"%@-%@", txtGCType.text, txtGCNum.text];
    int test=[da pmDoesMyCardExist:p_mygcname];
    if (test==1)  //It's an update in this case
    {
        txtMyOldGCName.text=p_mygcname;
    }
    NSString *temp=txtMyOldGCName.text;
    int retStatus=[da pmUpdateMyCard:txtGCType.text gcnum:txtGCNum.text gcpin:txtGCPIN.text credlogin:@"" credpass:@"" mygcname:p_mygcname myoldgcname:txtMyOldGCName.text];
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
        isDirty=@"SKIP";
    } 
}
-(int)pmLoadByMyCard:(MyCard *)mygc
{
    currCard=mygc;
    return 1;
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
            //TVCAppDelegate *appDelegate = (TVCAppDelegate *)[[UIApplication sharedApplication] delegate];
            //[appDelegate useNavController:[MyGCs class]];
            NSMutableArray *allViewControllers = [NSMutableArray arrayWithArray:[self.navigationController viewControllers]];
            for (UIViewController *aViewController in allViewControllers) {
                if ([aViewController isKindOfClass:[MyGCs class]]) {
                    [self.navigationController popToViewController:aViewController animated:NO];
                }
            }
        }
        if (retVal!=1) {
            //[CJMUtilities ShowAlert:@"Oops" Message:@"Couldn't Delete" ButtonText:@"Aw Snap"];
        }
        else
        {
            txtMyOldGCName.text=@"";
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

}
-(IBAction)DoDelete:(id)sender
{
    UIAlertView *av=[[UIAlertView alloc] initWithTitle:@"Confirm" message:[NSString stringWithFormat:@"%@%@",@"Are you sure you want to ",@"delete this?"] delegate:self cancelButtonTitle:@"No, My Mistake" otherButtonTitles:@"Yeah, I'm Sure", nil];
    [av show];
    buttontag=2;

}
-(void)DoBack
{
    if (![isDirty isEqualToString:@"SKIP"]) {
        [self isItDirty];
        if (isDirty==@"Y")
        {
            [CJMUtilities ShowAlert:@"Before you go..." Message:@"Make sure you save first, otherwise tap this button again to go back." ButtonText:@"OK"];
            isDirty=@"SKIP";
            return;
        }
    }
    [self.navigationController popViewControllerAnimated:YES];
}

-(void)isItDirty
{
    isDirty=@"";
    NSString *gctype=txtGCType.text;
    NSString *gcpin=txtGCPIN.text;
    NSString *gcnum=txtGCNum.text;
    
    if (currCard.p_gctype==nil) {currCard.p_gctype=@"";}
    if ([currCard.p_gctype isEqualToString:@"Unlisted - Manually Enter"]) {currCard.p_gctype=@"";}
    if (![currCard.p_gctype isEqualToString:gctype]) {isDirty=@"Y";}
    if (currCard.p_gcpin==nil) {currCard.p_gcpin=@"";}
    if (![currCard.p_gcpin isEqualToString:gcpin]) {isDirty=@"Y";}
    if (currCard.p_gcnum==nil) {currCard.p_gcnum=@"";}
    if (![currCard.p_gcnum isEqualToString:gcnum]) {isDirty=@"Y";}
}
-(void)MakeFocusView:(UITextField *)textField
{
    currentlySelected=@"";
    [pModDataText setText:textField.text];
    [pModDataLabel setText:textField.placeholder];
    if ([pModDataLabel.text isEqualToString:@"Card Vendor"]) {
        pModDataText.keyboardType=UIKeyboardTypeDefault;}
        currentlySelected=@"VendorSelected";
    if ([pModDataLabel.text isEqualToString:@"Card Number"])
    {
        pModDataText.keyboardType=UIKeyboardTypeNumberPad;
        currentlySelected=@"NumberSelected";
    }
    if ([pModDataLabel.text isEqualToString:@"Card PIN"])
    {
        pModDataText.keyboardType=UIKeyboardTypeNumberPad;
        currentlySelected=@"PINSelected";
    }
    [pModDataView setHidden:NO];
    [pModDataText becomeFirstResponder];
    //[self HandleTextDone];    
}
-(IBAction)DoDismiss:(id)sender
{
    NSString* OK=@"";
    OK=[self IsSelectionVaild];
    if ([currentlySelected isEqualToString:@"VendorSelected"]) {txtGCType.text=pModDataText.text;}
    if ([currentlySelected isEqualToString:@"PINSelected"]) {txtGCPIN.text=pModDataText.text;}
    if ([currentlySelected isEqualToString:@"NumberSelected"]) {txtGCNum.text=pModDataText.text;}
    if (OK==@"")
    {
        [pModDataView setHidden:YES];
        [self.view endEditing:YES];
        isDirty=@"";
    }
    else
    {
        [CJMUtilities ShowAlert:@"Cannot Save" Message:OK ButtonText:@"OK"];
        isDirty=@"SKIP";
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
    else if ([currentlySelected isEqualToString:@"PINSelected"]) {
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
    else if ([currentlySelected isEqualToString:@"NumberSelected"]) {
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
        [self MakeGCInfoInvisible];
    }
    else if (textField.tag==0) {
    }
    [self MakeFocusView:textField];
    return NO;
}
- (void)viewWillDisappear:(BOOL)animated {
    [super viewWillDisappear:animated];
    StaticData *sd=[StaticData sd];
    sd.pLoadGCKey=txtMyOldGCName.text;
}
- (BOOL)textFieldShouldReturn:(UITextField *)textField {
    [pModDataView setHidden:YES];
    return NO;
}
- (void)viewWillAppear:(BOOL)animated
{
}


- (void)didReceiveMemoryWarning
{
    // Releases the view if it doesn't have a superview.
    [super didReceiveMemoryWarning];
    
    // Release any cached data, images, etc that aren't in use.
}

- (void)goBack:(id)sender {
    [self.navigationController popViewControllerAnimated:YES];
}


#pragma mark - View lifecycle


- (void)viewDidLoad
{
    da = [DataAccess da];
    NSLog(@"merchantIn: %@",merchantIn);
    NSLog(@"myGCNameIn: %@",myGCNameIn);
    [self loadMerchantInfo];
    [self loadCurrCard];
    isDirty=@"";
    UIBarButtonItem *backButton = [[UIBarButtonItem alloc] initWithTitle:@"Back" style:UIBarButtonItemStylePlain target:self action:@selector(DoBack)];
    self.navigationItem.leftBarButtonItem = backButton;
    
    txtGCType.text=merchantIn;
    if (merchantInfo.p_url.length<=0) {
        txtGCType.enabled=YES;
        if (merchantIn == @"Unlisted - Manually Enter")
        {
            txtGCType.text=@"";
        }

    }
    else{
        txtGCType.enabled=NO;
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
@end
