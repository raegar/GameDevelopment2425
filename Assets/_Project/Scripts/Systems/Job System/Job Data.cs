using UnityEngine;

public enum TargetingType
{
    Invalid,
    Closest,
    Random
}

[CreateAssetMenu(fileName = "New Job Data", menuName = "Job System/Job Data")]
public class JobData : ScriptableObject
{
    public TargetingType targetingType = TargetingType.Closest; // which behaviour to use when searching for target
    public int resourceTarget; // if applicable
    public JobReward jobReward; // reward for completing job
}