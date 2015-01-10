//
//  Photo.h
//  NAVCON01
//
//  Created by Chris on 12/10/13.
//  Copyright (c) 2013 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface Photo : UIViewController<UIImagePickerControllerDelegate>
{
    IBOutlet UIImageView *uiImgV1;
    UIImageView *uiImgV2;
    IBOutlet UIActivityIndicatorView *spinner;
}
@property(nonatomic, retain) IBOutlet UIProgressView *progressbar;
@property(nonatomic, retain) IBOutlet UILabel *lblVersion;
@property(nonatomic, retain) NSTimer *timer;
@property(nonatomic, retain) UIImage *uiImgP;
@property(nonatomic, retain) UIImageView *uiImgP1;
@property(nonatomic, retain) UIImageView *uiImgP2;
-(void)processData:(NSString *)processData maxRecs:(int)maxRecs;
-(void)changeScreens;

@end
