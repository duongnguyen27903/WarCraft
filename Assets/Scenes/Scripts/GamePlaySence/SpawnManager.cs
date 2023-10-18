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
        private static SpawnManager Instance;
        public static SpawnManager instance
        {
            get
            {
                if( Instance == null)
                {
                    Instance = FindObjectOfType<SpawnManager>();
                }
                return Instance;
            }
        }
        //[SerializeField] private EnemyController EnemyPrefabs;
        //[SerializeField] private EnemyPool EnemiesPool;
        [SerializeField] private PlayerController player_prefab;
        [SerializeField] private bool active;
        [SerializeField] private int MinTotalEnemy;
        [SerializeField] private int MaxTotalEnemy;
        [SerializeField] private float SpawnInterval;
        [SerializeField] private EnemyPath[] Path;
        [SerializeField] private int TotalGroups;

        private EnemyPool enemies_pool;
        private ObjectPool objectPool;
        private ParticleFXPool particleFXPool;
        private bool IsSpawning;
        private PlayerController player;

        public PlayerController Player => player;

        private void Awake()
        {
            if( Instance == null)
            {
                Instance = this;
            }
            else if( Instance != this )
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            enemies_pool = FindObjectOfType<EnemyPool>();
            objectPool = FindObjectOfType<ObjectPool>();
            particleFXPool = FindObjectOfType<ParticleFXPool>();
        }
        public void StartBattle()
        {
            if( player == null)
                player = Instantiate(player_prefab);
            player.transform.position = Vector2.zero;
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
            return (IsSpawning == true || EnemyPool.instance.CheckActiveObj() == true) ? false : true;
        }

        public void Clear()
        {
            enemies_pool.ClearAllEnemies();
            objectPool.ClearProjectile();
            particleFXPool.ClearAllFX();
            StopAllCoroutines();
        }

        public void Create_Player()
        {
            
        }
        public void Destroy_Player()
        {
            if( player != null)
            Destroy(player.gameObject);
        }
    }
}