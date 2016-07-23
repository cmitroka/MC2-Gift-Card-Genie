//
//  MyGCs.m
//  GCG
//
//  Created by Chris on 9/18/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "MyGCs.h"
#import "DataAccess.h"
#import "GiftCard.h"
#import "AddModGC.h"
#import "LoadGC.h"
#import "StaticData.h"
#import "MyGCwDetails.h"
#import "Feedback.h"
#import "Settings.h"
#import "ViewGCs.h"
#import "TVCAppDelegate.h"
#import "OutOfLookups.h"
#import "CJMUtilities.h"
DataAccess *sql;
@implementation MyGCs
@synthesize toolbar,pSettings,pMyGCs,cmdLeft,cmdRight,cmdMid,tableView;
StaticData *sd;

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        Settings *z = [[Settings alloc] init];
        self.pSettings = z;

//        self.title = NSLocalizedString(@"My Cards", @"My Cards");
    }
    sql=[DataAccess da];
    /*
    NSMutableArray *temp=[sql pmGetMyCards];
    int itemp=temp.count;
    itemp=itemp-1;
    */
    return self;
}

-(IBAction)DoViewGCs:(id)sender
{
    ViewGCs *pViewGCs=[[ViewGCs alloc] init];
    [self.navigationController pushViewController:pViewGCs animated:YES];
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
    [self.navigationController.view addSubview:toolbar];
    [UIView animateWithDuration:UINavigationControllerHideShowBarDuration animations:^{
        [self setNeedsStatusBarAppearanceUpdate];
    }];
    /*
    StaticData *sd=[StaticData sd];
    if (sd.pDrawn!=@"1") {
        [self.navigationController.view addSubview:toolbar];
    }
    sd.pDrawn=@"1";
    */
}

- (void)viewDidUnload
{
    [super viewDidUnload];
}

- (void)viewWillAppear:(BOOL)animated
{
    [super viewWillAppear:animated];
    self.navigationController.navigationBar.hidden = NO;
}


- (void)viewDidAppear:(BOOL)animated
{
    [super viewDidAppear:animated];
    [self.navigationController.view addSubview:toolbar];
    [tableView reloadData];
    StaticData *sd=[StaticData sd];
    if (sd.pAlertMessage.length>0) {
        UIAlertView *errorAlert = [[UIAlertView alloc]
                                   initWithTitle:@"Info"
                                   message:sd.pAlertMessage
                                   delegate:self
                                   cancelButtonTitle:@"OK"
                                   otherButtonTitles:nil];
        [errorAlert show];
        sd.pAlertMessage=@"";
    }
    NSLog(@"%@",@"viewDidAppear");

    //[self.navigationController setNavigationBarHidden:NO];
    //self.navigationController.navigationBarHidden=YES;
    //self.title = NSLocalizedString(@"My Cards", @"My Cards");

}

- (void)viewWillDisappear:(BOOL)animated
{
    [super viewWillDisappear:animated];
}

- (void)viewDidDisappear:(BOOL)animated
{
    [super viewDidDisappear:animated];
}
/*
- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)interfaceOrientation
{
    // Return YES for supported orientations
    return (interfaceOrientation == UIInterfaceOrientationPortrait);
}
*/
#pragma mark - Table view data source


- (NSInteger)numberOfSectionsInTableView:(UITableView *)tableView
{
    // Return the number of sections.
    return 1;
}

- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section
{
    //DataAccess *da=[DataAccess da];
    NSMutableArray *temp=[sql pmGetMyCards];
    int retMe=temp.count;
    if (retMe==0) {
        [NoCardsView setHidden:NO];
    }
    else
    {
        [NoCardsView setHidden:YES];        
    }
    return retMe;
    //    x = [sql ppMyCards]
    //    NSLog(@"A");
    //    NSLog(@"B");
    //    NSLog(@"C");
    //    return 0; //s.favoriteMerchants.count;
}


- (UITableViewCell *)tableView:(UITableView *)ptableView cellForRowAtIndexPath:(NSIndexPath *)indexPath
{
    //MyGCwDetails *cell = [[MyGCwDetails alloc] init];
    NSString *it=@"MyGCwDetailsCellID";
    MyGCwDetails *cell = [tableView dequeueReusableCellWithIdentifier:it];
    if (cell == nil){
        NSLog(@"New Cell Made");
        
        NSArray *topLevelObjects = [[NSBundle mainBundle] loadNibNamed:@"MyGCwDetailsCell" owner:nil options:nil];
        
        for(id currentObject in topLevelObjects)
        {
            if([currentObject isKindOfClass:[MyGCwDetails class]])
            {
                cell = (MyGCwDetails *)currentObject;
                break;
            }
        }
    }
    [[cell GCMerch] setText:@"Test"];
    /*
    MyGCwDetails *cell = [ptableView dequeueReusableCellWithIdentifier:@"MyGCwDetailsCellID"];
    if (cell == nil) {
        cell = (MyGCwDetails *)[[UITableViewCell alloc] initWithStyle:UITableViewCellStyleSubtitle reuseIdentifier:@"MyGCwDetailsCellID"];
    }
    */
    MyCard *myCard=[sql.myCards objectAtIndex:indexPath.row];
    //cell.GCMerch = myCard.p_gctype;
    [[cell GCMerch] setText:myCard.p_gctype]; //myCard.p_mygcname
    cell.GCNumb.text=myCard.p_gcnum;
    
    if (myCard.p_lastknownbal.length==0)
    {
        cell.GCBal.text=@"$???";
    }
    else
    {
        cell.GCBal.text=myCard.p_lastknownbal;        
    }

    if (myCard.p_lastbaldate.length==0)
    {
        cell.GCDate.text=@"N/A";

    }
    else
    {
        cell.GCDate.text=myCard.p_lastbaldate;
    }

    
    //cell.detailTextLabel.text = myCard.p_gcnum;
	NSArray *documentPaths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
	NSString *documentsDir = [documentPaths objectAtIndex:0];
	NSString *path = [documentsDir stringByAppendingPathComponent:@"icon.png"];
    UIImage *theImage = [UIImage imageWithContentsOfFile:path];
    cell.imageView.image = theImage;
    return cell;
    
    
}


#pragma mark - Table view delegate

- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath
{
    int iAmntOfLookupsRemaining=[CJMUtilities ConvertNSStringToInt:sd.pAmntOfLookupsRemaining];
    if (iAmntOfLookupsRemaining<=0) {
        //OutOfLookups *v = [[OutOfLookups alloc] init];
        //[self.navigationController pushViewController:v animated:YES];
    }
        MyCard *myCard = [sql.myCards objectAtIndex:indexPath.row];
        sd.pLoadGCKey=myCard.p_mygcname;
        LoadGC *pLoadGC=[[LoadGC alloc] init];
        [toolbar removeFromSuperview];
        [self.navigationController pushViewController:pLoadGC animated:YES];
}

@end
