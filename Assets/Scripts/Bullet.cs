using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletSO bulletBehaviour;

    public float bulletSpeed;
    public float bulletDamage;

    private void Update()
    {
        transform.Translate(transform.forward * bulletSpeed * Time.deltaTime);
    }

    public void UpdateBehaviour()
    {
        if (bulletBehaviour == null)
        {
            Debug.LogWarning("??? shouldn't happen");
            return;
        }

        SetSize();
        SetSpeed();
        SetDamage();
    }

    private void SetSize()
    {
        float size = 1f;

        switch (bulletBehaviour.bulletSize)
        {
            case BulletSize.Small:
                size = 0.3f;
                break;
            case BulletSize.Normal:
                size = 0.5f;
                break;
            case BulletSize.Large:
                size = 0.8f;
                break;
        }

        transform.localScale = new Vector3(size, size, size);
    }

    private void SetSpeed()
    {
        bulletSpeed = bulletBehaviour.bulletSpeed;
    }

    private void SetDamage()
    {
        bulletDamage = bulletBehaviour.bulletDamage;
    }
}