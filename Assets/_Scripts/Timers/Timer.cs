#region

using System;
using System.Collections.Generic;
using _Scripts.Helpers;
using MEC;
using UnityEngine;

#endregion

namespace _Scripts.Timers
{
    public class Timer : ITimer, IDisposable
    {
        private CoroutineHandle _timerRoutine;

        public Timer(float loopTime)
        {
            LoopTime = loopTime;
        }

        public void Dispose()
        {
            if (IsStarted)
            {
                Coroutines.StopRoutineMEC(_timerRoutine);
            }
        }

        public event Action OnTimeElapsed;


        public float LoopTime { get; private set; }
        public bool IsStarted { get; private set; }

        public void Start()
        {
            if (IsStarted)
            {
                Debug.LogError("Timer is already started");
            }

            IsStarted = true;
            _timerRoutine = Coroutines.StartRoutineMEC(TimerRoutine());
        }

        public void Stop()
        {
            if (!IsStarted)
            {
                return;
            }

            IsStarted = false;
            Coroutines.StopRoutineMEC(_timerRoutine);
        }

        private IEnumerator<float> TimerRoutine()
        {
            while (true)
            {
                yield return Timing.WaitForSeconds(LoopTime);
                OnTimeElapsed?.Invoke();
            }
        }
    }
}