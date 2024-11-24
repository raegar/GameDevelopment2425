using UnityEngine;
using UnityEngine.AI;

public class UnitMovementScript : MonoBehaviour
{
    [Header("References")]
    public GameObject selectedIcon;
    public GameObject destinationIcon;
    public bool mouseOver = false;
    public NavMeshAgent agent;

    [Header("Animation")]
    public Animator animator;

    public enum Affiliation
    {
        Friendly,
        Enemy,
        Neutral
    }

    [Header("Attributes")]
    public Affiliation affiliation;
    [SerializeField] private Material friendlyColor, enemyColor, neutralColor;
    public float stoppingDistance = 0.5f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stoppingDistance;

        animator = GetComponent<Animator>();

        switch (affiliation)
        {
            case Affiliation.Friendly:
                selectedIcon.GetComponent<Renderer>().material = friendlyColor;
                destinationIcon.GetComponent<Renderer>().material = friendlyColor;
                break;
            case Affiliation.Enemy:
                selectedIcon.GetComponent<Renderer>().material = enemyColor;
                break;
            case Affiliation.Neutral:
                selectedIcon.GetComponent<Renderer>().material = neutralColor;
                break;
        }
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
            if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Walking")
            {
                //animator.Play("Idle");
            }
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                destinationIcon.SetActive(false);
                destinationIcon.transform.parent = this.transform;
            }
        }
        else
        {
            animator.SetBool("isWalking", true);
            if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Idle")
            {
                //animator.Play("Walking");
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
