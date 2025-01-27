using UnityEngine;

public abstract class BaseJob : ScriptableObject, IJob
{
    public string jobName = "New Job";
    [SerializeField] protected JobData jobData;
    public ITask[] tasks;
    public abstract void StartJob(Worker worker);
    public abstract void UpdateJob();
    public abstract bool IsComplete(); // returns bool depending on whether the job is complete
    public abstract void OnJobComplete();
    public abstract void PauseJob(Worker worker);
    public abstract void ResumeJob(Worker worker);
}
