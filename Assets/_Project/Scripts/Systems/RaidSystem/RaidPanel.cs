/* Author Jess
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose: This script handles the Panel for Raiding by adding vikings to raid
*/

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
        public int RaidPower;
        public int recommendPower;

        [Header("script")]
        public VikingListPanel listPanel;

        public void OpenRaidPanel()
        {
            UIManager.Instance.OpenPanel(this);
        }
        public void CloseRaidPanel()
        {
            UIManager.Instance.ClosePanel(this);
        }
        public void AddVikingToRaid(string vikingName)
        {
            List<GameObject> vikings = PopulationManager.Instance.ReturnAllVikings().ToList();
           for (int i = 0; i < vikings.Count; i++)
            {
                if (vikings[i].name == vikingName && vikings[i].activeInHierarchy)
                {
                    vikingInRaid.Add(vikings[i]);
                    vikings[i].transform.SetParent(vikingsToRaid);
                    vikings[i].SetActive(false);

                    //set viking name to button text
                    vikingNameButton.GetComponentInChildren<TextMeshProUGUI>().text = vikingName;
                    Instantiate(vikingNameButton).transform.SetParent(vikingNames);
                }
            }
        }
        public void RemoveVikingFromRaid(GameObject viking)
        {
            // dose not work right now
            vikingsToRaid.Find(viking.name).gameObject.SetActive(true);
            vikingInRaid.Remove(viking);
            Destroy(vikingNames.Find(viking.name).gameObject);
            listPanel.ListVikingNames();
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
