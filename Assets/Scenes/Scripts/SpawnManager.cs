using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace Section3
{
    
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private bool active;
        [SerializeField] private EnemyController EnemyPrefabs;
        [SerializeField] private int MinTotalEnemy;
        [SerializeField] private int MaxTotalEnemy;
        [SerializeField] private float SpawnInterval;
        [SerializeField] private EnemyPath[] Path;
        [SerializeField] private int TotalGroups;
        // Start is called before the first frame update
        void Start()
        {
            active = true;
            StartCoroutine(IESpawnGroup(TotalGroups));
        }

        // Update is called once per frame
        void Update()
        {

        }

        //[SerializeField] private bool active;
        //private IEnumerator TestCoroutine()
        //{
        //    yield return new WaitUntil(() => active);
        //    for( int i = 0; i < 5; i++ )
        //    {
        //        Debug.Log( i );
        //        yield return new WaitForSeconds(1);
        //    }
        //}

        private IEnumerator IESpawnGroup(int groups)
        {
            for ( int i = 0; i < groups; i++)
            {
                int totalEnemies = Random.Range(MinTotalEnemy, MaxTotalEnemy);
                EnemyPath path = Path[Random.Range(0, Path.Length)];
                yield return StartCoroutine(IESpawnEnemies(totalEnemies, path));
                yield return new WaitForSeconds(3);
            }
        }

        private IEnumerator IESpawnEnemies(int totalEnemies, EnemyPath path)
        {
            for( int i = 0;i < totalEnemies;i++)
            {
                yield return new WaitUntil(() => active);
                yield return new WaitForSeconds(SpawnInterval);
                EnemyController enemy = Instantiate(EnemyPrefabs, transform);
                enemy.Init(path.WayPoints);
            }
        }
    }

}