using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Playlist", menuName = "SoundSystem/Playlist", order = 1)]
public class Playlist : ScriptableObject
{
    public List<TrackData> playlist = new List<TrackData>();
}
