using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering;

public class audioManager : MonoBehaviour
{
    public static audioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private AudioSource stepSrc;

    private float musicVol = 1;
    private float sfxVol = 1;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        //musicSource.volume = musicVol;
    }

    private void Start()
    {
        stepSrc = gameObject.GetComponent<AudioSource>();
    }

    public void PlayMusic(string name)
    {

        Sound s = Array.Find(musicSounds, x => x.name == name);

        if(s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name, AudioSource src)
    {
        src.volume = sfxVol;

        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if(s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            src.PlayOneShot(s.clip);
        }
    }

    public void PlaySteps()
    {
        int sNum = UnityEngine.Random.Range(1, 6);

        Sound s = Array.Find(sfxSounds, x => x.name == ("Step" + sNum));

        if(s == null)
        {
            Debug.Log("Sound not found");
        }

        stepSrc.PlayOneShot(s.clip);
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicVol = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxVol = volume;
    }

}