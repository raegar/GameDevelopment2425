using UnityEngine;

public class PlayOnEnable : DragDropSound
{
    // Used for "static" sounds that need to loop on awake
    // Example: waterfall

    [SerializeField] private bool loop;
    private bool toggleOnEnable = false;
    public void OnPoolCreated()
    {
        soundReceiver.SoundBehaviour(clipIndexToPlay, loop);
        toggleOnEnable = true;
    }

    private void OnEnable()
    {
        if (toggleOnEnable)
        {
            soundReceiver.SoundBehaviour(clipIndexToPlay, loop);
        }
    }

    private void OnDisable()
    {
        if (soundReceiver.soundObject != null)
        {
            soundReceiver.StopSound();
        }
    }
}
