using System.Collections;
using _Scripts.Helpers;
using _Scripts.Managers;
using UnityEngine;

namespace _Scripts
{
    public class DelayedSpawner : MonoBehaviour
    {
        [SerializeField] private float delay;
        [SerializeField] private int spawnCount;
        [SerializeField] private bool isActive = true;


        private void Start()
        {
            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                if (isActive)
                {
                    for (int i = 0; i < spawnCount; i++)
                    {
                        EnemiesManager.Instance.SpawnEnemy();
                    }
                }
                yield return Helper.GetCachedWaitForSeconds(delay);
            }
        }
    }
}