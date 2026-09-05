//
//  PayHelper.h
//  UnityFramework
//

#import <Foundation/Foundation.h>
#import <AlipaySDK/AlipaySDK.h>
#import "WXApi.h"

@interface PayHelper : NSObject

///单例对象
+ (instancetype)shared;

///处理支付回调URL
- (void)handleAliPayURL:(NSURL *)url;
///支付宝支付
- (void)AliPayOrder:(NSString *)orderStr;
///支付宝使用的 Schema
@property (nonatomic, strong) NSString *ali_schemeStr;

///处理微信支付的回调
- (void)handleWxPayResp:(BaseResp *)resp;

@end