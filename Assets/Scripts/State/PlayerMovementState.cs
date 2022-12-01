using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : IState
{
    PlayerSM _playerSM;
    Animator _animator;
    Rigidbody _rb;
    FixedJoystick _moveJoystick;
    FixedJoystick _aimJoystick;
    Transform _transformPlayer;
    Transform _cameraTransform;
    Vector3 _moveDirection;
    Vector3 _aimDirection;
    private VoidEvent _moveTapEvent;
    private VoidEvent _moveUntapEvent;
    private VoidEvent _aimTapEvent;
    private VoidEvent _aimUntapEvent;
    private PlayerValuesSO _playerValuesSo;
    private VoidEvent _shootBulletEvent;
    float _fireRate = 0.5F;
    float _nextFire = 0.0F;
    public float turnSpeed = 0.2f;
    private Quaternion rotGoal;
    private bool isShoot=false;
    public PlayerMovementState(PlayerSM playerSM,Animator animator,Rigidbody rb,FixedJoystick moveJoystick,FixedJoystick aimJoystick,
        Transform transformPlayer,Transform cameraTransform,Vector3 moveDirection,Vector3 aimDirection,VoidEvent moveTapEvent,VoidEvent moveUntapEvent,float fireRate,
        float nextFire,VoidEvent shootBulletEvent,PlayerValuesSO playerValuesSo,VoidEvent aimTapEvent,VoidEvent aimUntapEvent)
    {
        _playerSM=playerSM;
        _animator=animator;
        _rb = rb;
        _moveJoystick = moveJoystick;
        _aimJoystick = aimJoystick;
        _transformPlayer = transformPlayer;
        _cameraTransform = cameraTransform;
        _moveDirection = moveDirection;
        _aimDirection = aimDirection;
        _moveTapEvent = moveTapEvent;
        _moveUntapEvent = moveUntapEvent;
        _aimTapEvent = aimTapEvent;
        _aimUntapEvent = aimUntapEvent;
        _shootBulletEvent = shootBulletEvent;
        _playerValuesSo = playerValuesSo;
        _fireRate = fireRate;
        _nextFire = nextFire;
    }
    public void Enter()
    {
        Debug.Log("STATE CHANGE - Movement");
        _moveTapEvent.Raise();
    }

    public void Tick()
    {
        
    }

    public void FixedTick()
    {
        bool _isRunning = _animator.GetBool("isRun");
        if(_moveJoystick.Horizontal!= 0 || _moveJoystick.Vertical != 0)
        {
            GetValue();
            _moveTapEvent.Raise();
            _rb.velocity=_moveDirection*_playerValuesSo.moveSpeed;

            if (!_isRunning)
            {
                _animator.SetFloat("moveJoyHoriztontal",_moveJoystick.Horizontal);
                _animator.SetFloat("moveJoyVertical",_moveJoystick.Vertical);
            }
            
            if (_rb.velocity != Vector3.zero && !isShoot)
            {
                rotGoal = Quaternion.LookRotation(_moveDirection);
                _transformPlayer.rotation = Quaternion.Slerp(_transformPlayer.rotation,rotGoal,turnSpeed);
            }
        }
        else
        {
            _rb.velocity= Vector3.zero;
            _moveUntapEvent.Raise();
        }
        if ((_aimJoystick.Horizontal >= 0.3f || _aimJoystick.Horizontal <= -0.3f) || (_aimJoystick.Vertical >= 0.3f || _aimJoystick.Vertical <= -0.3f))
        {
            GetValueAim();
            isShoot = true;
            _aimTapEvent.Raise();
            _playerValuesSo.moveSpeed = 2f;
            rotGoal = Quaternion.LookRotation(_aimDirection);
            _transformPlayer.rotation = Quaternion.Slerp(_transformPlayer.rotation,rotGoal,turnSpeed);
            _animator.SetBool("isShoot",true);
            _animator.SetFloat("moveJoyHoriztontal",_moveJoystick.Horizontal);
            _animator.SetFloat("moveJoyVertical",_moveJoystick.Vertical);

            AnimationMoveController();
        }
        else
        {
            isShoot = false;
            _playerValuesSo.moveSpeed = 5f;
            _aimUntapEvent.Raise();
        }
        
        if(_moveJoystick.Horizontal ==0 && _moveJoystick.Vertical==0 && _aimJoystick.Horizontal==0 && _aimJoystick.Vertical==0) 
        {
            _playerSM.ChangeStateToIdle();
        }
    }
    
    private void AnimationMoveController()
    {
        if (_aimJoystick.Direction.x > 0 && _aimJoystick.Direction.y > 0)
        {
            _animator.SetFloat("moveJoyHoriztontal", (_moveJoystick.Horizontal));
            _animator.SetFloat("moveJoyVertical", (_moveJoystick.Vertical));
        }
        else if (_aimJoystick.Direction.x > 0 && _aimJoystick.Direction.y < 0)
        {
            _animator.SetFloat("moveJoyHoriztontal", _moveJoystick.Horizontal);
            _animator.SetFloat("moveJoyVertical", _moveJoystick.Vertical);
        }
        else if (_aimJoystick.Direction.x < 0 && _aimJoystick.Direction.y < 0)
        {
            _animator.SetFloat("moveJoyHoriztontal", _moveJoystick.Horizontal);
            _animator.SetFloat("moveJoyVertical", _moveJoystick.Vertical);
        }
        else if (_aimJoystick.Direction.x < 0 && _aimJoystick.Direction.y > 0)
        {
            _animator.SetFloat("moveJoyHoriztontal", -1 * (_moveJoystick.Vertical));
            _animator.SetFloat("moveJoyVertical", -1 * (_moveJoystick.Horizontal));
        }
        else if (_aimJoystick.Direction.x == 1 && _aimJoystick.Direction.y == 0)
        {
            _animator.SetFloat("moveJoyHoriztontal", _moveJoystick.Horizontal);
            _animator.SetFloat("moveJoyVertical", _moveJoystick.Vertical);
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
