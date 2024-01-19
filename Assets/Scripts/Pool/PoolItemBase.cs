using System;
using UnityEngine;

namespace Pool
{
    
    public interface IPoolItem
    {
        void Spawn();
        void Recycle();
    }
    
    public class PoolItemBase : MonoBehaviour , IPoolItem
    {
        private void OnEnable()
        {
            Spawn();
        }

        private void OnDisable()
        {
            Recycle();
        }

        public virtual void Spawn()
        {
        }

        public virtual void Recycle()
        {
        }
    }
}