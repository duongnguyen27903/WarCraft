using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource Music;
    [SerializeField] private AudioClip HomeMusic;
    [SerializeField] private AudioClip BattleMusic;

    [SerializeField] private AudioSource Echo;

    [SerializeField] private AudioSource SFX;
    [SerializeField] private AudioClip LaserSFX;
    [SerializeField] private AudioClip PlasmaSFX;
    [SerializeField] private AudioClip HitSFX;
    [SerializeField] private AudioClip ExplosionSFX;

    public void PlayHomeMusic()
    {
        if( Music.clip ==  HomeMusic)
        {
            return;
        }
        Music.loop = true;
        Music.clip = HomeMusic;
        Music.Play();
    }
    public void PlayBattleMusic()
    {
        if( Music.clip == BattleMusic)
        {
            return;
        }
        Music.loop = true;
        Music.clip = BattleMusic;
        Music.Play();
    }

    public void PlayLaserSFX()
    {
        SFX.pitch = Random.Range(1f, 2f);
        SFX.PlayOneShot(LaserSFX);
    }
    public void PlayPlasmaSFX()
    {
        SFX.pitch = Random.Range(1f, 2f);
        SFX.PlayOneShot(PlasmaSFX);
    }
    public void PlayHitSFX()
    {
        SFX.pitch = Random.Range(1f, 2f);
        SFX.PlayOneShot(HitSFX);
    }
    public void PlayExplosionSFX()
    {
        Echo.pitch = Random.Range(1f, 2f);
        Echo.PlayOneShot(ExplosionSFX);
    }
}
