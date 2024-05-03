using Unilts.Tools.Singleton;
using UnityEngine;

namespace Manager
{
    public class EnemyManager : Singleton<EnemyManager>
    {
        private Transform _mainPlayer;


        protected override void Awake()
        {
            base.Awake();
            _mainPlayer = GameObject.FindWithTag("Player").transform;
        }

        public Transform GetMainPlayer() => _mainPlayer;
    }
}