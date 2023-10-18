using Section3;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Score;
    [SerializeField] private Image HpValue;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        gameManager.onScoreChanged += OnScoreChange;
        SpawnManager.instance.Player.onHpChanged += OnHpChange;
    }
    private void OnDisable()
    {
        gameManager.onScoreChanged -= OnScoreChange;
        SpawnManager.instance.Player.onHpChanged -= OnHpChange;
    }

    public void Pause_Pressed()
    {
        gameManager.Btn_Pause_Pressed();
    }
    //public void DisplayScore(int score)
    //{
    //    Score.text = $"Score : {score}";
    //}
    private void OnScoreChange(int score)
    {
        Score.text = $"Score : {score}";
    }

    private void OnHpChange(int current_hp, int max_hp)
    {
        HpValue.fillAmount = current_hp * 1f / max_hp ;
    }

}
