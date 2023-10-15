using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Section3
{
    public class EnemyPath : MonoBehaviour
    {
        [SerializeField] private Transform[] WayPoint;
        [SerializeField] private Color WayColor;
        [SerializeField] private bool show;
        public Transform[] WayPoints => WayPoint;
        private void OnDrawGizmos()
        {
            if( !show)
            {
                return;
            }
            if (WayPoint != null && WayPoint.Length >1 )
            {
                Gizmos.color = WayColor;
                for ( int i = 0; i < WayPoint.Length -1 ; i++ )
                {
                    Transform from = WayPoint[i];
                    Transform to = WayPoint[i+1];
                    Gizmos.DrawLine(from.position , to.position);
                }
                Gizmos.DrawLine(WayPoint[0].position , WayPoint[^1].position);
            }

        }
    }
}
