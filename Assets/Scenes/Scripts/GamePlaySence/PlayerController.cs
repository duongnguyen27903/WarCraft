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
        [SerializeField] private bool UseNewInputSystem;

        private int current_hp;
        private float TempCooldown;

        private GameManager gameManager;
        private AudioManager audioManager;

        private PlayerInput Player_Input;
        private Vector2 MovementInputValue;
        private bool AttackInputValue;

        private void OnEnable()
        {
            if( Player_Input == null)
            {
                Player_Input = new PlayerInput();
                Player_Input.Player.Movement.started += OnMovement ;
                Player_Input.Player.Movement.performed += OnMovement;
                Player_Input.Player.Movement.canceled += OnMovement;
                Player_Input.Player.Attack.started += OnAttack;
                Player_Input.Player.Attack.performed += OnAttack;
                Player_Input.Player.Attack.canceled += OnAttack;
                Player_Input.Enable();
            }
        }

        private void OnAttack(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if( context.started)
            {
                AttackInputValue = true;
            }
            else if( context.performed)
            {
                AttackInputValue = true;
            }
            else if( context.canceled)
            {
                AttackInputValue = false;
            }
        }

        private void OnMovement(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if (context.started)
            {
                MovementInputValue = context.ReadValue<Vector2>();
            }
            else if (context.performed)
            {
                MovementInputValue = context.ReadValue<Vector2>();
            }
            else if (context.canceled)
            {
                MovementInputValue = Vector2.zero;
            }
        }

        private void OnDisable()
        {
            Player_Input.Disable();
        }

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
            Vector2 direction = Vector2.zero;
            if ( !UseNewInputSystem )
            {
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                direction = new Vector2(horizontal, vertical);

                if (Input.GetKey(KeyCode.Space))
                {
                    if (TempCooldown <= 0)
                    {
                        Fire();
                        audioManager.PlayLaserSFX();
                        TempCooldown = FiringCooldown;
                    }
                }
            }
            else
            {
                direction = MovementInputValue;

                if( AttackInputValue == true)
                {
                    if (TempCooldown <= 0)
                    {
                        Fire();
                        audioManager.PlayLaserSFX();
                        TempCooldown = FiringCooldown;
                    }
                }
            }
            
            transform.Translate(MoveSpeed * Time.deltaTime * direction);
            TempCooldown -= Time.deltaTime;
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