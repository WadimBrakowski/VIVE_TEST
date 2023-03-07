using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Wave.OpenXR.Samples.Hand
{
    public class HandPinchPose : MonoBehaviour
    {
        [SerializeField] InputActionAsset ActionAsset;
        [SerializeField] InputActionReference RightPinchR;
        [SerializeField] InputActionReference LeftPinchR;

        public GameObject RightPinchPoseGObj;
        public GameObject LeftPinchPoseGObj;
        void OnEnable()
        {
            if (ActionAsset != null)
            {
                ActionAsset.Enable();
            }
        }

        void Start()
        {

        }


        void Update()
        {
            LeftPinchPoseGObj.transform.localPosition = Get_AimPosition(LeftPinchR);
            LeftPinchPoseGObj.transform.localRotation = Get_AimRotation(LeftPinchR);
            RightPinchPoseGObj.transform.localPosition = Get_AimPosition(RightPinchR);
            RightPinchPoseGObj.transform.localRotation = Get_AimRotation(RightPinchR);
        }

        Vector3 Get_AimPosition(InputActionReference _ActionReference)
        {
            Vector3 _Position = Vector3.zero;
            _Position = _ActionReference.action.ReadValue<UnityEngine.InputSystem.XR.PoseState>().position;
            return _Position;
        }
        Quaternion Get_AimRotation(InputActionReference _ActionReference)
        {
            Quaternion _Rotation = Quaternion.identity;
            _Rotation = _ActionReference.action.ReadValue<UnityEngine.InputSystem.XR.PoseState>().rotation;
            return _Rotation;
        }
    }
}
