using UnityEngine;

public class OnClickSound : DragDropSound
{
    // Used for sounds that need to play when the attached object is clicked
    // Example: viking grunt

    private float cooldownTime = 0.1f; // Adjust as needed
    private float cooldownPassed;

    private void OnMouseDown()
    {
        if (Time.time - cooldownPassed > cooldownTime)
        {
            soundReceiver.SoundBehaviour(clipIndexToPlay, false);
            cooldownPassed = Time.time;
        }
    }
}
