using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Section3
{
    public class EnemyPath : MonoBehaviour
    {
        [SerializeField] private Transform[] WayPoint;
        private void OnDrawGizmos()
        {
            if (WayPoint != null && WayPoint.Length >1 )
            {
                for( int i = 0; i < WayPoint.Length -1 ; i++ )
                {
                    Transform from = WayPoint[i];
                    Transform to = WayPoint[i+1];
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(from.position , to.position);
                }
                Gizmos.DrawLine(WayPoint[0].position , WayPoint[WayPoint.Length-1].position);
            }

        }
    }
}
