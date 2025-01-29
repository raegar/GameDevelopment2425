using UnityEngine;

public abstract class BaseJob : ScriptableObject, IJob
{
    public string jobName = "New Job";
    [SerializeField] protected JobData jobData;
    public ITask[] tasks;
    public virtual bool IsComplete()
    {
        if (tasks[tasks.Length - 1].IsComplete())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void OnJobComplete()
    {
        jobData.jobReward.GiveReward();
    }

    public virtual void PauseJob(Worker worker)
    {
        Debug.Log($"Worker {worker} paused {this} job.");
    }

    public virtual void ResumeJob(Worker worker)
    {
        Debug.Log($"Worker {worker} resumed {this} job.");
    }

    public virtual void StartJob(Worker worker)
    {
        throw new System.NotImplementedException();
    }

    public virtual void UpdateJob()
    {
        throw new System.NotImplementedException();
    }

    public virtual void CancelJob()
    {
        throw new System.NotImplementedException();
    }
}
