#region

using _Scripts.Managers;
using _Scripts.Timers;
using UnityEngine;

#endregion

namespace _Scripts
{
    public class DelayedSpawner : MonoBehaviour
    {
        [SerializeField, Min(0)] private float delay;
        [SerializeField, Min(0)] private int spawnCount;
        [SerializeField] private bool isActive = true;

        private ITimer _timer;

        private void Awake()
        {
            _timer = new Timer(delay);
            _timer.OnTimeElapsed += SpawnEnemies;
        }

        private void Start()
        {
            if (isActive)
            {
               _timer.Start(); 
            }
        }

        private void OnDestroy()
        {
            _timer.Stop();
        }

        private void SpawnEnemies()
        {
            for (int i = 0; i < spawnCount; i++)
            {
                EnemiesManager.Instance.SpawnEnemy();
            }
        }
    }
}