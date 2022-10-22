using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Enemy : Actor
{
    [SerializeField] protected Rigidbody myRigidbody;

    protected Character player;

    private void Awake()
    {
        player = Character.GetPlayerCharacter();
    }

    protected override void Update()
    {
        base.Update();

    }


    internal void OnDamage(int damage)
    {
        gameObject.SetActive(false);
    }
}
