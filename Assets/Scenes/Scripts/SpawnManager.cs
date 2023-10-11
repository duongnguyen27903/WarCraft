using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Section3
{
    public class SpawnManager : MonoBehaviour
    {
        //[SerializeField] private EnemyController EnemyPrefabs;
        //[SerializeField] private EnemyPool EnemiesPool;
        [SerializeField] private bool active;
        [SerializeField] private int MinTotalEnemy;
        [SerializeField] private int MaxTotalEnemy;
        [SerializeField] private float SpawnInterval;
        [SerializeField] private EnemyPath[] Path;
        [SerializeField] private int TotalGroups;

        private bool IsSpawning;
        public void StartBattle()
        {
            StartCoroutine(IESpawnGroup(TotalGroups));
        }
        private IEnumerator IESpawnGroup(int groups)
        {
            IsSpawning = true;
            for (int i = 0; i < groups; i++)
            {
                int totalEnemies = UnityEngine.Random.Range(MinTotalEnemy, MaxTotalEnemy);
                EnemyPath path = Path[UnityEngine.Random.Range(0, Path.Length)];
                yield return StartCoroutine(IESpawnEnemies(totalEnemies, path));
                //yield return new WaitForSeconds(3);
            }
            IsSpawning = false;
        }

        private IEnumerator IESpawnEnemies(int totalEnemies, EnemyPath path)
        {
            active = true;
            for (int i = 0; i < totalEnemies; i++)
            {
                yield return new WaitUntil(() => active);
                //EnemyController enemy = Instantiate(EnemyPrefabs, transform);
                EnemyController enemy = EnemyPool.instance.GetEnemiesPool(path.WayPoints[0].position, transform);
                enemy.gameObject.SetActive(true);
                enemy.Init(path.WayPoints);
                yield return new WaitForSeconds(SpawnInterval);
            }
        }

        public void ReleaseEnemies(EnemyController obj)
        {
            obj.gameObject.SetActive(false);
        }

        public bool IsClear()
        {
            return (IsSpawning == true || EnemyPool.instance.Enemies_Pool.Exists(item => item.isActiveAndEnabled == true) == true) ? false : true;
        }

        public void Clear()
        {
            StopAllCoroutines();
        }
    }
}