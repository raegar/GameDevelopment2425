using UnityEngine;

public class OnClickSound : DragDropSound
{
    //Used for sounds that need to play when the attached object is clicked
    //Example: viking grunt

    private void OnMouseDown()
    {
        soundReceiver.SoundBehaviour(clipIndexToPlay, false);
    }
}
