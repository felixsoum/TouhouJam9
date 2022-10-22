using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public BulletSpawnerSO baseSpawner;

    [Button]
    public void Shoot()
    {
        switch (baseSpawner.spawnShape)
        {
            case SpawnShape.Single:
                SpawnBullet(Quaternion.identity);
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

    private Vector3 GetForward()
    {
        // Fix this once we get a player reference
        return baseSpawner.targetsPlayer ? Vector3.one : transform.forward;
    }

    private void SpawnBullet(Quaternion rotation)
    {
        Bullet bullet = Instantiate(BulletManager.Instance.testBullet, transform.position + (0.1f * GetForward()), rotation).GetComponent<Bullet>();

        bullet.bulletBehaviour = baseSpawner.bulletSO;
        bullet.UpdateBehaviour();
    }

    private Quaternion GetConeAngle(float currentAngle, float totalAngle)
    {
        return Quaternion.AngleAxis(currentAngle - (totalAngle / 2), Vector3.up);
    }
}
