//
//  MyGCwDetails.h
//  NAVCON01
//
//  Created by Chris on 12/1/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface MyGCwDetails : UITableViewCell
{
    IBOutlet UILabel *GCMerch;
    IBOutlet UILabel *GCNumb;
    IBOutlet UILabel *GCBal;
    IBOutlet UILabel *GCDate;    
    IBOutlet UIImageView *GCImage;
    IBOutlet UIView *viewForBackground;
}
@property(nonatomic, retain) IBOutlet UILabel *GCMerch;
@property(nonatomic, retain) IBOutlet UILabel *GCNumb;
@property(nonatomic, retain) IBOutlet UILabel *GCBal;
@property(nonatomic, retain) IBOutlet UILabel *GCDate;    
@property(nonatomic, retain) IBOutlet UIImageView *GCImage;
@property(nonatomic, retain) IBOutlet UIView *viewForBackground;
@end
