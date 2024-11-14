using UnityEngine;

public class LoopOnAwake : DragDropSound
{
    // Used for "static" sounds that need to loop on awake
    // Example: waterfall

    [SerializeField] private bool loop;
    protected override void Awake()
    {
        base.Awake();
        soundReceiver.SoundBehaviour(clipIndexToPlay, loop);
    }
}
