using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Character.Enemy.Combat;
using Unilts.Tools.Singleton;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Manager
{
    public class EnemyManager : Singleton<EnemyManager>
    {
        private Transform _mainPlayer;

        private WaitForSeconds _waitForSeconds;

        [SerializeField] private List<GameObject> _allEnemies = new List<GameObject>();
        [SerializeField] private List<GameObject> _activeEnemies = new List<GameObject>();
        
        protected override void Awake()
        {
            base.Awake();
            _mainPlayer = GameObject.FindWithTag("Player").transform;

            _waitForSeconds = new WaitForSeconds(6f);
        }

        private void Start()
        {
            foreach (var e in _allEnemies.Where(e => e.activeSelf))
            {
                _activeEnemies.Add(e);
            }

            StartCoroutine(EnableEnemyUnitAttackCommand());
        }

        public Transform GetMainPlayer() => _mainPlayer;

        public void AddEnemyUnit(GameObject enemy)
        {
            if (!_allEnemies.Contains(enemy))
            {
                _allEnemies.Add(enemy);
            }
        }

        public void RemoveEnemyUnity(GameObject enemy)
        {
            if (_allEnemies.Contains(enemy))
            {
                _allEnemies.Remove(enemy);
            }
        }

        IEnumerator EnableEnemyUnitAttackCommand()
        {
            while (_activeEnemies.Count() > 0)
            {
                EnemyCombatControl enemyCombatControl;
                GameObject temp = _activeEnemies[Random.Range(0, _activeEnemies.Count())];
                
                if (temp.TryGetComponent(out enemyCombatControl))
                {
                    enemyCombatControl.SetAttackCommand(true);
                }
                
                yield return _waitForSeconds;
            }
            
        }
    }
}