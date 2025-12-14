using UnityEngine;

public class PlayerRotator2D : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 aimDir = Vector2.right;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetAimDirection(Vector2 dir)
    {
        if (dir.sqrMagnitude > 0.001f)
            aimDir = dir.normalized;
    }

    private void FixedUpdate()
    {
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        rb.MoveRotation(angle);
    }
}
