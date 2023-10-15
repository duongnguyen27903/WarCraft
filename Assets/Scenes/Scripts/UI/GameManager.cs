using Section3;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private HomePanel HomePanel;
    [SerializeField] private GamePlayPanel GamePlayPanel;
    [SerializeField] private PausePanel GamePausePanel;
    [SerializeField] private GameOverPanel GameOverPanel;
    private GameState game_state;
    private bool Win;
    private int Score;
    private SpawnManager spawnManager;
    private AudioManager audioManager;
    public enum GameState
    {
        Home,
        GamePlay,
        GamePause,
        GameOver
    }
    void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        audioManager = FindObjectOfType<AudioManager>();
        HomePanel.gameObject.SetActive(false);
        GamePlayPanel.gameObject.SetActive(false);
        GamePausePanel.gameObject.SetActive(false);
        GameOverPanel.gameObject.SetActive(false);
        Set_Game_State(GameState.Home);
    }
    public void Set_Game_State( GameState state)
    {
        game_state = state;
        HomePanel.gameObject.SetActive(game_state == GameState.Home);
        GamePlayPanel.gameObject.SetActive( game_state == GameState.GamePlay);
        GamePausePanel.gameObject.SetActive( game_state == GameState.GamePause);
        GameOverPanel.gameObject.SetActive(game_state == GameState.GameOver);

        if( game_state == GameState.GamePause)
        {
            Time.timeScale = 0;
        }
        else Time.timeScale = 1f;

        if( game_state == GameState.Home)
        {
            audioManager.PlayHomeMusic();
        }
        else audioManager.PlayBattleMusic();
    }

    public bool CanMove()
    {
        return game_state == GameState.GamePlay;
    }

    public void Btn_Pause_Pressed()
    {
        Set_Game_State(GameState.GamePause);
    }

    public void Btn_Play_Pressed()
    {
        Set_Game_State(GameState.GamePlay);
        spawnManager.StartBattle();
        spawnManager.Create_Player();
        Score = 0;
        GamePlayPanel.DisplayScore(Score);
    }

    public void Btn_Home_Pressed()
    {
        Set_Game_State(GameState.Home);
        spawnManager.Clear();
        spawnManager.Destroy_Player();
    }

    public void GameOver( bool win)
    {
        Win = win;
        Set_Game_State(GameState.GameOver);
        GameOverPanel.DisplayHighScore(Score);
        GameOverPanel.DisplayResult(Win);
    }

    public void AddScore( int value)
    {
        Score += value;
        GamePlayPanel.DisplayScore(Score);
        if( spawnManager.IsClear())
        {
            GameOver(true);
        }
    }
}
