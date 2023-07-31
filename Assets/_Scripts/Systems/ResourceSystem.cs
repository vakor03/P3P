#region

using System.Collections.Generic;
using System.Linq;
using _Scripts.Helpers;
using UnityEngine;

#endregion

namespace _Scripts.Systems
{
    public class ResourceSystem : Singleton<ResourceSystem>
    {
        private const string ENEMIES_RESOURCES_PATH = "Enemies";
        private Dictionary<EnemyType, EnemySO> _enemiesDictionary;
        public List<EnemySO> EnemiesSO { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            AssembleResources();
        }

        private void AssembleResources()
        {
            EnemiesSO = Resources.LoadAll<EnemySO>(ENEMIES_RESOURCES_PATH).ToList();
            _enemiesDictionary = EnemiesSO.ToDictionary(e => e.enemyType, e => e);
        }

        public EnemySO GetEnemy(EnemyType enemyType)
        {
            return _enemiesDictionary[enemyType];
        }

        public EnemySO GetRandomEnemy()
        {
            return EnemiesSO[Random.Range(0, EnemiesSO.Count)];
        }
    }
}