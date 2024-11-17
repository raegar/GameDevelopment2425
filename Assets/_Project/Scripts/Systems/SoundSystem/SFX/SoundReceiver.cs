using UnityEngine;

public class SoundReceiver : MonoBehaviour
{
    public SoundObject soundObject;

    public void SoundBehaviour(int clipIndex, bool loop)
    {
        GameObject soundObjectGameObject = SpatialSFXPool.Instance.MoveSoundObjectToMe(gameObject);
        if (soundObjectGameObject == null)
        {
            Debug.LogWarning("MoveSoundObjectToMe returned null - pool capacity might have been reached");
            return;
        }
        soundObject = soundObjectGameObject.GetComponent<SoundObject>();

        Debug.Log(soundObject);
        SFXManager.Instance.SetUpAudioSource(clipIndex, soundObject);
        if (loop)
        {
            soundObject.audioSource.loop = true;
        }
        else
        {
            soundObject.audioSource.loop = false;
        }
        soundObject.PlaySound(soundObject.audioSource.clip);
    }

    public void StopSound()
    {
        soundObject.audioSource.Stop();
    }
}
