using UnityEngine;

public class Worker : MonoBehaviour
{
    // This worker class will be attached to a viking game object in order to allow it to perform jobs/tasks with the Job System/Manager
    private WorkerState workerState = WorkerState.Idle;

    private IJob currentJob;
    private ITask currentTask;

    private IJob pausedJob;
    private ITask pausedTask;
    private bool hasPausedJob = false;

    private bool haveRandomDelay = false; // could make behaviour more life-like, NOT IMPLEMENTED YET

    private void Update()
    {
        switch (workerState)
        {
            case WorkerState.Idle:
                if (hasPausedJob)
                {
                    ResumePausedJob();
                    hasPausedJob = false;
                }
                else
                {
                    IJob newJob = FindNewJob();
                    if (newJob != null)
                    {
                        StartNewJob(newJob);
                        workerState = WorkerState.Working;
                    }
                }
                break;
            case WorkerState.Working:
                if (currentJob.IsComplete())
                {
                    currentJob.OnJobComplete();
                    workerState = WorkerState.Idle;
                }
                else
                {
                    currentJob.UpdateJob();
                }
                break;
            default:
                Debug.LogWarning("Worker state not implemented", this);
                break;
        }
    }

    private IJob FindNewJob()
    {
        if (1 == 2) // temporary, will always return null
        {
            // Find a new job from job manager
        }
        else
        {
            return null;
        }
    }

    private void StartNewJob(IJob job)
    {
        if (job == null)
        {
            Debug.LogWarning("No job found", this);
            return;
        }

        if (currentJob != null)
        {
            PauseCurrentJob();
        }

        currentJob = job;
        currentJob.StartJob(this);
    }

    private void ResumePausedJob()
    {
        if (pausedJob != null)
        {
            currentJob = pausedJob;
            currentTask = pausedTask;
            pausedJob = null;
            pausedTask = null;
            currentJob.ResumeJob(this);
            hasPausedJob = false;
        }
    }

    private void PauseCurrentJob()
    {
        if (currentJob != null)
        {
            currentJob.PauseJob(this);
            pausedJob = currentJob;
            pausedTask = currentTask;
            currentTask = null;
            currentJob = null;
            hasPausedJob = true;
        }
    }

    public IJob ReturnCurrentJob()
    {
        return currentJob;
    }
    public ITask ReturnCurrentTask()
    {
        return currentTask;
    }
}
