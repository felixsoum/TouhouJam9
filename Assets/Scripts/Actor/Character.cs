using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : Actor
{
    [SerializeField] BulletSpawner bulletSpawner;
    [SerializeField] Collider characterCollider;
    public CharacterMouseProxy MouseProxy { get; set; }
    private bool isPicked;
    public int currentHP = 100;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(BulletCoroutine());
    }

    IEnumerator BulletCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            var angle = new Vector3(0, IsFacingRight ? 90 : -90, 0);
            bulletSpawner.SpawnBullet(angle);
        }
    }

    public static Character GetPlayerCharacter()
    {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }

    protected override void Update()
    {
        base.Update();

        if (isPicked)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                float moveDelta = transform.position.x - hitInfo.point.x;
                if (Mathf.Abs(moveDelta) > 0.001f)
                {
                    SetIsFacingRight(transform.position.x < hitInfo.point.x);
                }
                transform.position = hitInfo.point;
            }


            if (Input.GetMouseButtonUp(0))
            {
                isPicked = false;
                material.SetFloat("_IsPicked", 0);
                //MouseProxy.Reset();
                Time.timeScale = 1;
                characterCollider.enabled = true;
            }
        }
    }

    private void OnMouseDown()
    {
        MouseDown();
    }

    internal void MouseDown()
    {
        isPicked = true;
        material.SetFloat("_IsPicked", 1);
        Time.timeScale = 0.05f;
        characterCollider.enabled = false;
    }

    public float GetHPRatio()
    {
        return currentHP / 100f;
    }

    internal void OnDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
        }
    }
}
