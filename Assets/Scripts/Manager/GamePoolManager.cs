using System;
using System.Collections.Generic;
using Unilts.Tools.Singleton;
using UnityEngine;

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

        [SerializeField] private List<PoolItem> _configPoolItem = new List<PoolItem>();
    }
}