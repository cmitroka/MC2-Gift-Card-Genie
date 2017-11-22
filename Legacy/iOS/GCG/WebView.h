//
//  WebView.h
//  GCG
//
//  Created by Chris on 8/10/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface WebView : UIViewController
{
    IBOutlet UIWebView *webView;
    IBOutlet UIActivityIndicatorView *spinner;
}
@property(nonatomic, retain) NSString *URL;
-(id)initWithURL:(NSString *)pURLIn;
@end
