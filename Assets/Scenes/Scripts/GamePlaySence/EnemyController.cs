using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Section3
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float MoveSpeed;
        [SerializeField] private Transform[] WayPoint;
        [SerializeField] private ProjectileController ProjectTile;
        [SerializeField] private Transform FiringPoint;
        [SerializeField] private float MinFiringCooldown;
        [SerializeField] private float MaxFiringCooldown;
        [SerializeField] private int Hp;

        private int current_hp;
        private float TempCooldown;
        private int CurrentWayPointIndex;
        private bool active;
        private SpawnManager spawnManager;
        private GameManager gameManager;
        private AudioManager audioManager;
        private float current_movespeed;
        private float SpeedMultiplier;

        void Start()
        {
            spawnManager = FindObjectOfType<SpawnManager>();
            gameManager = FindObjectOfType<GameManager>();
            audioManager = FindObjectOfType<AudioManager>();
        }
        
        void Update()
        {
            if (!active)
            {
                return;
            }
            int nextWayPoint = CurrentWayPointIndex + 1;
            if (nextWayPoint > WayPoint.Length - 1)
            {
                nextWayPoint = 0;
            }
            transform.position = Vector3.MoveTowards(transform.position, WayPoint[nextWayPoint].position, current_movespeed * Time.deltaTime);
            if (transform.position == WayPoint[nextWayPoint].position)
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
                audioManager.PlayPlasmaSFX();
                TempCooldown = UnityEngine.Random.Range(MinFiringCooldown, MaxFiringCooldown);
            }
            TempCooldown -= Time.deltaTime;
        }

        private void Fire()
        {
            //ProjectileController projectile = Instantiate(ProjectTile, FiringPoint.position, Quaternion.identity, null);

            ProjectileController obj = ObjectPool.instance.EnemyFiring();
            if (obj != null)
            {
                obj.Adjust(SpeedMultiplier);
                obj.gameObject.transform.position = FiringPoint.position;
                obj.gameObject.SetActive(true);
            }
        }
        public void Init(Transform[] way_point, float speed_multiplier)
        {
            WayPoint = way_point;
            SpeedMultiplier = speed_multiplier;
            current_movespeed = MoveSpeed * speed_multiplier;
            active = true;
            transform.position = way_point[0].position;
            TempCooldown = UnityEngine.Random.Range(MinFiringCooldown, MaxFiringCooldown)/speed_multiplier;
            current_hp = Hp;
        }
        public void Hit(int damage)
        {
            current_hp -= damage;
            audioManager.PlayHitSFX();
            GameObject hitFX = ParticleFXPool.instance.GetHitFX();
            if (hitFX != null)
            {
                hitFX.transform.position = gameObject.transform.position;
                hitFX.SetActive(true);
            }
            if (current_hp <= 0)
            {
                //Destroy(gameObject);
                GameObject explosionFX = ParticleFXPool.instance.GetExplosionFX();
                explosionFX.transform.position = gameObject.transform.position;
                explosionFX.SetActive(true);
                spawnManager.ReleaseEnemies(this);
                GameManager.instance.AddScore(1);
                audioManager.PlayExplosionSFX();
            }
        }
    }

}