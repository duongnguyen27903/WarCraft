using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HighScore;
    [SerializeField] private TextMeshProUGUI Result;

    private GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void DisplayHighScore(int score)
    {
        HighScore.text = $"HighScore : {score}";
    }

    public void BtnHome_Pressed()
    {
        gameManager.Btn_Home_Pressed();
    }

    public void DisplayResult(bool win)
    {
        if (win)
        {
            Result.text = "WIN";
        }
        else Result.text = "LOSE";
    }
}
