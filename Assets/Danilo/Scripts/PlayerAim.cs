using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public float aimSmooth = 10f;

    private Vector3 lastAimDirection = Vector3.forward;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movementInput = new Vector3(horizontal, 0, vertical);

        // If there is movement, update aim direction
        if (movementInput.sqrMagnitude > 0.1f)
            lastAimDirection = movementInput.normalized;

        // Rotate player to aim direction
        Quaternion targetRot = Quaternion.LookRotation(lastAimDirection, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, aimSmooth * Time.deltaTime);
    }
}
