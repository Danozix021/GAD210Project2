using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private int damage;
    private Transform owner;

    public void Init(Vector2 direction, float speed, float lifetime, int damage, Transform owner)
    {
        this.damage = damage;
        this.owner = owner;

        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * speed;

        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (owner != null && other.transform == owner) return;

        if (owner != null && other.transform.IsChildOf(owner)) return;


        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage, owner);
        }

        Destroy(gameObject);
    }
}
