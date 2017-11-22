//
//  RespNeedsCAPTCHA.h
//  GCG
//
//  Created by Chris on 9/29/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface RespNeedsCAPTCHA : UIViewController
{
    IBOutlet UIImageView *CAPTCHAImage;
    IBOutlet UITextField *txtAnswer;
    IBOutlet UIActivityIndicatorView *spinner;
    IBOutlet UIButton *btnSubmit;
}
@property(nonatomic, retain) NSTimer *timer;
-(id)initWithParamString:(NSString *)pString;
@end
