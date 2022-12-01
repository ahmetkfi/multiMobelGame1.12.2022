using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerSM : StateMachineMB
{
    [SerializeField] Animator _animator;
    [SerializeField] Rigidbody _rb;
    [SerializeField] private PlayerValuesSO _playerValuesSo;
    [SerializeField] private VoidEvent _shootBulletEvent;
    [SerializeField] private VoidEvent _moveTapEvent;
    [SerializeField] private VoidEvent _moveUntapEvent;
    [SerializeField] private VoidEvent _aimTapEvent;
    [SerializeField] private VoidEvent _aimUntapEvent;
    [SerializeField] private FixedJoystick _moveJoystick;
    [SerializeField] private FixedJoystick _aimJoystick;
    [SerializeField] private Transform _transformPlayer;
    [SerializeField] private Transform _cameraTransform;
    public Vector3 _moveDirection;
    public float _fireRate = 0.5F;
    private float _nextFire = 0.0F;
    public GameObject _bullet;
    public Transform _firePoint;
    public float _fireSpeed;
    public Vector3 _aimDirection;
    
    public PlayerIdleState IdleState{get; private set;}
    public PlayerMovementState MovementState{get; private set;}


    private void Awake() {
        IdleState = new PlayerIdleState(this,_animator,_rb,_moveJoystick,_aimJoystick,_cameraTransform);
        MovementState = new PlayerMovementState(this,_animator,_rb,_moveJoystick,_aimJoystick,_transformPlayer,_cameraTransform,
            _moveDirection,_aimDirection,_moveTapEvent,_moveUntapEvent,_fireRate,_nextFire,_shootBulletEvent,_playerValuesSo,_aimTapEvent,_aimUntapEvent);
    }

    private void Start() 
    {
        ChangeState(IdleState);
    }
    public void ChangeStateToMovement()
    {
        ChangeState(MovementState);
    }
    public void ChangeStateToIdle()
    {
        ChangeState(IdleState);
    }

}
