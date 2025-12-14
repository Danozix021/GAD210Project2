using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private string shootButton = "P1_Shoot";
    [Header("Aim Input")]
    [SerializeField] private string aimXAxis = "P1_AimX";
    [SerializeField] private string aimYAxis = "P1_AimY";
    [SerializeField] private float aimDeadZone = 0.25f;


    [Header("Weapon")]
    [SerializeField] private Transform weaponSocket;          
    [SerializeField] private WeaponBase startingWeaponPrefab; 

    private WeaponBase currentWeapon;


    private Vector2 lastAimDirection = Vector2.right;

    private void Start()
    {
        if (startingWeaponPrefab != null && weaponSocket != null)
        {
            Equip(Instantiate(startingWeaponPrefab, weaponSocket));
        }
    }

    private void Update()
    {
       
        UpdateAimDirection();

        bool held = Input.GetButton(shootButton);
        bool pressed = Input.GetButtonDown(shootButton);

        if (currentWeapon != null)
        {
            currentWeapon.HandleTrigger(held, pressed, lastAimDirection, transform);
        }
    }

    private void UpdateAimDirection()
    {
        float aimX = Input.GetAxis(aimXAxis);
        float aimY = Input.GetAxis(aimYAxis);

        Vector2 aimInput = new Vector2(aimX, aimY);

        if (aimInput.magnitude >= aimDeadZone)
        {
            lastAimDirection = aimInput.normalized;
        }
        else
        {

            var movement = GetComponent<PlayerMovement>();
            if (movement != null)
            {
                Vector2 moveDir = movement.GetLastMoveDirection();
                if (moveDir.sqrMagnitude > 0.01f)
                    lastAimDirection = moveDir;
            }
        }
        
        var rotator = GetComponent<PlayerRotator2D>();
        if (rotator != null)
            rotator.SetAimDirection(lastAimDirection);

        Debug.Log($"AimX={aimX:F2}, AimY={aimY:F2}");

    }

    public void Equip(WeaponBase newWeapon)
    {
        if (newWeapon == null) return;

        
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }

        currentWeapon = newWeapon;
        currentWeapon.transform.SetParent(weaponSocket, false);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
    }
}
