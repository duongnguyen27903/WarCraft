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
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Direction * Time.deltaTime * MoveSpeed);
        }

        public void Fire(float destroy_time)
        {
            Destroy(gameObject, destroy_time);
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
                Destroy(gameObject);
                EnemyController enemy;
                collision.gameObject.TryGetComponent(out enemy);
                enemy.Hit(Damage);
            }
            if (collision.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
                PlayerController player;
                collision.gameObject.TryGetComponent(out player);
                player.Hit(Damage);
            }
        }
    }

}