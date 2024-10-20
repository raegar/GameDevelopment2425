
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
    //The reason we store the vikingName as a tuple is to have easily access to both the surname and the first name. You can deconstruct it
    //into two separate strings like we do in start. This results in us having 1 tuple, as well as 2 strings one for first and one for last name.

    [SerializeField] private (string, string) vikingName; // <--- vikingName stored as a tuple

    //First and Last names extracted from the vikingName tuple.
    [SerializeField] private string vikingFirstName, vikingLastName, fatherName;
    [SerializeField] private bool isMale, isChild, usePatronymics;
    [SerializeField] private TextMeshPro nameText; // <--- TextMeshPro component to display the viking's name.

    private void Start()
    {
        vikingName = NamingManager.Instance.GetFullNameQuickly(isMale, isChild, usePatronymics, fatherName); // <--- The method called here provides complete functionality for getting a full name.
        (vikingFirstName, vikingLastName) = vikingName; // <--- Deconstruct the tuple into two separate strings.
        nameText.text = vikingFirstName + " " + vikingLastName; // <--- Display the viking's name in the scene.
        Debug.Log(vikingName);
    }
}
