using System;
using Random = UnityEngine.Random;

namespace _Scripts.Units.Players
{
    public class JumpRadiusController : IJumpRadiusController
    {
        private readonly float _jumpRadiusModifier;
        private readonly float _radiusOffset;

        private readonly int _stepsCount = 6;

        public JumpRadiusController(float jumpRadiusModifier, float radiusOffset)
        {
            _jumpRadiusModifier = jumpRadiusModifier;
            _radiusOffset = radiusOffset;
        }

        public float JumpRadius { get; private set; }
        public int JumpNumber { get; private set; }
        public event Action OnJumpRadiusChanged;

        public void ChangeJumpRadius()
        {
            int oldStepsCount = JumpNumber;
            while (oldStepsCount == JumpNumber)
            {
                JumpNumber = Random.Range(1, 1 + _stepsCount);
            }

            JumpRadius = JumpNumber * _jumpRadiusModifier + _radiusOffset;

            OnJumpRadiusChanged?.Invoke();
        }

        public void SetDefaultJumpRadius()
        {
            int oldStepsCount = JumpNumber;
            while (oldStepsCount == JumpNumber)
            {
                JumpNumber = Random.Range(1, 1 + _stepsCount);
            }

            JumpRadius = JumpNumber * _jumpRadiusModifier + _radiusOffset;
        }
    }
}