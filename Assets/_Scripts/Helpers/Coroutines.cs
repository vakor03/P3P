using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;

namespace _Scripts.Helpers
{
    public class Coroutines : MonoBehaviour
    {
        private static Coroutines _instance;

        private static Coroutines Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                var go = new GameObject("[COROUTINES MANAGER]");
                _instance = go.AddComponent<Coroutines>();
                DontDestroyOnLoad(_instance);
                return _instance;
            }
        }

        // public static Coroutine StartRoutine(IEnumerator routine)
        // {
        //     return Instance.StartCoroutine(routine);
        // }
        //
        // public static void StopRoutine(Coroutine routine)
        // {
        //     Instance.StopCoroutine(routine);
        // }

        public static CoroutineHandle StartRoutineMEC(IEnumerator<float> routineMEC)
        {
            return Timing.RunCoroutine(routineMEC);
        }

        public static void StopRoutineMEC(CoroutineHandle routine)
        {
            Timing.KillCoroutines(routine);
        }
    }
}