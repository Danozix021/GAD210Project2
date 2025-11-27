using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ArenaSwapper : MonoBehaviour
{
    public GameObject[] arenas;
    private int currentArena = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < arenas.Length; i++)
        {
            arenas[i].SetActive(false);
        }
            

        currentArena = Random.Range(0, arenas.Length);
        arenas[currentArena].SetActive(true);
        WeaponsSpawned();
        Debug.Log("Chosen arena: " + arenas[currentArena].name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ArenaSwap();
        }
    }

    public void ArenaSwap()
    {
        arenas[currentArena].SetActive(false);
        currentArena++;
        if (currentArena >= arenas.Length)
        {
            currentArena = 0;
        }

        arenas[currentArena].SetActive(true);
        WeaponsSpawned();
        Debug.Log("swapped arena: " + arenas[currentArena].name);
    }

    public void WeaponsSpawned()
    {
        

        WeaponSpawner[] spawners = arenas[currentArena].GetComponentsInChildren<WeaponSpawner>(true);
        foreach (var spawner in spawners)
        { 
            spawner.gameObject.SetActive(true);
            spawner.SpawnWeapons();
            Debug.Log("Found spawners: " + spawners.Length);
        }
    }
}
