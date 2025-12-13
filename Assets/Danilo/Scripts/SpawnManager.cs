using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] playerPrefabs;

    private List<Transform> availableSpawns = new List<Transform>();
    private List<GameObject> spawnedPlayers = new List<GameObject>();

    void Start()
    {
        SpawnPlayers();
        StartCoroutine(DelaySpawn());
    }

    public void SpawnPlayers()
    {

        availableSpawns.Clear();
        foreach (Transform sp in spawnPoints)
        {
            availableSpawns.Add(sp);

        }

        foreach (var players in spawnedPlayers)
        {
            if (players != null) 
                Destroy(players);
        }
        spawnedPlayers.Clear();

        int playerCount = Mathf.Min(availableSpawns.Count, playerPrefabs.Length);
        for (int i = 0; i < playerCount; i++)
        {
            if (availableSpawns.Count == 0) 
                break;

            int index = Random.Range(0, availableSpawns.Count);
            Transform spawn = availableSpawns[index];

            GameObject chosenPrefab = playerPrefabs[i];

            GameObject player = Instantiate(chosenPrefab, spawn.position, spawn.rotation);

            spawnedPlayers.Add(player);

            availableSpawns.RemoveAt(index);

        }
    }
    
    public void ResetRound()
    {
        SpawnPlayers();
    }

    IEnumerator DelaySpawn()
    { 
        yield return null;
        SpawnPlayers();
    }
}
