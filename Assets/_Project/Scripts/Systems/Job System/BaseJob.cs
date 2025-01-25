using UnityEngine;

public abstract class BaseJob : ScriptableObject, IJob
{
    public ITask[] tasks;
    public abstract void StartJob(Worker worker);
    public abstract void UpdateJob();
    public abstract bool IsComplete(); // returns bool depending on whether the job is complete
    public abstract void OnJobComplete();
    public abstract void PauseJob();
    public abstract void ResumeJob();
}
