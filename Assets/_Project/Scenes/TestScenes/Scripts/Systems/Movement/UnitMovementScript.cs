using UnityEngine;
using UnityEngine.AI;

public class UnitMovementScript : MonoBehaviour
{
    [Header("Main References")]
    public GameObject selectedIcon;
    public GameObject destinationIcon;
    public bool mouseOver = false;
    public NavMeshAgent agent;
    private LineRenderer lineRenderer; // LineRenderer for drawing the line

    [Header("Animation")]
    public Animator animator;

    [Header("Attributes")]
    public Affiliation affiliation;
    [SerializeField] private Material friendlyColor, enemyColor, neutralColor;
    public float stoppingDistance = 0.5f;

    [Header("Curved Line")]
    public int curveResolution = 100; // Number of points for the curve
    public bool useLines = true;

    public enum Affiliation
    {
        Friendly,
        Enemy,
        Neutral
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stoppingDistance;

        animator = GetComponent<Animator>();

        // Add a LineRenderer to the object
        lineRenderer = selectedIcon.AddComponent<LineRenderer>();
        lineRenderer.positionCount = curveResolution; // Set the number of positions
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

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
        
        lineRenderer.material = UnitControlManager.Instance.lineMaterial;

        // Toggle line functionality based on useLines
        lineRenderer.enabled = useLines;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && mouseOver)
        {
            UnitControlManager.Instance.SelectUnit(this);
        }

        if (!useLines)
        {
            // Disable the line functionality entirely
            if (lineRenderer != null)
                lineRenderer.enabled = false;
            return;
        }

        // Check if the agent has reached its destination
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            animator.SetBool("isWalking", false);
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                destinationIcon.SetActive(false);
                destinationIcon.transform.parent = this.transform;

                // Disable the line when not moving
                lineRenderer.enabled = false;
            }
        }
        else
        {
            animator.SetBool("isWalking", true);

            // Update the line while moving
            UpdateCurvedLine();
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

    private void UpdateCurvedLine()
    {
        if (!useLines || !destinationIcon.activeSelf)
        {
            lineRenderer.enabled = false; // Ensure the line is disabled if not in use
            return;
        }

        lineRenderer.enabled = true;

        // Adjust starting point to the bottom of selectedIcon
        Vector3 startPoint = selectedIcon.transform.position;
        Vector3 endPoint = destinationIcon.transform.position;

        // Generate curved line points
        Vector3[] curvePoints = GenerateCurvePoints(startPoint, endPoint);

        // Update LineRenderer positions
        lineRenderer.positionCount = curveResolution;
        lineRenderer.SetPositions(curvePoints);
    }

    private Vector3[] GenerateCurvePoints(Vector3 start, Vector3 end)
    {
        Vector3[] points = new Vector3[curveResolution];
        float step = 1f / (curveResolution - 1);

        for (int i = 0; i < curveResolution; i++)
        {
            float t = i * step; // Interpolation factor (0 to 1)
            Vector3 interpolatedPoint = Vector3.Lerp(start, end, t);

            // Raycast without hitting the viking or selectedIcon
            Ray ray = new Ray(new Vector3(interpolatedPoint.x, 1000, interpolatedPoint.z), Vector3.down);
            int terrainLayerMask = LayerMask.GetMask("Ground");
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, terrainLayerMask))
            {
                interpolatedPoint.y = hit.point.y;
            }

            points[i] = interpolatedPoint;
        }

        return points;
    }
}


