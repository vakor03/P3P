﻿using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace _Scripts
{
    public class PlayerVisuals : MonoBehaviour
    {
        private Transform _transform;
        private void Awake()
        {
            _transform = transform;
        }
        private void Start()
        {
            Player.Instance.OnJumpStarted += PlayerOnJumpStarted;
        }

        private void PlayerOnJumpStarted()
        {
            StartCoroutine(PlayerRotate());
        }

        private IEnumerator PlayerRotate()
        {
            float t = 0;
            float jumpDuration = Player.Instance.JumpDuration;
            Quaternion startRotation = _transform.rotation;
            
            Quaternion startRotationLocal = quaternion.Euler(0, 0, 0);
            Quaternion finalRotationLocal = quaternion.Euler(new Vector3(-1f,-1f,0) * (180 * Mathf.Deg2Rad));

            while (t < jumpDuration)
            {
                t += Time.deltaTime;
                var tNorm = Mathf.Clamp01(t / jumpDuration);

                var rotation = Quaternion.Lerp(startRotationLocal, finalRotationLocal, tNorm);

                _transform.rotation = rotation;
                yield return null;
            }

            _transform.rotation = startRotation;
        }
    }
}