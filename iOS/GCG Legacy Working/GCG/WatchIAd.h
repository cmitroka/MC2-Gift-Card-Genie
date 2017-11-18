//
//  WatchIAd.h
//  YMCA Check In
//
//  Created by Chris Mitroka on 9/5/16.
//  Copyright (c) 2016 MC2 Tech Services. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface WatchIAd : UIViewController
{
    IBOutlet UILabel *uiDesc;
    IBOutlet UIActivityIndicatorView *uiSpinner;
}
+ (WatchIAd *)wia;

@property(nonatomic, weak) IBOutlet UILabel *uiDescUnused;
@property(nonatomic, weak) IBOutlet UIActivityIndicatorView *uiSpinnerUnused;
@property (retain, nonatomic) NSURLConnection *connection;
@property (retain, nonatomic) NSMutableData *receivedData;
@property int pWatchAdDelayAmnt;
@property (nonatomic, retain) NSString *pWatchAdStatus;  //Closed; Watched
@property (nonatomic, retain) NSString *pWatchAdGoToPage; //AllEntries
@property (nonatomic, retain) NSString *pWatchAdNagDesc;  //Retrieving Report; Please Wait OR Retrieving Barcode; Please Wait OR Loading; Please Wait
@property (nonatomic, retain) NSString *pWatchAdDoneFrom; //NagScreen; NagScreenManual; Barcodes; Reports
@property (nonatomic, retain) NSString *pWatchAdIDtoLog;

@end
