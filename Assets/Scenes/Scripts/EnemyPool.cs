using Section3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool instance;
    public List<EnemyController> Enemies_Pool = new();
    [SerializeField] private EnemyController Enemies_prefab;

    //public EnemyController prefab;
    //public List<EnemyController> activeObjs;
    //public List<EnemyController> inactiveObjs;

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
        return enemy;
    }

    //public EnemyController SpawnEnemy(Vector3 position, Transform parent)
    //{
    //    if( inactiveObjs.Count == 0 )
    //    {
    //        EnemyController newenemy = EnemyController.Instantiate(prefab,parent);
    //        newenemy.gameObject.transform.position = position;
    //        activeObjs.Add(newenemy);
    //        return newenemy;
    //    }
    //    else
    //    {
    //        EnemyController oldenemy = inactiveObjs[0];
    //        oldenemy.gameObject.transform.SetParent(parent);
    //        oldenemy.gameObject.transform.position = position;
    //        oldenemy.gameObject.SetActive(true);
    //        activeObjs.Add(oldenemy);
    //        inactiveObjs.RemoveAt(0);
    //        return oldenemy;
    //    }
    //}

    //public void ReleaseEnemy( EnemyController obj)
    //{
    //    obj.gameObject.SetActive(false);
    //    inactiveObjs.Add(obj);
    //    activeObjs.Remove(obj);
    //}
}