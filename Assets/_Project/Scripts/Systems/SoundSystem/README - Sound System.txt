***************** SOUND SYSTEM README *****************
/* Author  : Ignacy | https://github.com/ID274
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose : This readme is an outline of the sound system's functionality, as well as a guide to testing the scene and getting familiar with the system.
 */

 >>>>>>>>>>>>>>>> Test Scene Tutorial <<<<<<<<<<<<<<<<<<<

	1. Open the SoundSystemTest scene.
	2. Enter playmode.
	3. Use Don's camera system controls to move the camera around (WASD, scroll wheel, right click drag and Q/E for rotation).
	4. Click the viking cubes for them to emit sounds.
	5. Toggle the footstep prefabs if you'd like to see that in action, however it is extremely heavy on the pool and causes issues unless the real max voices setting is increased.
	6. Click the buttons in the scene to test UI sounds and music.
	7. Explore - to properly visualise how it will fit in the final product, make sure to use a stereo/surround sound headset or speakers.

 >>>>>>>>>>>>>>>> Music Subsystem Overview <<<<<<<<<<<<<<<<<<<


	>>> Music Subsystem features

	- The ability to store audio clips together with song name and artist name if needed.
	- The ability to create playlists of the tracks. Useful for grouping them for different levels/locations/events. (example: battle music, forest etc)
	- The playlists can be cycled through, or overridden with a method to call a specific track.
	- Fade in/out of the tracks.

	@ Troubleshooting tips

	# If experiencing lag spikes when loading a track:
	- Ensure the audio clips are either in the .ogg format or are converted to .ogg format when built.
	- Ensure the "Preload Audio Data" option is checked in the audio clip settings.
	
	# Make sure:
	- Ensure there is a "Music Manager" object in the scene, and that the "Music Manager" object is not destroyed when loading a new scene.
	- Ensure that the "Music Manager" object is properly configured (use the prefab as-is or as a reference).
	- Ensure that the tracks and playlists that are assigned to the Music Manager are properly filled out and not null.

	If these don't help, contact me and we will see if there is time to work on a fix - currently these seem to help.

 >>>>>>>>>>>>>>>> SFX Subsystem Overview <<<<<<<<<<<<<<<<<<<

	>>> SFX Subsystem features

	- Audio pooling system for both performance and efficiency - making the most of the real voices limit.
	- Drag and drop scripts for different use-cases (PlayOnClick, PlayWhenCalled, PlayOnEnable). These can be used as-is or as a reference - they are abstracted and simple to use.
	- Looping sounds can be toggled on/off.
	- Highly customisable despite using a pool. Similarly to the Music Subsystem's track files, SFX files are stored in a similar way with more adjustable settings.
	- Checks in place to make sure the SoundObject returns to pool.

	@ Troubleshooting tips

	# Make sure:
	- The SoundObject prefab is properly set up and has the SoundObject script attached.
	- The object calling the sound has a SoundReceiver AND a custom or pre-made DragDropSound script attached. It doesn't have to inherit, as long as it has the necessary references.
	- You have created SoundData objects and filled them out. These are the audio clips that will be played and their settings.
	- You have filled out the dataList array in the SFXManager with the SoundData objects.
	- You are calling the correct index - remember lists/arrays start at 0.
	- You have an SFXManager in the scene. The prefab provided comes with the UISFXManager and the SpatialSFXPool scripts attached. If for any reason you'd like to decouple them,
	  make sure you test if they have all the necessary references.



 Additional Information:
	- There is currently no way of tracking the priority of each SoundObject's sound. This means that if the pool is full, one of the sounds will be replaced possibly at random.
	- The footstep functionality poses a lot of challenges and issues, and is in my opinion not worth the hassle - especially since we are targeting a mobile platform.
	- There is currently no way of adjusting some of the Audio Source attributes. These have been determined to be less commonly needed and therefore not included,
	  but can be added if necessary per request.



