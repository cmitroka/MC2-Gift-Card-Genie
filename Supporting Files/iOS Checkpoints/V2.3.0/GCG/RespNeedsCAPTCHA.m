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
    rs = [wa pmC4ContinueRequest:@"UDID" IDFileName:pRespID Answer:txtAnswer.text];
    [spinner startAnimating];
    timer=[NSTimer scheduledTimerWithTimeInterval:.1 target:self selector:@selector(monitorResp:) userInfo:rs repeats:YES];
    [btnSubmit setEnabled:NO];
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
        [GCGSpecific pmHandleResponse:retRS PassNavView:self.navigationController];
        UINavigationController *navController = self.navigationController;
        NSMutableArray *controllers = [self.navigationController.viewControllers mutableCopy];
        //[controllers removeLastObject];
        navController.viewControllers = controllers;
        [navController popViewControllerAnimated:YES];

    }
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

@end
