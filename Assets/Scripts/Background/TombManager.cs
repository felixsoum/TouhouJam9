using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombManager : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] float cooldown = 5f;
    [SerializeField] float lifetime = 5f;
    [SerializeField] float firstSpawnOffset = 45f;
    float cooldownTimer;
    Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        cooldownTimer = 0;
        Spawn();
        Spawn(firstSpawnOffset);
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

        Spawn(firstSpawnOffset);
        cooldownTimer = 0;
    }

    void Spawn(float offset = 0f)
    {
        GameObject platform = Instantiate(prefab, transform);
        platform.transform.position = originalPosition + new Vector3(0, 0, offset);
        Destroy(platform, lifetime);
    }
}
