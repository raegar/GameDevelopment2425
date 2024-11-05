/* Author  : Ignacy | https://github.com/ID274
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose : This script manages the SFX in the game. It is contacted by the SoundEmitter script to play or stop sounds.
 *           
 * Tip     : Check out the "README - Sound System" text file in SoundSystem/Scripts folder for more detailed information.
 */

using PatternLibrary;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    private AudioListener player;

    public float globalSFXVolume = 1;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        player = FindObjectOfType<AudioListener>();
        // Will need to get volume here from a settings system.
    }


    public void PlaySound(AudioSource audioSource, bool randomPitch, float minPitch, float maxPitch)
    {
        if (randomPitch)
        {
            audioSource.pitch = Random.Range(minPitch, maxPitch);
        }
        audioSource.Play();
    }
    
    public void StopSound(AudioSource audioSource)
    {
        audioSource.Stop();
    }

    public void StopAllSFX()
    {
        SoundEmitter[] soundEmitters = FindObjectsOfType<SoundEmitter>();
        foreach (SoundEmitter soundEmitter in soundEmitters)
        {
            soundEmitter.audioSource.Stop();
        }
    }

    public AudioListener GetListener()
    {
        return player;
    }
}
