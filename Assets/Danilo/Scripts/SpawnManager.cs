using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject playerPrefab;

    private List<Transform> availableSpawns = new List<Transform>();
    private List<GameObject> spawnedPlayers = new List<GameObject>();

    public ArenaSwapper arenaSwapper;

    void Start()
    {
        SpawnPlayers();
    }

    public void SpawnPlayers()
    {
        if (arenaSwapper == null)
        {
            Debug.LogError("ArenaSwapper not assigned!");
            return;
        }

        availableSpawns.Clear();
        foreach (Transform sp in spawnPoints)
        {
            if (sp.IsChildOf(arenaSwapper.arenas[arenaSwapper.currentArena].transform))
            {
                availableSpawns.Add(sp);
            }
        }

        foreach (var players in spawnedPlayers)
        {
            if (players != null) Destroy(players);
        }
        spawnedPlayers.Clear();

        int playerCount = Mathf.Min(availableSpawns.Count, 4);
        for (int i = 0; i < playerCount; i++)
        {
            if (availableSpawns.Count == 0) break;

            int index = Random.Range(0, availableSpawns.Count);
            Transform spawn = availableSpawns[index];

            GameObject player = Instantiate(playerPrefab, spawn.position, spawn.rotation);
            spawnedPlayers.Add(player);

            availableSpawns.RemoveAt(index);
        }
    }

    public void ResetRound()
    {
        SpawnPlayers();
    }
}
