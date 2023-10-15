using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFXPool : MonoBehaviour
{
    public static ParticleFXPool instance;
    private readonly List<GameObject> ShootingFXPool = new();
    private readonly List<GameObject> HitFXPool = new();
    private readonly List<GameObject> ExplosionFXPool = new();
    [SerializeField] private GameObject ShootingFX;
    [SerializeField] private GameObject HitFX;
    [SerializeField] private GameObject ExplosionFX;

    private void Awake()
    {
        if( instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        for( int i = 0; i < 5; i++ )
        {
            GameObject shoot_fx = Instantiate(ShootingFX);
            shoot_fx.SetActive(false);
            ShootingFXPool.Add(shoot_fx);
            GameObject hit_fx = Instantiate(HitFX);
            hit_fx.SetActive(false);
            HitFXPool.Add(hit_fx);
        }
    }
    public GameObject GetShootingFX()
    {
        for( int i = 0; i < ShootingFXPool.Count; i++ )
        {
            if( ShootingFXPool[i].activeInHierarchy == false)
            {
                return ShootingFXPool[i];
            }
        }
        return null;
    }

    public GameObject GetHitFX()
    {
        for( int i = 0; i < HitFXPool.Count; i++)
        {
            if(HitFXPool[i].activeInHierarchy == false)
            {
                return HitFXPool[i];
            }
        }
        return null;
    }

    public GameObject GetExplosionFX()
    {
        for( int i = 0;i < ExplosionFXPool.Count;i++)
        {
            if(ExplosionFXPool[i].activeInHierarchy == false)
            {
                return ExplosionFXPool[i];
            }
        }
        GameObject new_expolsion = Instantiate(ExplosionFX);
        new_expolsion.SetActive(false);
        ExplosionFXPool.Add(new_expolsion);
        return new_expolsion;
    }

    public void ClearAllFX()
    {
        for( int i = 0; i < HitFXPool.Count; i++)
        {
            HitFXPool[i].SetActive(false);
        }
        for( int i = 0;i< ExplosionFXPool.Count; i++)
        {
            ExplosionFXPool[i].SetActive(false);
        }
        for( int i = 0; i < ShootingFXPool.Count; i++)
        {
            ShootingFXPool[i].SetActive(false);
        }
    }
}
