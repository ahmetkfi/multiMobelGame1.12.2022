using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineMB : MonoBehaviour
{
    public IState CurrentState{get; private set;}
    public IState _prevState;
    bool _inTransition = false;

    public void ChangeState(IState newState)
    {
        if(CurrentState == newState || _inTransition)
            return;

        ChangeStateRoutine(newState);
    }

    public void RevertState()
    {
        if(_prevState != null)
            ChangeState(_prevState);
    }
    void ChangeStateRoutine(IState newState)
    {
        _inTransition = true;

        if(CurrentState !=null)
            CurrentState.Exit();

        if(_prevState!=null)
            _prevState=CurrentState;

        CurrentState=newState;

        if(CurrentState!=null)
            CurrentState.Enter();

        _inTransition=false;
    }

    private void Update() {
        if (CurrentState != null && !_inTransition)
            CurrentState.Tick();
    }

    private void FixedUpdate() {
        if (CurrentState != null && !_inTransition)
            CurrentState.FixedTick();
    }
}