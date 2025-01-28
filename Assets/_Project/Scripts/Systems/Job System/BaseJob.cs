using UnityEngine;

public abstract class BaseJob : ScriptableObject, IJob
{
    public string jobName = "New Job";
    [SerializeField] protected JobData jobData;
    public ITask[] tasks;
    public bool IsComplete()
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

    public void OnJobComplete()
    {
        jobData.jobReward.GiveReward();
    }

    public void PauseJob(Worker worker)
    {
        Debug.Log($"Worker {worker} paused {this} job.");
    }

    public void ResumeJob(Worker worker)
    {
        Debug.Log($"Worker {worker} resumed {this} job.");
    }

    public void StartJob(Worker worker)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateJob()
    {
        throw new System.NotImplementedException();
    }

    public void CancelJob()
    {
        throw new System.NotImplementedException();
    }
}
