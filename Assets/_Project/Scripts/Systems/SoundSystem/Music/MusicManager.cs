using PatternLibrary;
using System;
using System.Collections;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    public AudioSource musicSource; // the audio source component that plays the music

    [SerializeField] private Playlist currentPlaylist; // the currently selected playlist
    [SerializeField] private TrackData currentSong; // the currently playing song and its data (track name, artist name and the actual audio clip)
    public Playlist[] playlists; // array of available playlists

    [SerializeField] private float timeElapsed; // time elapsed since the current track started playing
    [SerializeField] private float fadeTime; // duration of the fade-out and fade-in effect

    private bool isFading; // flag to indicate if a fade operation is in progress

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        musicSource = GetComponent<AudioSource>(); // get the audio source component attached to this game object
        if (playlists.Length > 0)
        {
            currentPlaylist = playlists[0]; // set the initial playlist to the first one in the array
        }
    }

    public void DebugCurrentTrack()
    {
        TimeSpan t = TimeSpan.FromSeconds(musicSource.clip.length);
        string str = t.ToString("mm':'ss");
        Debug.Log($"Current Track: {currentSong.trackName} by {currentSong.artist} ({str})");
    }

    public void PlayMusic()
    {
        musicSource.Play();
        DebugCurrentTrack();
    }

    public void PlayPlaylist(bool fadeout)
    {
        if (!isFading)
        {
            // play the first track of the current playlist
            StartCoroutine(FadeOutAndPlay(fadeout, currentPlaylist.playlist[0]));
        }
    }

    public void PlayPlaylist(bool fadeout, Playlist playlist)
    {
        if (!isFading)
        {
            currentPlaylist = playlist; // set the current playlist to the specified one
            // play the first track of the new playlist
            StartCoroutine(FadeOutAndPlay(fadeout, currentPlaylist.playlist[0]));
        }
    }

    public void PlayNextTrack(bool fadeout)
    {
        if (!isFading && currentPlaylist.playlist.Count > 1) // check if there are multiple tracks in the playlist
        {
            // find the index of the current track in the playlist
            int currentTrackIndex = currentPlaylist.playlist.FindIndex(track => track.song == musicSource.clip);
            int nextTrackIndex = currentTrackIndex + 1; // get the index of the next track
            if (nextTrackIndex >= currentPlaylist.playlist.Count) // if it's the last track, loop back to the first
            {
                nextTrackIndex = 0;
            }
            // play the next track in the playlist
            StartCoroutine(FadeOutAndPlay(fadeout, currentPlaylist.playlist[nextTrackIndex]));
        }
    }

    public void PlayNextTrack(bool fadeout, TrackData track)
    {
        if (!isFading)
        {
            // find the index of the specified track in the current playlist
            int nextTrackIndex = currentPlaylist.playlist.FindIndex(t => t == track);
            // play the specified track
            StartCoroutine(FadeOutAndPlay(fadeout, currentPlaylist.playlist[nextTrackIndex]));
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void ToggleMusic()
    {
        // toggle between playing and pausing the music
        switch (musicSource.isPlaying)
        {
            case true:
                musicSource.Pause();
                break;
            case false:
                musicSource.Play();
                break;
        }
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    private void Update()
    {
        if (musicSource.isPlaying)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= musicSource.clip.length)
            {
                timeElapsed = 0;
                PlayNextTrack(false);
            }
        }
    }


    private IEnumerator FadeOutAndPlay(bool fadeout, TrackData nextTrack)
    {
        isFading = true; // set the flag to indicate a fade operation is in progress
        float startVolume = musicSource.volume; // store the initial volume

        if (fadeout)
        {
            // gradually reduce the volume to zero
            while (musicSource.volume > 0)
            {
                musicSource.volume -= startVolume * Time.deltaTime / fadeTime;
                yield return null; // wait for the next frame
            }
        }

        musicSource.Stop(); // stop the current track
        musicSource.clip = nextTrack.song; // set the next track to play
        currentSong = nextTrack; // update the current song
        timeElapsed = 0; // reset the time elapsed
        musicSource.Play(); // play the next track

        // gradually increase the volume to the initial value
        while (musicSource.volume < startVolume)
        {
            musicSource.volume += startVolume * Time.deltaTime / fadeTime;
            yield return null; // wait for the next frame
        }

        musicSource.volume = startVolume; // ensure the volume is set to the initial value
        DebugCurrentTrack(); // log the next track details
        isFading = false; // reset the flag to indicate the fade operation is complete
    }
}
