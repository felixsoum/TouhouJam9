using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BulletAimer : MonoBehaviour
{
    private BulletSpawner spawner;

    [SerializeField] Enemy currentTarget;

    private void Start()
    {
        spawner = GetComponent<BulletSpawner>();
    }

    private void Update()
    {
        var sortedList = GameManager.Instance.GetEnemyList().OrderBy(e => (transform.position - e.transform.position).sqrMagnitude);

        currentTarget = sortedList.FirstOrDefault();

        if (currentTarget != null)
        {
            RotateSpawner(currentTarget.transform);
        }
    }

    private void RotateSpawner(Transform target)
    {
        // Set spawner rotation here
    }
}
