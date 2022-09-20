/*******************************************************************************
Copyright © 2015-2022 Pico Technology Co., Ltd.All rights reserved.  

NOTICE：All information contained herein is, and remains the property of 
Pico Technology Co., Ltd. The intellectual and technical concepts 
contained hererin are proprietary to Pico Technology Co., Ltd. and may be 
covered by patents, patents in process, and are protected by trade secret or 
copyright law. Dissemination of this information or reproduction of this 
material is strictly forbidden unless prior written permission is obtained from
Pico Technology Co., Ltd. 
*******************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.XR.PXR
{
    public class PXR_System
    {
        /// <summary>
        /// Initializes the system service for a specified game object. Must be called before calling other system related functions.
        /// </summary>
        /// <param name="objectName">The name of the game object to initialize the system service for.</param>
        public static void InitSystemService(string objectName)
        {
            PXR_Plugin.System.UPxr_InitToBService();
            PXR_Plugin.System.UPxr_SetUnityObjectName(objectName);
            PXR_Plugin.System.UPxr_InitAudioDevice();
        }

        /// <summary>
        /// Binds the system service. Must be called before calling other system related functions.
        /// </summary>
        public static void BindSystemService()
        {
            PXR_Plugin.System.UPxr_BindSystemService();
        }

        /// <summary>
        /// Unbinds the system service.
        /// </summary>
        public static void UnBindSystemService()
        {
            PXR_Plugin.System.UPxr_UnBindSystemService();
        }

        /// <summary>
        /// Turns on the battery service.
        /// </summary>
        /// <param name="objName">The name of the game object to turn on the battery service for.</param>
        /// <returns>Whether the power service has been turned on: `true`-succeeded; `false`-failed.</returns>
        public static bool StartBatteryReceiver(string objName)
        {
            return PXR_Plugin.System.UPxr_StartBatteryReceiver(objName);
        }

        /// <summary>
        /// Turns off the battery service.
        /// </summary>
        /// <returns>Whether the power service has been turned off: `true`-succeeded; `false`-failed.</returns>
        public static bool StopBatteryReceiver()
        {
            return PXR_Plugin.System.UPxr_StopBatteryReceiver();
        }

        /// <summary>
        /// Sets the brightness for the current HMD.
        /// </summary>
        /// <param name="brightness">Target brightness. The valid value ranges from `0` to `255`.</param>
        /// <returns>Whether the brightness has been set successfully: `true`-succeeded; `false`-failed.</returns>
        public static bool SetCommonBrightness(int brightness)
        {
            return PXR_Plugin.System.UPxr_SetBrightness(brightness);
        }

        /// <summary>
        /// Gets the brightness of the current HMD.
        /// </summary>
        /// <returns>An int value that indicates the brightness. The value ranges from `0` to `255`.</returns>
        public static int GetCommonBrightness()
        {
            return PXR_Plugin.System.UPxr_GetCurrentBrightness();
        }

        /// <summary>
        /// Gets the brightness level of the current screen.
        /// </summary>
        /// <returns>An int array. The first bit is the total brightness level supported, the second bit is the current brightness level, and it is the interval value of the brightness level from the third bit to the end bit.</returns>
        public static int[] GetScreenBrightnessLevel()
        {
            int[] currentLight = { 0 };
            currentLight = PXR_Plugin.System.UPxr_GetScreenBrightnessLevel();
            return currentLight;
        }

        /// <summary>
        /// Sets a brightness level for the current screen.
        /// </summary>
        /// <param name="brightness">Brightness mode: `0`-system default brightness setting; `1`-custom brightness setting.</param>
        /// <param name="level">Brightness level. The valid value ranges from `1` to `255`. If `brightness` is set to `1`, set a desired brightness level; if `brightness` is set to `0`, the system default brightness setting is adopted.</param>
        public static void SetScreenBrightnessLevel(int brightness, int level)
        {
            PXR_Plugin.System.UPxr_SetScreenBrightnessLevel(brightness, level);
        }

        /// <summary>
        /// Initializes the audio device.
        /// </summary>
        /// <returns>Whether the audio device has been initialized: `true`-succeeded; `false`-failed.</returns>
        public static bool InitAudioDevice()
        {
            return PXR_Plugin.System.UPxr_InitAudioDevice();
        }

        /// <summary>
        /// Turns on the volume service for a specified game pbject.
        /// </summary>
        /// <param name="objName">The name of the game object to turn on the volume service for.</param>
        /// <returns>Whether the volume service has been turned on: `true`-succeeded; `false`-failed.</returns>
        public static bool StartAudioReceiver(string objName)
        {
            return PXR_Plugin.System.UPxr_StartAudioReceiver(objName);
        }

        /// <summary>
        /// Turns off the volume service.
        /// </summary>
        /// <returns>Whether the volume service has been turned off: `true`-succeeded; `false`-failed.</returns>
        public static bool StopAudioReceiver()
        {
            return PXR_Plugin.System.UPxr_StopAudioReceiver();
        }

        /// <summary>
        /// Gets the maximum volume. Call InitAudioDevice to initialize the audio device before calling this function.
        /// </summary>
        /// <returns>An int value that indicates the maximum volume.</returns>
        public static int GetMaxVolumeNumber()
        {
            return PXR_Plugin.System.UPxr_GetMaxVolumeNumber();
        }

        /// <summary>
        /// Gets the current volume. Call InitAudioDevice to initialize the audio device before calling this function.
        /// </summary>
        /// <returns>An int value that indicates the current volume. The value ranges from `0` to `15`.</returns>
        public static int GetCurrentVolumeNumber()
        {
            return PXR_Plugin.System.UPxr_GetCurrentVolumeNumber();
        }

        /// <summary>
        /// Increases the volume. Call InitAudioDevice to initialize the audio device before calling this function.
        /// </summary>
        /// <returns>Whether the volume has been increased: `true`-succeeded; `false`-failed.</returns>
        public static bool VolumeUp()
        {
            return PXR_Plugin.System.UPxr_VolumeUp();
        }

        /// <summary>
        /// Decreases the volume. Call InitAudioDevice to initialize the audio device before calling this function.
        /// </summary>
        /// <returns>Whether the volume has been decreased: `true`-succeeded; `false`-failed.</returns>
        public static bool VolumeDown()
        {
            return PXR_Plugin.System.UPxr_VolumeDown();
        }

        /// <summary>
        /// Sets the volume. Call InitAudioDevice to initialize the audio device before calling this function.
        /// </summary>
        /// <param name="volume">The target volume. The valid value ranges from `0` to `15`.</param>
        /// <returns>Whether the target volume has been set: `true`-succeeded; `false`-failed.</returns>
        public static bool SetVolumeNum(int volume)
        {
            return PXR_Plugin.System.UPxr_SetVolumeNum(volume);
        }

        /// <summary>
        /// Checks whether the current device has valid permission for the game.
        /// </summary>
        /// <returns>Whether the permission is valid. Values: `Null`; `Invalid`; `Valid`.</returns>
        public static PXR_PlatformSetting.simulationType IsCurrentDeviceValid()
        {
            return PXR_Plugin.PlatformSetting.UPxr_IsCurrentDeviceValid();
        }

        /// <summary>
        /// Uses the appID to get whether the entitlement required by an app is present.
        /// </summary>
        /// <param name="appid">The appID.</param>
        /// <returns>Whether the entitlement required by the app is present: true-present; false-not present.</returns>
        public static bool AppEntitlementCheck(string appid)
        {
            return PXR_Plugin.PlatformSetting.UPxr_AppEntitlementCheck(appid);
        }

        /// <summary>
        /// Uses the publicKey to get the entitlement check result.
        /// </summary>
        /// <param name="publicKey">The publickey.</param>
        /// <returns>The entitlement check result: true-succeeded; false-failed.</returns>
        public static bool KeyEntitlementCheck(string publicKey)
        {
            return PXR_Plugin.PlatformSetting.UPxr_KeyEntitlementCheck(publicKey);
        }

        /// <summary>
        /// Use the appID to get the error code of the entitlement check result.
        /// </summary>
        /// <param name="appId">The appID.</param>
        /// <returns>The entitlement check result: -3-timeout; -2-service not exist (old versions of ROM have no service. If the app needs to be limited to operating in old versions, this state needs processing); -1-invalid params; 0-success.</returns>
        public static int AppEntitlementCheckExtra(string appId)
        {
            return PXR_Plugin.PlatformSetting.UPxr_AppEntitlementCheckExtra(appId);
        }

        /// <summary>
        /// Use the publicKey to get the error code of the entitlement check result.
        /// </summary>
        /// <param name="publicKey">The publickey.</param>
        /// <returns>The entitlement check result: -3-timeout; -2-service not exist (old versions of ROM have no Service. If the app needs to be limited to operating in old versions, this state needs processing); -1:invalid params; 0-success.</returns>
        public static int KeyEntitlementCheckExtra(string publicKey)
        {
            return PXR_Plugin.PlatformSetting.UPxr_KeyEntitlementCheckExtra(publicKey);
        }

        /// <summary>
        /// Gets the SDK version.
        /// </summary>
        /// <returns>The SDK version.</returns>
        public static string GetSDKVersion()
        {
            return PXR_Plugin.System.UPxr_GetSDKVersion();
        }

        /// <summary>
        /// Gets the predicted time a frame will be displayed after being rendered.
        /// </summary>
        /// <returns>The predicted time (in miliseconds).</returns>
        public static double GetPredictedDisplayTime()
        {
            return PXR_Plugin.System.UPxr_GetPredictedDisplayTime();
        }

        /// <summary>
        /// Sets the extra latency mode. Note: Call this function once only.
        /// </summary>
        /// <param name="mode">The latency mode: `0`-ExtraLatencyModeOff (Disable ExtraLatencyMode mode. This option will display the latest rendered frame for display); `1`-ExtraLatencyModeOn (Enable ExtraLatencyMode mode. This option will display one frame prior to the latest rendered frame); `2`-ExtraLatencyModeDynamic (Use system default setup).</param>
        /// <returns>Whether the extra latency mode has been set: `true`-succeeded; `false`-failed.</returns>
        public static bool SetExtraLatencyMode(int mode)
        {
            return PXR_Plugin.System.UPxr_SetExtraLatencyMode(mode);
        }

        /// <summary>
        /// Gets the specified type of device information.
        /// </summary>
        /// <param name="type">The target informaiton type. Enumerations: `ELECTRIC_QUANTITY`-battery; `PUI_VERSION`-PUI version; `EQUIPMENT_MODEL`-device model; `EQUIPMENT_SN`-device SN code; `CUSTOMER_SN`-customer SN code; `INTERNAL_STORAGE_SPACE_OF_THE_DEVICE`-device storage; `DEVICE_BLUETOOTH_STATUS`-bluetooth status; `BLUETOOTH_NAME_CONNECTED`-bluetooth name; `BLUETOOTH_MAC_ADDRESS`-bluetooth MAC address; `DEVICE_WIFI_STATUS`-Wi-Fi connection status; `WIFI_NAME_CONNECTED`-connected Wi-Fi name; `WLAN_MAC_ADDRESS`-WLAN MAC address; `DEVICE_IP`-device IP address; `CHARGING_STATUS`-device charging status.</param>
        /// <returns>The specified type of device information. For `CHARGING_STATUS`, an int value will be returned: `2`-charging; `3`-not charging.</returns>
        public static string StateGetDeviceInfo(SystemInfoEnum type)
        {
            return PXR_Plugin.System.UPxr_StateGetDeviceInfo(type);
        }

        /// <summary>
        /// Controls the device to shut down or reboot.
        /// </summary>
        /// <param name="deviceControl">Device action. Enumerations: `DEVICE_CONTROL_REBOOT`; `DEVICE_CONTROL_SHUTDOWN`.</param>
        /// <param name="callback">Callback: `1`-failed to shut down or reboot the device; `2`-no permission for device control.</param>
        public static void ControlSetDeviceAction(DeviceControlEnum deviceControl, Action<int> callback)
        {
            PXR_Plugin.System.UPxr_ControlSetDeviceAction(deviceControl, callback);
        }

        /// <summary>
        /// Installs or uninstalls app silently.
        /// </summary>
        /// <param name="packageControl">The action. Enumerations: `PACKAGE_SILENCE_INSTALL`-silent installation; `PACKAGE_SILENCE_UNINSTALL`-silent uninstallation.</param>
        /// <param name="path">The path to the app package for silent installation or the name of the app package for silent uninstallation.</param>
        /// <param name="callback">Callback: `0`-succeeded; `1`-failed; `2`-no permission for this operation.</param>
        public static void ControlAPPManager(PackageControlEnum packageControl, string path, Action<int> callback)
        {
            PXR_Plugin.System.UPxr_ControlAPPManager(packageControl, path, callback);
        }

        /// <summary>
        /// Sets a Wi-Fi that the device is automatically connected to.
        /// </summary>
        /// <param name="ssid">Wi-Fi name.</param>
        /// <param name="pwd">Wi-Fi password.</param>
        /// <param name="callback">Callback: `true`-connected; `false`-failed to connect.</param>
        public static void ControlSetAutoConnectWIFI(string ssid, string pwd, Action<bool> callback)
        {
            PXR_Plugin.System.UPxr_ControlSetAutoConnectWIFI(ssid, pwd, callback);
        }

        /// <summary>
        /// Removes the Wi-Fi that the device is automatically connected to.
        /// </summary>
        /// <param name="callback">Callback: `true`-removed; `false`-failed to remove.</param>
        public static void ControlClearAutoConnectWIFI(Action<bool> callback)
        {
            PXR_Plugin.System.UPxr_ControlClearAutoConnectWIFI(callback);
        }

        /// <summary>
        /// Sets the Home key event.
        /// </summary>
        /// <param name="eventEnum">Target event. Enumerations: `SINGLE_CLICK`; `DOUBLE_CLICK`.</param>
        /// <param name="function">The function of the event. Enumerations: `VALUE_HOME_GO_TO_SETTING`-go to Settings; `VALUE_HOME_RECENTER`-recenter; `VALUE_HOME_DISABLE`-disable the event of the Home key; `VALUE_HOME_GO_TO_HOME`-go to Home.</param>
        /// <param name="callback">Callback: `true`-set; `false`-failed to set.</param>
        public static void PropertySetHomeKey(HomeEventEnum eventEnum, HomeFunctionEnum function, Action<bool> callback)
        {
            PXR_Plugin.System.UPxr_PropertySetHomeKey(eventEnum, function, callback);
        }

        /// <summary>
        /// Sets extended settings for the Home key.
        /// </summary>
        /// <param name="eventEnum">Target event. Enumerations: `SINGLE_CLICK`; `DOUBLE_CLICK`.</param>
        /// <param name="function">The function of the event. Enumerations: `VALUE_HOME_GO_TO_SETTING`-go to Settings; `VALUE_HOME_RECENTER`-recenter; `VALUE_HOME_DISABLE`-disable the event of the Home key; `VALUE_HOME_GO_TO_HOME`-go to Home.</param>
        /// <param name="timesetup">The interval of key pressing is set only if there is the double click event or long pressing event. When shortly pressing the Home key, pass `0`.</param>
        /// <param name="pkg">Pass `null`.</param>
        /// <param name="className">Pass `null`.</param>
        /// <param name="callback">Callback: `true`-set; `false`-failed to set.</param>
        public static void PropertySetHomeKeyAll(HomeEventEnum eventEnum, HomeFunctionEnum function, int timesetup, string pkg, string className, Action<bool> callback)
        {
            PXR_Plugin.System.UPxr_PropertySetHomeKeyAll(eventEnum, function, timesetup, pkg, className, callback);
        }

        /// <summary>
        /// Sets the Power key event.
        /// </summary>
        /// <param name="isSingleTap">Whether it is a single click event: `true`-single click event; `false`-long pressing event.</param>
        /// <param name="enable">Key enabling status: `true`-enabled; `false`-not enabled.</param>
        /// <param name="callback">Callback: `0`-set; `1`-failed to set; `11`-press the Power key for no more than 5s.</param>
        public static void PropertyDisablePowerKey(bool isSingleTap, bool enable, Action<int> callback)
        {
            PXR_Plugin.System.UPxr_PropertyDisablePowerKey(isSingleTap, enable, callback);
        }

        /// <summary>
        /// Sets the time the screen turns off when the device is not in use.
        /// </summary>
        /// <param name="timeEnum">Screen off timeout. Enumerations: `Never`-never off; `THREE`-3s; `TEN`-10s; `THIRTY`-30s; `SIXTY`-60s; `THREE_HUNDRED`-5 mins; `SIX_HUNDRED`-10 mins.</param>
        /// <param name="callback">Callback: `0`-set; `1`-failed to set; `10`-the screen off timeout should not be greater than the system sleep timeout.</param>
        public static void PropertySetScreenOffDelay(ScreenOffDelayTimeEnum timeEnum, Action<int> callback)
        {
            PXR_Plugin.System.UPxr_PropertySetScreenOffDelay(timeEnum, callback);
        }

        /// <summary>
        /// Sets the time the system sleeps when the device is not in use.
        /// </summary>
        /// <param name="timeEnum">System sleep timeout. Enumerations: `Never`-never sleep; `FIFTEEN`-15s; `THIRTY`-30s; `SIXTY`-60s; `THREE_HUNDRED`-5 mins; `SIX_HUNDRED`-10 mins; `ONE_THOUSAND_AND_EIGHT_HUNDRED`-30 mins.</param>
        public static void PropertySetSleepDelay(SleepDelayTimeEnum timeEnum)
        {
            PXR_Plugin.System.UPxr_PropertySetSleepDelay(timeEnum);
        }

        /// <summary>
        /// Switches specified system function on/off.
        /// </summary>
        /// <param name="systemFunction">Function name. Enumerations: `SFS_USB`-USB debugging; `SFS_AUTOSLEEP`-auto sleep; `SFS_SCREENON_CHARGING`-screen-on charging; `SFS_OTG_CHARGING`-OTG charging; `SFS_RETURN_MENU_IN_2DMODE`-display the Return icon on 2D screen; `SFS_COMBINATION_KEY`-combination key; `SFS_CALIBRATION_WITH_POWER_ON`-calibration with power on; `SFS_SYSTEM_UPDATE`-system update; `SFS_CAST_SERVICE`-casting service, not valid when using Pico Enterprise Solution; `SFS_EYE_PROTECTION`-eye-protection mode; `SFS_SECURITY_ZONE_PERMANENTLY`-disable 6DoF safety boundary permanently; `SFS_Auto_Calibration`-auto recenter/recalibrate; `SFS_USB_BOOT`-USB plug-in boot; `SFS_VOLUME_UI`-global volume UI; `SFS_CONTROLLER_UI`-global controller connected UI; `SFS_NAVGATION_SWITCH`-navigation bar; `SFS_SHORTCUT_SHOW_RECORD_UI`-Screen Recording button UI; `SFS_SHORTCUT_SHOW_FIT_UI`-Pico fit UI; `SFS_SHORTCUT_SHOW_CAST_UI`-Screencast button UI; `SFS_SHORTCUT_SHOW_CAPTURE_UI`-Screenshot button UI; `SFS_STOP_MEM_INFO_SERVICE`-force quit 2D application; `SFS_USB_FORCE_HOST`-set Neo3 device as the host device; `SFS_SET_DEFAULT_SAFETY_ZONE`-set default boundary for Neo3 device; `SFS_ALLOW_RESET_BOUNDARY`-allow to reset customized boundary; `SFS_BOUNDARY_CONFIRMATION_SCREEN`-whether to display the boundary confirmation screen; `SFS_LONG_PRESS_HOME_TO_RECENTER`-long press the Home key to recenter; `SFS_POWER_CTRL_WIFI_ENABLE`-Neo3 device stays connected to the network when the device sleeps/turns off; ` SFS_WIFI_DISABLE`-disable Wi-Fi for Neo3 device.</param>
        /// <param name="switchEnum">Whether to switch the function on/off: `S_ON`-switch on; `S_OFF`-switch off.</param>
        public static void SwitchSystemFunction(SystemFunctionSwitchEnum systemFunction, SwitchEnum switchEnum)
        {
            PXR_Plugin.System.UPxr_SwitchSystemFunction(systemFunction, switchEnum);
        }

        /// <summary>
        /// Sets the USB configuration mode.
        /// </summary>
        /// <param name="uSBConfigModeEnum">USB configuration mode. Enumerations: `MTP`-MTP mode; `CHARGE`-charging mode.</param>
        public static void SwitchSetUsbConfigurationOption(USBConfigModeEnum uSBConfigModeEnum)
        {
            PXR_Plugin.System.UPxr_SwitchSetUsbConfigurationOption(uSBConfigModeEnum);
        }

        /// <summary>
        /// Turns the screen on.
        /// </summary>
        public static void ScreenOn()
        {
            PXR_Plugin.System.UPxr_ScreenOn();
        }

        /// <summary>
        /// Turns the screen off.
        /// </summary>
        public static void ScreenOff()
        {
            PXR_Plugin.System.UPxr_ScreenOff();
        }

        /// <summary>
        /// Acquires the wake lock.
        /// </summary>
        public static void AcquireWakeLock()
        {
            PXR_Plugin.System.UPxr_AcquireWakeLock();
        }

        /// <summary>
        /// Releases the wake lock.
        /// </summary>
        public static void ReleaseWakeLock()
        {
            PXR_Plugin.System.UPxr_ReleaseWakeLock();
        }

        /// <summary>
        /// Enables the Confirm key.
        /// </summary>
        public static void EnableEnterKey()
        {
            PXR_Plugin.System.UPxr_EnableEnterKey();
        }

        /// <summary>
        /// Disables the Confirm key.
        /// </summary>
        public static void DisableEnterKey()
        {
            PXR_Plugin.System.UPxr_DisableEnterKey();
        }

        /// <summary>
        /// Enables the Volume Key.
        /// </summary>
        public static void EnableVolumeKey()
        {
            PXR_Plugin.System.UPxr_EnableVolumeKey();
        }

        /// <summary>
        /// Disables the Volume Key.
        /// </summary>
        public static void DisableVolumeKey()
        {
            PXR_Plugin.System.UPxr_DisableVolumeKey();
        }

        /// <summary>
        /// Enables the Back Key.
        /// </summary>
        public static void EnableBackKey()
        {
            PXR_Plugin.System.UPxr_EnableBackKey();
        }

        /// <summary>
        /// Disables the Back Key.
        /// </summary>
        public static void DisableBackKey()
        {
            PXR_Plugin.System.UPxr_DisableBackKey();
        }

        /// <summary>
        /// Writes the configuration file to the /data/local/tmp/ path.
        /// </summary>
        /// <param name="path">The path to the configuration file, e.g., `/data/local/tmp/config.txt`.</param>
        /// <param name="content">The content of the configuration file.</param>
        /// <param name="callback">Callback: `true`-written; `false`-failed to be written.</param>
        public static void WriteConfigFileToDataLocal(string path, string content, Action<bool> callback)
        {
            PXR_Plugin.System.UPxr_WriteConfigFileToDataLocal(path, content, callback);
        }

        /// <summary>
        /// Resets all keys to default configuration.
        /// </summary>
        /// <param name="callback">Callback: `true`-reset; `false`-failed to reset.</param>
        public static void ResetAllKeyToDefault(Action<bool> callback)
        {
            PXR_Plugin.System.UPxr_ResetAllKeyToDefault(callback);
        }

        /// <summary>
        /// Sets an app as the launcher app.
        /// </summary>
        /// <param name="switchEnum">Switch. Enumerations: `S_ON`-set the app as the launcher app; `S_OFF`-cancel setting the app as the launcher app.</param>
        /// <param name="packageName">The app package name.</param>
        public static void SetAPPAsHome(SwitchEnum switchEnum, string packageName)
        {
            PXR_Plugin.System.UPxr_SetAPPAsHome(switchEnum, packageName);
        }

        /// <summary>
        /// Force quits app(s) by passing app PID or package name.
        /// </summary>
        /// <param name="pids">An array of app PID(s).</param>
        /// <param name="packageNames">An array of package name(s).</param>
        public static void KillAppsByPidOrPackageName(int[] pids, string[] packageNames)
        {
            PXR_Plugin.System.UPxr_KillAppsByPidOrPackageName(pids, packageNames);
        }

        /// <summary>
        /// Force quits background app(s) expect those in the allowlist.
        /// </summary>
        /// <param name="packageNames">An array of package name(s) to be added to the allowlist. The corresponding app(s) in the allowlist will not be force quit.</param>
        public static void KillBackgroundAppsWithWhiteList(string[] packageNames)
        {
            PXR_Plugin.System.UPxr_KillBackgroundAppsWithWhiteList(packageNames);
        }

        /// <summary>
        /// Freezes the screen to the front. The screen will turn around with the HMD. Note: This function only supports G2 4K series.
        /// </summary>
        /// <param name="freeze">Whether to freeze the screen: `true`-freeze; `false`-stop freezing.</param>
        public static void FreezeScreen(bool freeze)
        {
            PXR_Plugin.System.UPxr_FreezeScreen(freeze);
        }

        /// <summary>
        /// Turns on the screencast function.
        /// </summary>
        public static void OpenMiracast()
        {
            PXR_Plugin.System.UPxr_OpenMiracast();
        }

        /// <summary>
        /// Gets the status of the screencast function.
        /// </summary>
        /// <returns>The status of the screencast function: `true`-screencast on; `false`-screencast off.</returns>
        public static bool IsMiracastOn()
        {
            return PXR_Plugin.System.UPxr_IsMiracastOn();
        }

        /// <summary>
        /// Turns off the screencast function.
        /// </summary>
        public static void CloseMiracast()
        {
            PXR_Plugin.System.UPxr_CloseMiracast();
        }

        /// <summary>
        /// Starts scanning for devices that can be used for screen casting.
        /// </summary>
        public static void StartScan()
        {
            PXR_Plugin.System.UPxr_StartScan();
        }

        /// <summary>
        /// Stops scanning for devices that can be used for screen casting.
        /// </summary>
        public static void StopScan()
        {
            PXR_Plugin.System.UPxr_StopScan();
        }

        /// <summary>
        /// Casts the screen to the specified device.
        /// </summary>
        /// <param name="modelJson">A modelJson structure containing the following fields: `deviceAddress`, `deviceName`, and `isAvailable` (`true`-device available; `false`-device not available).</param>
        public static void ConnectWifiDisplay(string modelJson)
        {
            PXR_Plugin.System.UPxr_ConnectWifiDisplay(modelJson);
        }

        /// <summary>
        /// Stops casting the screen to the current device.
        /// </summary>
        public static void DisConnectWifiDisplay()
        {
            PXR_Plugin.System.UPxr_DisConnectWifiDisplay();
        }

        /// <summary>
        /// Forgets the device that have been connected for screencast.
        /// </summary>
        /// <param name="address">Device address.</param>
        public static void ForgetWifiDisplay(string address)
        {
            PXR_Plugin.System.UPxr_ForgetWifiDisplay(address);
        }

        /// <summary>
        /// Renames the device connected for screencast (only the name for local storage).
        /// </summary>
        /// <param name="address">The MAC address of the device.</param>
        /// <param name="newName">The new device name.</param>
        public static void RenameWifiDisplay(string address, string newName)
        {
            PXR_Plugin.System.UPxr_RenameWifiDisplay(address, newName);
        }

        /// <summary>
        /// Returns a wdmodel list of the device(s) for screencast.
        /// </summary>
        public static void SetWDModelsCallback()
        {
            PXR_Plugin.System.UPxr_SetWDModelsCallback();
        }

        /// <summary>
        /// Returns a JSON array of the device(s) for screencast.
        /// </summary>
        public static void SetWDJsonCallback()
        {
            PXR_Plugin.System.UPxr_SetWDJsonCallback();
        }

        /// <summary>
        /// Manually updates the device list for screencast.
        /// </summary>
        /// <param name="callback">The device list for screencast.</param>
        public static void UpdateWifiDisplays(Action<string> callback)
        {
            PXR_Plugin.System.UPxr_UpdateWifiDisplays(callback);
        }

        /// <summary>
        /// Gets the information of the current connected device.
        /// </summary>
        /// <returns>The information of the current connected device.</returns>
        public static string GetConnectedWD()
        {
            return PXR_Plugin.System.UPxr_GetConnectedWD();
        }

        /// <summary>
        /// Switches the large space scene on.
        /// </summary>
        /// <param name="open">Whether to switch the large space scene on: `true`-switch on; `false`-not to switch on.</param>
        /// <param name="callback">Callback: `true`-succeeded; `false`-failed.</param>
        public static void SwitchLargeSpaceScene(bool open, Action<bool> callback)
        {
            PXR_Plugin.System.UPxr_SwitchLargeSpaceScene(open, callback);
        }

        /// <summary>
        /// Gets the status of the large space scene.
        /// </summary>
        /// <param name="callback">Callback: `true`-status got; `false`-failed to get the status.</param>
        public static void GetSwitchLargeSpaceStatus(Action<string> callback)
        {
            PXR_Plugin.System.UPxr_GetSwitchLargeSpaceStatus(callback);
        }

        /// <summary>
        /// Saves the large space map.
        /// </summary>
        /// <returns>Whether the large space map has been saved: `true`-saved; `false`-failed to save.</returns>
        public static bool SaveLargeSpaceMaps()
        {
            return PXR_Plugin.System.UPxr_SaveLargeSpaceMaps();
        }

        /// <summary>
        /// Exports map(s).
        /// </summary>
        /// <param name="callback">Callback: `true`-exported; `false`-failed to export.</param>
        public static void ExportMaps(Action<bool> callback)
        {
            PXR_Plugin.System.UPxr_ExportMaps(callback);
        }

        /// <summary>
        /// Imports map(s).
        /// </summary>
        /// <param name="callback">Callback: `true`-imported; `false`-failed to import.</param>
        public static void ImportMaps(Action<bool> callback)
        {
            PXR_Plugin.System.UPxr_ImportMaps(callback);
        }

        /// <summary>
        /// Gets the sensor's status.
        /// </summary>
        /// <returns>The sensor's status: `0`-null; `1`-3DoF; `3`-6DoF.</returns>
        public static int GetSensorStatus()
        {
            return PXR_Plugin.System.UPxr_GetSensorStatus();
        }

        /// <summary>
        /// Sets the system display frequency rate.
        /// </summary>
        /// <param name="rate">The frequency rate: `72`; `90`; `120`. Other values are invalid.</param>
        public static void SetSystemDisplayFrequency(float rate)
        {
            PXR_Plugin.System.UPxr_SetSystemDisplayFrequency(rate);
        }

        /// <summary>
        /// Gets the system display frequency rate.
        /// </summary>
        /// <returns>The system display frequency rate.</returns>
        public static float GetSystemDisplayFrequency()
        {
            return PXR_Plugin.System.UPxr_GetSystemDisplayFrequency();
        }

        /// <summary>
        /// Gets the predicted status of the sensor.
        /// </summary>
        /// <param name="sensorState">Sensor's coordinate: `pose`-coordinate inside the app; `globalPose`-global coordinate.</param>
        /// <param name="sensorFrameIndex">Sensor frame index.</param>
        /// <returns>The predicted status of the sensor.</returns>
        public static int GetPredictedMainSensorStateNew(ref PxrSensorState2 sensorState, ref int sensorFrameIndex) {
            return PXR_Plugin.System.UPxr_GetPredictedMainSensorStateNew(ref sensorState, ref sensorFrameIndex);
        }
        
        public static int ContentProtect(int data) {
            return PXR_Plugin.System.UPxr_ContentProtect(data);
        }

        /// <summary>
        /// Gets the CPU utilization of the current device.
        /// </summary>
        /// <returns>The CPU utilization of the current device.</returns>
        public static float[] GetCpuUsages() {
            return PXR_Plugin.System.UPxr_GetCpuUsages();
        }

        /// <summary>
        /// Gets device temperature in Celsius.
        /// </summary>
        /// <param name="type">The requested type of device temperature: `0`-CPU temperature (DEVICE_TEMPERATURE_CPU); `1`-GPU temperature (DEVICE_TEMPERATURE_GPU); `2`-battery temperature (DEVICE_TEMPERATURE_BATTERY); `3`-surface temperature (DEVICE_TEMPERATURE_SKIN).</param>
        /// <param name="source">The requested source of device temperature: `0`-current temperature (TEMPERATURE_CURRENT); `1`-temperature threshold for throttling (TEMPERATURE_THROTTLING); `2`-temperature threshold for shutdown (TEMPERATURE_SHUTDOWN); `3`-temperature threshold for throttling (TEMPERATURE_THROTTLING_BELOW_VR_MIN). For source `3`, if the actual temperature is higher than the threshold, the lowest clock frequency for VR mode will not be met.</param>
        /// <returns>An array of requested device temperatures in Celsius.</returns>
        public static float[] GetDeviceTemperatures(int type, int source) {
            return PXR_Plugin.System.UPxr_GetDeviceTemperatures(type, source);
        }

        /// <summary>
        /// Captures the current screen.
        /// </summary>
        public static void Capture() {
            PXR_Plugin.System.UPxr_Capture();
        }

        /// <summary>
        /// Records the screen. Call this function again to stop recording.
        /// </summary>
        public static void Record() {
            PXR_Plugin.System.UPxr_Record();
        }

        /// <summary>
        /// Connects the device to a specified Wi-Fi.  
        /// </summary>
        /// <param name="ssid">Wi-Fi name.</param>
        /// <param name="pwd">Wi-Fi password.</param>
        /// <param name="ext">Reserved parameter, pass `0` by default.</param>
        /// <param name="callback">The callback for indicating whether the Wi-Fi connection is successful: `0`-connected; `1`-password error; `2`-unknown error.</param>
        public static void ControlSetAutoConnectWIFIWithErrorCodeCallback(String ssid, String pwd, int ext, Action<int> callback) {
            PXR_Plugin.System.UPxr_ControlSetAutoConnectWIFIWithErrorCodeCallback(ssid, pwd, ext, callback);
        }

        /// <summary>
        /// Keeps an app active. In other words, improves the priority of an app, thereby making the system not to force quit the app.
        /// </summary>
        /// <param name="appPackageName">App package name.</param>
        /// <param name="keepAlive">Whether to keep the app active (i.e., whether to enhance the priority of the app): `true`-yes; `false`-no.</param>
        /// <param name="ext">Reserved parameter, pass `0` by default.</param>
        public static void AppKeepAlive(String appPackageName, bool keepAlive, int ext) {
            PXR_Plugin.System.UPxr_AppKeepAlive(appPackageName, keepAlive, ext);
        }
        
        /// <summary>
        /// Schedules automatic startup for the device. Note: Supported by Neo 3 series only.
        /// </summary>
        /// <param name="year">Year, for example, `2022`.</param>
        /// <param name="month">Month, for example, `2`.</param>
        /// <param name="day">Day, for example, `22`.</param>
        /// <param name="hour">Hour, for example, `22`.</param>
        /// <param name="minute">Minute, for example, `22`.</param>
        /// <param name="open">Whether to enable scheduled auto startup for the device: `true`-enable; `false`-disable.</param>
        public static void TimingStartup(int year, int month, int day, int hour, int minute, bool open) {
            PXR_Plugin.System.UPxr_TimingStartup(year, month, day, hour, minute, open);
        }

        /// <summary>
        /// Schedules automatic shutdown for the device. Note: Supported by Neo 3 series only.
        /// </summary>
        /// <param name="year">Year, for example, `2022`.</param>
        /// <param name="month">Month, for example, `2`.</param>
        /// <param name="day">Day, for example, `22`.</param>
        /// <param name="hour">Hour, for example, `22`.</param>
        /// <param name="minute">Minute, for example, `22`.</param>
        /// <param name="open">Whether to enable scheduled auto shutdown for the device: `true`-enable; `false`-disable.</param>
        public static void TimingShutdown(int year, int month, int day, int hour, int minute, bool open) {
            PXR_Plugin.System.UPxr_TimingShutdown(year, month, day, hour, minute, open);
        }

        /// <summary>
        /// Displays a specified settings screen. Note: Supported by Neo 3 series only.
        /// </summary>
        /// <param name="settingsEnum">The enumerations of settings screen:
        /// `START_VR_SETTINGS_ITEM_WIFI`-the Wi-Fi settings screen;
        /// `START_VR_SETTINGS_ITEM_BLUETOOTH`-the bluetooth settings screen;
        /// `START_VR_SETTINGS_ITEM_CONTROLLER`-the controller settings screen;
        /// `START_VR_SETTINGS_ITEM_LAB`-the lab settings screen;
        /// `START_VR_SETTINGS_ITEM_BRIGHTNESS`-the brightness settings screen;
        /// `START_VR_SETTINGS_ITEM_GENERAL)`-the general settings screen;
        /// `START_VR_SETTINGS_ITEM_NOTIFICATION`-the notification settings screen.
        /// </param>
        /// <param name="hideOtherItem">Whether to display the selected settings screen: `true`-display; `false`-hide.</param>
        /// <param name="ext">Reserved parameter, pass `0` by default.</param>
        public static void StartVrSettingsItem(StartVRSettingsEnum settingsEnum, bool hideOtherItem, int ext) {
            PXR_Plugin.System.UPxr_StartVrSettingsItem(settingsEnum, hideOtherItem, ext);
        }

        /// <summary>
        /// Changes the Volume key's function to that of the Home and Enter key's, or restores the volume adjustment function to the Volume key.
        /// </summary>
        /// <param name="switchEnum">Whether to change the Volume key's function: `S_ON`-change; `S_OFF`-do not change.</param>
        /// <param name="ext">Reserved parameter, pass `0` by default.</param>
        public static void SwitchVolumeToHomeAndEnter(SwitchEnum switchEnum, int ext) {
            PXR_Plugin.System.UPxr_SwitchVolumeToHomeAndEnter(switchEnum, ext);
        }

        /// <summary>
        /// Gets whether the Volume key's function has been changed to that of the Home and Enter key's.
        /// </summary>
        /// <returns>`S_ON`-changed; `S_OFF`-not changed.</returns>
        public static SwitchEnum IsVolumeChangeToHomeAndEnter() {
            return PXR_Plugin.System.UPxr_IsVolumeChangeToHomeAndEnter();
        }
    }
}

