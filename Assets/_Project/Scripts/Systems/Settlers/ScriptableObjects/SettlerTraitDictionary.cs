/* Author       : Don MacSween
 * email        : dm1200@student.aru.ac.uk
 * License      : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Last Modified: 16/10/2024
 * Purpose      : This script is the data container for settler traits, a scriptable object has been used to allow for easy modification by the designers of settler stats in the future.
 */
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using AYellowpaper.SerializedCollections.Editor;

namespace SettlerSystem
{

    [CreateAssetMenu(fileName = "SettlerTraitDictionary", menuName = "GameData/Settlers/SettlerTraits", order = 1)]
    public class SettlerTraitDictionary : ScriptableObject
    {
        [SerializedDictionary("Trait", "List of Modifiers")]
        public SerializedDictionary<string, List<SettlerModifiers>> traits = new SerializedDictionary<string, List<SettlerModifiers>>();
    }
}