/* Author  : Don MacSween
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose : This script is a state for the state manager that initializes the game from the first scene load
 *              it is intended to load the main game menu and instantiate the systems requred from the systemsToInstantiate ScriptableObject
 */

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StateInitializeFromLoad : BaseState
{
    // A Scriptable Object Class that contains a list of systems to instantiate
    public SystemsToInstantiate systemsToInstantiate;
    // A reference to the loading progress bar in the scene
    public Slider progressBar;
    // A reference to the loading operation so we can update the progress bar
    private AsyncOperation loadOperation;

    /// <summary>
    /// Called by the state machine when the state is entered
    /// </summary>
    public override void StateEnter()
    {
        base.StateEnter();
        Debug.Log("StateInitializeFromLoad Entered");
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
        StartCoroutine(InitializeSystems());

    }
    /// <summary>
    /// Loads the main menu scene and instantiates the systems from the systemsToInstantiate ScriptableObject
    /// </summary>
    /// <returns></returns>
    IEnumerator InitializeSystems()
    {
        //choose which scene to load via scriptable object
        loadOperation = SceneManager.LoadSceneAsync(systemsToInstantiate.mainMenuSceneIndex);
        // don't allow the scene to activate until we are ready
        loadOperation.allowSceneActivation = false;
        while (loadOperation.progress < 0.9f)
        {
            // if we have a progress bar update it as the scene loads
            if (progressBar != null) { progressBar.value = loadOperation.progress; }
            yield return null;
        }
        if (systemsToInstantiate.listOfSystems.Count > 0)
        {
            // make the remaining progress bar increments based on the number of systems to instantiate
            var counter = systemsToInstantiate.listOfSystems.Count / 0.1f;
            foreach (var system in systemsToInstantiate.listOfSystems)
            {
                Instantiate(system);
                // if we have a progress bar update it as the systems instantiate
                if (progressBar != null) { progressBar.value += counter; }
                yield return null;
            }
        }
        // if we have a progress bar set it to fully done.
        if (progressBar != null) { progressBar.value = 1f; }
        loadOperation.allowSceneActivation = true;
    }
    /// <summary>
    /// Called by the state machine when the state is updated
    /// Not requred for this state
    /// </summary>
    public override void StateUpdate()
    {
        base.StateUpdate();
        Debug.Log("StateInitializeFromLoad Update");
    }
    /// <summary>
    /// Called by the state machine when the state is exited
    /// Not requred for this state
    /// </summary>
    public override void StateExit()
    {
        base.StateExit();
        Debug.Log("StateInitializeFromLoad Exited");
    }
}
