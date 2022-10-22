using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : SingletonComponent<BulletManager>
{
    [HorizontalLine(color: EColor.Red)] 
    public GameObject testBullet;
}
