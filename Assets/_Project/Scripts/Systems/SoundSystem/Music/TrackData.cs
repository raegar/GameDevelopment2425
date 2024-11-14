using UnityEngine;

[CreateAssetMenu(fileName = "TrackName", menuName = "SoundSystem/TrackData", order = 1)]
public class TrackData : ScriptableObject
{
    public string trackName;
    public string artist;
    public AudioClip song;
}
