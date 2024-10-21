
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

using PatternLibrary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting.YamlDotNet.Serialization;
using UnityEngine;

public class NamingManager : Singleton<NamingManager>
{
    // This dictionary is used to store the full names of vikings and prevent duplicate names.
    private Dictionary<string, bool> fullnameRecord = new Dictionary<string, bool>();

    [Header("Name Lists\n-These lists are populated from the text files. \n-They are used to assign names to vikings.")] // These should not be modified directly, try modifying the text files instead.
    [SerializeField] private List<string> maleNames = new List<string>();
    [SerializeField] private List<string> femaleNames = new List<string>();

    // Default placeholder name arrays")]
    private List<string> defaultMaleNames = new List<string>(); // Default set of names in case of missing file.
    private List<string> defaultFemaleNames = new List<string>();

    private string path1, path2, defaultPath1, defaultPath2, internalDirectoryPath, externalDirectoryPath;

    protected override void Awake()
    {
        base.Awake();

        //- The persistentDataPath ensures that the path of the text files is always the same.
        //- This is important because the path is used to save and load the text files.
        //- The path should point to: %userprofile%\AppData\LocalLow\<companyname>\<productname>
        //- Unity Docs: https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html

        externalDirectoryPath = Application.persistentDataPath + "/NameFiles";
        internalDirectoryPath = Application.streamingAssetsPath + "/NameFiles";
        Debug.Log($"Data path: {externalDirectoryPath}");
        if (!Directory.Exists(externalDirectoryPath))
        {
            Directory.CreateDirectory(externalDirectoryPath); // <--- This will create the external directory if it does not exist.
        }
        
        // No writing to StreamingAssets directory as it is read-only!!!

        path1 = externalDirectoryPath + "/MaleNames.txt";
        path2 = externalDirectoryPath + "/FemaleNames.txt";

        defaultPath1 = internalDirectoryPath + "/DefaultMaleNames.txt";
        defaultPath2 = internalDirectoryPath + "/DefaultFemaleNames.txt";



        VerifyTextFiles();
        LoadTextFiles();
        TestDuplicateNames();
    }

