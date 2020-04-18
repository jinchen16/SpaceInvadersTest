using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    [SerializeField]
    private AudioSource _audioSource;

    // TODO::Create a dictionary to save different audioclips.
    public AudioClip loseClip;

    private void Awake()
    {
        instance = this;
    }

    public void PlayLoseSFX()
    {
        _audioSource.clip = loseClip;
        _audioSource.Play();
    }
}
