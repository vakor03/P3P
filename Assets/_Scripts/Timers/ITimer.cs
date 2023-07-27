using System;

namespace _Scripts.Timers
{
    public interface ITimer
    {
        event Action OnTimeElapsed;
        float LoopTime { get; }
        bool IsStarted { get; }
        void Start();
        void Stop();
    }
}