using System.Collections;
using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    public AudioSource audioSource;

    [Header("Audio Source Attributes")]
    [SerializeField] private float emissionRange = 1f;
    [SerializeField] private bool randomPitch;
    [SerializeField] private float minPitch = 0.95f, maxPitch = 1.05f, standardPitch, volume;
    [SerializeField] private bool loop, playOnAwake;
    [SerializeField] private float minDelay, maxDelay; // Added delay for the loop that is randomised to make the sound more natural
    private float loopDelay = 0f; // Added delay for the loop
    [SerializeField] private float percentageVolumeDecrease = 0;

    [Header("Player Interaction")]
    [SerializeField] private Vector3 playerPos;
    [SerializeField] private float distanceFromPlayer;

    private float timeElapsed = 0; // This float is used to simulate a disabled sound playing, so that it isn't restarted each time player comes in range.
    private float clipLength; // The length of the audio clip
    private bool isPaused = false; // Flag to control pausing
    private Coroutine loopCoroutine; // Reference to the loop coroutine

    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError($"No audio source found on {gameObject.name}");
            }
        }
        // Set initial audio source attributes
        audioSource.pitch = standardPitch;
        audioSource.maxDistance = emissionRange;
        audioSource.volume = SoundManager.Instance.globalSFXVolume - (percentageVolumeDecrease / 100);
        playerPos = SoundManager.Instance.GetListener().transform.position;

        if (playOnAwake)
        {
            if (loop)
            {
                PlayLoopedSound();
            }
            else
            {
                PlaySound();
            }
        }
    }

    private void FixedUpdate()
    {
        clipLength = audioSource.clip.length;
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= clipLength)
        {
            timeElapsed = 0;
        }

        if (playerPos != null)
        {
            // If distance from the player is greater than the emission range, disable the audio source as we don't need it.
            distanceFromPlayer = Vector3.Distance(playerPos, transform.position);
            if (distanceFromPlayer > emissionRange && audioSource.enabled)
            {
                audioSource.enabled = false;
            }
            else if (distanceFromPlayer <= emissionRange && !audioSource.enabled)
            {
                audioSource.enabled = true;
                audioSource.time = timeElapsed; // Schedule the sound to play as if it was enabled the whole time.
                audioSource.Play();
            }
        }
        else
        {
            // If the player is not found, get the listener again
            playerPos = SoundManager.Instance.GetListener().transform.position;
        }
    }

    public void PlaySound()
    {
        if (audioSource.enabled)
        {
            // Play the sound via the SoundManager using either the standard pitch or a random pitch within the specified range
            SoundManager.Instance.PlaySound(audioSource, randomPitch, minPitch, maxPitch);
        }
    }

    public void PlayLoopedSound()
    {
        if (audioSource.enabled && loopCoroutine == null)
        {
            loopCoroutine = StartCoroutine(PlayContinuousSound());
        }
    }

    public void PauseLoopedSound()
    {
        isPaused = true;
    }

    public void ResumeLoopedSound()
    {
        isPaused = false;
    }

    public void StopLoopedSound()
    {
        if (loopCoroutine != null)
        {
            StopCoroutine(loopCoroutine);
            loopCoroutine = null;
            audioSource.Stop(); // Stop the audio source
        }
    }

    public void StopSound()
    {
        if (audioSource.enabled)
        {
            // Stop the sound via the SoundManager
            SoundManager.Instance.StopSound(audioSource);
            StopLoopedSound();
        }
    }

    private IEnumerator PlayContinuousSound()
    {
        while (true)
        {
            if (!isPaused)
            {
                if (minDelay != maxDelay)
                {
                    loopDelay = Random.Range(minDelay, maxDelay); // Randomize the delay
                }
                else
                {
                    loopDelay = minDelay;
                }
                PlaySound();
                yield return new WaitForSeconds(clipLength + loopDelay); // Wait for the clip length plus the delay
            }
            else
            {
                yield return null; // Wait until the next frame
            }
        }
    }
}