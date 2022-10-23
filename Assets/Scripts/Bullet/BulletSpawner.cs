using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [Expandable]
    public BulletSpawnerSO baseSpawner;

    [SerializeField] bool isPlayerBullet;
    public bool isEnabled;

    private void Start()
    {
        isEnabled = true;
    }

    private IEnumerator AutomaticShoot(float seconds)
    {
        while (isEnabled)
        {
            yield return new WaitForSeconds(seconds);
            Shoot();
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
                SpawnBullet(Vector3.zero);
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

                float circleAngle = 360f;
                float circleCount = baseSpawner.circleCount;

                float circleAngleIncrement = circleAngle / circleCount;

                for (int i = 0; i < circleCount; ++i)
                {
                    SpawnBullet(GetConeAngle(i * circleAngleIncrement, circleAngle));
                }

                break;
        }
    }

    private IEnumerator DisableAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        DisableSpawner();
    }

    private void DisableSpawner()
    {
        isEnabled = false;
    }

    //private float GetCorrectRotation()
    //{
    //    float angle = Vector3.Angle(transform.position, Character.GetPlayerCharacter().transform.position);

    //    return baseSpawner.targetsPlayer ? angle : 0f;
    //}

    public void SpawnBullet(Vector3 dir)
    {
        Bullet bullet = PoolManager.Instance.GetPooledBullet(isPlayerBullet);

        bullet.transform.position = transform.position;
        bullet.SetBulletDir(dir, this.transform.rotation);

        bullet.bulletBehaviour = baseSpawner.bulletSO;
        bullet.UpdateBehaviour();
    }

    private Vector3 GetConeAngle(float currentAngle, float totalAngle)
    {
        //Vector3 v3 = Quaternion.AngleAxis(currentAngle - (totalAngle / 2), Vector3.forward) * Vector3.up;

        return new Vector3(0f, currentAngle - (totalAngle / 2), 0f);
    }
}