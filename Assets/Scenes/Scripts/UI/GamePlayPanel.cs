using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Score;
    private GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Pause_Pressed()
    {
        gameManager.Btn_Pause_Pressed();
    }
    public void DisplayScore(int score)
    {
        Score.text = $"Score : {score}";
    }
    
}
