using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RoundManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    private bool roundEnding = false;    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        WeaponsSpawned();
    }
    // Update is called once per frame
    void Update()
    {
        if (roundEnding) return;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length == 1)
        {
            roundEnding = true;
            Debug.Log(players[0].name + " wins!");
            Invoke(nameof(ResetRound), 2f);
        }
        else if (players.Length == 0)
        {
            roundEnding = true;
            Debug.Log("No one wins!");
            Invoke(nameof(ResetRound), 2f);
        }
    }

    public void WeaponsSpawned()
    {
        // Find all spawners in scene (including inactive)
        WeaponSpawner[] spawners = FindObjectsOfType<WeaponSpawner>(true);

        foreach (var spawner in spawners)
        {
            if (spawner == null) continue;
            // optional: ensure the spawner GameObject is active if you rely on that
            spawner.gameObject.SetActive(true);
            spawner.SpawnWeapons();
        }

        Debug.Log("Spawned weapons. Spawners found: " + spawners.Length);
    }

    private void ClearWeapons()
    {
        GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");

        foreach (var w in weapons)
        {
            if (w != null)
                Destroy(w);
        }

        Debug.Log("Cleared weapons. Count: " + weapons.Length);
    }

    public void ResetRound()
    {
        roundEnding = false;

        ClearWeapons();
        WeaponsSpawned();

        if (spawnManager != null)
            spawnManager.ResetRound();
        else
            Debug.LogWarning("SpawnManager not assigned to RoundManager.");
    }
}



