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
                viking.transform.localScale = Vector3.one;
                //set viking name to button text
                vikingNameButton.GetComponentInChildren<TextMeshProUGUI>().text = viking.name;
                Instantiate(vikingNameButton).transform.SetParent(vikingNames);
            }
        }
        public void RemoveVikingFromRaid(GameObject viking)
        {
            vikingsToRaid.Find(name).gameObject.SetActive(true);
            vikingInRaid.Remove(viking);
        }
        public void StartRaid()
        {
            if (vikingInRaid.Count > 0)
            {
                RaidSystem.instance.StartRaid(vikingInRaid);
                for (int i = 0; i < vikingsToRaid.childCount; i++)
                {
                    Destroy(vikingsToRaid.GetChild(i).gameObject);
                }
                Instantiate(addVikingButton).transform.SetParent(vikingNames);
                CloseRaidPanel();
            }
            else
            {
                Debug.Log("No vikings in raid");
            }

        }
    }
}
