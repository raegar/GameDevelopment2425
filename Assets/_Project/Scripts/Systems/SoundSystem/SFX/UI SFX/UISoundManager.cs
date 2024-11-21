using PatternLibrary;
using System.Collections.Generic;
using UnityEngine;

public class UISoundManager : Singleton<UISoundManager>
{
    public List<SoundData> dataList;

    public AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(int index)
    {
        SetUpData(dataList[index]);
        audioSource.PlayOneShot(audioSource.clip);
    }

    public void StopUISounds()
    {
        audioSource.enabled = false;
        audioSource.enabled = true; // PlayOneShot cannot be stopped, so we disable and re-enable the audio source to stop the sound
    }

    private void SetUpData(SoundData soundData)
    {
        audioSource.clip = soundData.audioClip;
        audioSource.priority = soundData.priority;
        audioSource.volume = soundData.volume;
    }
}
