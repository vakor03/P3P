#region

using System.Collections.Generic;
using MEC;
using Unity.Mathematics;
using UnityEngine;

#endregion

namespace _Scripts.Animations
{
    public class RotateAnimation : IAnimation
    {
        private readonly float _duration;
        private readonly Transform _transform;

        private bool _animationRunning;
        private CoroutineHandle _currentAnimationRoutine;

        public RotateAnimation(Transform transform, float duration)
        {
            _transform = transform;
            _duration = duration;
        }

        public void PerformAnimation()
        {
            if (_animationRunning)
            {
                Debug.LogError($"{typeof(RotateAnimation)} is already running");
            }
            
            _currentAnimationRoutine = Timing.RunCoroutine(RotateAnimationRoutine());
        }

        public void StopAnimation()
        {
            if (!_animationRunning)
            {
                return;
            }

            Timing.KillCoroutines(_currentAnimationRoutine);
        }

        private IEnumerator<float> RotateAnimationRoutine()
        {
            _animationRunning = true;
            
            float t = 0;
            float jumpDuration = _duration;
            Quaternion startRotation = _transform.rotation;

            Quaternion a = new Quaternion(1, 1, 0, 0);
            a.Normalize();
            Quaternion startRotationLocal = quaternion.Euler(0, 0, 0);
            // Quaternion finalRotationLocal = quaternion.Euler(new Vector3(-1f,-1f,0) * (180 * Mathf.Deg2Rad));
            Quaternion finalRotationLocal = a;
            while (t < jumpDuration)
            {
                t += Timing.DeltaTime;
                var tNorm = Mathf.Clamp01(t / jumpDuration);

                var rotation = Quaternion.Slerp(startRotationLocal, finalRotationLocal, tNorm);

                _transform.rotation = rotation;
                yield return Timing.WaitForOneFrame;
            }

            _transform.rotation = startRotation;

            _animationRunning = false;
        }
    }
}