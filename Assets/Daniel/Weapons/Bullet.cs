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
        // Prevent hitting the shooter
        if (owner != null && other.transform == owner) return;

        // Example damage interface (we'll add later)
        // var health = other.GetComponent<Health>();
        // if (health != null) health.TakeDamage(damage);

        Destroy(gameObject);
    }
}
