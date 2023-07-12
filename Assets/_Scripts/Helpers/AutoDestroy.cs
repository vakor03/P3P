using System;
using UnityEngine;

namespace _Scripts.Helpers
{
    public class AutoDestroy : MonoBehaviour
    {
        [SerializeField] private float delay;

        private void Start()
        {
            Destroy(gameObject, delay);
        }
    }
}