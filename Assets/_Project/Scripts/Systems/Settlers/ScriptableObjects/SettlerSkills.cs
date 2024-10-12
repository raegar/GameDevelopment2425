/* Author       : Don MacSween
 * email        : dm1200@student.aru.ac.uk
 * License      : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Last Modified: 11/10/2024
 * Purpose      : This script is the data container for settler base stats, a scriptable object has been used to allow for easy modification by the designers of settler stats in the future.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using AYellowpaper.SerializedCollections.Editor;


[CreateAssetMenu(fileName = "SettlerBaseStats", menuName = "GameData/Settlers/SettlerSkills", order = 1)]
public class SettlerSkills : ScriptableObject
{
    [SerializedDictionary("Skill", "Initial Value")]
    public SerializedDictionary<string,int> settlerSkills = new SerializedDictionary<string, int>();
}
