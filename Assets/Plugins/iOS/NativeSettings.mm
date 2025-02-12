#import <UIKit/UIKit.h>

extern "C" {
    void _OpenSettings() {
        [[UIApplication sharedApplication] openURL:[NSURL URLWithString:UIApplicationOpenSettingsURLString] options:@{} completionHandler:nil];
    }
}