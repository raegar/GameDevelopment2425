/* Author Jess
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose: This script handles starting a raid and giving the results
*/

using System.Collections.Generic;
using UnityEngine;

namespace raidSystem
{
    public class RaidSystem : MonoBehaviour
    {
        public static RaidSystem instance;
        public VictoryPanel victoryPanel;
        public Transform vikingsInRaid;

        private int digit;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        public void StartRaid(List<GameObject> vikings)
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
            GetRaidResults();
        }
        public void GetRaidResults()
        {
            victoryPanel.OpenPanel();
        }
    }
}
