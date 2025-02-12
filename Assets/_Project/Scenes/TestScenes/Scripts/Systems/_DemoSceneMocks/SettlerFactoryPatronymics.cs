using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;
namespace SettlerSystem
{
    public class SettlerFactoryPatronymics : SettlerFactory
    {
        [Header("Patronymics for Demo")]
        [SerializeField] private string fatherNameTest;
        [SerializeField] private StatsType statsType;
        private Gender gender;
        public enum StatsType
        {
            Base,
            Custom,
            Random
        }


        public override GameObject CreateCustom(SocialStatus socialStatus = SocialStatus.Unassigned, int familyID = 0, bool usePatronymics = true, Gender gender = Gender.Male, bool randomFather = false)
        {
            GameObject _go = new GameObject();
            _go.tag = "Settler";
            Settler settler = _go.AddComponent<Settler>();
            settler.gender = gender;
            settler.familyID = familyID;
            settler.forename = NamingManager.Instance.ChooseFirstName(settler.gender);

            string fatherNameTestInstance = "";

            if (randomFather == true)
            {
                fatherNameTestInstance = NamingManager.Instance.ChooseFirstName(Gender.Male);
            }
            else
            {
                fatherNameTestInstance = fatherNameTest;
            }

            if (fatherNameTest.Length > 0)
            {
                settler.surname = NamingManager.Instance.ChooseLastName(settler.gender, usePatronymics, true, fatherNameTestInstance);
            }
            else
            {
                settler.surname = NamingManager.Instance.ChooseLastName(settler.gender, false, true, fatherNameTestInstance);
            }

            _go.name = $"Settler_{settler.forename}_{settler.surname}";
            settler.skills = new SerializedDictionary<string, int>(settlerSkillDictionary.skills);
            settler.traits = new SerializedDictionary<string, List<SettlerModifiers>>();
            settler.stats = SetStats(statsType);
            return _go;
        }

        public SettlerStats SetStats(StatsType statsType)
        {
            bool wasBornInBase = false;
            int health = 0;
            int maxHealth = 0;
            float hunger = 0;
            float fatigue = 0;
            float happiness = 0;
            int attack = 0;
            int defence = 0;
            int moneyOwned = 0;
            SocialStatus socialStatus = SocialStatus.Unassigned;

            switch (statsType)
            {
                case StatsType.Base:
                    wasBornInBase = settlerBaseStats.wasBornInSettlement;
                    health = settlerBaseStats.maxHealth;
                    maxHealth = settlerBaseStats.maxHealth;
                    hunger = settlerBaseStats.hunger;
                    fatigue = settlerBaseStats.fatigue;
                    happiness = settlerBaseStats.happiness;
                    attack = settlerBaseStats.attack;
                    defence = settlerBaseStats.defence;
                    moneyOwned = settlerBaseStats.moneyOwned;
                    socialStatus = settlerBaseStats.socialStatus;

                    return new SettlerStats(wasBornInBase, health, maxHealth, socialStatus, 0, hunger, fatigue, happiness, attack, defence, moneyOwned);

                case StatsType.Custom:
                    // Set custom stats here, remember to change the return and add logic
                    return new SettlerStats(wasBornInBase, health, maxHealth, socialStatus, 0, hunger, fatigue, happiness, attack, defence, moneyOwned);

                case StatsType.Random:
                    // Set random stats here, remember to change the return and add logic
                    int bornInSettlementRNG = Random.Range(0, 2);
                    if (bornInSettlementRNG == 0)
                    {
                        wasBornInBase = true;
                    }
                    else
                    {
                        wasBornInBase = false;
                    }
                    health = Random.Range(0, settlerBaseStats.maxHealth);
                    maxHealth = Random.Range(health, settlerBaseStats.maxHealth);
                    hunger = Random.Range(0, 1f);
                    fatigue = Random.Range(0, 1f);
                    happiness = Random.Range(0, 1f);
                    attack = Random.Range(0, 20);
                    defence = Random.Range(0, 20);
                    moneyOwned = Random.Range(0, 10000);
                    int randomStatus = Random.Range(0, 3);
                    switch (randomStatus)
                    {
                        case 0:
                            socialStatus = SocialStatus.Thrall;
                            break;
                        case 1:
                            socialStatus = SocialStatus.Karl;
                            break;
                        case 2:
                            socialStatus = SocialStatus.Warrior;
                            break;
                        case 3:
                            socialStatus = SocialStatus.Jarl;
                            break;
                    }
                    return new SettlerStats(wasBornInBase, health, maxHealth, socialStatus, 0, hunger, fatigue, happiness, attack, defence, moneyOwned);

                default:
                    return new SettlerStats(wasBornInBase, health, maxHealth, socialStatus, 0, hunger, fatigue, happiness, attack, defence, moneyOwned);
            }
        }
    }
}
