using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private FixedJoystick _joystick;
    public float _moveSpeed;
    [SerializeField] private Transform _transformPlayer;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform cameraTransform;
    public Vector3 moveDirection;
    private void FixedUpdate()
    {
        bool _isRunning = _animator.GetBool("isRun");
        GetValue();
        _rb.velocity=moveDirection*_moveSpeed;

        if(_joystick.Horizontal!= 0 || _joystick.Vertical != 0)
        {
            if(!_isRunning)
                _animator.SetBool("isRun",true);

            if (_rb.velocity != Vector3.zero)
            {
                _transformPlayer.rotation = Quaternion.LookRotation(_rb.velocity);
            }
            
        } 
        else 
        {
            if(_isRunning)
                _animator.SetBool("isRun",false);
        }
    }

    public void GetValue()
    {
        //Get the input direction
        float inputX = _joystick.Horizontal;
        float inputY = _joystick.Vertical;
        Vector3 inputDirection = new Vector3(inputX, 0, inputY);

        //Get the camera horizontal rotation
        Vector3 faceDirection = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z);

        //Get the angle between world forward and camera
        float cameraAngle = Vector3.SignedAngle(Vector3.forward, faceDirection, Vector3.up);

        //Finally rotate the input direction horizontally by the cameraAngle
        moveDirection = Quaternion.Euler(0, cameraAngle, 0) * inputDirection;
    }
}
