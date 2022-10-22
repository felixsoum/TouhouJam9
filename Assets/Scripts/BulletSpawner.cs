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
        Bullet bullet = Instantiate(BulletManager.Instance.testBullet, transform.position + (1 * GetForward()), Quaternion.identity).GetComponent<Bullet>();

        SetupBullet(bullet);
    }

    public void SetupBullet(Bullet bullet)
    {
        bullet.bulletBehaviour = baseSpawner.bulletSO;
        bullet.UpdateBehaviour();
    }

    public Vector3 GetForward()
    {
        Vector3 v3 = Vector3.zero;

        switch (baseSpawner.spawnShape)
        {
            case SpawnShape.Single:
                if (baseSpawner.targetsPlayer)
                {
                    // Calc here after we get ref to player
                    return v3;
                }
                else
                {
                    return transform.forward;
                }
            case SpawnShape.Cone:

                //float coneAngle = 


                break;
            case SpawnShape.Circle:

                break;
        }

        return v3;
    }
}
