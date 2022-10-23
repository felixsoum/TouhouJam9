using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingletonComponent<PoolManager>
{
    [BoxGroup("Pooled ChaserEnemies")] public GameObject pooledChaserEnemy;
    [BoxGroup("Pooled ChaserEnemies")] public int pooledChaserEnemyCount;
    [BoxGroup("Pooled ChaserEnemies")] public List<Enemy> chaserEnemyPool = new List<Enemy>();

    [BoxGroup("Pooled ShooterEnemies")] public GameObject pooledShooterEnemy;
    [BoxGroup("Pooled ShooterEnemies")] public int pooledShooterEnemyCount;
    [BoxGroup("Pooled ShooterEnemies")] public List<Enemy> shooterEnemyPool = new List<Enemy>();

    [BoxGroup("Pooled Player Bullets")] public GameObject pooledPlayerBullets;
    [BoxGroup("Pooled Player Bullets")] public int pooledPlayerBulletCount;
    [BoxGroup("Pooled Player Bullets")] public List<Bullet> playerBulletPool = new List<Bullet>();

    [BoxGroup("Pooled Player Bullets")] public GameObject pooledEnemyBullets;
    [BoxGroup("Pooled Player Bullets")] public int pooledEnemyBulletCount;
    [BoxGroup("Pooled Player Bullets")] public List<Bullet> enemyBulletPool = new List<Bullet>();

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < pooledChaserEnemyCount; ++i)
        {
            Enemy enemy = Instantiate(pooledChaserEnemy).GetComponent<Enemy>();
            enemy.gameObject.SetActive(false);
            chaserEnemyPool.Add(enemy);
        }

        for (int i = 0; i < pooledChaserEnemyCount; ++i)
        {
            Enemy enemy = Instantiate(pooledShooterEnemy).GetComponent<Enemy>();
            enemy.gameObject.SetActive(false);
            shooterEnemyPool.Add(enemy);
        }

        for (int i = 0; i < pooledPlayerBulletCount; ++i)
        {
            Bullet bullet = Instantiate(pooledPlayerBullets).GetComponent<Bullet>();
            bullet.gameObject.SetActive(false);
            playerBulletPool.Add(bullet);
        }

        for (int i = 0; i < pooledEnemyBulletCount; ++i)
        {
            Bullet bullet = Instantiate(pooledEnemyBullets).GetComponent<Bullet>();
            bullet.gameObject.SetActive(false);
            enemyBulletPool.Add(bullet);
        }
    }

    public Enemy GetPooledChaserEnemy()
    {
        for (int i = 0; i < chaserEnemyPool.Count; ++i)
        {
            var e = chaserEnemyPool[i];

            if (!e.gameObject.activeInHierarchy)
            {
                e.gameObject.SetActive(true);
                return e;
            }
        }

        Debug.LogWarning("Did not pre-warm enough enemies, instantiating");
        Enemy enemy = Instantiate(pooledChaserEnemy).GetComponent<Enemy>();
        enemy.gameObject.SetActive(true);
        chaserEnemyPool.Add(enemy);

        return enemy;
    }

    public Enemy GetPooledShooterEnemy()
    {
        for (int i = 0; i < shooterEnemyPool.Count; ++i)
        {
            var e = shooterEnemyPool[i];

            if (!e.gameObject.activeInHierarchy)
            {
                e.gameObject.SetActive(true);
                return e;
            }
        }

        Debug.LogWarning("Did not pre-warm enough enemies, instantiating");
        Enemy enemy = Instantiate(pooledShooterEnemy).GetComponent<Enemy>();
        enemy.gameObject.SetActive(true);
        shooterEnemyPool.Add(enemy);

        return enemy;
    }

    public Bullet GetPooledBullet(bool isPlayerBullet)
    {
        var bulletPool = isPlayerBullet ? playerBulletPool : enemyBulletPool;
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
        Bullet bullet = Instantiate(isPlayerBullet ? pooledPlayerBullets : pooledEnemyBullets).GetComponent<Bullet>();
        bullet.gameObject.SetActive(true);
        bulletPool.Add(bullet);

        return bullet;
    }
}
