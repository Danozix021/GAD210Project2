using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private WeaponData data;

    [Header("References")]
    [SerializeField] private Transform muzzle;

    private float nextFireTime = 0f;

    public WeaponData Data => data;

    public void HandleTrigger(bool triggerHeld, bool triggerPressedThisFrame, Vector2 aimDirection, Transform owner)
    {
        if (data == null || muzzle == null) return;

        bool wantsToFire =
            (data.fireMode == FireMode.FullAuto && triggerHeld) ||
            (data.fireMode == FireMode.SemiAuto && triggerPressedThisFrame);

        if (!wantsToFire) return;

        TryFire(aimDirection, owner);
    }

    private void TryFire(Vector2 aimDirection, Transform owner)
    {
        if (Time.time < nextFireTime) return;

        if (aimDirection.sqrMagnitude < 0.0001f)
            return;

        nextFireTime = Time.time + (1f / data.fireRate);

        FireProjectiles(aimDirection.normalized, owner);
    }

    private void FireProjectiles(Vector2 baseDir, Transform owner)
    {
        int pellets = Mathf.Max(1, data.pellets);

        if (pellets == 1 || data.spreadAngle <= 0f)
        {
            SpawnBullet(baseDir, owner);
            return;
        }

        float total = data.spreadAngle;
        float step = (pellets == 1) ? 0f : total / (pellets - 1);
        float start = -total / 2f;

        for (int i = 0; i < pellets; i++)
        {
            float angle = start + step * i;
            Vector2 dir = Rotate(baseDir, angle);
            SpawnBullet(dir, owner);
        }
    }

    private void SpawnBullet(Vector2 dir, Transform owner)
    {
        GameObject proj = Instantiate(data.projectilePrefab, muzzle.position, Quaternion.identity);

        Bullet bullet = proj.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.Init(dir, data.projectileSpeed, data.projectileLifetime, data.damage, owner);
        }
    }

    private Vector2 Rotate(Vector2 v, float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        float sin = Mathf.Sin(rad);
        float cos = Mathf.Cos(rad);

        return new Vector2(
            v.x * cos - v.y * sin,
            v.x * sin + v.y * cos
        );
    }
}
