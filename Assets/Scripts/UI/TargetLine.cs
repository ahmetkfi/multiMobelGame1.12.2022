using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLine : MonoBehaviour
{
    [SerializeField] private Transform arrowIconTransform;
    [SerializeField] FixedJoystick _aimJoystick;
    [SerializeField] Transform _cameraTransform;
    Vector3 _aimDirection;

    void FixedUpdate()
    {
        GetValueAim();
        if(_aimDirection !=Vector3.zero)
            arrowIconTransform.rotation = Quaternion.LookRotation(_aimDirection);
    }
    
    
    public void GetValueAim()
    {
        //Get the input direction
        float inputX = _aimJoystick.Horizontal;
        float inputY = _aimJoystick.Vertical;
        Vector3 inputDirection = new Vector3(inputX, 0, inputY);

        //Get the camera horizontal rotation
        Vector3 faceDirection = new Vector3(_cameraTransform.forward.x, 0, _cameraTransform.forward.z);

        //Get the angle between world forward and camera
        float cameraAngle = Vector3.SignedAngle(Vector3.forward, faceDirection, Vector3.up);

        //Finally rotate the input direction horizontally by the cameraAngle
        _aimDirection = Quaternion.Euler(0, cameraAngle, 0) * inputDirection;
    }
}