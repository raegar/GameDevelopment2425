public interface IJob
{
    void StartJob(Worker worker);
    void UpdateJob();
    bool IsComplete();
    void OnJobComplete();
    void PauseJob();
    void ResumeJob();
}
