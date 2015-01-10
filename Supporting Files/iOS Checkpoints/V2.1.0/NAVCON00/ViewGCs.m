//
//  ViewGCs.m
//  NAVCON01
//
//  Created by Chris on 7/12/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "ViewGCs.h"
#import "DataAccess.h"
#import "CJMUtilities.h"
#import "AddModGC.h"
NSMutableArray *dataSource;
NSMutableArray *stateIndex;
NSMutableArray *PosIndex;
int TotCnt;
int SetOnce;

@implementation ViewGCs
- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        self.title = NSLocalizedString(@"All Merchants", @"All Merchants");
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
    
    stateIndex = [[NSMutableArray alloc] init];
    PosIndex = [[NSMutableArray alloc] init];
    TotCnt=0;
    SetOnce=0;
    DataAccess *da=[DataAccess da];
    dataSource=[da pmGetMerchantsAll];
    int test=dataSource.count;
    NSString *testamnt=[CJMUtilities ConvertIntToNSString:test];
    NSLog(testamnt,NULL);

    for (int i=0; i<[dataSource count]; i++){
        //---get the first char of each state---
        char alphabet = [[dataSource objectAtIndex:i] characterAtIndex:0];
        NSString *uniChar = [NSString stringWithFormat:@"%C", alphabet];
        
        //---add each letter to the index array---
        if (![stateIndex containsObject:uniChar])
        {            
            [stateIndex addObject:uniChar];
        }        
    }
    
    self.navigationItem.title = @"All Merchants";   
    [super viewDidLoad];
    //[self.navigationController.view addSubview:toolbar];
    // Uncomment the following line to preserve selection between presentations.
    // self.clearsSelectionOnViewWillAppear = NO;
    
    // Uncomment the following line to display an Edit button in the navigation bar for this view controller.
    // self.navigationItem.rightBarButtonItem = self.editButtonItem;
}

- (void)viewDidUnload
{
    [super viewDidUnload];
    // Release any retained subviews of the main view.
    // e.g. self.myOutlet = nil;
}

- (void)viewWillAppear:(BOOL)animated
{
    [super viewWillAppear:animated];
    self.navigationController.navigationBarHidden=NO;
}

- (void)viewDidAppear:(BOOL)animated
{
    [super viewDidAppear:animated];
}

- (void)viewWillDisappear:(BOOL)animated
{
    [super viewWillDisappear:animated];
}

- (void)viewDidDisappear:(BOOL)animated
{
    [super viewDidDisappear:animated];
}

- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)interfaceOrientation
{
    // Return YES for supported orientations
    return (interfaceOrientation == UIInterfaceOrientationPortrait);
}

#pragma mark - Table view data source

/*
- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section
{
    DataAccess *da=[DataAccess da];
    dataSource=[da pmGetMerchantsAll];
    return dataSource.count+1;
    
}
*/

#pragma mark - Table view delegate
- (NSInteger)numberOfSectionsInTableView:(UITableView *)tableView {
    
    return [stateIndex count];
}
- (NSString *)tableView:(UITableView *)tableView titleForHeaderInSection:(NSInteger)section {
    
    return [stateIndex objectAtIndex:section];
    
}
- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
    
    //---get the letter in each section; e.g., A, B, C, etc.---
    NSString *alphabet = [stateIndex objectAtIndex:section];
    
    //---get all states beginning with the letter---
    NSPredicate *predicate = 
    [NSPredicate predicateWithFormat:@"SELF beginswith[c] %@", alphabet];
    NSArray *states = [dataSource filteredArrayUsingPredicate:predicate];
    
    //---return the number of states beginning with the letter---
    TotCnt=TotCnt+states.count;
    NSString *temp=[CJMUtilities ConvertIntToNSString:TotCnt];
    [PosIndex addObject:temp];
    return [states count];    
}
- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {

    static NSString *CellIdentifier = @"Cell";
    
    UITableViewCell *cell = 
    [tableView dequeueReusableCellWithIdentifier:CellIdentifier];
    if (cell == nil) {
        cell = [[UITableViewCell alloc] 
                 initWithStyle:UITableViewCellStyleDefault 
                 reuseIdentifier:CellIdentifier];
    }
    
    //---get the letter in the current section---
    NSString *alphabet = [stateIndex objectAtIndex:[indexPath section]];
    
    //---get all states beginning with the letter---
    NSPredicate *predicate = 
    [NSPredicate predicateWithFormat:@"SELF beginswith[c] %@", alphabet];
    NSArray *states = [dataSource filteredArrayUsingPredicate:predicate];
    if ([states count]>0) {
        //---extract the relevant state from the states object---
        NSString *cellValue = [states objectAtIndex:indexPath.row];
        if (SetOnce==0) 
        {
            SetOnce=1;
            cellValue=@"Unlisted - Manually Enter";
        }
        cell.textLabel.text = cellValue;        
        //cell.accessoryType = UITableViewCellAccessoryDetailDisclosureButton;
    }
    return cell;
}
- (NSArray *)sectionIndexTitlesForTableView:(UITableView *)tableView {
    return stateIndex;
}
- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath
{
    int x=indexPath.row;
    int y=indexPath.section;
    NSString *merchName=@"";
    NSString *temp0=[PosIndex objectAtIndex:0];
    NSString *startpos=[PosIndex objectAtIndex:y];
    if (y>0) {
        temp0=[PosIndex objectAtIndex:y-1];
    }
    int tempint=[CJMUtilities ConvertNSStringToInt:startpos];
    tempint--;
    int trueposition=tempint+x;
    if (indexPath.section==0)
    {
        merchName = @"Unlisted - Manually Enter";
    }
    else
    {
        merchName = [dataSource objectAtIndex:trueposition];
    }
    AddModGC *addModGC=[[AddModGC alloc] initWithMerchantName:merchName myGCName:@""];
    [self.navigationController pushViewController:addModGC animated:YES];
}
@end
