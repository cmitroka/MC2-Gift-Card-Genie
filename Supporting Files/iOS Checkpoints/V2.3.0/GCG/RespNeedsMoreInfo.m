//
//  RespNeedsMoreInfo.m
//  GCG
//
//  Created by Chris on 9/29/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "RespNeedsMoreInfo.h"
#import "GCGSpecific.h"
#import "CJMUtilities.h"
#import "WebAccess.h"
#import <QuartzCore/QuartzCore.h>
#import "StaticData.h"
#import "DataAccess.h"
static NSString *pFileNameID;
static NSString *pMessage;


@implementation RespNeedsMoreInfo
@synthesize timer, tvQuestion;
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
-(IBAction)DoTest:(id)sender
{
    NSString *retRS = [NSString stringWithFormat:@"%@%@%@%@%@",@"GCNEEDSMOREIXXX",gcgLINEDEL,@"123456789",gcgPIECEDEL,@"ZIP"];
    NSMutableArray *tempArray=[CJMUtilities ConvertNSStringToNSMutableArray:retRS delimiter:gcgLINEDEL];
    NSString *rsType=[tempArray objectAtIndex:0];
    NSLog(rsType);
    
    [GCGSpecific pmHandleResponse:retRS PassNavView:self.navigationController];
    
    if ([rsType isEqualToString:gcgGCNEEDSMORRINFO]||[rsType isEqualToString:gcgGCCAPTCHA])   {}
    else
    {
        StaticData *sd=[StaticData sd];
        UINavigationController *navController = self.navigationController;
        NSMutableArray *controllers = [self.navigationController.viewControllers mutableCopy];
        navController.viewControllers = controllers;
        int imax=sd.pPopAmount;
        for (int i=0; i<imax; i++)
        {
            if (i>0)
            {
                [controllers removeLastObject];
                [navController popViewControllerAnimated:NO];
            }
        }    
        [navController popViewControllerAnimated:YES];
        sd.pPopAmount=0;
    }
    return;    
    
}
-(IBAction)DoContinueRequest:(id)sender
{
    //[CJMUtilities ShowAlert:@"Coming Soon" Message:@"This feature will be available (to some extent)in the next version." ButtonText:@"OK"];
    WebAccess *wa=[[WebAccess alloc]init];
    NSString *rs =@"";
    rs = [wa pmC4ContinueRequest:@"UDID" IDFileName:pFileNameID Answer:txtAnswer.text];
    [spinner startAnimating];    
    timer=[NSTimer scheduledTimerWithTimeInterval:.1 target:self selector:@selector(monitorResp:) userInfo:rs repeats:YES];
    [self TurnButtonOff];
}
-(void)monitorResp:(NSTimer*)theTimer
{
    StaticData *sd=[StaticData sd];
    NSString *retRS = @"";
    NSString *MonitorThis=(NSString*)[theTimer userInfo];
    DataAccess *da=[DataAccess da];
    retRS=[da pmGetSetting:MonitorThis];
    if ([retRS isEqualToString:@""])
    {
    }
    else
    {
        [GCGSpecific pmHandleResponse:retRS PassNavView:self.navigationController];
        [da pmUpdateSetting:@"ContinueRequestResult" svalue:@""];
    }
    if (sd.pGoBack==@"1")
    {
        StaticData *sd=[StaticData sd];
        UINavigationController *navController = self.navigationController;
        NSMutableArray *controllers = [self.navigationController.viewControllers mutableCopy];
        navController.viewControllers = controllers;
        int imax=sd.pPopAmount;
        for (int i=0; i<imax; i++)
        {
            if (i>0)
            {
                [controllers removeLastObject];
                [navController popViewControllerAnimated:NO];
            }
        }    
        [navController popViewControllerAnimated:YES];
        sd.pPopAmount=0;
        sd.pGoBack=@"";
    }
}

-(id)initWithParamString:(NSString *)pString
{
    NSMutableArray *pieceArray=[CJMUtilities ConvertNSStringToNSMutableArray:pString delimiter:gcgPIECEDEL];
    pFileNameID=[pieceArray objectAtIndex:0];
    pMessage=[pieceArray objectAtIndex:1];
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
    [[self.tvQuestion layer] setBorderColor:[[UIColor grayColor] CGColor]];
    [[self.tvQuestion layer] setBorderWidth:2.3];
    [[self.tvQuestion layer] setCornerRadius:15];
    tvQuestion.text = pMessage;
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

@end
