/* Author Jess
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose: This script handles starting a raid and giving the results
*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace raidSystem
{
    public class RaidSystem : MonoBehaviour
    {
        public static RaidSystem instance;
        public VictoryPanel victoryPanel;
        public Transform vikingsInRaid;
        public Button raidPanelButton;
        public float raidLength = 60;

        List<GameObject> raidList;
        bool raidStarted = false;
        float raidTime;
        int digit;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        private void Update()
        {
            if (raidStarted == true)
            {
                if (raidTime >= raidLength)
                {
                    raidStarted = false;
                    EndRaid(raidList);
                }
            }
        }
        public void StartRaid(List<GameObject> vikings)
        {
            raidPanelButton.enabled = false;
            raidStarted = true;
            raidTime = Time.time;
            raidList = vikings;
        }

        public void EndRaid(List<GameObject> vikings)
        {
            digit = Random.Range(0, 101);
            if (digit <= 30)
            {
                Debug.Log("raid lost");
                foreach (GameObject viking in vikings)
                {
                    Destroy(viking);
                }
            }
            else
            {
                Debug.Log("Raid won");
                for (int i = 0; i < vikingsInRaid.childCount; i++)
                {
                    GameObject viking = vikingsInRaid.GetChild(i).gameObject;
                    viking.SetActive(true);
                    viking.transform.SetParent(null);
                }
            }
            raidPanelButton.enabled = true;
            victoryPanel.OpenPanel();
        }
    }
}
