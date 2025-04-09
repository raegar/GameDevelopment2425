using UnityEngine;

public class StructureDetector : MonoBehaviour
{
    public bool isHittingStructure { get; set; } = false;

    //public bool CheckCollision()
    //{
    //    if (gameObject.GetComponent<Collider>().CompareTag("Structure"))
    //    {
    //        isHittingStructure = true;
    //        return true;
    //    }
    //    return false;
    //}

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Structure"))
        {
            isHittingStructure = true;
            Debug.Log("collider");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if the colliding object has the tag "Structure"
        if (other.CompareTag("Structure"))
        {
            isHittingStructure = true;
            Debug.Log("trigger");
        }
    }

    public bool GetIsHittingStructure() { return isHittingStructure; }
}
