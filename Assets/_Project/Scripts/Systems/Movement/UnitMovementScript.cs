using UnityEngine;
using UnityEngine.AI;

public class UnitMovementScript : MonoBehaviour
{
    [Header("Main References")]
    public bool mouseOver = false;
    public NavMeshAgent agent;

    [Header("Animation")]
    public Animator animator;

    [Header("Attributes")]
    public Affiliation affiliation;
    public float stoppingDistance = 0.5f;

    private SelectionEffects selectionEffects;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stoppingDistance;

        animator = GetComponent<Animator>();

        selectionEffects = GetComponent<SelectionEffects>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && mouseOver)
        {
            UnitControlManager.Instance.SelectUnit(this);
        }

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

    private void OnMouseOver()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }
}


