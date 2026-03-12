//
//  PayHelper.m
//  UnityFramework
//

#import "PayHelper.h"

@interface PayHelper()

@end

@implementation PayHelper

static PayHelper *_sharedInstance = nil;

static dispatch_once_t onceToken;

+ (instancetype)shared {
    dispatch_once(&onceToken, ^{
        _sharedInstance = [[PayHelper alloc] init];
    });
    return _sharedInstance;
}

- (instancetype)init {
    self = [super init];
    if (self) {
    }
    return self;
}


- (void)AliPayOrder:(NSString *)orderStr {
    [[AlipaySDK defaultService] payOrder:orderStr fromScheme:self.ali_schemeStr callback:^(NSDictionary *resultDic) {
        NSLog(@"🌤🌤🌤 %s", __func__);
        [self executeCompleteBlock:resultDic];
    }];
}

- (void)handleAliPayURL:(NSURL *)url {
    if ([url.scheme isEqualToString:self.ali_schemeStr]) {
        [[AlipaySDK defaultService] processOrderWithPaymentResult:url standbyCallback:^(NSDictionary *resultDic) {
            NSLog(@"🌤🌤🌤 %s", __func__);
            [self executeCompleteBlock:resultDic];
        }];
    }
}

- (void)executeCompleteBlock:(NSDictionary *)resultDic {
    //    返回码    含义
    //    9000    订单支付成功
    //    8000    正在处理中，支付结果未知（有可能已经支付成功），请查询商户订单列表中订单的支付状态
    //    4000    订单支付失败
    //    5000    重复请求
    //    6001    用户中途取消
    //    6002    网络连接出错
    //    6004    支付结果未知（有可能已经支付成功），请查询商户订单列表中订单的支付状态
    //    其它    其它支付错误
    
    NSInteger resultCode = [resultDic[@"resultStatus"] integerValue];
    NSString *msg = nil;
    if (resultCode ==  9000) {
        msg = @"支付成功";
    } else if (resultCode ==  8000) {
        msg = @"结果未知";
    } else if (resultCode ==  5000) {
        msg = @"重复请求";
    } else if (resultCode ==  6001) {
        msg = @"支付取消";
    } else if (resultCode ==  6002) {
        msg = @"网络出错";
    } else if (resultCode ==  6004) {
        msg = @"结果未知";
    } else {
        msg = @"支付失败";
    }
    
    NSMutableDictionary *tempdic = [NSMutableDictionary dictionaryWithDictionary:resultDic];
    [tempdic setObject:msg forKey:@"iOS_Add_Msg"];
    
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:tempdic options:NSJSONWritingPrettyPrinted error:nil];
    NSString *jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    NSLog(@"🌤🌤🌤 iOS 收到支付宝回调结果: resultDic = %@",resultDic);
    NSString *data = [NSString stringWithFormat:@"{\"Name\":\"%@\",\"Data\":%d}", "AlipayPayResult", [msg UTF8String]];
    UnitySendMessage("Master", "ReceiveNativeMsg", [data UTF8String]);
}



///注册微信支付
- (void)registerApp:(NSString *)WXAppKey universalLink:(NSString *)WXAppUniversalLink {
    [WXApi registerApp:WXAppKey universalLink:WXAppUniversalLink];
}

///微信支付
- (void)WxPayReq:(PayReq *)req {
    //调起微信支付,包装参数
    [WXApi sendReq:req completion:^(BOOL success) {}];
}

///处理微信支付的回调
- (void)handleWxPayResp:(BaseResp *)resp {
    
    if ([resp isKindOfClass:[PayResp class]]) {
        //微信支付回调中errCode值列表：
        //名称   描述        解决方案
        //0     成功        展示成功页面
        //-1    错误        可能的原因：签名错误、未注册APPID、项目设置APPID不正确、注册的APPID与设置的不匹配、其他异常等。
        //-2    用户取消     无需处理。发生场景：用户不支付了，点击取消，返回APP。
        NSString *msg = @"";
        if (resp.errCode == WXSuccess) {
            msg = @"支付成功";
        } else if(resp.errCode == WXErrCodeUserCancel) {
            msg = @"用户取消";
        } else {
            msg = @"支付失败";
        }
        NSMutableDictionary *tempdic = [NSMutableDictionary dictionary];
        [tempdic setObject:msg forKey:@"iOS_Add_Msg"];
        [tempdic setObject:@(resp.errCode) forKey:@"errCode"];
        if (resp.errStr) {
            [tempdic setObject:resp.errStr forKey:@"errStr"];
        }
        [tempdic setObject:@(resp.type) forKey:@"type"];
        
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:tempdic options:NSJSONWritingPrettyPrinted error:nil];
        NSString *jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        NSLog(@"🌤🌤🌤 iOS 收到微信回调结果: tempdic = %@", tempdic);
        NSString *data = [NSString stringWithFormat:@"{\"Name\":\"%@\",\"Data\":%d}", "WeChatPayResult", [msg UTF8String]];
        UnitySendMessage("Master", "ReceiveNativeMsg", [data UTF8String]);
    } else {
        NSLog(@"🌤🌤🌤 iOS 收到微信支付回调对象错误! 这不是支付完成的类实例对象");
    }
}

@end


#ifdef __cplusplus
extern "C" {
#endif

///支付宝
extern void IOSAliPay(const char *payOrder, const char *scheme) {
    NSString *p = [NSString stringWithUTF8String:payOrder];
    NSString *s = [NSString stringWithUTF8String:scheme];
    [PayHelper shared].ali_schemeStr = s;
    [[PayHelper shared] AliPayOrder:p];
}


///需要注册微信
extern void IOSRegisterWxApi(const char *WXAppKey, const char *WXAppUniversalLink) {
    NSString *k = [NSString stringWithUTF8String:WXAppKey];
    NSString *l = [NSString stringWithUTF8String:WXAppUniversalLink];
    [[PayHelper shared] registerApp:k universalLink:l];
}

///微信
extern void IOSWxPay(const char *openID, const char *partnerId, const char *prepayId, const char *nonceStr, const char *timeStamp, const char *package, const char *sign) {
    //调起微信支付,包装参数
    PayReq *req      = [[PayReq alloc] init];
    req.openID       = [NSString stringWithUTF8String:openID];
    req.partnerId    = [NSString stringWithUTF8String:partnerId];
    req.prepayId     = [NSString stringWithUTF8String:prepayId];
    req.nonceStr     = [NSString stringWithUTF8String:nonceStr];
    req.timeStamp    = (UInt32)[[NSString stringWithUTF8String:timeStamp] longLongValue];
    req.package      = [NSString stringWithUTF8String:package];
    req.sign         = [NSString stringWithUTF8String:sign];
    [[PayHelper shared] WxPayReq:req];
}

#ifdef __cplusplus
}
#endif
