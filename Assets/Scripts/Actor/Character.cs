using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : Actor
{
    private const float FadeSpeed = 2f;
    [SerializeField] BulletSpawner bulletSpawner;
    [SerializeField] Collider characterCollider;
    [SerializeField] PlayerHUD playerHUD;
    [SerializeField] Transform headTransform;

    public CharacterMouseProxy MouseProxy { get; set; }
    private bool isPicked;
    public int currentHP = 100;

    float fadeValue;

    public float Stamina { get; set; } = 1f;

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

        if (!isAlive)
        {
            fadeValue += FadeSpeed * Time.deltaTime;
            fadeValue = Mathf.Clamp01(fadeValue);
            material.SetFloat("_Fade", fadeValue);
        }

        if (isPicked)
        {
            Stamina -= 0.2f * Time.unscaledDeltaTime;
            Stamina = Mathf.Clamp01(Stamina);

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


            if (Input.GetMouseButtonUp(0) || (isPicked && !isAlive) || Stamina == 0)
            {
                isPicked = false;
                material.SetFloat("_IsPicked", 0);
                //MouseProxy.Reset();
                Time.timeScale = 1;
                characterCollider.enabled = true;
            }
        }
        else
        {
            Stamina += 0.15f * Time.unscaledDeltaTime;
            Stamina = Mathf.Clamp01(Stamina);
        }
    }

    private void OnMouseDown()
    {
        if (!isAlive)
        {
            return;
        }
        MouseDown();
    }

    internal void MouseDown()
    {
        if (Stamina < 0.1f)
        {
            return;
        }
        Stamina -= 0.1f;
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
        if (!isAlive)
        {
            return;
        }

        OnHit();
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            isAlive = false;
            playerHUD.ShowDeathScreen();
        }
    }

    public Vector3 GetHeadPosition()
    {
        return headTransform.position;
    }
}
