/* Author Jess
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose: This script handles the Panel for Raiding by adding vikings to raid
*/

using SettlerSystem;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace raidSystem
{
    public class RaidPanel : PanelBase
    {
        [Header("Buttons")]
        public Button vikingNameButton;
        public Button addVikingButton;
        
        [Header("Transforms")]
        public Transform vikingsToRaid;
        public Transform vikingNames;

        List<GameObject> vikingInRaid = new List<GameObject>();

        [Header("power amounts")]
        public int recommendedPower = 4;

        public TextMeshProUGUI raidPowerText;
        public TextMeshProUGUI recommendedPowerText;

        [Header("Rewards")]
        public int silver = 10;
        public int prestige = 15;

        public TextMeshProUGUI silverText;
        public TextMeshProUGUI prestigeText;

        [Header("script")]
        public VikingListPanel listPanel;

        public int amountOfVikingsToRaid = 4;

        public void OpenRaidPanel()
        {
            UIManager.Instance.OpenPanel(this);
        }
        public void CloseRaidPanel()
        {
            UIManager.Instance.ClosePanel(this);
            UIManager.Instance.ClosePanel(listPanel);
        }
        public void AddVikingToRaid(string viking)
        {
            List<GameObject> vikings = PopulationManager.Instance.ReturnAllVikings().ToList();
           for (int i = 0; i < vikings.Count; i++)
            {
                string foreName = vikings[i].GetComponent<GrabSettlerFromFactory>().foreName;
                string surname = vikings[i].GetComponent<GrabSettlerFromFactory>().surName;
                if (foreName == viking && vikings[i].activeInHierarchy)
                {
                    vikingInRaid.Add(vikings[i]);
                    vikings[i].transform.SetParent(vikingsToRaid);
                    vikings[i].SetActive(false);

                    //set viking name to button text
                    vikingNameButton.GetComponentInChildren<TextMeshProUGUI>().text = foreName + " " + surname;
                    Instantiate(vikingNameButton, vikingNames).name = foreName;
                    UpdateRewards();
                    return;
                }
            }
        }
        public void RemoveVikingFromRaid(string vikingName)
        {
            for (int i = 0; i <= vikingInRaid.Count; i++)
            {
                if (vikingInRaid[i].GetComponent<GrabSettlerFromFactory>().foreName == vikingName)
                {
                    vikingInRaid[i].SetActive(true);
                    vikingInRaid[i].transform.SetParent(null);
                    vikingInRaid.Remove(vikingInRaid[i]);
                    listPanel.ListVikingNames();
                    UpdateRewards();
                    return;
                }
            }
        }
        public void RemoveAllVikingsFromraid()
        {
            for(int i = 0; i  < vikingInRaid.Count;i++)
            {
                RemoveVikingFromRaid(vikingInRaid[i].GetComponent<GrabSettlerFromFactory>().foreName);
            }
        }
        public void StartRaid()
        {
            if (vikingInRaid.Count >= amountOfVikingsToRaid)
            {
                GameObject[] population = PopulationManager.Instance.ReturnAllVikings();
                if (vikingInRaid.Count == population.Length)
                {
                    Debug.Log("To many vikings in raid. need one to look after settlement");
                }
                else
                {
                    for (int i = 1; i < vikingNames.childCount; i++)
                    {
                        Destroy(vikingNames.GetChild(i).gameObject);
                    }
                    RaidSystem.instance.StartRaid(vikingInRaid);
                    CloseRaidPanel();
                    listPanel.CloseVikingListpanel();
                }
            }
            else
            {
                Debug.Log("Not enough vikings in raid. Need " + amountOfVikingsToRaid + " to raid");
            }
        }
        public bool CheckIfVikingInRaid(string vikingName)
        {
            for(int i = 0; i < vikingInRaid.Count;i++)
            {
                if (vikingInRaid[i].GetComponent<GrabSettlerFromFactory>().foreName.Equals(vikingName))
                {
                    return true;
                }
            }
            return false;
        }
        public void UpdateRewards()
        {
            silverText.text = "Silver: " + (silver * vikingInRaid.Count).ToString();
            prestigeText.text = "Prestige: " + (prestige * vikingInRaid.Count).ToString();
            raidPowerText.text = "Raid Power: " + vikingInRaid.Count.ToString();
            recommendedPowerText.text = "Recommended Power: " + recommendedPower.ToString();
        }
    }
}
