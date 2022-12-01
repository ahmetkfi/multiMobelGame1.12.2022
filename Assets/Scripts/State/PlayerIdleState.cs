using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IState
{
    PlayerSM _playerSM;
    Animator _animator;
    FixedJoystick _moveJoystick;
    FixedJoystick _aimJoystick;
    public PlayerIdleState(PlayerSM playerSM,Animator animator,Rigidbody rb,FixedJoystick moveJoystick,
        FixedJoystick aimJoystick,Transform cameraTransform)
    {
        _playerSM=playerSM;
        _animator=animator;
        _moveJoystick = moveJoystick;
        _aimJoystick = aimJoystick;
    }
    public void Enter()
    {
        Debug.Log("STATE CHANGE - Idle");
        _animator.SetBool("isRun",false);
        _animator.SetBool("isShoot",false);
        _animator.SetFloat("moveJoyVertical",_moveJoystick.Vertical);
        _animator.SetFloat("moveJoyHoriztontal",_moveJoystick.Horizontal);
    }

    public void Tick()
    {
        
    }

    public void FixedTick()
    {
        if((_moveJoystick.Horizontal != 0 || _moveJoystick.Vertical != 0)||(_aimJoystick.Horizontal != 0 || _aimJoystick.Vertical != 0))
        {
            _playerSM.ChangeStateToMovement();
        } 
        
        
    }

    public void Exit()
    {
        
    }
}
