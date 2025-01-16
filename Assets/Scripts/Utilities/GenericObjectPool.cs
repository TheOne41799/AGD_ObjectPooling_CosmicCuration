using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmicCuration.Utilities
{
    public class GenericObjectPool<T> where T : class
    {
        private List<PooledITem<T>> pooledItems = new List<PooledITem<T>>();

        protected T GetItem()
        {
            if(pooledItems.Count > 0)
            {
                PooledITem<T> item = pooledItems.Find(item => !item.isUsed);

                if(item != null)
                {
                    item.isUsed = true;
                    return item.Item;
                }
            }

            return CreateNewPooledItem();
        }

        private T CreateNewPooledItem()
        {
            PooledITem<T> newItem = new PooledITem<T>();
            newItem.Item = CreateItem();
            newItem.isUsed = true;

            pooledItems.Add(newItem);

            return newItem.Item;
        }

        protected virtual T CreateItem()
        {
            throw new NotImplementedException("Child class has not implemented CreateItem()");
        }

        public class PooledITem<T>
        {
            public T Item;
            public bool isUsed;
        }
    }
}
