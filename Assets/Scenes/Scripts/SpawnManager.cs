using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Section3
{
    [System.Serializable]
    public class EnemyPool
    {
        public EnemyController prefab;
        public List<EnemyController> inactiveObjs;
        public List<EnemyController> activeObjs;
        public EnemyController Spawn(Vector3 position, Transform parent)
        {
            if( inactiveObjs.Count == 0)
            {
                EnemyController newObj = GameObject.Instantiate(prefab,parent);
                newObj.transform.position = position;
                activeObjs.Add(newObj);
                return newObj;
            }
            else
            {
                EnemyController oldObj = inactiveObjs[0];
                inactiveObjs.Remove(oldObj);
                oldObj.gameObject.SetActive(true);
                oldObj.transform.SetParent(parent);
                oldObj.transform.position = position;
                activeObjs.Add(oldObj);
                return oldObj;
            }
        }

        public void Release(EnemyController obj)
        {
            if( activeObjs.Contains(obj) )
            {
                obj.gameObject.SetActive(false);
                activeObjs.Remove(obj);
                inactiveObjs.Add(obj);
            }
        }
    }

    [System.Serializable]
    public class ProjectTilePool
    {
        public ProjectTileController prefab;
        public List<ProjectTileController> inactiveObj;
        public List<ProjectTileController> activeObj;

        public ProjectTileController Create(Vector3 position, Transform parent)
        {
            if (inactiveObj.Count == 0)
            {
                ProjectTileController newObj = GameObject.Instantiate(prefab, parent);
                newObj.transform.position = position;
                activeObj.Add(newObj);
                return newObj;
            }
            else
            {
                ProjectTileController oldObj = inactiveObj[^1];
                inactiveObj.Remove(oldObj);
                oldObj.gameObject.SetActive(true);
                oldObj.transform.SetParent(parent);
                oldObj.transform.position = position;
                activeObj.Add(oldObj);
                return oldObj;
            }
        }

        public void Release(ProjectTileController Obj)
        {
            if (activeObj.Contains(Obj))
            {
                activeObj.Remove(Obj);
                inactiveObj.Add(Obj);
                Obj.gameObject.SetActive(false);
            }
        }
    }

    public class SpawnManager : MonoBehaviour
    {
        //[SerializeField] private EnemyController EnemyPrefabs;
        [SerializeField] private EnemyPool EnemiesPool;
        [SerializeField] private ProjectTilePool PlayerProjectilePool;
        [SerializeField] private ProjectTilePool EnemyProjectilePool;
        [SerializeField] private bool active;
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
                EnemyController enemy = EnemiesPool.Spawn(path.WayPoints[0].position,transform);
                enemy.Init(path.WayPoints);
            }
        }
        public void ReleaseEnemy(EnemyController obj)
        {
            EnemiesPool.Release(obj);
        }

        public ProjectTileController SpawnPlayerProjectile(Vector3 position)
        {
            ProjectTileController obj = PlayerProjectilePool.Create(position,transform);
            obj.SetFromPlayer(true);
            return obj;
        }

        public void ReleasePlayerProjectile( ProjectTileController projectile)
        {
            PlayerProjectilePool.Release(projectile); 
        }

        public ProjectTileController SpawnEnemyProjectile(Vector3 position)
        {
            ProjectTileController obj = EnemyProjectilePool.Create(position, transform);
            obj.SetFromPlayer(false);
            return obj;
        }

        public void ReleaseEnemyProjectile( ProjectTileController projectile)
        {
            EnemyProjectilePool.Release(projectile);
        }
    }

}