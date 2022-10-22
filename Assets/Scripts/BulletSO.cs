using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletSize
{
    Small,
    Normal,
    Large,
};

[CreateAssetMenu(fileName = "Bullet SO", menuName = "Bullets/BulletSO")]
public class BulletSO : ScriptableObject
{
    public BulletSize bulletSize;

    public float bulletSpeed;

    public int bulletDamage;
}
