using UnityEngine;

public class SoundReceiver : MonoBehaviour
{
    public void SoundBehaviour(int clipIndex, bool loop)
    {
        SoundObject soundObject = SpatialSFXPool.Instance.MoveSoundObjectToMe(gameObject).GetComponent<SoundObject>();
        Debug.Log(soundObject);
        SFXManager.Instance.SetUpAudioSource(clipIndex, soundObject);
        if (loop)
        {
            soundObject.audioSource.loop = true;
        }
        soundObject.PlaySound(soundObject.audioSource.clip);
    }
}
