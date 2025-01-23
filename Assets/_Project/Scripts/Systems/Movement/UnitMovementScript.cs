using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(SelectionEffects))]
public class UnitMovementScript : MonoBehaviour
{
    [Header("Main References")]
    public NavMeshAgent agent;

    [Header("Animation")]
    public Animator animator;

    [Header("Attributes")]
    public float stoppingDistance = 0.5f;

    private SelectionEffects selectionEffects;

    private void Awake()
    {
        SetReferences();
    }

    private void SetReferences()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stoppingDistance;
        animator = GetComponent<Animator>();
        selectionEffects = GetComponent<SelectionEffects>();
    }

    private void Update()
    {
        UpdateVisualEffects();
    }

    private void UpdateVisualEffects()
    {
        // Check if the agent has reached its destination
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            animator.SetBool("isWalking", false);
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                if (selectionEffects != null)
                {
                    selectionEffects.DisableIndicators();
                }
            }
        }
        else
        {
            animator.SetBool("isWalking", true);

            // Update the line while moving
            if (selectionEffects != null)
            {
                selectionEffects.EnableIndicators();
                selectionEffects.UpdateCurvedLine();
            }
        }
    }
}