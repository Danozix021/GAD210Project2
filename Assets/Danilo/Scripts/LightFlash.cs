using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlash : MonoBehaviour
{
    public Light2D globalLight;
    public float flashDuration;
    public float flashTimer;
    private bool canFlash;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canFlash = true;
        StartCoroutine(Flash());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Flash()
    {
        yield return new WaitForSeconds(flashTimer);

        while (canFlash == true)
        {
            canFlash = false;
            globalLight.intensity = 1;
            yield return new WaitForSeconds(flashDuration);
            globalLight.intensity = 0;
            yield return new WaitForSeconds(flashTimer);
            canFlash = true;
        }
    }
}
