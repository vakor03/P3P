using System;

namespace _Scripts.Units.Players
{
    public interface IJumpRadiusController
    {
        float JumpRadius { get; }
        int JumpNumber { get; }
        event Action OnJumpRadiusChanged;
    }
}