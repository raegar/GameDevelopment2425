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
        public List<GameObject> vikingInRaid = new List<GameObject>();
        [Header("Transforms")]
        public Transform vikings;
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
            if (vikings.childCount - 1 > 1)
            {
                GameObject viking = FindFirstObjectByType<GrabSettlerFromFactory>().gameObject;
                vikingInRaid.Add(viking);
                viking.transform.SetParent(vikingsToRaid);
                viking.SetActive(false);
                //set viking name to button text
                vikingNameButton.GetComponentInChildren<TextMeshProUGUI>().text = viking.GetComponent<GrabSettlerFromFactory>().name;
                Instantiate(vikingNameButton).transform.SetParent(vikingNames);
            }
            else
            {
                Debug.Log("need at least one viking in settlement");
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
                foreach (GameObject go in vikingNames)
                {
                    Destroy(go);
                }
                CloseRaidPanel();
            }
            else
            {
                Debug.Log("No vikings in raid");
            }

        }
    }
}
