/* Author  : Don MacSween
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose : This script is a state for the state manager that initializes the game from the first scene load
 *              it is intended to load the main game menu and instantiate the systems requred from the systemsToInstantiate ScriptableObject
 */

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using GameProjectManager;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public bool isPaused = false;
    public int mainMenuSceneIndex = 1;
    public int mainGameSceneIndex = 2;
    public string fileToLoad;
    // loaded seperately to allow for showing the loading screen
    public GameObject UIManagerPrefab;
    public List<GameObject> systemsToInstantiate;
    // A reference to the loading progress bar in the scene
    public Slider progressBar;
    // A reference to the loading operation so we can update the progress bar
    private AsyncOperation loadOperation;

    void Awake()
    {
        if (UIManager.Instance != null)
        {
            Instantiate(UIManagerPrefab, transform.root);
        }
        else
        {
            Debug.LogError("UIManager reference missing");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // UIManager.Instance.OpenPanel("Loader"); // add string override
        LoadMenuScene();
    }
    private void LoadMenuScene()
    {   
        // try to find a progress bar in the scene
        var foundSliderObjects = FindObjectsOfType<Slider>();
        if (foundSliderObjects.Length > 0)
        {
            // reset the properties of the progress bar to what we need
            progressBar = foundSliderObjects[0];
            progressBar.maxValue = 1f;
            progressBar.minValue = 0f;
            progressBar.value = 0f;
        }
        else
        {
            // The script will still work without a progress bar - but it should be there.
            Debug.LogError("No progress bar found in scene");
        }
        // Using a a coroutine to spread the loading and instantiation of systems over multiple frames
        StartCoroutine(ILoadMenuScene());
    }
    /// <summary>
    /// Loads the main menu scene and instantiates the systems from the systemsToInstantiate ScriptableObject
    /// </summary>
    /// <returns></returns>
    IEnumerator ILoadMenuScene()
    {
        //choose which scene to load via scriptable object
        loadOperation = SceneManager.LoadSceneAsync(mainMenuSceneIndex);
        // don't allow the scene to activate until we are ready
        loadOperation.allowSceneActivation = false;
        while (loadOperation.progress < 0.9f)
        {
            // if we have a progress bar update it as the scene loads
            if (progressBar != null) { progressBar.value = loadOperation.progress; }
            yield return null;
        }
        var counter = systemsToInstantiate.Count / 0.1f;
        foreach (var system in systemsToInstantiate)
        {
            Instantiate(system);
            // if we have a progress bar update it as the systems instantiate
            if (progressBar != null) { progressBar.value += counter; }
            yield return null;
        }
        // if we have a progress bar set it to fully done.
        if (progressBar != null) { progressBar.value = 1f; }
        loadOperation.allowSceneActivation = true;
        // UIManager.Instance.ClosePanel("Loader"); // add string override
        // UIManager.Instance.OpenPanel("MainMenu"); // add string override
        // SoundManager.Instance.PlayMusic("MainMenu");
    }

    public void LoadMainGame(string LoadFile = "new")
    {
        fileToLoad = LoadFile;
        // UIManager.Instance.OpenPanel("Loader"); // add string override
        // try to find a progress bar in the scene
        Slider[] foundSliderObjects = FindObjectsOfType<Slider>();
        if (foundSliderObjects.Length > 0)
        {
            // reset the properties of the progress bar to what we need
            progressBar = foundSliderObjects[0];
            progressBar.maxValue = 1f;
            progressBar.minValue = 0f;
            progressBar.value = 0f;
        }
        else
        {
            // The script will still work without a progress bar - but it should be there.
            Debug.LogError("No progress bar found in scene");
        }
        // Using a a coroutine to spread the loading and instantiation of systems over multiple frames
        StartCoroutine(ILoadGameScene());
    }

    IEnumerator ILoadGameScene()
    {
        //choose which scene to load via scriptable object
        loadOperation = SceneManager.LoadSceneAsync(mainGameSceneIndex);
        // don't allow the scene to activate until we are ready
        loadOperation.allowSceneActivation = false;
        while (loadOperation.progress < 0.9f)
        {
            // if we have a progress bar update it as the scene loads
            if (progressBar != null) { progressBar.value = loadOperation.progress; }
            yield return null;
        }
        if (fileToLoad == "new")
        {
            SaveManager.Instance.NewData();
        }
        else
        {
            SaveManager.Instance.LoadData(fileToLoad);
        }
        // UIManager.Instance.ClosePanel("Loader"); //Add string override
        // other system stuff here
        // if we have a progress bar set it to fully done.
        if (progressBar != null) { progressBar.value = 1f; }
        loadOperation.allowSceneActivation = true;
        // ready to play
    }

    public void ContinueGame()
    {
        if (PlayerPrefs.HasKey("LastSave"))
        {
            LoadMainGame(PlayerPrefs.GetString("LastSave"));
        }
    }

    public void NewGame()
    {
        LoadMainGame("new");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}


