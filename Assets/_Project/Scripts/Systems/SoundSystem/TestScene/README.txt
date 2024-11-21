Date: 17/11/2024

Test Scene Findings:

-	Footsteps might need to be reconsidered, they are causing a lot of issues, and provide a ridiculous amount
	of overhead for what they are worth.
-	We will likely need to increase the real voices limit upon further testing, for now I've set it to 64.
-	Aside from that, everything seems to work except:

Errors:
-	An error message regarding the pool appears in the console, but they don't seem to affect the game. To recreate, limit the real voices to 32 or lower, 
	and enter playmode.
-	This doesn't throw an error, but for some reason the first 2 times you press "NextSong", it causes a giant lag spike."

What went well:
-	The UI sounds work
-	The music works
-	The SFX work
-	The footsteps work, but they need to be either greatly limited or removed entirely.

Missing functionality that might be added in the future:
-	There is currently no way to sort the actual pool by priority and call back SoundObjects that are playing. This means that determining what sounds should
	be played is a bit of a mess as its left up to Unity to decide.