/* Author Jess
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose: This script handles the Panel for Raiding by adding vikings to raid
*/

using SettlerSystem;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace raidSystem
{
    public class RaidPanel : PanelBase
    {
        public Button vikingNameButton;
        public Button addVikingButton;
        public List<GameObject> vikingInRaid = new List<GameObject>();
        [Header("Transforms")]
        public Transform vikingsToRaid;
        public Transform vikingNames;

        [Header("power amounts")]
        public int RaidPower;
        public int recommendPower;
        public void OpenRaidPanel()
        {
            UIManager.Instance.OpenPanel(this);
        }
        public void CloseRaidPanel()
        {
            UIManager.Instance.ClosePanel(this);
        }
        public void AddVikingToRaid()
        {
            GameObject viking = FindFirstObjectByType<GrabSettlerFromFactory>().gameObject;
            if (viking != null && viking.activeInHierarchy)
            {
                vikingInRaid.Add(viking);
                viking.transform.SetParent(vikingsToRaid);
                viking.SetActive(false);
                //set viking name to button text
                vikingNameButton.GetComponentInChildren<TextMeshProUGUI>().text = viking.name;
                Instantiate(vikingNameButton).transform.SetParent(vikingNames);
            }
        }
        public void RemoveVikingFromRaid(GameObject viking)
        {
            // dose not work right now
            //vikingsToRaid.Find(viking.name).gameObject.SetActive(true);
            //vikingInRaid.Remove(viking);
            //Destroy(vikingNames.Find(viking.name).gameObject);
        }
        public void StartRaid()
        {
            if (vikingInRaid.Count > 0)
            {
                for (int i = 1; i < vikingNames.childCount; i++)
                {
                    Destroy(vikingNames.GetChild(i).gameObject);
                }
                RaidSystem.instance.StartRaid(vikingInRaid);
                CloseRaidPanel();
            }
            else
            {
                Debug.Log("No vikings in raid");
            }

        }
    }
}
