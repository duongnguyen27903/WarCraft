using Section3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool instance;
    public List<EnemyController> Enemies_Pool = new();
    [SerializeField] private EnemyController Enemies_prefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public EnemyController GetEnemiesPool(Vector3 position, Transform parent)
    {
        for (int i = 0; i < Enemies_Pool.Count; i++)
        {
            if (Enemies_Pool[i].gameObject.activeInHierarchy == false)
            {
                Enemies_Pool[i].transform.position = position;
                return Enemies_Pool[i];
            }
        }
        EnemyController enemy = Instantiate(Enemies_prefab);
        enemy.gameObject.SetActive(false);
        enemy.transform.SetParent(parent);
        enemy.transform.position = position;
        Enemies_Pool.Add(enemy);
        return enemy;
    }

    public bool CheckActiveObj()
    {
        for( int i = 0; i< Enemies_Pool.Count;i++)
        {
            if(Enemies_Pool[i].gameObject.activeInHierarchy == true)
            {
                return true;
            }
        }
        return false;
    }

    public void ClearAllEnemies()
    {
        for( int i=0; i< Enemies_Pool.Count; i++)
        {
            Enemies_Pool[i].gameObject.SetActive(false);
        }
    }
}