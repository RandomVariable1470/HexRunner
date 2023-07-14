using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameState gameState;
    public PlayerReference playerRef;
    public bool isPlaying;

    public static GameManager instance;
    public static Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateState(GameState.Menu);
        isPlaying = false;
    }

    public void UpdateState(GameState newState)
    {
        gameState = newState;

        switch (newState)
        {
            case GameState.Menu:
                Menu();
                break;

            case GameState.Play:
                Play();
                break;

            case GameState.SelectColor:
                SelectColor();
                break;

            case GameState.Victory:
                Victory();
                break;

            case GameState.Loose:
                Loose();
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void RestartLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(currentSceneName);
    }
    private void Menu()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    private void Play()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    private void SelectColor()
    {
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    private void Victory()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    private void Loose()    
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}

public enum GameState
{
    Menu,
    Play,
    SelectColor,
    Victory,
    Loose
}