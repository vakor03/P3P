﻿using System;

namespace _Scripts.Timers
{
    public interface ITickTimer : ITimer
    {
        float NormalizedTime { get; }
        event Action OnTimerChanged;
    }
}