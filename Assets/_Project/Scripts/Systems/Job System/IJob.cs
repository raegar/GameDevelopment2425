public interface IJob
{
    void StartJob(Worker worker);
    void UpdateJob();
    bool IsComplete();
    void OnJobComplete();
    void PauseJob(Worker worker); // uses worker reference for debugging
    void ResumeJob(Worker worker);
}
