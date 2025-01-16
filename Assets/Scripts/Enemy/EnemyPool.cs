using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmicCuration.Enemy
{
    public class EnemyPool
    {
        private EnemyView enemyView;
        private EnemyScriptableObject enemyScriptableObject;
        private List<PooledEnemy> pooledEnemies = new List<PooledEnemy>();

        public EnemyPool(EnemyView enemyView, EnemyScriptableObject enemyScriptableObject)
        {
            this.enemyView = enemyView;
            this.enemyScriptableObject = enemyScriptableObject;
        }

        public EnemyController GetEnemy()
        {
            if(pooledEnemies.Count > 0)
            {
                PooledEnemy pooledEnemy = pooledEnemies.Find(item => !item.isUsed);

                if(pooledEnemy != null)
                {
                    pooledEnemy.isUsed = true;
                    return pooledEnemy.enemyController;
                }
            }

            return CreateNewPooledEnemy();
        }

        private EnemyController CreateNewPooledEnemy()
        {
            PooledEnemy pooledEnemy = new PooledEnemy();
            pooledEnemy.enemyController = new EnemyController(enemyView, enemyScriptableObject.enemyData);
            pooledEnemy.isUsed = true;
            pooledEnemies.Add(pooledEnemy);

            return pooledEnemy.enemyController;
        }

        public void ReturnToEnemyPool(EnemyController enemyController)
        {
            PooledEnemy pooledEnemy = pooledEnemies.Find(item => item.enemyController.Equals(enemyController));
            pooledEnemy.isUsed = false;
        }

        public class PooledEnemy
        {
            public EnemyController enemyController;
            public bool isUsed;
        }
    }
}