using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameState
    {
        Menu,
        Game,
        LevelComplete,
        GameOver
    }

    private GameState gmaestate;

    public static Action<GameState> onGameStateChanged;
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameState(GameState gamestate)
    {
        this.gmaestate = gamestate;
        onGameStateChanged?.Invoke(gamestate);

        Debug.Log("Game State Changed to : "+gamestate);
    }

    public bool IsGameState()
    {
        return gmaestate == GameState.Game;
    }
}
