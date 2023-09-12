using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Section3
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float MoveSpeed;
        [SerializeField] private ProjectTileController ProjectTile;
        [SerializeField] private Transform FiringPoint;
        [SerializeField] private float FiringCooldown;
        [SerializeField] private int Hp;

        private int current_hp;
        private float TempCooldown;
        // Start is called before the first frame update
        void Start()
        {
            current_hp = Hp;
        }

        // Update is called once per frame
        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector2 direction = new Vector2(horizontal, vertical);
            transform.Translate(direction * Time.deltaTime * MoveSpeed);

            if( Input.GetKey(KeyCode.Space) )
            {
                if( TempCooldown <=0 )
                {
                    Fire();
                    TempCooldown = FiringCooldown;
                }
                TempCooldown -= Time.deltaTime;
            }
        }
        private void Fire()
        {
            ProjectTileController projectTile =  Instantiate(ProjectTile, FiringPoint.position,Quaternion.identity,null);
            projectTile.Fire(3f);
        }
        public void Hit(int damage)
        {
            current_hp -= damage;
            if (current_hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
