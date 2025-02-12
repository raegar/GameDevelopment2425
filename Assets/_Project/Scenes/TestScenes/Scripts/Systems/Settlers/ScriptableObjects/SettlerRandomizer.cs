/* Author       : Don MacSween
 * email        : dm1200@student.aru.ac.uk
 * License      : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Last Modified: 14/10/2024
 * Purpose : This script is a data container for the randomisation ranges when new settlers are created in the SettlerFactory.
 */

using UnityEngine;

namespace SettlerSystem 
{ 
    [CreateAssetMenu(fileName = "SettlerRandomizer", menuName = "GameData/Settlers/settlerRandomiser", order = 1)]
    public class SettlerRandomizer : ScriptableObject
    {
        //--------- Stats randomisation ------------
        [Tooltip("0> Male 100< Female"), Range(0, 100)]
        public int genderCreationBias = 50;

        [Tooltip("Percentage chance of the settler being a thrall"), Range(0, 100)]
        public int thrallStatus = 30;
        [Tooltip("Percentage chance of the settler being a Karl"), Range(0, 100)]
        public int karlStatus = 50;
        [Tooltip("Percentage chance of the settler being a Warrior"), Range(0, 100)]
        public int warriorStatus = 20;
        // there can be only one Jarl in a settlement will need to impliment a way to elect a  new one if one dies.
        // insert other social statuses here if they are added to the game

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
