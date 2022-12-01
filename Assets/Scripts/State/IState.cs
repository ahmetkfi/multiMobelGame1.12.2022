using System.Collections;
using System;
using UnityEngine;

public interface IState{
    void Enter();
    void Tick();
    void FixedTick();
    void Exit();
}