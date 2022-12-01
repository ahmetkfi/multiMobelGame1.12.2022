using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowIcon : MonoBehaviour
{
    [SerializeField] private Transform arrowIconTransform;
    [SerializeField] FixedJoystick _moveJoystick;
    [SerializeField] Transform _cameraTransform;
    Vector3 _moveDirection;

    void FixedUpdate()
    {
        GetValue();
        if(_moveDirection !=Vector3.zero)
            arrowIconTransform.rotation = Quaternion.LookRotation(_moveDirection);
    }
    
    
    public void GetValue()
    {
        //Get the input direction
        float inputX = _moveJoystick.Horizontal;
        float inputY = _moveJoystick.Vertical;
        Vector3 inputDirection = new Vector3(inputX, 0, inputY);

        //Get the camera horizontal rotation
        Vector3 faceDirection = new Vector3(_cameraTransform.forward.x, 0, _cameraTransform.forward.z);

        //Get the angle between world forward and camera
        float cameraAngle = Vector3.SignedAngle(Vector3.forward, faceDirection, Vector3.up);

        //Finally rotate the input direction horizontally by the cameraAngle
        _moveDirection = Quaternion.Euler(0, cameraAngle, 0) * inputDirection;
    }
}
