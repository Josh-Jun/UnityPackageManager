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
using UnityEngine;
using UnityEngine.XR;

namespace Unity.XR.PXR
{
    public static class PXR_Input
    {
        public enum ControllerDevice
        {
            G2 = 3,
            Neo2,
            Neo3,
            NewController = 10
        }

        public enum Controller
        {
            LeftController,
            RightController,
        }

        /// <summary>
        /// Gets the current dominant controller.
        /// </summary>
        /// <returns>The current dominant controller: `LeftController`; `RightController`.</returns>
        public static Controller GetDominantHand()
        {
            return (Controller)PXR_Plugin.Controller.UPxr_GetControllerMainInputHandle();
        }

        /// <summary>
        /// Sets a controller as the dominant controller.
        /// </summary>
        /// <param name="controller">The controller to be set as the dominant controller: `0`-left controller; `1`-right controller.</param>
        public static void SetDominantHand(Controller controller)
        {
            PXR_Plugin.Controller.UPxr_SetControllerMainInputHandle((UInt32)controller);
        }

        /// <summary>
        /// Sets controller vibration, including vibration amplitude and duration.
        /// </summary>
        /// <param name="strength">Vibration amplitude. The valid value ranges from `0` to `1`. The greater the value, the stronger the vibration amplitude. To stop controller vibration, call this function again and set this parameter to `0`.</param>
        /// <param name="time">Vibration duration. The valid value ranges from `0` to `65535` (in milliseconds).</param>
        /// <param name="controller">The controller to set vibration for: `0`-left controller; `1`-right controller.</param>
        public static void SetControllerVibration(float strength, int time, Controller controller)
        {
            PXR_Plugin.Controller.UPxr_SetControllerVibration((UInt32)controller, strength, time);
        }

        /// <summary>
        /// Gets the device model.
        /// </summary>
        /// <returns>The device model. Enumerations: `G2`, `Neo2`, `Neo3`, `NewController`.</returns> 
        public static ControllerDevice GetControllerDeviceType()
        {
            return (ControllerDevice)PXR_Plugin.Controller.UPxr_GetControllerType();
        }

        /// <summary>
        /// Gets controller's connection status.
        /// </summary>
        /// <param name="controller">The controller to get the connection status for: `0`-left controller; `1`-right controller.</param>
        /// <returns>The connection status of the specified controller: `true`-connected; `false`-not connected.</returns>
        public static bool IsControllerConnected(Controller controller)
        {
            var state = false;
            switch (controller)
            {
                case Controller.LeftController:
                    InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(PXR_Usages.controllerStatus, out state);
                    return state;
                case Controller.RightController:
                    InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(PXR_Usages.controllerStatus, out state);
                    return state;
            }
            return state;
        }

        /// <summary>
        /// Sets the offset of the controller's display position to its real position.
        /// </summary>
        /// <param name="hand">The controller to set an offset for: `0`-left controller; `1`-right controller.</param>
        /// <param name="offset">The offset (in meters).</param>
        public static void SetControllerOriginOffset(Controller controller, Vector3 offset)
        {
            PXR_Plugin.Controller.UPxr_SetControllerOriginOffset((int)controller, offset);
        }

        /// <summary>
        /// Gets the predicted rotation of a specified controller after a specified time.
        /// </summary>
        /// <param name="hand">The controller to get the predicted rotation for: `0`-left controller; `1`-right controller.</param>
        /// <param name="predictTime">The time for prediction (in milliseconds).</param>
        /// <returns>The predicted rotation value.</returns>
        public static Quaternion GetControllerPredictRotation(Controller controller, double predictTime)
        {
            PxrControllerTracking pxrControllerTracking = new PxrControllerTracking();
            float[] headData = new float[7] { 0, 0, 0, 0, 0, 0, 0 };

            PXR_Plugin.Controller.UPxr_GetControllerTrackingState((uint)controller, predictTime,headData, ref pxrControllerTracking);

            return new Quaternion(
                pxrControllerTracking.localControllerPose.pose.orientation.x,
                pxrControllerTracking.localControllerPose.pose.orientation.y, 
                pxrControllerTracking.localControllerPose.pose.orientation.z,
                pxrControllerTracking.localControllerPose.pose.orientation.w);
        }

        /// <summary>
        /// Gets the predicted position of a specified controller after a specified time.
        /// </summary>
        /// <param name="hand">The controller to get the predicted position for: `0`-left controller; `1`-right controller.</param>
        /// <param name="predictTime">The time for prediction (in milliseconds).</param>
        /// <returns>The predicted position value.</returns>
        public static Vector3 GetControllerPredictPosition(Controller controller, double predictTime)
        {
            PxrControllerTracking pxrControllerTracking = new PxrControllerTracking();
            float[] headData = new float[7] { 0, 0, 0, 0, 0, 0, 0 };

            PXR_Plugin.Controller.UPxr_GetControllerTrackingState((uint)controller, predictTime, headData, ref pxrControllerTracking);

            return new Vector3(
                pxrControllerTracking.localControllerPose.pose.position.x, 
                pxrControllerTracking.localControllerPose.pose.position.y,
                pxrControllerTracking.localControllerPose.pose.position.z);
        }

        public static int SetControllerEnableKey(bool isEnable, PxrControllerKeyMap Key) {
            return PXR_Plugin.Controller.UPxr_SetControllerEnableKey(isEnable, Key);
        }
    }
}

