//
//  SplashScreen00.m
//  GCG
//
//  Created by Chris Mitroka on 3/3/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//
#import <Storekit/StoreKit.h>

#import "SplashScreen00.h"
#import "DataAccess.h"
#import "WebAccess.h"
#import "StaticData.h"
#import "CJMUtilities.h"
#import "TVCAppDelegate.h"
#import "TVCMasterViewController.h"
#import "GCGSpecific.h"
#import "SFHFKeychainUtils.h"
#import "IAP.h"
#import "MyGCs.h"
#import "SwitchViewController.h"
#import "WatchIAd.h"

@interface SplashScreen00()
-(void)changeScreens01;
@end

@implementation SplashScreen00
StaticData *sd;
@synthesize progressbar,timer,lblVersion;

NSString *allMerchantCount;
int allMerchantCountInt;
NSString *allMerchantData;

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
    progressbar.progress=0;
    timer=[NSTimer scheduledTimerWithTimeInterval:.001 target:self selector:@selector(doInit) userInfo:nil repeats:NO];
    return;
}
- (void)viewWillAppear:(BOOL)animated
{
    [super viewWillAppear:animated];
    sd=[StaticData sd];
    [lblVersion setText:sd.pVersion];

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

-(void)processData:(NSString *)processData maxRecs:(int)maxRecs;
{
    DataAccess *da=[DataAccess da];
    NSString *merchant, *url, *showCardNum, *showPIN, *minCardLen, *maxCardLen, *minPINLen, *maxPINLen, *isLookupManual, *notes;
    
    //CleanName-URLToUse-showCardNum-showCardPIN-TrueCardNumMin-CardNumMax-TruePINMin-PINMax-IsLookupManual-GeneralNote
    
    int zIntCurrentCount=0;
    float zFltProgress=0;
    NSMutableArray *pieceArray = [[NSMutableArray alloc] initWithObjects:nil];
    pieceArray=[CJMUtilities ConvertNSStringToNSMutableArray:processData delimiter:gcgLINEDEL];
    for (NSString *tempx in pieceArray) 
    {
        NSMutableArray *temp=[CJMUtilities ConvertNSStringToNSMutableArray:tempx delimiter:gcgPIECEDEL];
        if (temp.count<7)break;
        merchant=[temp objectAtIndex:0];
        url=[temp objectAtIndex:1];
        showCardNum=[temp objectAtIndex:2];
        showPIN=[temp objectAtIndex:3];
        minCardLen=[temp objectAtIndex:4];
        int iminCardLen=[CJMUtilities ConvertNSStringToInt:minCardLen];
        maxCardLen=[temp objectAtIndex:5];
        int imaxCardLen=[CJMUtilities ConvertNSStringToInt:maxCardLen];
        minPINLen=[temp objectAtIndex:6];
        int iminPINLen=[CJMUtilities ConvertNSStringToInt:minPINLen];
        maxPINLen=[temp objectAtIndex:7];
        int imaxPINLen=[CJMUtilities ConvertNSStringToInt:maxPINLen];
        isLookupManual=[temp objectAtIndex:8];
        notes=[temp objectAtIndex:9];
        [da pmInsertMerchant:merchant url:url showCardNum:showCardNum showCardPIN:showPIN minCardLen:iminCardLen maxCardLen:iminCardLen minPINLen:iminPINLen maxPINLen:imaxPINLen isLookupManual:isLookupManual note:notes];
        zIntCurrentCount++;
        float m_amnt_remaining=maxRecs-zIntCurrentCount;
        float test1=m_amnt_remaining/maxRecs;
        zFltProgress=1-test1;
        progressbar.progress=zFltProgress;
        [[NSRunLoop currentRunLoop] runUntilDate:[NSDate date]];
    }
    [self changeScreens01];
}
-(void)doLoad
{
    DataAccess *da=[DataAccess da];    
    BOOL doUpdate=false;
    NSString *LastUpdatedInAppStr=[da pmGetSetting:@"LastUpdate"];
    NSString *LastUpdatedOnServerStr=sd.pLastDBUpdate;
    //LastUpdatedInAppStr=@"2010-10-10";
    NSDate *LastUpdatedInApp=[CJMUtilities ConvertNSStringDateToNSDate:LastUpdatedInAppStr];
    NSDate *LastUpdatedOnServer=[CJMUtilities ConvertNSStringDateToNSDate:LastUpdatedOnServerStr];
    if (LastUpdatedInAppStr==@"UNDEFINED") {
        doUpdate=true;
    } else {
        BOOL test=[CJMUtilities NSDate1GreaterThanNSDate2:LastUpdatedOnServer date2:LastUpdatedInApp];
        if (test==true) {
            doUpdate=true;
        } else {
            doUpdate=false;
        }
    }
    if (LastUpdatedOnServerStr.length==0) {
    } else {
        int OK=[da pmUpdateSetting:@"LastUpdate" svalue:sd.pLastDBUpdate];
    }
    int zIntMerchantCount=999;
    NSString *rs=@"";
    //doUpdate=true;        //to force an updateß
    if (doUpdate==NO) {        
            [self changeScreens01];
            return;            
    }
    WebAccess *wa=[[WebAccess alloc]init];
    zIntMerchantCount=wa.pmGetMerchantCount;
    allMerchantCount=[CJMUtilities ConvertIntToNSString:zIntMerchantCount];
    allMerchantCountInt=zIntMerchantCount;
    rs=wa.pmDownloadAllData;
    if ((rs==@"")||(zIntMerchantCount==0))
    {
        [self changeScreens01];
    }
    else
    {
        [da pmDeleteMerchants];
        [da pmUpdateSetting:@"LastUpdate" svalue:LastUpdatedOnServerStr];
        [self processData:rs maxRecs:zIntMerchantCount];
    }
}

-(void)changeScreens01
{
        [timer invalidate];
        timer=nil;
        int pAmntOfLookupsRemaining=[CJMUtilities ConvertNSStringToInt:sd.pAmntOfLookupsRemaining];

        TVCAppDelegate *appDelegate = (TVCAppDelegate *)[[UIApplication sharedApplication] delegate];
        if (pAmntOfLookupsRemaining<=1)
        {
            sd.pDemoAcknowledged=@"FromSplash";
            [appDelegate useNavController:[IAP class]];
            return;
        }
    else
    {
        [appDelegate useNavController:[TVCMasterViewController class]];
    }
}


-(void)changeScreens
{
    [timer invalidate];
    timer=nil;
    TVCAppDelegate *appDelegate = (TVCAppDelegate *)[[UIApplication sharedApplication] delegate];
    [appDelegate useNavController:[TVCMasterViewController class]];
    //[appDelegate useNavController:[MyGCs class]];
    //[appDelegate useViewController:[SwitchViewController class]];
}
-(void)doInit
{
    sd.pAdUnitID=@"ca-app-pub-2250341510214691/3397200761";
    WebAccess *wa=[[WebAccess alloc]init];
    BOOL OK=[wa pmIsConnectedToInternet];
    if (OK==NO)
    {
        sd.pMode=@"Offline";
        sd.pAlertMessage=@"You dont seem to be online right now. You'll still be able to use the app, but functionality will be limited.";
        sd.pAmntOfLookupsRemaining=@"-1";
        [self changeScreens];
        //byp - remove line below
        //timer=[NSTimer scheduledTimerWithTimeInterval:.001 target:self selector:@selector(doLoad) userInfo:nil repeats:NO];                            
        return;
    }
    NSString *uuid=nil;
    NSString *boolAlwaysUpdate=[SFHFKeychainUtils pmGetValueForSetting:@"AlwaysUpdate"];
    DataAccess *da = [DataAccess da];
    if (boolAlwaysUpdate.boolValue==TRUE)
    {
        [da pmUpdateSetting:@"LastUpdate" svalue:@"1900-01-01"];        
    }
    uuid = [SFHFKeychainUtils pmGetValueForSetting:@"UUID"];
    NSString *appStatus=nil;
    appStatus=[SFHFKeychainUtils pmGetValueForSetting:@"AppStatus"];
    //[SFHFKeychainUtils pmDeleteSetting:@"AppStatus"];
    NSLog(appStatus, NULL);
    if (uuid.length<5)
    {
        uuid=[CJMUtilities GetGeneratedUUID];
    }
    OK=[SFHFKeychainUtils pmUpdateSettingName:@"UUID" SettingValue:uuid];
    sd.pUUID=uuid;
    //sd.pUUID=@"GCG";  //TEST
    //sd.pUUID=@"703417D7-BE51-4739-A935-5794550EE5D8";
    NSString *retRS=@"";
    retRS=wa.pmDoAppStartup;
    retRS=retRS;
    NSMutableArray *temp=[CJMUtilities ConvertNSStringToNSMutableArray:retRS delimiter:gcgPIECEDEL];
    if (temp.count>0)
    {
        sd.pCAPTCHAURLInfo=[temp objectAtIndex:0];
        sd.pSystemMessage=[temp objectAtIndex:1];
        sd.pAmntOfLookupsRemaining=[temp objectAtIndex:2];
        //sd.pAmntOfLookupsRemaining=@"0";

        NSLog(sd.pAmntOfLookupsRemaining, NULL);
        //sd.pAmntOfLookupsRemaining=@"1";
        sd.pLastDBUpdate=[temp objectAtIndex:3];
        NSLog(sd.pLastDBUpdate, NULL);
        sd.pAmntForADollar=[temp objectAtIndex:4];
        NSLog(sd.pAmntForADollar, NULL);        
        sd.pCostForApp=[temp objectAtIndex:5];
        NSLog(sd.pCostForApp, NULL);        
        if (sd.pAmntOfLookupsRemaining.integerValue > 5)
        {
            [SFHFKeychainUtils pmUpdateSettingName:@"AppStatus" SettingValue:@"Purchased"];
        }
        else
        {
            [SFHFKeychainUtils pmUpdateSettingName:@"AppStatus" SettingValue:@"NotPurchased"];
        }
        
        //[SFHFKeychainUtils pmDeleteSetting:@"AppStatus"];
        appStatus=[SFHFKeychainUtils pmGetValueForSetting:@"AppStatus"];
        NSLog(appStatus, NULL);
        if (sd.pSystemMessage.length > 0)
        {
            sd.pAlertMessage=sd.pSystemMessage;
        }        
    }
    else
    {
        sd.pMode=@"Offline";
        sd.pAlertMessage=@"GCG is having trouble communicating with the server. You'll still be able to use the app, but functionality will be limited.";
        sd.pAmntOfLookupsRemaining=@"-1";
        [self changeScreens];
        return;

    }
    timer=[NSTimer scheduledTimerWithTimeInterval:.001 target:self selector:@selector(doLoad) userInfo:nil repeats:NO];                    
}
- (void)alertView:(UIAlertView *)alertView didDismissWithButtonIndex:(NSInteger)buttonIndex {
    int testVal=buttonIndex;
    if(testVal==0) 
    {
        //exit(0);
    }
}
/*
 -(BOOL)isHostAvailable
 {
 //return NO; // force for offline testing
 Reachability *hostReach = [Reachability reachabilityForInternetConnection];
 NetworkStatus netStatus = [hostReach currentReachabilityStatus];
 return !(netStatus == NotReachable);
 }
 */

@end
