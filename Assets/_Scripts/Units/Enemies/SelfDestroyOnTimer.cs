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
            var destroyRoutine = DestroyRoutineMEC().CancelWith(gameObject);
            Coroutines.StartRoutineMEC(destroyRoutine);
        }

        private IEnumerator<float> DestroyRoutineMEC()
        {
            yield return Timing.WaitForSeconds(destroyTime);
            Destroy(gameObject);
        }
    }
}