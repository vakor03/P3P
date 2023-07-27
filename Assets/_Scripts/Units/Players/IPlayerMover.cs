#region

using System;

#endregion

namespace _Scripts.Units.Players
{
    public interface IPlayerMover
    {
        event Action OnJumpStarted;
        event Action OnJumpFinished;
    }
}