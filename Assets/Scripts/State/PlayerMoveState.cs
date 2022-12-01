using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : IState
{
    PlayerSM _playerSM;
    Animator _animator;
    Rigidbody _rb;
    FixedJoystick _moveJoystick;
    Transform _transformPlayer;
    Transform _cameraTransform;
    float _moveSpeed;
    Vector3 _moveDirection;
    private VoidEvent _moveTapEvent;
    private VoidEvent _moveUntapEvent;
    
    public PlayerMoveState(PlayerSM playerSM,Animator animator,Rigidbody rb,FixedJoystick moveJoystick,
        Transform transformPlayer,Transform cameraTransform,float moveSpeed,Vector3 moveDirection,VoidEvent moveTapEvent,VoidEvent moveUntapEvent)
    {
        _playerSM=playerSM;
        _animator=animator;
        _rb = rb;
        _moveJoystick = moveJoystick;
        _transformPlayer = transformPlayer;
        _cameraTransform = cameraTransform;
        _moveSpeed = moveSpeed;
        _moveDirection = moveDirection;
        _moveTapEvent = moveTapEvent;
        _moveUntapEvent = moveUntapEvent;
    }
    public void Enter()
    {
        Debug.Log("STATE CHANGE - Move");
        _animator.SetBool("isRun",true);
        _moveTapEvent.Raise();
    }

    public void Tick()
    {
        
    }

    public void FixedTick()
    {
        GetValue();
        _rb.velocity=_moveDirection*_moveSpeed;

        if(_moveJoystick.Horizontal!= 0 || _moveJoystick.Vertical != 0)
        {
            if (_rb.velocity != Vector3.zero)
            {
                _transformPlayer.rotation = Quaternion.LookRotation(_rb.velocity);
            }
            
        } 
        else 
        {
            _moveUntapEvent.Raise();
            _playerSM.ChangeStateToIdle();
        }
    }

    public void Exit()
    {
        _animator.SetBool("isRun",false);
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
