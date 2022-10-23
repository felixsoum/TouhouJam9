using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [Expandable]
    public BulletSpawnerSO baseSpawner;

    public bool isEnabled;

    private void Start()
    {
        isEnabled = true;

        // Yikes but it's simplest LOL
        //InvokeRepeating("Shoot", baseSpawner.spawnRate, baseSpawner.spawnRate);

        //if (baseSpawner.hasDuration)
        //{
        //    Invoke("DisableSpawner", baseSpawner.spawnDuration);
        //}
    }

    [Button]
    public void Shoot()
    {
        if (!isEnabled)
        {
            return;
        }

        switch (baseSpawner.spawnShape)
        {
            case SpawnShape.Single:
                SpawnBullet(new Vector3(0f, GetCorrectRotation(), 0f));
                break;

            case SpawnShape.Cone:

                float coneAngle = baseSpawner.coneAngle / 2;
                int coneCount = baseSpawner.coneCount;

                float coneAngleIncrement = coneAngle / (coneCount - 1);

                for (int i = 0; i < coneCount; ++i)
                {
                    SpawnBullet(GetConeAngle(i * coneAngleIncrement, coneAngle));
                }

                break;
            case SpawnShape.Circle:

                float circleAngle = 180f;
                float circleCount = baseSpawner.circleCount;

                float circleAngleIncrement = circleAngle / circleCount;

                for (int i = 0; i < circleCount; ++i)
                {
                    SpawnBullet(GetConeAngle(i * circleAngleIncrement, circleAngle));
                }

                break;
        }
    }

    private void DisableSpawner()
    {
        CancelInvoke("Shoot");
        isEnabled = false;
        //Destroy(this);
    }

    private float GetCorrectRotation()
    {
        float angle = Vector3.Angle(transform.position, Character.GetPlayerCharacter().transform.position);

        return baseSpawner.targetsPlayer ? angle : 0f;
    }

    public void SpawnBullet(Vector3 dir)
    {
        Bullet bullet = PoolManager.Instance.GetPooledBullet();

        bullet.transform.position = transform.position;
        bullet.SetBulletDir(dir);

        bullet.bulletBehaviour = baseSpawner.bulletSO;
        bullet.UpdateBehaviour();
    }

    private Vector3 GetConeAngle(float currentAngle, float totalAngle)
    {
        //Vector3 v3 = Quaternion.AngleAxis(currentAngle - (totalAngle / 2), Vector3.forward) * Vector3.up;

        return new Vector3(0f, currentAngle - (totalAngle / 2), 0f);
    }
}