using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnShape
{
    Single,
    Cone,
    Circle,
};

[CreateAssetMenu(fileName = "Bullet Spawner SO", menuName = "Bullets/BulletSpawnerSO")]
public class BulletSpawnerSO : ScriptableObject
{
    public SpawnShape spawnShape;
    public BulletSO bulletSO;

    public float spawnDuration;
    public float spawnRate;

    public bool isSpinning;
    [ShowIf("isSpinning")]
    public float spinSpeed;

    public bool targetsPlayer;

    [ShowIf("spawnShape", SpawnShape.Cone)]
    public float coneAngle;
    [ShowIf("spawnShape", SpawnShape.Cone)]
    public int coneCount;

    [ShowIf("spawnShape", SpawnShape.Circle)]
    public int circleCount;
}
