using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    [SerializeField] private bool toggleScript = true;
    private void LateUpdate()
    {
        if (toggleScript)
        {
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        }
    }
}
