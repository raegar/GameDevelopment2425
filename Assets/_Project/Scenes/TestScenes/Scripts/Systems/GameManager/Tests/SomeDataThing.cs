using System.IO;
using UnityEngine;
using GameProjectManager;
using TMPro;

public class SomeDataThing : MonoBehaviour, IPersistantDataContainer
{
    public string uniqueIdentifier = "SomeDataThing.txt";
    [SerializeField] private int someData;
    [SerializeField] private TMP_Text dataText;

    private void Awake()
    {
        // use something like this to make sure the uniqueIdentifier is unique
        // please dont all call your files "DATA.txt" or it will break :)
        uniqueIdentifier = this.gameObject.name + uniqueIdentifier;
        RegisterDataProvider();
    }

    public void RegisterDataProvider()
    {
        GameManager.Instance.RegisterDataProvider(this);
    }
    /// <summary>
    /// Sets up any data for a new game here.
    /// </summary>
    public void NewGameData()
    {
        // the data we need to create will be very specific to the object / system the developer is writing
        // how you create this data is up to you, it just needs to be done here
        someData = Random.Range(1, 100);
    }
    /// <summary>
    /// This will be called by the GameManager when it is time to save the data
    /// </summary>
    /// <param name="location">The folder this data will be saved to</param>
    public void SaveData(string location)
    {
        // the data we need to save will be very specific to the object / system the developer is writing
        // how you create this data is up to you, it just needs to be saved to a file that you can load later
        File.WriteAllText(location + "/" + uniqueIdentifier, someData.ToString());
    }
    /// <summary>
    /// This will be called by the GameManager when it is time to load the data
    /// </summary>
    /// <param name="location">The folder where this data is loaded from</param>
    public void LoadData(string location)
    {
        // the data we need to load will be very specific to the object / system the developer is writing
        // how you parse this data into your objects is up to you, it just needs to be read from a file
        someData = int.Parse(File.ReadAllText(location + "/" + uniqueIdentifier));
    }
    /// <summary>
    /// For testing purposes only to show in the demo.
    /// </summary>
    /// <param name="newData"></param>
    public void ChangeData(int newData) { someData = newData; }

    private void Update()
    {
        dataText.text = someData.ToString();
    }
}

