using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Section3
{
    [System.Serializable]
    public class EnemyPool
    {
        public EnemyController prefab;
        public List<EnemyController> activeObjs;
        public List<EnemyController> inactiveObjs;

        public EnemyController SpawnEnemy(Vector3 position, Transform parent)
        {
            if( inactiveObjs.Count == 0 )
            {
                EnemyController newenemy = EnemyController.Instantiate(prefab,parent);
                newenemy.gameObject.transform.position = position;
                activeObjs.Add(newenemy);
                return newenemy;
            }
            else
            {
                EnemyController oldenemy = inactiveObjs[0];
                oldenemy.gameObject.transform.SetParent(parent);
                oldenemy.gameObject.transform.position = position;
                oldenemy.gameObject.SetActive(true);
                activeObjs.Add(oldenemy);
                inactiveObjs.RemoveAt(0);
                return oldenemy;
            }
        }

        public void ReleaseEnemy( EnemyController obj)
        {
            obj.gameObject.SetActive(false);
            inactiveObjs.Add(obj);
            activeObjs.Remove(obj);
        }
    }

    public class SpawnManager : MonoBehaviour
    {
        //[SerializeField] private EnemyController EnemyPrefabs;
        [SerializeField] private EnemyPool EnemiesPool;
        [SerializeField] private bool active;
        [SerializeField] private int MinTotalEnemy;
        [SerializeField] private int MaxTotalEnemy;
        [SerializeField] private float SpawnInterval;
        [SerializeField] private EnemyPath[] Path;
        [SerializeField] private int TotalGroups;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(IESpawnGroup(TotalGroups));
        }

        // Update is called once per frame
        void Update()
        {

        }

        private IEnumerator IESpawnGroup(int groups)
        {
            for (int i = 0; i < groups; i++)
            {
                int totalEnemies = UnityEngine.Random.Range(MinTotalEnemy, MaxTotalEnemy);
                EnemyPath path = Path[UnityEngine.Random.Range(0, Path.Length)];
                yield return StartCoroutine(IESpawnEnemies(totalEnemies, path));
                //yield return new WaitForSeconds(3);
            }
        }

        private IEnumerator IESpawnEnemies(int totalEnemies, EnemyPath path)
        {
            active = true;
            for (int i = 0; i < totalEnemies; i++)
            {
                yield return new WaitUntil(() => active);
                //EnemyController enemy = Instantiate(EnemyPrefabs, transform);
                EnemyController enemy = EnemiesPool.SpawnEnemy(path.WayPoints[0].position, transform);
                enemy.Init(path.WayPoints);
                yield return new WaitForSeconds(SpawnInterval);
            }
        }

        public void ReleaseEnemies(EnemyController obj)
        {
            EnemiesPool.ReleaseEnemy(obj);
        }
    }
}