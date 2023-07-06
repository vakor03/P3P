using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace _Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float speed;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        [SerializeField] private float jumpDuration = 5f;
        [SerializeField] private float jumpHeight = 5f;
        private float _jumpStart;
        private bool _isJumping;
        private Vector3 _jumpStartLocation;
        private Vector3 _jumpEndLocation;

        private void Update()
        {
            Vector3 movementVector = ReadMovement();
            bool jumping = ReadJump();

            if (jumping)
            {
                float jumpRadius = 5f;
                _jumpStart = Time.time;
                _isJumping = true;
                _jumpStartLocation = _transform.position;
                _jumpEndLocation = _transform.position + movementVector * jumpRadius;

                StartCoroutine(PlayerJump());
                StartCoroutine(PlayerRotate());
            }

            if (_isJumping && _jumpStart + jumpDuration < Time.time)
            {
                _isJumping = false;
                _transform.position = _jumpEndLocation;
            }

            if (!_isJumping)
            {
                Move(movementVector);
            }
        }

        private IEnumerator PlayerJump()
        {
            float t = (Time.time - _jumpStart) / jumpDuration;

            while (t < jumpDuration)
            {
                t += Time.deltaTime;

                _transform.position = Parabola(_jumpStartLocation, _jumpEndLocation, jumpHeight, t / jumpDuration);
                yield return null;
            }

            _transform.position = _jumpEndLocation;
        }

        private IEnumerator PlayerRotate()
        {
            float t = (Time.time - _jumpStart) / jumpDuration;

            while (t < jumpDuration)
            {
                t += Time.deltaTime;
                var tNorm = Mathf.Clamp01(t / jumpDuration);

                var rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), quaternion.Euler(0, 360, 0), tNorm);
                // var rotation = Quaternion.Lerp(new Quaternion(0,0,0,1), new Quaternion(0,0,0,-1), tNorm);

                _transform.rotation = rotation;
                yield return null;
            }


            _transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        private Vector3 ReadMovement()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            Vector2 movementVector = new Vector2(x, y).normalized;
            Vector3 movementVector3 = new Vector3(movementVector.x, 0, movementVector.y);

            return movementVector3;
        }

        private bool ReadJump()
        {
            return !_isJumping && Input.GetKeyDown(KeyCode.Space);
        }

        private void Move(Vector3 movementVector)
        {
            _transform.position += Time.deltaTime * speed * movementVector;
        }

        private Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
        {
            float a = -4;
            float b = 4;

            float curY = (a * t * t + b * t) * height + start.y;
            float curX = Mathf.Lerp(start.x, end.x, t);
            float curZ = Mathf.Lerp(start.z, end.z, t);

            return new Vector3(curX, curY, curZ);
        }
    }
}