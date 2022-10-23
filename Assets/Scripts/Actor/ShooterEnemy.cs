using System.Collections;
using UnityEngine;

public class ShooterEnemy : Enemy
{
    [SerializeField] float speed = 10f;
    [SerializeField] BulletSpawner bulletSpawner;
    [SerializeField] AudioSource shootAudio;
    Vector3 targetPosition;

    protected override void Start()
    {
        base.Start();
    }

    private void OnEnable()
    {
        StartCoroutine(AICoroutine());
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        StopCoroutine(AICoroutine());
    }

    IEnumerator AICoroutine()
    {
        while (true)
        {
            targetPosition = GameManager.GetRandomPointAroundOrigin(4f);
            float wait = 5f;
            while (Vector3.Distance(transform.position, targetPosition) > 0.001f && wait > 0)
            {
                wait -= Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(1f);
            shootAudio.Play();
            bulletSpawner.Shoot();
            yield return new WaitForSeconds(4f);
        }
    }
}
