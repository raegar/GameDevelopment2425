using UnityEngine;

public abstract class BaseTask : ScriptableObject, ITask
{
    public virtual void StartTask()
    {

    }
    public virtual void UpdateTask()
    {

    }
    public virtual bool IsComplete()
    {
        return false;
    }
    public virtual bool HasNextTask()
    {
        return false;
    }
    public virtual void OnTaskComplete()
    {

    }
    public virtual void PauseTask()
    {

    }
    public virtual void ResumeTask()
    {

    }
}