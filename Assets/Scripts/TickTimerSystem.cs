using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickTimerSystem : MonoBehaviour
{

    public  class OnTickEventArgs : EventArgs{
        public int tick;
    }
    public static event EventHandler<OnTickEventArgs> OnTick;
    public static event EventHandler<OnTickEventArgs> OnTick_5;

    private const float TICK_TIMER_MAX = .2f;

    private int tick;
    private float tickTimer;

    private void Awake() {
        tick = 0;
    }

    private void Update() {
        tickTimer += Time.deltaTime;
        if(tickTimer >= TICK_TIMER_MAX){
            tickTimer -= TICK_TIMER_MAX;
            tick++;
            OnTick?.Invoke(this, new OnTickEventArgs{tick = tick});

            if(tick % 5 == 0){
                OnTick_5?.Invoke(this, new OnTickEventArgs{tick = tick});
            }
        }
    }

}
