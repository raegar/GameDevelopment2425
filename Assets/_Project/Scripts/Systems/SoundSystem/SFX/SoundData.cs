using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "NewSoundData", menuName = "SoundSystem/SoundData", order = 51)]
public class SoundData : ScriptableObject
{
    [Header("The clip to be played")]
    public AudioClip audioClip;

    [Header("(0 to 256) How important is the sound in the scene")]
    [Range(0, 256)]
    public int priority = 128;

    [Header("(0 to 1) Volume AFTER settings have been applied to Audio Mixer")]
    [Range(0f, 1f)]
    public float volume = 1f;

    [Header("(-3 to 3) Pitch affects playback speed. Try to stay between 0.5 and 1.5")]
    [Range(-3f, 3f)]
    public float pitch = 1f;

    [Header("Random pitch?")]
    public bool randomPitch;

    [Header("The range in pitch (recommended to not exceed 1)")]
    public float randomPitchRange = 0.1f;

    [Header("(0.1 to 500) Range at which the sound can be heard")]
    [Range(0.1f, 500f)] // Unity's default max distance for 3D sound range is 500
    public float maxDistance = 500f;
}
