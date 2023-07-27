// using System;
// using System.Collections;
// using System.Collections.Generic;
// using _Scripts.Helpers;
// using MEC;
// using UnityEngine;
//
// namespace _Scripts.Timers
// {
//     public class TickTimer : ITickTimer
//     {
//         private float _currentTime;
//         private Coroutine _timerRoutine;
//
//         public event Action OnTimerChanged;
//         public event Action OnTimeElapsed;
//         public float LoopTime { get; }
//         public bool IsStarted { get; private set; }
//
//         public float NormalizedTime => _currentTime / LoopTime;
//
//         public TickTimer(float loopTime)
//         {
//             LoopTime = loopTime;
//         }
//
//         public void Start()
//         {
//             IsStarted = true;
//             _currentTime = 0f;
//             _timerRoutine = Coroutines.StartRoutine(TimerRoutine());
//         }
//
//         public void Stop()
//         {
//             IsStarted = false;
//             Coroutines.StopRoutine(_timerRoutine);
//         }
//
//
//         private IEnumerator<float> TimerRoutine()
//         {
//             while (true)
//             {
//                 _currentTime += Time.deltaTime;
//                 OnTimerChanged?.Invoke();
//
//                 if (_currentTime >= LoopTime)
//                 {
//                     OnTimeElapsed?.Invoke();
//                     _currentTime -= LoopTime;
//                 }
//
//                 yield return Timing.WaitForOneFrame;
//             }
//         }
//     }
// }