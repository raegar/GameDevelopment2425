using UnityEngine;

public abstract class BaseTask : ScriptableObject, ITask
{
    public abstract void StartTask();
    public abstract void UpdateTask();
    public abstract bool IsComplete();
    public abstract bool HasNextTask();
    public abstract void OnTaskComplete();
    public abstract void PauseTask();
    public abstract void ResumeTask();
}