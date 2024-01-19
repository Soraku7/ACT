using System;
using System.Collections.Generic;
using Unilts.Tools.Singleton;
using UnityEngine;
using UnityEngine.Serialization;

namespace Manager
{
    public class GamePoolManager : Singleton<GamePoolManager>
    {
        [Serializable]
        private class PoolItem
        {
            public string ItemName;
            public GameObject Item;
            public int InitMaxCount;
        }

        [SerializeField] private List<PoolItem> configPoolItem = new List<PoolItem>();

        private Dictionary<string, Queue<GameObject>> _poolCenter = new Dictionary<string, Queue<GameObject>>();

        private GameObject _poolItemParent;

        private void Start()
        {
            _poolItemParent = new GameObject("PoolItems");
            _poolItemParent.transform.SetParent(this.transform);
            InitPool();
        }

        
        private void InitPool()
        {
            if (configPoolItem.Count == 0) return;

            for (var i = 0; i < configPoolItem.Count; i++)
            {
                for (int j = 0; j < configPoolItem[i].InitMaxCount; j ++)
                {
                    var item = Instantiate(configPoolItem[i].Item, _poolItemParent.transform, true);
                    item.SetActive(false);
                    if (!_poolCenter.ContainsKey(configPoolItem[i].ItemName))
                    {
                        //当前对象池未存在当前对象
                        _poolCenter.Add(configPoolItem[i].ItemName , new Queue<GameObject>());
                        _poolCenter[configPoolItem[i].ItemName].Enqueue(item);
                    }
                    else
                    {
                        _poolCenter[configPoolItem[i].ItemName].Enqueue(item);
                    }
                }
            }
        }

        /// <summary>
        /// 获取对象池对象
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        public void TryGetPoolItem(string itemName , Vector3 position , Quaternion rotation)
        {
            Debug.Log(itemName);
            if (_poolCenter.ContainsKey(itemName))
            {
                var item = _poolCenter[itemName].Dequeue();
                item.transform.position = position;
                item.transform.rotation = rotation;
                item.SetActive(true);
                _poolCenter[itemName].Enqueue(item);
            }
            else
            {
                Debug.Log("当前池子不存在" + itemName);
            }
        }
        
        public GameObject TryGetPoolItem(string itemName)
        {
            if (_poolCenter.ContainsKey(itemName))
            {
                var item = _poolCenter[name].Dequeue(); 
                item.SetActive(true);
                _poolCenter[name].Enqueue(item);
                return item;
            }
            
            Debug.Log("当前池子不存在" + name);

            return null;
        }
    }
}