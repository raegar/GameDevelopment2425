
/* Author  : Ignacy | https://github.com/ID274
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose : This script is a test script used to demonstrate the naming system in action.
 *           It stores the viking's name as a tuple and extracts the first and last names from it, allowing use of multiple types.
 *           To use this script, attach it to a GameObject in the scene and assign the isMale, and isChild variables in the inspector.       
 */

using TMPro;
using UnityEngine;

public class TestViking : MonoBehaviour
{
    [SerializeField] private (string, string) vikingName; // <--- vikingName stored as a tuple

    //First and Last names extracted from the vikingName tuple.
    [SerializeField] private string vikingFirstName, vikingLastName;
    [SerializeField] private bool isMale, isChild;
    [SerializeField] private TextMeshPro nameText; // <--- TextMeshPro component to display the viking's name.

    private void Start()
    {
        vikingName = NamingManager.Instance.GetFullNameQuickly(isMale, isChild); // <--- The method called here provides complete functionality for getting a full name.
        (vikingFirstName, vikingLastName) = vikingName; // <--- Deconstruct the tuple into two separate strings.
        nameText.text = vikingFirstName + " " + vikingLastName; // <--- Display the viking's name in the scene.
        Debug.Log(vikingName);
    }
}
