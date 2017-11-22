//
//  Feedback.h
//  GCG
//
//  Created by Chris on 6/12/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface Feedback : UIViewController
{
    IBOutlet UIButton *hideFeedbackButton;
    IBOutlet UITextView *tvFeedback;
    IBOutlet UITextField *tfName;
    IBOutlet UITextField *tfContactInfo;    
}
@end
