using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlash : MonoBehaviour
{
    public Light2D globalLight;
    public float flashTimer;
    private bool canFlash;
    [Range(0,1)]
    public float variance=.1f;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        canFlash = true;
        StartCoroutine(Flash());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Flash()
    {

        while(1<10)
        {
            yield return new WaitForSeconds(flashTimer + flashTimer * Random.Range(-variance, variance));
            animator.SetTrigger("Strike");
            
        }
    }
}
