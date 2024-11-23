using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using DG.Tweening;

[Serializable]
public struct Clips
{
    public AudioClip fSkill;
    public AudioClip move;
    public AudioClip button;
    public AudioClip turn;
    public AudioClip walk;
    public AudioClip clear;
}

[Serializable]
public struct Clips3D
{
    public AudioClip tick;
    public AudioClip tack;
    public AudioClip killEnemy;
    public AudioClip missile;
    public AudioClip sniper;
    public AudioClip attach;
}

public class SoundManager : SingleTon<SoundManager>
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider SFXSlider;
    private AudioSource _audioSource;

    [Header("Sound")]
    public AudioClip inGameBGM;
    public AudioClip mainBGM;
    public AudioSource bgmSource;
    public AudioSource loopSource;
    public AudioSource echoSource;

    public Clips3D clips3D;
    public Clips clips;
    [HideInInspector] public bool loopPlaying = false;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        //bgmSource.clip = mainBGM;
        //bgmSource.Play();
        Apply();
    }
    public void Apply()
    {
        BGMSlider.value = JsonManager.Instance.BGM;
        SFXSlider.value = JsonManager.Instance.SFX;
        audioMixer.SetFloat("BGM", Mathf.Log10(BGMSlider.value) * 20f);
        audioMixer.SetFloat("SFX", Mathf.Log10(SFXSlider.value) * 20f);
    }

    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20f);
        JsonManager.Instance.BGM = volume;
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20f);
        JsonManager.Instance.SFX = volume;
    }
    public void PlayAudio(AudioClip clip, float volumn = 0.4f)
    {
        _audioSource.PlayOneShot(clip, volumn);
    }
    public void PlayAudio(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip, 1f);
    }
    public void PlayEcho(AudioClip clip, float volumn = 0.4f)
    {
        echoSource.PlayOneShot(clip, volumn);
    }
    public void PlayButton(float volumn)
    {
        PlayAudio(clips.button, volumn);
    }
    public void PlayLoop(AudioClip clip, bool play, float volumn = 0.4f)
    {
        if (play)
        {
            loopSource.clip = clip;
            loopSource.Play();
            loopPlaying = true;
        }
        else
        {
            loopSource.Stop();
            loopPlaying = false;
        }
    }
    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.Play();
    }
    public void ChangeSmooth(AudioClip clip)
    {
        bgmSource.DOFade(0, 1f).OnComplete(() =>
        {
            bgmSource.Stop();
            bgmSource.volume = 1f;
            bgmSource.clip = clip;
            bgmSource.Play();
        });
    }
    public void StopSmooth()
    {
        bgmSource.DOFade(0, 1f).OnComplete(() =>
        {
            bgmSource.Stop();
        });
    }
    public void Pause()
    {
        bgmSource.Pause();
        loopSource.Pause();
        echoSource.Pause();
        _audioSource.Pause();
    }
    public void Unpause()
    {
        bgmSource.UnPause();
        loopSource.UnPause();
        echoSource.UnPause();
        _audioSource.UnPause();
    }
}