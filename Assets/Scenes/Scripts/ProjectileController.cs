using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Section3
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] private float MoveSpeed;
        [SerializeField] private Vector2 Direction;
        [SerializeField] private int Damage;

        private float life_time;

        void OnEnable()
        {
            if( gameObject.layer == 8 )
            {
                life_time = 1f;
            }
            else if( gameObject.layer == 9 )
            {
                life_time = 10f;
            }
        }
        void Update()
        {
            transform.Translate(MoveSpeed * Time.deltaTime * Direction);
            if (life_time <= 0)
            {
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }
            life_time -= Time.deltaTime;
        }

        // function make physic collision
        //private void OnCollisionEnter2D(Collision2D collision)
        //{
        //    Debug.Log(collision.gameObject.name);
        //}

        //function check trigger collision
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                //Destroy(gameObject);
                gameObject.SetActive(false);
                collision.gameObject.TryGetComponent(out EnemyController enemy);
                enemy.Hit(Damage);
            }
            if (collision.gameObject.CompareTag("Player"))
            {
                //Destroy(gameObject);
                gameObject.SetActive(false);
                collision.gameObject.TryGetComponent(out PlayerController player);
                player.Hit(Damage);
            }
        }
    }
}