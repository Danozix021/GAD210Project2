using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WeaponSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] weapons;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnWeapons()
    {
        foreach (Transform point in spawnPoints)
        { 
            if (point == null) continue;

            int weaponIndex = Random.Range(0, weapons.Length);
            Instantiate(weapons[weaponIndex], point.position, point.rotation);
            Debug.Log("Spawning weapon at: " + point.name);
        }
    }
}
