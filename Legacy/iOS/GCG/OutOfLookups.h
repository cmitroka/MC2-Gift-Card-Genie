//
//  OutOfLookups.h
//  GCG
//
//  Created by Chris Mitroka on 6/11/16.
//
//
#import <UIKit/UIKit.h>

@interface OutOfLookups : UIViewController
{
    IBOutlet UIButton *btnPurchase;
    IBOutlet UIButton *btnWatchAd;
    IBOutlet UIButton *btnExit;
}

@property (retain, nonatomic) NSURLConnection *connection;
@property (retain, nonatomic) NSMutableData *receivedData;
@end
