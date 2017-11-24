//
//  MyGCwDetails.m
//  GCG
//
//  Created by Chris on 12/1/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "MyGCwDetails.h"
@implementation MyGCwDetails
@synthesize GCBal, GCDate,GCNumb,GCImage,GCMerch,viewForBackground;
- (id)initWithStyle:(UITableViewCellStyle)style reuseIdentifier:(NSString *)reuseIdentifier
{
    self = [super initWithStyle:style reuseIdentifier:reuseIdentifier];
    if (self) {
        // Initialization code
    }
    return self;
}

- (void)setSelected:(BOOL)selected animated:(BOOL)animated
{
    [super setSelected:selected animated:animated];

    // Configure the view for the selected state
}

@end
