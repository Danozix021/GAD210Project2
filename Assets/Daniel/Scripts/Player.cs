using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Player : MonoBehaviour
{

    [SerializeField] private GameObject flashLight;
    [SerializeField] private string flashButton = "P1_Flash";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(flashButton))
        {
            flashLight.SetActive(false);
        }
        else if (Input.GetButtonUp(flashButton))
        {
            flashLight.SetActive(true);
        }
    }
}
