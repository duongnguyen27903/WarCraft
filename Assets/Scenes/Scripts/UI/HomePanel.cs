using Section3;
using UnityEngine;

public class HomePanel : MonoBehaviour
{
    private GameManager gameManager;
    private SpawnManager spawnManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        //spawnManager = FindObjectOfType<SpawnManager>();
    }

    public void Play_Pressed()
    {
       gameManager.Btn_Play_Pressed();
       //spawnManager.Activate();
    }
}
