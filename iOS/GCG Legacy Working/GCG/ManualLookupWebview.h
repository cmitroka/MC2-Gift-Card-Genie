//
//  ManualLookupWebview.h
//  GCG
//
//  Created by Chris Mitroka on 10/3/16.
//
//

#import <UIKit/UIKit.h>

@interface ManualLookupWebview : UIViewController<UIAlertViewDelegate>
{
    IBOutlet UIWebView *webView;
    IBOutlet UIActivityIndicatorView *spinner;
    IBOutlet UITextField *txtCardNumb;
    IBOutlet UITextField *txtPIN;
}

@end
