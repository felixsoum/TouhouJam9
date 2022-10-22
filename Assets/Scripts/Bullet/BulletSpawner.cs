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
        InvokeRepeating("Shoot", baseSpawner.spawnRate, baseSpawner.spawnRate);

        if (baseSpawner.hasDuration)
        {
            Invoke("DisableSpawner", baseSpawner.spawnDuration);
        }
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
                SpawnBullet(Quaternion.Euler(GetCorrectRotation()));
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

    private Vector3 GetCorrectRotation()
    {
        // Fix this once we get a player reference
        Debug.LogError($"{baseSpawner.targetsPlayer}, {(Character.GetPlayerCharacter().transform.position - transform.position).normalized}");

        return baseSpawner.targetsPlayer ? (Character.GetPlayerCharacter().transform.position - transform.position).normalized : transform.forward;
    }

    private void SpawnBullet(Quaternion rotation)
    {
        Bullet bullet = PoolManager.Instance.GetPooledBullet();

        bullet.transform.position = transform.position;
        bullet.transform.rotation = rotation;

        bullet.bulletBehaviour = baseSpawner.bulletSO;
        bullet.UpdateBehaviour();
    }

    private Quaternion GetConeAngle(float currentAngle, float totalAngle)
    {
        return Quaternion.AngleAxis(currentAngle - (totalAngle / 2), Vector3.up);
    }
}
