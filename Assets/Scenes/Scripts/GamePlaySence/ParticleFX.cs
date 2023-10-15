using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFX : MonoBehaviour
{
    [SerializeField] private float LifeTime;
    private float current_life_time;
    private void OnEnable()
    {
        current_life_time = LifeTime;
    }

    void Update()
    {
        if( current_life_time <= 0)
        {
            gameObject.SetActive(false);
        }
        current_life_time -= Time.deltaTime;
    }
}