    void VerifyTextFiles() // <--- This method creates text files if they do not exist. If missing, default names are used.
    {
        if (!File.Exists(defaultPath1))
        {
            Debug.LogError("Placeholder for male names is missing. Please create a text file in " + internalDirectoryPath);
        }
        else
        {
            defaultMaleNames = File.ReadAllLines(defaultPath1).ToList();
        }
        if (defaultMaleNames.Count < 1)
        {
            Debug.LogError("Placeholder for male names is empty. Please populate the text file in " + internalDirectoryPath);
        }

        if (!File.Exists(defaultPath2))
        {
            Debug.LogError("Placeholder for female names is missing. Please create a text file in " + internalDirectoryPath);
        }
        else
        {
            defaultFemaleNames = File.ReadAllLines(defaultPath2).ToList();
        }
        if (defaultFemaleNames.Count < 1)
        {
            Debug.LogError("Placeholder for female names is empty. Please populate the text file in " + internalDirectoryPath);
        }

        if (!File.Exists(path1))
        {
            File.WriteAllText(path1, string.Join("\n", defaultMaleNames));
        }
        string maleNamesText = File.ReadAllText(path1);
        if (maleNamesText.Length == 0)
        {
            File.WriteAllText(path1, string.Join("\n", defaultMaleNames));
        }
        if (!File.Exists(path2))
        {
            File.WriteAllText(path2, string.Join("\n", defaultFemaleNames));
        }
        string femaleNamesText = File.ReadAllText(path2);
        if (femaleNamesText.Length == 0)
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
    public string ChooseFirstName(Gender gender)
    {
        string chosenName = "";
        switch (gender)
        {
            case Gender.Male:
                if (maleNames.Count > 0)
                {
                    chosenName = maleNames[Random.Range(0, maleNames.Count)];
                }
                else
                {
                    Debug.LogError("Error: maleNames list is empty. Check the text file or the default names.");
                }
                break;
            case Gender.Female:
                if (femaleNames.Count > 0)
                {
                    chosenName = femaleNames[Random.Range(0, femaleNames.Count)];
                }
                else
                {
                    Debug.LogError("Error: femaleNames list is empty. Check the text file or the default names.");
                }
                break;
        }
        return chosenName;
    } // <--- Use this one for all vikings/children. This method will assign a first name to a viking.

    public string ChooseLastName(Gender gender) // <--- Non-child logic, use this when not needing patronymics and want random last names.
    {
        string chosenName = "";
        string surname = "";

        switch (gender)
        {
            case Gender.Male:
                if (maleNames.Count > 0)
                {
                    chosenName = maleNames[Random.Range(0, maleNames.Count)];
                }
                else
                {
                    Debug.LogError("Error: maleNames list is empty. Check the text file or the default names.");
                }
                break;
            case Gender.Female:
                if (femaleNames.Count > 0)
                {
                    chosenName = femaleNames[Random.Range(0, femaleNames.Count)];
                }
                else
                {
                    Debug.LogError("Error: femaleNames list is empty. Check the text file or the default names.");
                }
                break;
        }
        surname = chosenName;

        return surname;
    }

    public string ChooseLastName(Gender gender, bool isChild, bool usePatronymic, string fatherName)
    {
        string chosenName = "";
        string surname = "";
        if (usePatronymic)
        {
            if (isChild)
            {
                surname = PatronymicSurname(fatherName, gender);
            }
            else
            {
                switch (gender)
                {
                    case Gender.Male:
                        if (maleNames.Count > 0)
                        {
                            chosenName = maleNames[Random.Range(0, maleNames.Count)];
                        }
                        else
                        {
                            Debug.LogError("Error: maleNames list is empty. Check the text file or the default names.");
                        }
                        break;
                    case Gender.Female:
                        if (femaleNames.Count > 0)
                        {
                            chosenName = femaleNames[Random.Range(0, femaleNames.Count)];
                        }
                        else
                        {
                            Debug.LogError("Error: femaleNames list is empty. Check the text file or the default names.");
                        }
                        break;
                }
                surname = chosenName;
            }
        }
        else
        {
            switch (gender)
            {
                case Gender.Male:
                    if (maleNames.Count > 0)
                    {
                        chosenName = maleNames[Random.Range(0, maleNames.Count)];
                    }
                    else
                    {
                        Debug.LogError("Error: maleNames list is empty. Check the text file or the default names.");
                    }
                    break;
                case Gender.Female:
                    if (femaleNames.Count > 0)
                    {
                        chosenName = femaleNames[Random.Range(0, femaleNames.Count)];
                    }
                    else
                    {
                        Debug.LogError("Error: femaleNames list is empty. Check the text file or the default names.");
                    }
                    break;
            }
            surname = chosenName;
        }

        return surname;
    }

    public string PatronymicSurname(string surname, Gender gender)
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
        string genderSuffix = "";
        string genderSuffixNoSpecial = "";
        if (gender == Gender.Male) // <--- Select appropriate suffix based on gender
        {
            genderSuffix = "son"; // <--- Select appropriate suffix based on gender
            genderSuffixNoSpecial = "son"; // <--- This one incorporates the possessive "s" for names that aren't given a custom suffix.
        }
        else if (gender == Gender.Female)
        {
            genderSuffix = "dottir";
            genderSuffixNoSpecial = "sdottir";
        }
        else
        {
            Debug.LogError("Gender not assigned. Please assign a gender when using the PatronymicSurname method.");
        }

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

    private (string, string) RecordFullName(Gender gender, bool isChild, string firstName, string lastName) // <--- This method records the full name of a viking and returns it as a tuple (string, string).
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
                firstName = ChooseFirstName(gender); // <--- Choose a new first name and then check it against the same surname.
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

    public (string, string) GetFullNameQuickly(Gender gender, bool isChild, bool usePatronymics) // <--- This method is used to get a full name quickly. It returns a tuple (string, string).
    {
        string firstName = ChooseFirstName(gender);
        string lastName = ChooseLastName(gender); // <--- Use this one for viking without child/patronymics logic.
        (string, string) fullName = RecordFullName(gender, isChild, firstName, lastName);
        Debug.Log($"Full name: {fullName.ToString()}.");
        return fullName;
    }
    public (string, string) GetFullNameQuickly(Gender gender, bool isChild, bool usePatronymics, string fatherName) // <--- This method is used to get a full name quickly. It returns a tuple (string, string).
    {
        string firstName = ChooseFirstName(gender);
        string lastName = ChooseLastName(gender, isChild, usePatronymics, fatherName); // <--- Use this one for children and use of patronymics.
        (string, string) fullName = RecordFullName(gender, isChild, firstName, lastName);
        Debug.Log($"Full name: {fullName.ToString()}.");
        return fullName;
    }

    public void TestDuplicateNames() // <--- This method is used to test the naming system with duplicate names.
    {
        // Test with a set of duplicate names
        string firstName = "John";
        string lastName = "Doe";

        // First attempt to add the name
        var result1 = RecordFullName(Gender.Male, false, firstName, lastName);
        Debug.Log($"- First attempt: {result1}");

        // Second attempt to add the same name
        var result2 = RecordFullName(Gender.Male, false, firstName, lastName);
        Debug.Log($"- Second attempt: {result2}");

        // Third attempt with a different first name but same last name
        var result3 = RecordFullName(Gender.Male, false, "Jane", lastName);
        Debug.Log($"- Third attempt: {result3}");

        // Clear the records after testing
        fullnameRecord.Clear();
        Debug.Log("Full name records cleared after testing.");
    }
}
