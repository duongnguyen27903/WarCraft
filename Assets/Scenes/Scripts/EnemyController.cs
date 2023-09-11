using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Section3
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float MoveSpeed;
        [SerializeField] private Transform[] WayPoint;

        private int CurrentWayPointIndex;
        private bool active;
        // Start is called before the first frame update
        void Start()
        {

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
        }

        public void Init(Transform[] way_point)
        {
            WayPoint = way_point;
            active = true;
            transform.position = way_point[0].position;
        }
    }

}