using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Enemy : Actor
{
    [SerializeField] Rigidbody myRigidbody;

    Character player;

    private void Awake()
    {
        player = Character.GetPlayerCharacter();
    }

    protected override void Update()
    {
        base.Update();

    }

    protected void FixedUpdate()
    {
        var direction = player.transform.position - transform.position;
        direction.Normalize();
        SetIsFacingRight(direction.x > 0);
        myRigidbody.AddForce(direction * Time.fixedDeltaTime * 10f);
    }
}
