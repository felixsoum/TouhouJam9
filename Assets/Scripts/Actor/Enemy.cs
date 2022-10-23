using UnityEngine;

public class Enemy : Actor
{
    [SerializeField] protected Rigidbody myRigidbody;

    protected Character player;

    private void Awake()
    {
        player = Character.GetPlayerCharacter();
    }

    protected override void Update()
    {
        base.Update();

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Character>();
            player.OnDamage(10);
        }
    }

    internal void OnDamage(int damage)
    {
        gameObject.SetActive(false);
    }
}
