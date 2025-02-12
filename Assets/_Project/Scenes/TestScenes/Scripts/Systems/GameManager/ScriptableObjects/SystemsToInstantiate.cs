/* Author  : Don MacSween
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose : This script acts a a data container for the systems that need to be instantiated when the game starts
 *           created as a ScriptableObject so that it can be easily modified in the editor
 */
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SystemsToInstantiate", menuName = "GameData/GameManager/SystemsToInstantiate")]
public class SystemsToInstantiate : ScriptableObject
{
    // List of systems to instantiate
    public List<GameObject> listOfSystems;
    // scene index of the main menu in : File -> Build Settings -> Scenes in Build
    public int mainMenuSceneIndex = 2;
    // scene index of the main menu : File -> Build Settings -> Scenes in Build
    public int mainGameSceneIndex = 3;
}
