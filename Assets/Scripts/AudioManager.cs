using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    // Player
    [SerializeField] private AudioSource _audioSource1;

    // Clips
    [SerializeField] private AudioClip _buttonClip;
    [SerializeField] private AudioClip _tzClip;
    [SerializeField] private AudioClip _grabClip;
    [SerializeField] private AudioClip _placeClip;
    [SerializeField] private AudioClip _oopsClip;

    private void Awake()
    {
        if (Instance != null) Destroy(this);
        Instance = this;
    }

    public void PlaySFX_Button()
    {
        _audioSource1.PlayOneShot(_buttonClip);
    }

    public void PlaySFX_TZ()
    {
        _audioSource1.PlayOneShot(_tzClip);
    }

    public void PlaySFX_EggGrab()
    {
        _audioSource1.PlayOneShot(_grabClip);
    }

    public void PlaySFX_EggPlace()
    {
        _audioSource1.PlayOneShot(_placeClip);
    }

    public void PlaySFX_Oops()
    {
        _audioSource1.PlayOneShot(_oopsClip);
    }   
}
