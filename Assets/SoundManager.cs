using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip GameStartSound;
    public AudioClip StartCoundDownSound;
    public AudioClip EndCountDownSound;

    public AudioClip DartFlySound;
    public AudioClip BallonBoomSound;

    public AudioClip ButtonClickSound;

    AudioSource audioSource;

    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlaySound(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }

    public void ClickButton()
    {
        audioSource.PlayOneShot(ButtonClickSound);
    }
}
