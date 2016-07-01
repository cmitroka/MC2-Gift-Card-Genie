//
//  ViewGCs.m
//  GCG
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
        //self.title = NSLocalizedString(@"All Merchants", @"All Merchants");
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
    //self.navigationItem.title = @"All Merchants";
    [super viewDidLoad];
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
}

- (void)viewWillDisappear:(BOOL)animated
{
    [super viewWillDisappear:animated];
}

- (void)viewDidDisappear:(BOOL)animated
{
    [super viewDidDisappear:animated];
}



- (NSInteger)numberOfSectionsInTableView:(UITableView *)tableView
{
    return 1;    //count of section
}


- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
    
    return dataSource.count;    //count number of row from counting array hear cataGorry is An
}



- (UITableViewCell *)tableView:(UITableView *)tableView
         cellForRowAtIndexPath:(NSIndexPath *)indexPath
{
    static NSString *MyIdentifier = @"MyIdentifier";
    static NSString *cellValue = @"Merchant Name";
    UITableViewCell *cell = [tableView dequeueReusableCellWithIdentifier:MyIdentifier];
    if (cell == nil)
    {
        cell = [[UITableViewCell alloc] initWithStyle:UITableViewCellStyleDefault
                                       reuseIdentifier:MyIdentifier];
    }
    if (indexPath.row>=dataSource.count) {
        return cell;
    }
    cellValue = [dataSource objectAtIndex:indexPath.row];
    cell.textLabel.text = cellValue;
    
    //[cell.imageView setImageWithURL:[NSURL URLWithString:@"http://example.com/image.jpg"]
    //               placeholderImage:[UIImage imageNamed:@"placeholder"]];
    return cell;
}

- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath
{
    NSString *merchName=@"";
    merchName = [dataSource objectAtIndex:indexPath.item];
    AddModGC *addModGC=[[AddModGC alloc] initWithMerchantName:merchName myGCName:@""];
    [self.navigationController pushViewController:addModGC animated:YES];
}
@end
