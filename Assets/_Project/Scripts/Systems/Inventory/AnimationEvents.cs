using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public AudioClip sound;
    private AudioSource m_MyAudioSource;

    void Awake()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
    }
    public void PlayAnimationEventSound()
    {
        m_MyAudioSource.clip = sound;
        m_MyAudioSource.Play();
    }
}
