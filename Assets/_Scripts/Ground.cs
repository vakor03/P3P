#region

using System;
using _Scripts.Helpers;
using UnityEngine;

#endregion

namespace _Scripts
{
    public class Ground : StaticInstance<Ground>
    {
        [SerializeField] private LayerMask interactionLayer;
        private Camera _mainCamera;


        private Ray _ray;

        protected override void Awake()
        {
            base.Awake();

            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CheckClickPosition();
            }
        }

        public event Action<GroundClickedEventArgs> OnClicked;

        private void CheckClickPosition()
        {
            _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            float raycastDistance = 100f;
            if (Physics.Raycast(_ray, out RaycastHit hit, raycastDistance, interactionLayer))
            {
                if (hit.transform == transform)
                {
                    OnClicked?.Invoke(new GroundClickedEventArgs { HitPoint = hit.point });
                }
            }
        }

        public struct GroundClickedEventArgs
        {
            public Vector3 HitPoint;
        }
    }
}