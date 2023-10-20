using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WavesData",menuName = "Create Wave Data")]
public class WavesData : ScriptableObject
{
    [Range(1, 10)] public int totalGroups;
    [Range(1, 10)] public int minTotalEnemies;
    [Range(1, 10)] public int maxTotalEnemies;
    [Range(1, 10)] public float speedMultiplier;
}
