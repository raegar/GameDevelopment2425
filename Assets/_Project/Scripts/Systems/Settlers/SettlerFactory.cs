/* Author(s)    : Don MacSween
 * email(s)        : dm1200@student.aru.ac.uk
 * License      : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Last Modified: 11/10/2024
 * Purpose : This script is designed as sole method that components can use to create a settler.
 *           The create method can be overloaded to allow for different configurations of settler to be created.
 *           This class follows the factory design pattern which constructs a complex object behind a common interface
 *           and returns the object as a product of the factory.
 */
using PatternLibrary;
using UnityEngine;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
namespace SettlerSystem
{
    public class SettlerFactory : Singleton<SettlerFactory>
    {
        // source of Settler randomization
        [SerializeField] private SettlerRandomizer settlerRandomizer;
        [SerializeField] private SettlerBaseStats settlerBaseStats;
        [SerializeField] private SettlerSkillDictionary settlerSkillDictionary;
        protected override void Awake()
        {
            base.Awake();
            if (settlerRandomizer == null || settlerBaseStats == null || settlerSkillDictionary == null)
            {
                Debug.LogError("SettlerFactory not set up correctly in the inspector");
            }
        }

        public GameObject Create(SocialStatus socialStatus = SocialStatus.Unassigned, int familyID = 0)
        {
            GameObject _go = new GameObject();
            _go.tag = "Settler";
            Settler settler = _go.AddComponent<Settler>();
            settler.gender = GenerateRandomisedGender();
            settler.familyID = familyID;
            settler.forename = GenerateRandomisedForeName(settler.gender);
            settler.surname = GenerateRandomisedSurName();
            _go.name = $"Settler_{settler.forename}_{settler.surname}";
            settler.skills = new SerializedDictionary<string,int>(settlerSkillDictionary.skills);
            settler.traits = new SerializedDictionary<string, List<SettlerModifiers>>();
            //settler.stats = settlerBaseStats.stats;
            return _go;
        }

        /// <summary>
        /// Generates a random gender based on the bias in the SettlerRandomizer
        /// </summary>
        /// <returns>enum Gender</returns>
        private Gender GenerateRandomisedGender()
        {
           
            if (Random.Range(0, 100) < settlerRandomizer.genderCreationBias)
            { return Gender.Male; } else {return Gender.Female;}
        }
        /// <summary>
        /// Generates a random social status based on the bias in the SettlerRandomizer
        /// </summary>
        /// <returns>enum SocialStatus</returns>
        private SocialStatus GenerateRandomisedSocialStatus()
        {
            // although the SettlerRandomizer asks for a percentage chance of each social status, allow for human error through summing the values
            int _random = Random.Range(0, settlerRandomizer.thrallStatus + settlerRandomizer.karlStatus + settlerRandomizer.warriorStatus);
            Debug.Log($"{_random} social random");
            if (_random < settlerRandomizer.thrallStatus) {return SocialStatus.Thrall;}
            if (_random >= settlerRandomizer.thrallStatus && _random < (settlerRandomizer.thrallStatus + settlerRandomizer.karlStatus))
            {return SocialStatus.Karl;}
            if (_random > (settlerRandomizer.thrallStatus + settlerRandomizer.karlStatus)) {return SocialStatus.Warrior;}
            else
            {
                // should never occur but if it does return unassigned
                Debug.LogError("Social status randomisation error");
                return SocialStatus.Unassigned;
            }
        }

        private string GenerateRandomisedForeName(Gender gender)
        {
            if (gender == Gender.Male)
            {
                return "Eric";
            }
            else
            {
                return "Helga";
            }
        }

        private string GenerateRandomisedSurName()
        {
            return "Bloodaxe";
        }
    }
}

      
  

