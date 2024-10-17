/* Author       : Don MacSween
 * email        : dm1200@student.aru.ac.uk
 * License      : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Last Modified: 11/10/2024
 * Purpose      : This script is the data container for settler base stats, a scriptable object has been used to allow for easy modification by the designers of settler stats in the future.
 */

using UnityEngine;

namespace SettlerSystem
{
    [CreateAssetMenu(fileName = "SettlerBaseStats", menuName = "GameData/Settlers/SettlerBaseStat", order = 1)]
    public class SettlerBaseStats : ScriptableObject
    {
        [Tooltip("Was the settler born in the settlement (true) or migrated to it (false)")]
        public bool wasBornInSettlement =  true; // idea not yet discussed in team, but they have to come from somewhere :)

        [Tooltip("The current health of the settler")]
        public int health;      // value range not yet determined

        [Tooltip("The maximum health of the settler")]
        public int maxHealth;  // value range not yet determined

        [Tooltip("Uses the Gender emum from SettlerEnumerators.cs- Gender.Male / Gender.Female")]
        public Gender gender;     // there is no historical evidence of other gender descriptors from the Settler era

        [Tooltip("Uses the SocalStatus emum from SettlerEnumerators.cs- SocalStatus.Thrall / SocalStatus.Karl / SocalStatus.Warrior / SocalStatus.Jarl")]
        public SocialStatus socialStatus; // the social status of the settler

        [Tooltip("The age of the settler in game days")]
        public int ageInDays = 1;  // average life expectancy of a Settler was 35-40 years (but death by old age has not been discussed)

        [Tooltip("The hunger level of the settler 0-1"), Range(0f, 1f)]
        public float hunger = 1f;      // 0-1 decreases over game time & according to task - settler will stop tasks and go to eat when it reaches 0

        [Tooltip("The fatigue level of the settler 0-1"), Range(0f, 1f)]
        public float fatigue = 1f;       // 0-1 decreases over game time & according to task - settler will stop tasks and rest when it reaches 0

        [Tooltip("The happiness level of the settler 0-1"), Range(0f, 1f)]
        public float happiness = .5f; // can be used to modify other factors in the game - productivity, status, attack etc. -can fluctuate based on other factors

        [Tooltip("The attack power of the settler")]
        public int attack;      // values not yet determined

        [Tooltip("The defence power of the settler")]
        public int defence;     // the defence power of the settler

        [Tooltip("The personal wealth of the settler")]
        public int moneyOwned; // The personal wealth of the settler,could impact happiness, or used to buy personal items
    }
}