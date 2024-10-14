/* Author       : Don MacSween
 * email        : dm1200@student.aru.ac.uk
 * License      : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Last Modified: 11/10/2024
 * Purpose      : This script is the data container for settler base stats, a scriptable object has been used to allow for easy modification by the designers of settler stats in the future.
 */
/* Author       : Don MacSween
 * email        : dm1200@student.aru.ac.uk
 * License      : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Last Modified: 14/10/2024
 * Purpose : This script is a data container for the randomisation ranges when new settlers are created
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vikings 
{ 
    [CreateAssetMenu(fileName = "VikingRandomizer", menuName = "GameData/Settlers/VikingRandomizerr", order = 1)]
    public class VikingRandomizer : ScriptableObject
    {

        [Tooltip("0> Male 100< Female"), Range(0, 100)]
        public int genderCreationBias = 50;
        [Tooltip("Percentage chance of the settler being a thrall"), Range(0, 100)]
        int thrallStatus = 30;
        [Tooltip("Percentage chance of the settler being a Karl"), Range(0, 100)]
        int karlStatus = 50;
        [Tooltip("Percentage chance of the settler being a Warrior"), Range(0, 100)]
        int warriorStatus = 19;
        [Tooltip("Percentage chance of the settler being a Jarl"), Range(0, 100)]
        int jarlStatus = 1;
        // inset other social statuses here if they are added to the game

        public int minHealth = 50;
        public int maxHealth = 100;

        public int minMaxHealth = 10;
        public int maxMaxHealth = 20;

        public int minAttack = 50;
        public int maxAttack = 100;
      
        public int minDefence = 50;     
        public int maxDefence = 100;    

        public int minMoneyOwned = 10; // born settlers have no money till they come of age
        public int maxMoneyOwned = 100;// born settlers have no money till they come of age
    }
}
