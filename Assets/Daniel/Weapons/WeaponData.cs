using UnityEngine;

public enum FireMode
{
    SemiAuto,   // tap to shoot
    FullAuto    // hold to shoot
}

[CreateAssetMenu(menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("General")]
    public string weaponName = "Starter Pistol";
    public FireMode fireMode = FireMode.SemiAuto;

    [Header("Firing")]
    [Min(0.05f)] public float fireRate = 4f;   // shots per second
    [Min(1)] public int pellets = 1;          // shotgun uses > 1
    [Range(0f, 25f)] public float spreadAngle = 0f; // degrees

    [Header("Projectile")]
    public GameObject projectilePrefab;
    [Min(1f)] public float projectileSpeed = 15f;
    [Min(0f)] public float projectileLifetime = 2f;

    [Header("Damage")]
    [Min(0)] public int damage = 10;
}
