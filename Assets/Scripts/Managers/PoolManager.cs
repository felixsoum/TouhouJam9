using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingletonComponent<PoolManager>
{
    [BoxGroup("Pooled Enemies")] public GameObject pooledEnemy;
    [BoxGroup("Pooled Enemies")] public int pooledEnemyCount;
    [BoxGroup("Pooled Enemies")] public List<Enemy> enemyPool = new List<Enemy>();

    [BoxGroup("Pooled Bullets")] public GameObject pooledBullet;
    [BoxGroup("Pooled Bullets")] public int pooledBulletCount;
    [BoxGroup("Pooled Bullets")] public List<Bullet> bulletPool = new List<Bullet>();

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < pooledEnemyCount; ++i)
        {
            Enemy enemy = Instantiate(pooledEnemy).GetComponent<Enemy>();
            enemy.gameObject.SetActive(false);
            enemyPool.Add(enemy);
        }

        for (int i = 0; i < pooledBulletCount; ++i)
        {
            Bullet bullet = Instantiate(pooledBullet).GetComponent<Bullet>();
            bullet.gameObject.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    public Enemy GetPooledEnemy()
    {
        for (int i = 0; i < enemyPool.Count; ++i)
        {
            var e = enemyPool[i];

            if (!e.gameObject.activeInHierarchy)
            {
                e.gameObject.SetActive(true);
                return e;
            }
        }

        Debug.LogWarning("Did not pre-warm enough enemies, instantiating");
        Enemy enemy = Instantiate(pooledEnemy).GetComponent<Enemy>();
        enemy.gameObject.SetActive(true);
        enemyPool.Add(enemy);

        return enemy;
    }

    public Bullet GetPooledBullet()
    {
        for (int i = 0; i < bulletPool.Count; ++i)
        {
            var b = bulletPool[i];

            if (!b.gameObject.activeInHierarchy)
            {
                b.gameObject.SetActive(true);
                return b;
            }
        }

        Debug.LogWarning("Did not pre-warm enough bullets, instantiating");
        Bullet bullet = Instantiate(pooledBullet).GetComponent<Bullet>();
        bullet.gameObject.SetActive(true);
        bulletPool.Add(bullet);

        return bullet;
    }
}
