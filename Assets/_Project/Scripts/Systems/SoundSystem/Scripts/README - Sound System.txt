***************** SOUND SYSTEM README *****************
/* Author  : Ignacy | https://github.com/ID274
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose : This readme is an outline of the sound system's functionality, as well as a guide to testing the scene and getting familiar with the system.
 */

>>>>>>>>>>>>>>>> Test Scene Tutorial <<<<<<<<<<<<<<<<<<<

>>> Scene Overview:

In the scene you should find a bunch of labelled buttons, and 2 ball objects representing vikings. One of them is responsible for footstep sounds and the other for 
grunts.

>>> Testing the Sound Emitter:

To test the sound emitter, enter playmode and use the play sound buttons. The three buttons on the right correspond to the footstep object whereas the 2 bigger buttons
correspond to the grunt object. If using a headset, you should notice that the sound is directional and a different volume depending on the distance from the camera.
To test this, play around with the camera position in the editor while in playmode while you have the footsteps on loop. You will be able to clearly tell the difference
based on distance from and angle to the camera. You can also stop the sounds with the stop buttons.

>>> Testing the Music Manager:

To test the music manager, enter playmode and use the music/playlist labelled buttons. This one should be self-explanatory for the most part. The difference between the
"Play Playlist" and the "Next Song In Playlist" buttons is that the former will play the playlist from the start, whereas the latter will play the next song in the
playlist.

The "Play/Pause Music" button will toggle the music on and off while preserving the position in the audio clip.


>>>>>>>>>>>>>>>> Important Information <<<<<<<<<<<<<<<<<<<

The MusicManager and SoundManager are singletons, and as such should not be destroyed when changing scenes. This is to ensure that the music and sound effects are
working at all times. These must be made into prefabs and placed in the appropriate folder.

The SoundEmitter prefab should be attached to any object that should make sound in the game world. This means any sound that should use 3D sound, so not UI.
Feel free to use multiple SoundEmitters for a single object as it should be somewhat lightweight.

The system will not work if:

@ The SoundManager or MusicManager are not in the scene.
@ The SoundEmitter is not attached to an object that has an Audio Source component.
@ The Audio Listener is not attached to the camera object.
@ The Audio Listener is not in range of the SoundEmitter.
@ The Audio Listener is disabled.
@ If nothing is calling the appropriate methods. The system is intended to be customised to the designers' needs, meaning it needs to be called from the appropriate
  places in the game code, as well as modified on a case by case basis (footsteps and grunt sounds will be set up differently for instance). It is set up using the
  buttons in the test scene, but in the real game, it will need to be called via scripts.


>>>>>>>>>>>>>>>> Key Features <<<<<<<<<<<<<<<<<<<

>>> SoundEmitter:

The sound emitter prefab is used to play sounds in 3D space. This means that it will only be heard if an Audio Listener is in range.
With my this implementation, the Audio Source is disabled if the distance from the player (the camera in our case which has the Audio Listener)
is outside of the emissionRange. This is done to save performance, as the sound will not be heard anyway. Other notable features of the sound emitter are:

-	The pitch can be randomised between two values. This is useful for things like footstep sounds or other repetitive sounds.
-	The emission range is adjustable in the inspector. This allows for variation in sound ranges. In our case, it could be used to make footsteps only hear from up close,
	while a waterfall could be heard from further away.
-	There is a standard pitch value. This is useful if we wanted non-randomised sounds to have a consistent pitch. The main use case for this would be viking grunts. 
	Similar to the reason for randomising footstep pitch - if every viking sounded the same, it would be very noticeable and annoying/immersion breaking. To avoid this,
	each viking prefab could have a random pitch assigned when created, and passed into their SoundEmitter. This also avoids needing to give different vikings different
	sound clips to achieve the same effect, which would be very time and resource consuming.
-	The loop toggle allows for sound to be replayed when it finishes, with an optional delay. This allows for a smooth implementation of repetitive sounds like footsteps.
	The delay has minimum and maximum values for randomisation, which allows for an even more natural sound, while still leaving an option of a static delay by setting
	the minimum and maximum to the same value.
-	There is functionality for decreasing a specific SoundEmitter's volume by a percentage. This can be useful for when different sound effects are at different volumes,
	or if the designers decide one sound should be quieter/louder than another but don't want to have to edit the sound file itself.

The idea is to try to simplify the process of adding sound to the game, while still allowing for a lot of customisation. This will hopefully make the designer's job
easier than having to configure the Audio Sources directly.

>>> SoundManager:

The SoundManager is a singleton that manages the playing of sound effects. The SoundEmitter(s) in scene will communicate with the SoundManager to play their sounds.
Some of the notable features of the SoundManager class are as follows:

-	The ability to stop all sound effects with a method. This is useful for things like pausing the game, or when the gameplay state changes (for instance a cutscene
	plays or the player dies).
-	A get listener method that returns the Audio Listener in the scene. This is useful for the SoundEmitter to check if a player object exists. In our case, the
	Audio Listener is attached to the camera, so we can be sure that it will always exist.
-	The PlaySound method is used for both regular and random pitched sounds.
-	The SoundEmitters grab the global volume from the SoundManager. This will be useful when working with the settings system as the player will adjust their volume 
	settings.

>>> MusicManager:

The MusicManager is a singleton that manages the playing of music. It uses Playlists and TrackData objects to play music. This allows for custom playlists for different
purposes, such as for different levels and different stages of the game. It could also be linked to seasons etc. The MusicManager has the following features:

-	The ability to Play, Pause and Stop music. This is crucial for a good sound management system as they are basic functions of a music player.
-	The ability to change the volume of the music audio source. This is useful for when the player wants to adjust the volume of the music, or 
	when the music should be quieter during a cutscene or dialogue. As it is a singleton, it can easily communicate with a settings menu or system to adjust the volume.
-	The music fades out and then in when changing songs. This seemless transition will make the music feel more natural and less jarring when changing songs.

> Playlists:

-	Playlists are scriptable objects that contain a list of TrackData objects. This is useful for having songs play in a specific order, keeping different scenario's
	music separate and organised, as well as for easily modifying them.

> TrackData:

-	TrackData objects contain the audio clip to be played, the name of the track, and the volume of the track. This is useful for when the music manager needs to
	play a specific song. It is useful for debugging as well, as the name of the track can be displayed in the console. It is also a way to neatly organise the music
	clips as the asset names are limited in length and can be hard to read.


