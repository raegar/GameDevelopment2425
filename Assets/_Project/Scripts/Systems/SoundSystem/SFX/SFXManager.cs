using PatternLibrary;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{
    public SoundData[] dataList;

    public void StopSFXPooled(GameObject soundObject)
    {
        SpatialSFXPool.Instance.ReturnToPool(soundObject.gameObject);
    }

    public void StopSFX(AudioSource audioSource)
    {
        audioSource.Stop();
    }

    public void StopAllSFX()
    {
        SoundObject[] soundObjects = FindObjectsOfType<SoundObject>();
        foreach (SoundObject soundObject in soundObjects)
        {
            soundObject.StopAllCoroutines();
            SpatialSFXPool.Instance.ReturnToPool(soundObject.gameObject);
        }
    }

    public void SetUpAudioSource(int index, SoundObject soundObject)
    {
        soundObject.SetUpData(dataList[index]);
    }
}
