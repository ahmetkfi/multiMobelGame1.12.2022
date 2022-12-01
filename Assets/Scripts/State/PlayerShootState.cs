using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootState : IState
{
    PlayerSM _playerSM;
    Animator _animator;
    Rigidbody _rb;
    FixedJoystick _moveJoystick;
    FixedJoystick _aimJoystick;
    Transform _transformPlayer;
    Transform _cameraTransform;
    float _moveSpeed;
    float _fireRate = 0.5F;
    float _nextFire = 0.0F;
    private VoidEvent _shootBulletEvent;
    Vector3 _aimDirection;
    private PlayerValuesSO _playerValuesSo;
    private VoidEvent _moveTapEvent;
    private VoidEvent _moveUntapEvent;
    private VoidEvent _aimTapEvent;
    private VoidEvent _aimUntapEvent;
    public PlayerShootState(PlayerSM playerSM,Animator animator,Rigidbody rb,FixedJoystick moveJoystick,FixedJoystick aimJoystick,
        Transform transformPlayer,Transform cameraTransform,float moveSpeed,float fireRate,
        float nextFire,Vector3 aimDirection,VoidEvent shootBulletEvent,PlayerValuesSO playerValuesSo,VoidEvent moveTapEvent,VoidEvent moveUntapEvent
        ,VoidEvent aimTapEvent,VoidEvent aimUntapEvent)
    {
        _playerSM=playerSM;
        _animator=animator;
        _rb = rb;
        _moveJoystick = moveJoystick;
        _aimJoystick = aimJoystick;
        _transformPlayer = transformPlayer;
        _cameraTransform = cameraTransform;
        _moveSpeed = moveSpeed;
        _fireRate = fireRate;
        _nextFire = nextFire;
        _aimDirection = aimDirection;
        _shootBulletEvent = shootBulletEvent;
        _playerValuesSo = playerValuesSo;
        _moveTapEvent = moveTapEvent;
        _moveUntapEvent = moveUntapEvent;
        _aimTapEvent = aimTapEvent;
        _aimUntapEvent = aimUntapEvent;
    }
    public void Enter()
    {
        _aimTapEvent.Raise();
        Debug.Log("STATE CHANGE - Shoot");
    }

    public void Tick()
    {
       
    }

    public void FixedTick()
    {
        if ((_aimJoystick.Horizontal >= 0.3f || _aimJoystick.Horizontal <= -0.3f) || (_aimJoystick.Vertical >= 0.3f || _aimJoystick.Vertical <= -0.3f))
        {
            GetValue();
            Vector3 lookAtPoint = _transformPlayer.position + _aimDirection;
            _transformPlayer.LookAt(lookAtPoint);
            _nextFire += Time.deltaTime;
            if(_nextFire>=_fireRate && _playerValuesSo.bulletCount!=0)
            {
                _nextFire = 0;
                _playerValuesSo.bulletCount--;
                _shootBulletEvent.Raise();
            }
        }
        

        if (_aimJoystick.Horizontal == 0 && _aimJoystick.Vertical == 0)
        {
            _aimUntapEvent.Raise();
            _playerSM.ChangeStateToIdle();
        }
            
    }
    
    public void GetValue()
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
    public void Exit()
    {
        
    }
}
