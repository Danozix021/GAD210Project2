using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RoundManager : MonoBehaviour
{
    public ArenaSwapper arenaSwapper;
    public SpawnManager spawnManager;
    private bool roundEnding = false;    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
            Invoke(nameof(SwapArena), 2f);
        }
        else if (players.Length == 0)
        {
            roundEnding = true;
            Debug.Log("No one wins!");
            Invoke(nameof(SwapArena), 2f);
        }
    }

    void SwapArena()
    {
        roundEnding = false;
        arenaSwapper.ArenaSwap();

        spawnManager.ResetRound();
    }


}
