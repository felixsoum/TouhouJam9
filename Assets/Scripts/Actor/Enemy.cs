using System.Collections;
using UnityEngine;

public class Enemy : Actor
{
    [SerializeField] protected Rigidbody myRigidbody;

    protected Character player;
    [SerializeField] int hp = 2;

    protected override void Awake()
    {
        base.Awake();
        player = Character.GetPlayerCharacter();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Character>();
            player.OnDamage(50);
        }
    }

    internal void OnDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            isAlive = false;
            player.AddExp(20);
            GameManager.Instance.enemiesKilled++;
            OnDeath();
            StartCoroutine(DeathCoroutine());
        }
        else
        {
            OnHit();
        }

    }

    IEnumerator DeathCoroutine()
    {
        float fade = 0;
        while (fade < 1)
        {
            fade += 2f * Time.deltaTime;
            fade = Mathf.Clamp01(fade);
            material.SetFloat("_Fade", fade);
            yield return null;
        }

        // If die logic
        GameManager.Instance.RemoveEnemy(this);
        fade = 0;
        material.SetFloat("_Fade", fade);
        gameObject.SetActive(false);
        isAlive = true;
    }
}
