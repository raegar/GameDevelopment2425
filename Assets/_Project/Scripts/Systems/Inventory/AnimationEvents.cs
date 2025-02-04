using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public AudioClip cutwood;
    private AudioSource m_MyAudioSource;

    void Awake()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
    }
    public void PlayCutWoodSound()
    {
        m_MyAudioSource.clip = cutwood;
        m_MyAudioSource.Play();
    }
}
