using UnityEngine;

public class SoundObject : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject linkedObject;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetUpData(SoundData soundData)
    {
        audioSource.clip = soundData.audioClip;
        audioSource.priority = soundData.priority;
        audioSource.volume = soundData.volume;
        audioSource.pitch = soundData.pitch;
        audioSource.maxDistance = soundData.maxDistance;

        if (soundData.randomPitch)
        {
            audioSource.pitch = Random.Range(audioSource.pitch - soundData.randomPitchRange, audioSource.pitch + soundData.randomPitchRange);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
        if (!audioSource.loop)
        {
            StartCoroutine(SpatialSFXPool.Instance.ReturnToPoolAfterSound(gameObject, clip.length));
        }
    }

    private void FixedUpdate()
    {
        if (linkedObject != null && !audioSource.isPlaying)
        {
            SpatialSFXPool.Instance.StopCoroutine(SpatialSFXPool.Instance.ReturnToPoolAfterSound(gameObject, audioSource.clip.length));
            SpatialSFXPool.Instance.ReturnToPoolAfterSoundInstant(gameObject);
        }
    }

}
