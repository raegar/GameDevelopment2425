using PatternLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager : Singleton<JobManager>
{
    // The point of this manager is to keep track of all available jobs, and assign them to the appropriate worker when they ask for a job.

    [SerializeField] private List<IJob> jobList = new List<IJob>(); // total list of jobs that the settlement has, either available or not
    [SerializeField] private List<IJob> availableJobs = new List<IJob>(); // the jobs available to be assigned to workers right now

    protected override void Awake()
    {
        base.Awake();
    }

    public void AddNewJob(IJob jobToAdd)
    {
        jobList.Add(jobToAdd);
        availableJobs.Add(jobToAdd);
    }

    public void RemoveJob(IJob jobToRemove)
    {
        availableJobs.Remove(jobToRemove);
    }

    public IJob AssignJob(Worker worker)
    {
        // Assign a job to a worker based on the job requirements, as well as distance to job location (*meaning for example, rock for mining)
        // HOW THIS WILL HAPPEN I HAVE NO IDEA

        // FOR NOW WE WILL JUST ASSIGN THE FIRST JOB IN LIST
        if (availableJobs.Count > 0)
        {
            IJob job = availableJobs[0];
            
            RemoveJob(job);

            return job;
        }
        else
        {
            return null;
        }
    }
}
