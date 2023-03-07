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
using UnityEngine.UI;
using UnityEngine.XR;

namespace Wave.OpenXR.Samples.OpenXRInput
{
    [RequireComponent(typeof(Text))]
    public class EyeDataText : MonoBehaviour
    {
        const string LOG_TAG = "Wave.OpenXR.Samples.OpenXRInput.EyeDataText";
        void DEBUG(string msg) { Debug.Log(LOG_TAG + msg); }
        void INTERVAL(string msg) { if (printIntervalLog) { DEBUG(msg); } }

        [SerializeField]
        private InputActionReference m_EyePose = null;
        public InputActionReference EyePose { get => m_EyePose; set => m_EyePose = value; }
        bool getTracked(InputActionReference actionReference)
        {
            bool tracked = false;

            if (OpenXRHelper.VALIDATE(actionReference, out string value))
            {
                if (actionReference.action.activeControl.valueType == typeof(UnityEngine.InputSystem.XR.PoseState))
                {
                    tracked = actionReference.action.ReadValue<UnityEngine.InputSystem.XR.PoseState>().isTracked;
                    INTERVAL("getTracked(" + tracked + ")");
                }
            }
            else
            {
                INTERVAL("getTracked() invalid input: " + value);
            }

            return tracked;
        }
        InputTrackingState getTrackingState(InputActionReference actionReference)
        {
            InputTrackingState state = InputTrackingState.None;

            if (OpenXRHelper.VALIDATE(actionReference, out string value))
            {
                if (actionReference.action.activeControl.valueType == typeof(UnityEngine.InputSystem.XR.PoseState))
                {
                    state = actionReference.action.ReadValue<UnityEngine.InputSystem.XR.PoseState>().trackingState;
                    INTERVAL("getTrackingState(" + state + ")");
                }
            }
            else
            {
                INTERVAL("getTrackingState() invalid input: " + value);
            }

            return state;
        }
        Vector3 getPosition(InputActionReference actionReference)
        {
            var position = Vector3.zero;

            if (OpenXRHelper.VALIDATE(actionReference, out string value))
            {
                if (actionReference.action.activeControl.valueType == typeof(UnityEngine.InputSystem.XR.PoseState))
                {
                    position = actionReference.action.ReadValue<UnityEngine.InputSystem.XR.PoseState>().position;
                    INTERVAL("getPosition(" + position.x.ToString() + ", " + position.y.ToString() + ", " + position.z.ToString() + ")");
                }
            }
            else
            {
                INTERVAL("getPosition() invalid input: " + value);
            }

            return position;
        }
        Quaternion getRotation(InputActionReference actionReference)
        {
            var rotation = Quaternion.identity;

            if (OpenXRHelper.VALIDATE(actionReference, out string value))
            {
                if (actionReference.action.activeControl.valueType == typeof(UnityEngine.InputSystem.XR.PoseState))
                {
                    rotation = actionReference.action.ReadValue<UnityEngine.InputSystem.XR.PoseState>().rotation;
                    INTERVAL("getRotation(" + rotation.x.ToString() + ", " + rotation.y.ToString() + ", " + rotation.z.ToString() + ", " + rotation.w.ToString() + ")");
                }
            }
            else
            {
                INTERVAL("getRotation() invalid input: " + value);
            }

            return rotation;
        }

        private Text m_Text = null;

        private void Awake()
        {
            m_Text = GetComponent<Text>();
        }

        int printFrame = 0;
        private bool printIntervalLog = false;
        private void Update()
        {
            printFrame++;
            printFrame %= 300;
            printIntervalLog = (printFrame == 0);

            if (m_Text == null) { return; }

            m_Text.text = "Eye ";

            bool tracked = getTracked(m_EyePose);
            m_Text.text += "tracked: " + tracked + "\n";

            InputTrackingState trackingState = getTrackingState(m_EyePose);
            m_Text.text += "tracking state: " + trackingState + "\n";

            Vector3 position = getPosition(m_EyePose);
            m_Text.text += "position (" + position.x.ToString() + ", " + position.y.ToString() + ", " + position.z.ToString() + ")\n";
        }
    }
}
