
/* Author  : Ignacy | https://github.com/ID274
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose : This script handles the management of the name dictionary and text files for the name system.
 *           It is also in charge of handling the naming conventions and naming logic for the vikings.
 *           As it is a singleton it can be accessed from any script in the project.
 *           Current intent is for this script to be used when creating new vikings either 
 *           as starting vikings or new viking children, but to be as reusable as possible if the design changes.
 *           
 * Tip     : Use the GetFullNameQuickly method to get a full name as a tuple. See the TestViking script for an example.
 */

using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class NamingManager : MonoBehaviour
{
    static public NamingManager Instance { get; private set; }

    // This dictionary is used to store the full names of vikings and prevent duplicate names.
    private Dictionary<string, bool> fullnameRecord = new Dictionary<string, bool>();

    [Header("Name Lists\n-These lists are populated from the text files. \n-They are used to assign names to vikings.")] // These should not be modified directly, try modifying the text files instead.
    [SerializeField] private List<string> maleNames = new List<string>();
    [SerializeField] private List<string> femaleNames = new List<string>();

    // Default placeholder name arrays")]
    private string[] defaultMaleNames = { "Birger", "Bjorn", "Bo", "Erik", "Frode", "Gorm", "Halfdan", "Harald", "Knud", "Svend" }; // Default set of names in case of missing file.
    private string[] defaultFemaleNames = { "Astrid", "Frida", "Gertrud", "Hilda", "Helga", "Sigrid", "Tora", "Yrsa", "Ulfhild", "Ase" };

    private string path1, path2;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // <--- This will prevent the object from being destroyed when a new scene is loaded.
        }
        else
        {
            Destroy(this);
        }

        //- The persistentDataPath ensures that the path of the text files is always the same.
        //- This is important because the path is used to save and load the text files.
        //- The path should point to: %userprofile%\AppData\LocalLow\<companyname>\<productname>
        //- Unity Docs: https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html

        string directoryPath = Application.persistentDataPath + "/TextFiles";
        Debug.Log($"Data path: {directoryPath}");
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath); // <--- This will create the directory if it does not exist.
        }
        path1 = directoryPath + "/MaleNames.txt";
        path2 = directoryPath + "/FemaleNames.txt";

        VerifyTextFiles();
        LoadTextFiles();
        TestDuplicateNames();
    }

    void VerifyTextFiles() // <--- This method creates text files if they do not exist. If missing, default names are used.
    {
        string maleNamesText = File.ReadAllText(path1);
        string femaleNamesText = File.ReadAllText(path2);
        if (!File.Exists(path1))
        {
            File.WriteAllText(path1, string.Join("\n", defaultMaleNames));
        }
        else if (maleNamesText.Length == 0)
        {
            File.WriteAllText(path1, string.Join("\n", defaultMaleNames));
        }
        if (!File.Exists(path2))
        {
            File.WriteAllText(path2, string.Join("\n", defaultFemaleNames));
        }
        else if (femaleNamesText.Length == 0)
        {
            File.WriteAllText(path2, string.Join("\n", defaultFemaleNames));
        }
    }

    private void LoadTextFiles() // <--- This method loads the text files into the lists.
    {
        //- The text files are loaded into the lists maleNames, femaleNames.
        //- The text files are loaded from the persistentDataPath.

        maleNames = File.ReadAllLines(path1).ToList();
        femaleNames = File.ReadAllLines(path2).ToList();
    }
    public string ChooseFirstName(bool isMale)
    {
        string chosenName = "";
        switch (isMale)
        {
            case true:
                chosenName = maleNames[Random.Range(0, maleNames.Count)];
                break;
            case false:
                chosenName = femaleNames[Random.Range(0, femaleNames.Count)];
                break;
        }
        return chosenName;
    } // <--- Use this one for all vikings/children. This method will assign a first name to a viking.

    public string ChooseLastName(bool isMale) // <--- Non-child logic
    {
        string chosenName = "";
        string surname = "";

        switch (isMale)
        {
            case true:
                chosenName = maleNames[Random.Range(0, maleNames.Count)];
                break;
            case false:
                chosenName = femaleNames[Random.Range(0, femaleNames.Count)];
                break;
        }
        surname = chosenName;

        return surname;
    }

    public string ChooseLastName(bool isMale, bool isChild, bool usePatronymic, string fatherName)
    {
        string chosenName = "";
        string surname = "";
        if (usePatronymic)
        {
            if (isChild)
            {
                surname = PatronymicSurname(fatherName, isMale);
            }
            else
            {
                switch (isMale)
                {
                    case true:
                        chosenName = maleNames[Random.Range(0, maleNames.Count)];
                        break;
                    case false:
                        chosenName = femaleNames[Random.Range(0, femaleNames.Count)];
                        break;
                }
                surname = chosenName;
            }
        }
        else
        {
            switch (isMale)
            {
                case true:
                    chosenName = maleNames[Random.Range(0, maleNames.Count)];
                    break;
                case false:
                    chosenName = femaleNames[Random.Range(0, femaleNames.Count)];
                    break;
            }
            surname = chosenName;
        }

        return surname;
    }

    public string PatronymicSurname(string surname, bool isMale)
    {
        // For more information on how the Patronymic/Viking naming system functioned, here is the resource I used: https://www.ellipsis.cx/~liana/names/norse/sg-viking.html

        // Dictionary to store name endings and their corresponding suffixes
        var nameEndings = new Dictionary<string, string> // <--- This dictionary is used to store the name endings and their corresponding suffixes.
        {
            { "dan", "ar" },
            { "endr", "ar" },
            { "freor", "ar" },
            { "froor", "ar" },
            { "gautr", "ar" },
            { "mundr", "ar" },
            { "roor", "ar" },
            { "undr", "ar" },
            { "unn", "ar" }, // special case for "unn"
            { "uror", "ar" },
            { "varor", "ar" },
            { "vior", "ar" },
            { "vindr", "ar" },
            { "poror", "ar" },
            { "prondr", "ar" },
            { "iorn", "jarnar" }, // almost certain bjorn and biorn follow the same naming conventions, could do with a bit more indepth research
            { "orn", "arnar" },
            { "i", "a" },
            { "a", "u" },
            { "nn", "ns" },
            { "ll", "ls" },
            { "rr", "rs" },
            { "r", "s" },
            { "ir", "is" }
        };

        // Find the appropriate suffix based on the surname ending
        string genderSuffix = isMale ? "son" : "dottir"; // <--- Select appropriate suffix based on gender
        string genderSuffixNoSpecial = isMale ? "son" : "sdottir"; // <--- This one incorporates the possessive "s" for names that aren't given a custom suffix.

        if (surname.EndsWith("unn")) // <--- Special case for "unn"
        {
            return surname.Substring(0, surname.Length - 1) + nameEndings["unn"] + genderSuffix;
        }

        var first15Keys = nameEndings.Keys.Take(15).ToList();
        foreach (var key in first15Keys)
        {
            if (surname.EndsWith(key) && key != "uror")
            {
                return surname + nameEndings[key] + genderSuffix;
            }

            else if (surname.EndsWith(key) && key == "uror")
            {
                return surname.Substring(0, surname.Length - 2) + nameEndings[key] + genderSuffix;
            }
        }

        foreach (var ending in nameEndings.Keys)
        {
            if (surname.EndsWith(ending))
            {
                return surname.Substring(0, surname.Length - ending.Length) + nameEndings[ending] + genderSuffix;
            }
        }
        // Default case if no matching ending is found
        return surname + genderSuffixNoSpecial;
    } // <--- This method creates a patronymic surname based on the name of the father.

    private (string, string) RecordFullName(bool isMale, bool isChild, string firstName, string lastName) // <--- This method records the full name of a viking and returns it as a tuple (string, string).
    {
        int counter = 0;
        while (true)
        {
            counter++;
            if (counter > 30)
            {
                Debug.LogError("Error: Unable to record full name. Dictionary length might be too low or available names exhausted.");
                return ("", ""); // <--- Ensure a return value here
            }

            string fullName = firstName + " " + lastName;

            if (counter > 1) // <--- This will reroll the first name if it is a duplicate.
            {
                firstName = ChooseFirstName(isMale); // <--- Choose a new first name and then check it against the same surname.
                fullName = firstName + " " + lastName;

                if (fullnameRecord.ContainsKey(fullName))
                {
                    continue;
                }
                else
                {
                    fullnameRecord.Add(fullName, true);
                    (string, string) nameTuple = (firstName, lastName); // <--- This will store the full name in a tuple.
                    Debug.Log($"Full name recorded: {nameTuple.ToString()}."); // <--- This will print the full name of the viking to the console.
                    return nameTuple;
                }
            }
            else
            {
                if (fullnameRecord.ContainsKey(fullName))
                {
                    continue;
                }
                else
                {
                    fullnameRecord.Add(fullName, true);
                    (string, string) nameTuple = (firstName, lastName); // <--- This will store the full name in a tuple.
                    Debug.Log($"Full name recorded: {nameTuple.ToString()}."); // <--- This will print the full name of the viking to the console.
                    return nameTuple;
                }
            }
        }
    }

    public (string, string) GetFullNameQuickly(bool isMale, bool isChild, bool usePatronymics) // <--- This method is used to get a full name quickly. It returns a tuple (string, string).
    {
        string firstName = ChooseFirstName(isMale);
        string lastName = ChooseLastName(isMale); // <--- Use this one for viking without child/patronymics logic.
        (string, string) fullName = RecordFullName(isMale, isChild, firstName, lastName);
        Debug.Log($"Full name: {fullName.ToString()}.");
        return fullName;
    }
    public (string, string) GetFullNameQuickly(bool isMale, bool isChild, bool usePatronymics, string fatherName) // <--- This method is used to get a full name quickly. It returns a tuple (string, string).
    {
        string firstName = ChooseFirstName(isMale);
        string lastName = ChooseLastName(isMale, isChild, usePatronymics, fatherName); // <--- Use this one for children and use of patronymics.
        (string, string) fullName = RecordFullName(isMale, isChild, firstName, lastName);
        Debug.Log($"Full name: {fullName.ToString()}.");
        return fullName;
    }

    public void TestDuplicateNames() // <--- This method is used to test the naming system with duplicate names.
    {
        // Test with a set of duplicate names
        string firstName = "John";
        string lastName = "Doe";

        // First attempt to add the name
        var result1 = RecordFullName(true, false, firstName, lastName);
        Debug.Log($"- First attempt: {result1}");

        // Second attempt to add the same name
        var result2 = RecordFullName(true, false, firstName, lastName);
        Debug.Log($"- Second attempt: {result2}");

        // Third attempt with a different first name but same last name
        var result3 = RecordFullName(true, false, "Jane", lastName);
        Debug.Log($"- Third attempt: {result3}");

        // Clear the records after testing
        fullnameRecord.Clear();
        Debug.Log("Full name records cleared after testing.");
    }
}
