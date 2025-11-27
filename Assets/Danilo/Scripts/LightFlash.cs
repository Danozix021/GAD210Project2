using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlash : MonoBehaviour
{
    public Light2D globalLight;
    public float flashDuration;
    public float flashTimer;
    private bool canFlash;
    public GameObject lightningFlash;
    public float lightningFlashDuration;

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
            lightningFlash.SetActive(true);
            yield return new WaitForSeconds(lightningFlashDuration);
            globalLight.intensity = 1;
            yield return new WaitForSeconds(flashDuration);
            globalLight.intensity = 0;
            lightningFlash.SetActive(false);
            yield return new WaitForSeconds(flashTimer);
            canFlash = true;
        }
    }
}
