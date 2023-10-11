using Section3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    private GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void BtnPlay_Pressed()
    {
        gameManager.Btn_Play_Pressed();
    }
    public void BtnHome_Pressed()
    {
        gameManager.Btn_Home_Pressed();
    }
}
