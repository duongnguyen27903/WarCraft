using Section3;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    private readonly List<GameObject> PlayerProjectilePool = new();
    private readonly List<GameObject> EnemyProjectilePool = new();
    private readonly int amountToPool = 20;
    [SerializeField] private GameObject PlayerProjectile;
    [SerializeField] private GameObject EnemyProjectile;

    private SpawnManager spawnManager;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject player_bullet = Instantiate(PlayerProjectile);
            player_bullet.transform.SetParent(spawnManager.transform);
            player_bullet.SetActive(false);
            PlayerProjectilePool.Add(player_bullet);
            GameObject enemy_bullet = Instantiate(EnemyProjectile);
            enemy_bullet.transform.SetParent(spawnManager.transform);
            enemy_bullet.SetActive(false);
            EnemyProjectilePool.Add(enemy_bullet);
        }
        
    }
    public GameObject PlayerFiring()
    {
        for( int i = 0;i < PlayerProjectilePool.Count; i++)
        {
            if( PlayerProjectilePool[i].activeInHierarchy == false )
            {
                return PlayerProjectilePool[i];
            }
        }
        return null;
    }

    public GameObject EnemyFiring()
    {
        for( int i = 0; i < EnemyProjectilePool.Count; i++)
        {
            if(EnemyProjectilePool[i].activeInHierarchy == false )
            {
                return EnemyProjectilePool[i];
            }
        }
        return null;
    }
}
