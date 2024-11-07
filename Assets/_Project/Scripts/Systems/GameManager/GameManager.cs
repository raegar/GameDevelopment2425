/* Author  : Don MacSween
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose : This is the game's main Manager designed to manage the state and flow of the game
 * 
 */
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PatternLibrary;

namespace GameProjectManager
{
    public class GameManager : Singleton<GameManager>
    {
        public StateMachine gameState = new StateMachine();

        // stsatemachine states
        public BaseState initalizeFromLoad;
        public BaseState mainMenu;
        public BaseState mainGame;
        public BaseState gamePaused;
        public BaseState gameFileOperation;
       
        // SaveSystem
        public Dictionary<IPersistantDataContainer, bool> registeredPersistantDataContainers
                                                       = new Dictionary<IPersistantDataContainer, bool>();
        public int numRegisteredContainers;
        public string savePath;
        // using a subfolder so we dont clash with other unity games.
        public string defaultSaveFolder = "AllfatherSaves";
        public string saveName = "Game";
        public int saveCounter;
        public List<string> gameSaves = new List<string>();

        /// <summary>
        ///  Unity MonoBehaviour called once on object creation
        /// </summary>
        protected override void Awake()
        { 
            // execute the base singleton functionality
            base.Awake();
            // ValidateStates();
            InitialiseSaveSystem();
            numRegisteredContainers = registeredPersistantDataContainers.Count;
        }

        /// <summary>
        /// Gets everything ready for the save system to work
        /// </summary>
        private void InitialiseSaveSystem() 
        {
            // Get the path to save in for this platform
            savePath = Application.persistentDataPath;
            // Check if the default save folder exists and create it if it doesn't
            if (!Directory.Exists(savePath + "/" + defaultSaveFolder))
            { Directory.CreateDirectory(savePath + "/" + defaultSaveFolder); }
            // concatinate the two to get the full path
            savePath = savePath + "/" + defaultSaveFolder;
            // Get all the subfolders in the save folder each folder represents a save
            string[] subfolders = Directory.GetDirectories(savePath, "*", SearchOption.TopDirectoryOnly);
            // loop through and find any existing saves - irritating feature of GetDirectories is that it inserts a \ into the path
            foreach (string subfolder in subfolders) {gameSaves.Add(subfolder.Replace("\\","/"));}
            // reminder that count is the number of elements in the list not the index
            saveCounter = gameSaves.Count;
        }

        /// <summary>
        /// Called by objects implimenting the IPersistantDataContainer interface on Awake()
        /// </summary>
        /// <param name="container">The data container to be registered</param>
        public void RegisterDataProvider(IPersistantDataContainer container)
        {
            // add each container to the dictionary
            registeredPersistantDataContainers.Add(container, true);
        }

        /// <summary>
        /// Provides a list of all the saves in the default save folder for use in the UI
        /// </summary>
        /// <returns></returns>
        public List<string> ListSaves() { return gameSaves; }

        /// <summary>
        ///  Loops through all registered data containers and calls their SaveData method
        /// </summary>
        public void SaveData(string name)
        {
            if (Directory.Exists(savePath + "/" + name))
            {
                Debug.LogError("Save already exists");
            }
            else
            {
                // create a new save folder
                Directory.CreateDirectory(savePath + "/" + name);
                gameSaves.Add(savePath + "/" + name);
                foreach (var container in registeredPersistantDataContainers)
                {
                    container.Key.SaveData(savePath + "/" + name);
                }
            }
        }

        /// <summary>
        ///  Override of SaveDate where no name is provided
        ///  Loops through all registered data containers and calls their SaveData method
        /// </summary>
        public void SaveData()
        {
            // if no name is provided use the default
            saveCounter++;
            name = saveName + saveCounter.ToString();
            if (gameSaves.Contains(savePath + "/" + name))
            {
                saveCounter++;
                SaveData();
            }
            // refactor this later as it's own method with more error checking
            Directory.CreateDirectory(savePath + "/" + name);
            // may need to move this into a coroutine if it gets slow
            gameSaves.Add(savePath + "/" + name);
            foreach (var container in registeredPersistantDataContainers)
            {
                container.Key.SaveData(savePath + "/" + name);
            }
        }


        /// <summary>
        /// Overwrites a specific save in the game by deleting it and then saving it again
        /// </summary>
        /// <param name="name"></param>
        public void OverwriteData (string name)
        {
            DeleteData(name);
            SaveData(name);
        }
        /// <summary>
        /// Deletes a specific save from the game
        /// </summary>
        /// <param name="name"></param>
        public void DeleteData(string name)
        {
            if (Directory.Exists(savePath + "/" + name))
            {
                Directory.Delete(savePath + "/" + name, true);
                gameSaves.Remove(savePath +"/" + name);
            } 
            else
            {
                Debug.LogError("Save does not exist");
                return;
            }
        }
        
        /// <summary>
        /// Deletes all saved from the game USE WITH EXTREME CAUTION!
        /// </summary>
        public void DeleteAllData()
        {
            foreach (var save in gameSaves)
            {
                if (Directory.Exists(save))
                {
                    Directory.Delete(save, true);
                }
            }
            gameSaves.Clear();
        }

        /// <summary>
        /// Loops through all registered data containers and calls their LoadData method
        /// providing a path they can load their data from
        /// </summary>
        public void LoadData(string saveName)
        {
            if (Directory.Exists(savePath + "/" + saveName))
            {
                foreach (var container in registeredPersistantDataContainers)
                {
                    container.Key.LoadData(savePath + "/" + saveName);
                }
            }
            else
            {
                Debug.LogError("Save does not exist");
                return;
            }
            
        }

        /// <summary>
        ///  Loops through all registered data containers and calls their NewData method
        /// </summary>
        public void NewGame()
        {
            foreach (var container in registeredPersistantDataContainers)
            {
                container.Key.NewGameData();
            }
        }
        // LoadLast - extension for later
        // NewData
        // Pause
        // Resume
        // Quit

    }
}
