using UnityEngine;

public class Job_MineRock : BaseJob
{
    public override bool IsComplete()
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

    public override void OnJobComplete()
    {
        jobData.jobReward.GiveReward();
    }

    public override void PauseJob(Worker worker)
    {
        Debug.Log($"Worker {worker} paused {this} job.");
    }

    public override void ResumeJob(Worker worker)
    {
        Debug.Log($"Worker {worker} resumed {this} job.");
    }

    public override void StartJob(Worker worker)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateJob()
    {
        throw new System.NotImplementedException();
    }
}
