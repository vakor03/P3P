using System.Collections.Generic;
using _Scripts.Helpers;
using MEC;
using UnityEngine;

namespace _Scripts.Units.Enemies
{
    public class SelfDestroyOnTimer : MonoBehaviour
    {
        [SerializeField] private float destroyTime;

        private void Start()
        {
            Coroutines.StartRoutineMEC(DestroyRoutineMEC());
        }

        private IEnumerator<float> DestroyRoutineMEC()
        {
            yield return Timing.WaitForSeconds(destroyTime);
            Destroy(gameObject);
        }
    }
}