using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Section3
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float MoveSpeed;
        [SerializeField] private Transform[] WayPoint;
        [SerializeField] private ProjectTileController ProjectTile;
        [SerializeField] private Transform FiringPoint;
        [SerializeField] private float MinFiringCooldown;
        [SerializeField] private float MaxFiringCooldown;
        [SerializeField] private int Hp;

        private int current_hp;
        private float TempCooldown;
        private int CurrentWayPointIndex;
        private bool active;
        private SpawnManager spawnManager;

        // Start is called before the first frame update
        void Start()
        {
            spawnManager = FindObjectOfType<SpawnManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if(!active)
            {
                return;
            }
            int nextWayPoint = CurrentWayPointIndex + 1;
            if( nextWayPoint > WayPoint.Length-1 )
            {
                nextWayPoint = 0;
            }
            transform.position = Vector3.MoveTowards(transform.position, WayPoint[nextWayPoint].position, MoveSpeed * Time.deltaTime);
            if( transform.position == WayPoint[nextWayPoint].position )
            {
                CurrentWayPointIndex = nextWayPoint;
            }
            Vector3 direction = WayPoint[nextWayPoint].position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            
            //firing
            if (TempCooldown <= 0)
            {
                Fire();
                TempCooldown = Random.Range(MinFiringCooldown,MaxFiringCooldown);
            }
            TempCooldown -= Time.deltaTime;
        }

        private void Fire()
        {
            //ProjectTileController projectTile = Instantiate(ProjectTile, FiringPoint.position, Quaternion.identity, null);
            ProjectTileController projectile = spawnManager.SpawnEnemyProjectile(FiringPoint.position);
            projectile.Fire();
        }
        public void Init(Transform[] way_point)
        {
            WayPoint = way_point;
            active = true;
            transform.position = way_point[0].position;
            TempCooldown = Random.Range(MinFiringCooldown, MaxFiringCooldown);
            current_hp = Hp;
        }
        public void Hit( int damage )
        {
            current_hp -= damage;
            if( current_hp <= 0 )
            {
                //Destroy(gameObject);
                spawnManager.ReleaseEnemy(this);
            }
        }
    }

}