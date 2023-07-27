using System;
using _Scripts.Helpers;
using _Scripts.Units.Enemies;
using UnityEngine;

namespace _Scripts.UI
{
    public class ScoreCounter : StaticInstance<ScoreCounter>
    {
        public event Action OnScoreChanged;
        public int Score
        {
            get => _score;
            private set
            {
                if (value == _score)
                {
                    return;
                }

                _score = value;
                OnScoreChanged?.Invoke();
            }
        }

        private int _score;

        private void Start()
        {
            EnemyBase.OnAnyEnemyDead += EnemyBaseOnAnyEnemyDead;
        }

        private void EnemyBaseOnAnyEnemyDead(Vector3 position)
        {
            Score++;
        }
    }
}