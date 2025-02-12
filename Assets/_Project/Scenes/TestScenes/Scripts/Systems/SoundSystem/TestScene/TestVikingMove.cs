using System.Collections;
using UnityEngine;

public class TestVikingMove : MonoBehaviour
{
    public Vector3 areaCenter;
    public Vector3 areaSize;
    public float speed = 2.0f;

    private Vector3 target;

    private PlayWhenCalled playWhenCalledScript;
    private bool footstepInProgress = false;

    private float initialDelay; // make the footsteps start at different times
    private bool waited = false;

    private void Awake()
    {
        playWhenCalledScript = GetComponent<PlayWhenCalled>();
        initialDelay = Random.Range(0.0f, 6.0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetRandomTarget();
        StartCoroutine(WaitDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if (waited)
        {
            MoveViking();
        }
    }

    void MoveViking()
    {
        // Move towards the target
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Check if the Viking has reached the target
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            // Set a new random target
            SetRandomTarget();
        }

        if (playWhenCalledScript != null && !footstepInProgress)
        {
            StartCoroutine(FootstepDelay());
        }
    }

    void SetRandomTarget()
    {
        float randomX = Random.Range(areaCenter.x - areaSize.x / 2, areaCenter.x + areaSize.x / 2);
        float randomY = Random.Range(areaCenter.y - areaSize.y / 2, areaCenter.y + areaSize.y / 2);
        float randomZ = Random.Range(areaCenter.z - areaSize.z / 2, areaCenter.z + areaSize.z / 2);
        target = new Vector3(randomX, randomY, randomZ);
    }

    public IEnumerator FootstepDelay()
    {
        footstepInProgress = true;
        playWhenCalledScript.PlaySound();
        yield return new WaitForSeconds(4 / speed);
        footstepInProgress = false;
    }

    public IEnumerator WaitDelay()
    {
        yield return new WaitForSeconds(initialDelay);
        waited = true;
    }
}