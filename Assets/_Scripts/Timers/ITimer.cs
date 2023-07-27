#region

using System;

#endregion

namespace _Scripts.Timers
{
    public interface ITimer
    {
        float LoopTime { get; }
        bool IsStarted { get; }
        event Action OnTimeElapsed;
        void Start();
        void Stop();
    }
}