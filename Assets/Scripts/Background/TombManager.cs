using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombManager : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] float cooldown = 5f;
    [SerializeField] float lifetime = 5f;
    float cooldownTimer;
    Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        cooldownTimer = 0;
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (prefab == null)
        {
            return;
        }

        if (cooldownTimer < cooldown)
        {
            cooldownTimer += Time.deltaTime;
            return;
        }

        Spawn();
        cooldownTimer = 0;
    }

    void Spawn()
    {
        GameObject platform = Instantiate(prefab, transform);
        platform.transform.position = originalPosition;
        Destroy(platform, lifetime);
    }
}
