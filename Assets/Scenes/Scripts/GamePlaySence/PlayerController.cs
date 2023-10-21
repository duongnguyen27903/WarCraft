using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Section3
{
    public class PlayerController : MonoBehaviour
    {
        public Action<int, int> onHpChanged;

        [SerializeField] private float MoveSpeed;
        [SerializeField] private ProjectileController ProjectTile;
        [SerializeField] private Transform FiringPoint;
        [SerializeField] private float FiringCooldown;
        [SerializeField] private int Hp;

        private int current_hp;
        private float TempCooldown;

        private GameManager gameManager;
        private AudioManager audioManager;
        
        // Start is called before the first frame update
        void Start()
        {
            current_hp = Hp;
            if( onHpChanged != null)
            {
                onHpChanged(current_hp, Hp);
            }
            gameManager = FindObjectOfType<GameManager>();
            audioManager = FindObjectOfType<AudioManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if(!gameManager.CanMove())
            {
                return;
            }
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector2 direction = new(horizontal, vertical);
            transform.Translate(MoveSpeed * Time.deltaTime * direction);

            if (Input.GetKey(KeyCode.Space))
            {
                if (TempCooldown <= 0)
                {
                    Fire();
                    audioManager.PlayLaserSFX();
                    TempCooldown = FiringCooldown;
                }
                TempCooldown -= Time.deltaTime;
            }
        }

        public void PowerUp()
        {
            current_hp = Hp;
            if (onHpChanged != null)
            {
                onHpChanged(current_hp, Hp);
            }
        }
        private void Fire()
        {
            //ProjectileController projectile = Instantiate(ProjectTile, FiringPoint.position, Quaternion.identity, null);
            //projectile.Fire();

            GameObject bullet = ObjectPool.instance.PlayerFiring();
            if( bullet != null)
            {
                bullet.transform.position = FiringPoint.position;
                bullet.SetActive(true);
            }

            GameObject shootFX = ParticleFXPool.instance.GetShootingFX();
            if( shootFX != null )
            {
                shootFX.transform.position = FiringPoint.position;
                shootFX.SetActive(true);
            }
        }
        public void Hit(int damage)
        {
            current_hp -= damage;
            if( onHpChanged != null)
            {
                onHpChanged(current_hp, Hp);
            }
            audioManager.PlayHitSFX();
            GameObject hitFX = ParticleFXPool.instance.GetHitFX();
            if( hitFX != null )
            {
                hitFX.transform.position = this.transform.position;
                hitFX.SetActive(true);
            }
            if (current_hp <= 0)
            {
                audioManager.PlayExplosionSFX();
                GameObject expolsionFX = ParticleFXPool.instance.GetExplosionFX();
                expolsionFX.transform.position = gameObject.transform.position;
                expolsionFX.SetActive(true);
                Destroy(gameObject);
                gameManager.Set_Game_State(GameManager.GameState.GameOver);
                gameManager.GameOver(false);
                Time.timeScale = 0;
                
                GameObject obj = GameObject.Find("/GameManager/Canvas/GameOverPanel/NextLevel");
                if (obj != null)
                {
                    Debug.Log(obj.name);
                    obj.SetActive(false);
                }
                if ( GameManager.instance.Current_Wave == GameManager.instance.Get_Waves.Length-1 )
                {
                    //using Find to search gameObject with their name and path by level
                    
                }
            }
        }
    }
}