//
//  Photo.m
//  NAVCON01
//
//  Created by Chris on 12/10/13.
//  Copyright (c) 2013 __MyCompanyName__. All rights reserved.
//

#import "Photo.h"
#import "TVCAppDelegate.h"

@implementation Photo
@synthesize timer, uiImgP1;

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization
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
    [super viewDidLoad];
    
        timer=[NSTimer scheduledTimerWithTimeInterval:.001 target:self selector:@selector(doLoad) userInfo:nil repeats:NO];
    
}
- (void)imagePickerController:(UIImagePickerController *)picker didFinishPickingMediaWithInfo:(NSDictionary *)info {
            NSLog(@"imagePickerController", NULL);
    UIImage *img = [info objectForKey:UIImagePickerControllerEditedImage];
    if(!img) img = [info objectForKey:UIImagePickerControllerOriginalImage];
    [uiImgV1 setImage:img];
    [self dismissModalViewControllerAnimated:YES];   
    //put code for store image
}         
- (void)doLoad
{
    UIImagePickerController* imagePicker = [[UIImagePickerController alloc] init];    
    imagePicker.sourceType = UIImagePickerControllerSourceTypeCamera;
    imagePicker.delegate=self;
    //imagePicker.mediaTypes = [NSArray arrayWithObjects:(NSString *) kUTTypeImage,nil];
    imagePicker.allowsEditing = NO;
    [self presentModalViewController:imagePicker animated:YES];

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

@end
