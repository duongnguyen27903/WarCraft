using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Section3
{
    public class ProjectTileController : MonoBehaviour
    {
        [SerializeField] private float MoveSpeed;
        [SerializeField] private Vector2 Direction;
        [SerializeField] private int Damage;

        private bool from_player;
        private SpawnManager spawnManager;
        private float life_time;
        // Start is called before the first frame update
        void Start()
        {
            spawnManager = FindObjectOfType<SpawnManager>();
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(MoveSpeed * Time.deltaTime * Direction);
            if( life_time <= 0 )
            {
                Release();
            }
            life_time -= Time.deltaTime;
        }
        public void SetFromPlayer(bool value)
        {
            from_player = value;
        }

        public void Fire()
        {
            if (from_player)
            {
                life_time = 3f;
            }
            else life_time = 10f;
        }

        private void Release()
        {
            if (from_player)
                spawnManager.ReleasePlayerProjectile(this);
            else 
                spawnManager.ReleaseEnemyProjectile(this);
        }

        // function make physic collision
        //private void OnCollisionEnter2D(Collision2D collision)
        //{
        //    Debug.Log(collision.gameObject.name);
        //}

        //function check trigger collision
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Trigger" + collision.gameObject.name);
            if(collision.gameObject.CompareTag("Enemy"))
            {
                Release();
                collision.gameObject.TryGetComponent(out EnemyController enemy);
                enemy.Hit(Damage);
            }
            if (collision.gameObject.CompareTag("Player"))
            {
                Release();
                collision.gameObject.TryGetComponent(out PlayerController player);
                player.Hit(Damage);
            }
        }
    }

}