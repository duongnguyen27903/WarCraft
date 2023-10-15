using Section3;
using UnityEngine;

public class HomePanel : MonoBehaviour
{
    private GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Play_Pressed()
    {
       gameManager.Btn_Play_Pressed();
    }
}
