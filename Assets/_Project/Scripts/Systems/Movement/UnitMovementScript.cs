using UnityEngine;
using UnityEngine.AI;

public class UnitMovementScript : MonoBehaviour
{
    [Header("References")]
    public GameObject selectedIcon;
    public GameObject destinationIcon;
    public bool mouseOver = false;
    public NavMeshAgent agent;

    public enum Affiliation
    {
        Friendly,
        Enemy,
        Neutral
    }

    [Header("Attributes")]
    public Affiliation affiliation;
    [SerializeField] private Color friendlyColor, enemyColor, neutralColor;
    public float stoppingDistance = 0.5f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stoppingDistance;

        switch (affiliation)
        {
            case Affiliation.Friendly:
                selectedIcon.GetComponent<Renderer>().material.color = friendlyColor;
                destinationIcon.GetComponent<Renderer>().material.color = friendlyColor;
                break;
            case Affiliation.Enemy:
                selectedIcon.GetComponent<Renderer>().material.color = enemyColor;
                break;
            case Affiliation.Neutral:
                selectedIcon.GetComponent<Renderer>().material.color = neutralColor;
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
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                destinationIcon.SetActive(false);
                destinationIcon.transform.parent = this.transform;
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
