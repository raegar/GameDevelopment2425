public interface ITask
{
    void StartTask();
    void UpdateTask();
    bool IsComplete();
    bool HasNextTask();
    void OnTaskComplete();
    void PauseTask();
    void ResumeTask();
}
