using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Section3
{
    public class ProjectTileController : MonoBehaviour
    {
        [SerializeField] private float MoveSpeed;
        [SerializeField] private Vector2 Direction; 
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
    }

}