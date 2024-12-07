/* Author  : Don MacSween
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose : This is the interface of all objects that require persistant data
 *           Each object that implements this interface must register itself with the GameManager
 *           so that when the GameManager is saving or loading data it can call the appropriate methods
 *           locaton is the path to the subdirectory that the object will use to save or load data
 *           each folder will act a save slot
 */
public interface IPersistantDataContainer
{
    /// <summary>
    ///  Registers the object with the GameManager singleton
    /// this MUST be called in the Awake() method of the object
    // and must contain the following line:
    // GameManager.Instance.RegisterDataProvider(this);
    /// </summary>
    public void RegisterDataProvider();
    
    /// <summary>
    /// Provides a method where developers can create a new game data for that object
    /// </summary>
    public void NewGameData();
    
    /// <summary>
    /// provides a method where developers can save the data of that object
    /// </summary>
    /// <param name="location">the folder path in which to save the objects data</param>
    public void SaveData(string location);

    /// <summary>
    /// provides a method where developers can save the data of that object
    /// </summary>
    /// <param name="location">the folder path in which to save the objects data</param>
    public void LoadData(string location);
}
