using System;
using UnityEngine;

public class SpawnVikingEvent : MonoBehaviour
{
    public event Action OnEventTriggered;

    public virtual void Invoke()
    {
        OnEventTriggered?.Invoke();
    }
}
