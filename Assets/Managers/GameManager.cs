using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    // public static event Action<GameState> OnGameStateChanged;

    public GameState State;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.GenerateGrid);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.RecolorGrid:
                GridManager.Instance.RecolorGrid();
                break;
            case GameState.SpawnPlayer:
                UnitManager.Instance.SpawnPlayers();
                break;
            case GameState.Gameplay:
                break;
            case GameState.GameOver:
                Debug.Log("here gamemanager");
                Gameover();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        // OnGameStateChanged?.Invoke(newState);
    }

    public void Gameover()
    {
        Debug.Log("called");
        SceneManager.LoadScene("GameOverScene");
    }
}

public enum GameState
{
    GenerateGrid,
    RecolorGrid,
    SpawnPlayer,
    Gameplay,
    GameOver
}
