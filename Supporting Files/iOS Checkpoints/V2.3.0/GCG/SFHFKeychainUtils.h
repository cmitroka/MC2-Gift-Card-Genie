//  SFHFKeychainUtils.h

#import <UIKit/UIKit.h>


@interface SFHFKeychainUtils : NSObject {
    
}

+ (NSString *) pmGetValueForSetting: (NSString *) setting;
+ (BOOL) pmUpdateSettingName: (NSString *) setting SettingValue: (NSString *) settoval;
+ (BOOL) pmDeleteSetting: (NSString *) setting;

@end