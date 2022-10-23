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
            player.OnDamage(10);
        }
    }

    internal void OnDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            isAlive = false;
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
            fade += Time.deltaTime;
            fade = Mathf.Clamp01(fade);
            material.SetFloat("_Fade", fade);
            yield return null;
        }

        // If die logic
        GameManager.Instance.RemoveEnemy(this);
        gameObject.SetActive(false);
        isAlive = true;
    }
}
