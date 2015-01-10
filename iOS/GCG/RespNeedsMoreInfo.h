//
//  RespNeedsMoreInfo.h
//  GCG
//
//  Created by Chris on 9/29/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface RespNeedsMoreInfo : UIViewController
{
    IBOutlet UIButton *btnSubmit;
    IBOutlet UIButton *btnTest;
    IBOutlet UITextField *txtAnswer;
    IBOutlet UIActivityIndicatorView *spinner;
}
@property(nonatomic, retain) IBOutlet UITextView *tvQuestion;
@property(nonatomic, retain) NSTimer *timer;
-(id)initWithParamString:(NSString *)pString;
-(void)monitorResp:(NSTimer*)theTimer;
@end
