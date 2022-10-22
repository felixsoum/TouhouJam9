using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : Enemy
{
    [SerializeField] float speed = 10f;

    Vector3 targetPosition;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(AICoroutine());
    }

    IEnumerator AICoroutine()
    {
        while (true)
        {
            targetPosition = GameManager.GetRandomPointAroundOrigin(4f);
            while (Vector3.Distance(transform.position, targetPosition) > 0.001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
