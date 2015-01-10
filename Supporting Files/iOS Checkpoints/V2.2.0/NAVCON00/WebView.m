//
//  WebView.m
//  NAVCON01
//
//  Created by Chris on 8/10/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "WebView.h"

@implementation WebView
@synthesize URL;
- (void)webViewDidStartLoad:(UIWebView *)webViewX {
    [spinner startAnimating];
    //NSLog(@"Started loading");
    webView.hidden=true;
}
-(id)initWithURL:(NSString *)pURLIn
{
    URL=pURLIn;
    return self;
}

- (void)webView:(UIWebView *)wv didFailLoadWithError:(NSError *)error {
	//NSLog(@"webView:didFailLoadWithError:");
	[UIApplication sharedApplication].networkActivityIndicatorVisible = NO;
	//[UtilsA ShowAlert:@"Connection?" Message:@"Seems you don't have an internet connection right now; check and try again." ButtonText:@"OK"];
	NSString *errorString = [error localizedDescription];
	NSString *errorTitle = [NSString stringWithFormat:@"Error"];
	
	UIAlertView *errorView = [[UIAlertView alloc] initWithTitle:errorTitle message:errorString delegate:self cancelButtonTitle:nil otherButtonTitles:@"OK", nil];
	[errorView show];
}
- (void)webViewDidFinishLoad:(UIWebView *)webViewX {
    [spinner stopAnimating];
    webView.hidden=false;
    //NSLog(@"Finshed loading");
    spinner.hidden=YES;
}
- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        self.title = NSLocalizedString(@"Page 4", @"Page 4");
    }
    return self;
}
- (void)viewDidLoad
{
    [webView loadRequest:[NSURLRequest requestWithURL:[NSURL URLWithString:URL]]];
}


@end
