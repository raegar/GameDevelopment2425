using PatternLibrary;
using UnityEngine;

public class UISoundManager : Singleton<UISoundManager>
{
    [SerializeField] private SoundData[] dataList;

    public AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        Debug.Log($"UI Sound List Size: {dataList.Length}. Instance ID: {GetInstanceID()}", this);
    }

    public void PlaySound(int index)
    {
        if (dataList.Length <= index)
        {
            Debug.LogError($"UI Sound Index Out of Range: {index}. Datalist length: {dataList.Length}. Instance ID: {GetInstanceID()}", this);
            return;
        }
        else
        {
            Debug.Log($"UI Sound Index Called: {index}, {dataList[index]}");
            SetUpData(dataList[index]);
            audioSource.PlayOneShot(audioSource.clip);
        }
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
