
/* Author  : Ignacy | https://github.com/ID274
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose : This script is a test script used to demonstrate the naming system in action.
 *           It stores the viking's name as a string and extracts the first and last names from it.
 *           To use this script, attach it to a GameObject in the scene and assign values in the inspector, or use the NameSystemTest scene.       
 */

using TMPro;
using UnityEngine;

public class TestViking : MonoBehaviour
{
    [SerializeField] private string vikingFullName;

    [SerializeField] private string vikingFirstName, vikingLastName, fatherName;
    [SerializeField] private bool isChild, usePatronymics;
    [SerializeField] private TextMeshPro nameText; // <--- TextMeshPro component to display the viking's name.
    [SerializeField] private Gender gender;

    private void Start()
    {
        vikingFullName = NamingManager.Instance.GetFullNameQuickly(gender, isChild, usePatronymics, fatherName); // <--- The method called here provides complete functionality for getting a full name.
        string[] nameSplit = vikingFullName.Split(" "); // <--- Split the full name into first and last names.
        vikingFirstName = nameSplit[0];
        vikingLastName = nameSplit[1];
        nameText.text = vikingFullName; // <--- Display the viking's name in the scene.
        Debug.Log(vikingFullName);
    }
}
