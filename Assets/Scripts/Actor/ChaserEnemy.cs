using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserEnemy : Enemy
{
    [SerializeField] float acceleration = 1f;
    [SerializeField] float maxSpeed = 10f;

    Vector3 velocity;

    protected override void Update()
    {
        base.Update();
        var direction = player.transform.position - transform.position;
        direction.Normalize();
        SetIsFacingRight(direction.x > 0);

        velocity += direction * acceleration * Time.deltaTime;
        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

        transform.position += velocity * Time.deltaTime;
    }
}
