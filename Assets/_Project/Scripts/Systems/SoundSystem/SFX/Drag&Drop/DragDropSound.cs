using UnityEngine;

public class DragDropSound : MonoBehaviour
{
    protected SoundReceiver soundReceiver;
    [SerializeField] protected int clipIndexToPlay;
    protected virtual void Awake()
    {
        soundReceiver = GetComponent<SoundReceiver>();
        if (soundReceiver == null)
        {
            soundReceiver = gameObject.AddComponent<SoundReceiver>();
        }
    }
}
