using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_IdleBehaviour : MonoBehaviour
{
    public event EventHandler OnRecoveryHealth;

    [SerializeField,Range(-3f,3f)] private float recoverySpeed;
    [SerializeField] private float idleTimeMax;
    [SerializeField] private float recoveryTimerMax = 5f;

    private float idleTime;
    private float recoveryTime;
    private AI_MainCore ai_MainCore;
    private void Start ( )
    {
        ai_MainCore = GetComponent<AI_MainCore>();
    }
    private void Update ( )
    {
        StopIdle();
    }
    private void Recovering ( )
    {
        recoveryTime += Time.deltaTime * recoverySpeed;
        if( recoveryTime > recoveryTimerMax)
        {
            recoveryTime = 0;
            //Recuperacion
            OnRecoveryHealth?.Invoke(this, EventArgs.Empty);
        }
    }
    private void ResetTimers ( )
    {
        recoveryTime = 0;
        idleTime = 0;
    }
    private void StopIdle ( )
    {
        idleTime += Time.deltaTime;
        if( idleTime > idleTimeMax)
        {
            idleTime = 0;
            ai_MainCore.SetState(State.Patrol);
        }
    }
}
