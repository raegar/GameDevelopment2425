
public class PlayWhenCalled : DragDropSound
{
    // Used for sounds that need to be called often, or with a random pitch
    // Example: footsteps

    public void PlaySound()
    {
        soundReceiver.SoundBehaviour(clipIndexToPlay, false);
    }
}
