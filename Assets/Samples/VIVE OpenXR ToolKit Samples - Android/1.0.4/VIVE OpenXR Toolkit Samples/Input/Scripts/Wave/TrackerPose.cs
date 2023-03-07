// "Wave SDK 
// © 2020 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the Wave SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

namespace Wave.OpenXR.Samples.OpenXRInput
{
    public class TrackerPose : MonoBehaviour
    {
        const string LOG_TAG = "Wave.OpenXR.Samples.OpenXRInput.TrackerPose";
        void DEBUG(string msg) { Debug.Log(LOG_TAG + " " + (IsLeft ? "Left" : "Right") + ", " + msg); }
        void INTERVAL(string msg) { if (printIntervalLog) { DEBUG(msg); } }

        #region Inspector
        public bool IsLeft = false;

        [SerializeField]
        private InputActionReference m_DevicePose = null;
        public InputActionReference DevicePose { get { return m_DevicePose; } set { m_DevicePose = value; } }
        bool getDeviceTracked(InputActionReference actionReference)
        {
            bool tracked = false;

            if (OpenXRHelper.VALIDATE(actionReference, out string value))
            {
                if (actionReference.action.activeControl.valueType == typeof(UnityEngine.InputSystem.XR.PoseState))
                {
                    tracked = actionReference.action.ReadValue<UnityEngine.InputSystem.XR.PoseState>().isTracked;
                    INTERVAL("getDeviceTracked(" + tracked + ")");
                }
            }
            else
            {
                INTERVAL("getDeviceTracked() invalid input: " + value);
            }

            return tracked;
        }
        InputTrackingState getDeviceTrackingState(InputActionReference actionReference)
        {
            InputTrackingState state = InputTrackingState.None;

            if (OpenXRHelper.VALIDATE(actionReference, out string value))
            {
                if (actionReference.action.activeControl.valueType == typeof(UnityEngine.InputSystem.XR.PoseState))
                {
                    state = actionReference.action.ReadValue<UnityEngine.InputSystem.XR.PoseState>().trackingState;
                    INTERVAL("getDeviceTrackingState(" + state + ")");
                }
            }
            else
            {
                INTERVAL("getDeviceTrackingState() invalid input: " + value);
            }

            return state;
        }
        Vector3 getDevicePosition(InputActionReference actionReference)
        {
            var position = Vector3.zero;

            if (OpenXRHelper.VALIDATE(actionReference, out string value))
            {
                if (actionReference.action.activeControl.valueType == typeof(UnityEngine.InputSystem.XR.PoseState))
                {
                    position = actionReference.action.ReadValue<UnityEngine.InputSystem.XR.PoseState>().position;
                    INTERVAL("getDevicePosition(" + position.x.ToString() + ", " + position.y.ToString() + ", " + position.z.ToString() + ")");
                }
            }
            else
            {
                INTERVAL("getDevicePosition() invalid input: " + value);
            }

            return position;
        }
        Quaternion getDeviceRotation(InputActionReference actionReference)
        {
            var rotation = Quaternion.identity;

            if (OpenXRHelper.VALIDATE(actionReference, out string value))
            {
                if (actionReference.action.activeControl.valueType == typeof(UnityEngine.InputSystem.XR.PoseState))
                {
                    rotation = actionReference.action.ReadValue<UnityEngine.InputSystem.XR.PoseState>().rotation;
                    INTERVAL("getDeviceRotation(" + rotation.x.ToString() + ", " + rotation.y.ToString() + ", " + rotation.z.ToString() + ", " + rotation.w.ToString() + ")");
                }
            }
            else
            {
                INTERVAL("getDeviceRotation() invalid input: " + value);
            }

            return rotation;
        }

        [SerializeField]
        private InputActionReference m_PrimaryButton = null;
        public InputActionReference PrimaryButton { get { return m_PrimaryButton; } set { m_PrimaryButton = value; } }

        [SerializeField]
        private InputActionReference m_Menu = null;
        public InputActionReference Menu { get { return m_Menu; } set { m_Menu = value; } }
        bool getButton(InputActionReference actionReference)
        {
            bool pressed = false;

            if (OpenXRHelper.VALIDATE(actionReference, out string value))
            {
                if (actionReference.action.activeControl.valueType == typeof(bool))
                    pressed = actionReference.action.ReadValue<bool>();
                if (actionReference.action.activeControl.valueType == typeof(float))
                    pressed = actionReference.action.ReadValue<float>() > 0;
            }
            else
            {
                INTERVAL("getButton() invalid input: " + value);
            }

            return pressed;
        }
        #endregion

        int printFrame = 0;
        protected bool printIntervalLog = false;
        private void Update()
        {
            printFrame++;
            printFrame %= 300;
            printIntervalLog = (printFrame == 0);

            var tracked = getDeviceTracked(m_DevicePose);
            var trackingState = getDeviceTrackingState(m_DevicePose);
            var position = getDevicePosition(m_DevicePose);
            var rotation = getDeviceRotation(m_DevicePose);

            if (getButton(m_PrimaryButton))
                DEBUG("Update() " + m_PrimaryButton.name + " is pressed.");
            if (getButton(m_Menu))
                DEBUG("Update() " + m_Menu.name + " is pressed.");

            if (tracked)
            {
                transform.localPosition = position;
                transform.localRotation = rotation;
            }
            else
            {
                if (printIntervalLog)
                    DEBUG("Update() Tracker is not tracked.");
            }
        }
    }
}
