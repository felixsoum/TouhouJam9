using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Character : Actor
{
    private const float FadeSpeed = 2f;
    float bulletFireRate = 3f;
    [SerializeField] BulletSpawner bulletSpawner;
    [SerializeField] BulletSpawner bulletSpawner2;
    [SerializeField] Collider characterCollider;
    [SerializeField] PlayerHUD playerHUD;
    [SerializeField] Transform headTransform;
    [SerializeField] GameObject beam;

    public CharacterMouseProxy MouseProxy { get; set; }
    private bool isPicked;
    public int currentHP = 100;
    public int currentExp;
    public int maxExp = 100;

    public bool IsBeamActive { get; set; }

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
            yield return new WaitForSeconds(bulletFireRate);
            var angle = new Vector3(0, IsFacingRight ? 90 : -90, 0);
            bulletSpawner.SpawnBullet(angle);
            bulletSpawner2.SpawnBullet(angle);
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
                if (Mathf.Abs(moveDelta) > 0.01f)
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
        if (Stamina < 0.05f)
        {
            return;
        }
        Stamina -= 0.05f;
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

    internal void AddExp(int exp)
    {
        currentExp += exp;
        if (currentExp >= maxExp)
        {
            currentExp %= maxExp;
            playerHUD.LevelUp();
        }
    }

    internal void UpgradeFireRate()
    {
        bulletFireRate /= 2f;
    }

    internal void UpgradeBeam()
    {
        if (IsBeamActive)
        {
            return;
        }
        IsBeamActive = true;

        StartCoroutine(BeamCoroutine());
    }

    IEnumerator BeamCoroutine()
    {
        while (true)
        {
            beam.SetActive(true);
            beam.transform.position = GetHeadPosition();
            beam.transform.localEulerAngles = new Vector3(0, Random.Range(0, 360f), 0);
            yield return new WaitForSeconds(0.5f);
            beam.SetActive(false);
            yield return new WaitForSeconds(5f);
        }
    }
}
