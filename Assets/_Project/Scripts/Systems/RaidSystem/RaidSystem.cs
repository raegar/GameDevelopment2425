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
            else if (digit <= 50 && digit > 30)
            {
                Debug.Log("Raid won");
                int amount = vikings.Count;
                if (digit < 70)
                {

                }
                else
                {
                    if (amount > 1)
                    {
                        int number = Random.Range(0, amount);
                        Destroy(vikings[number]);
                    }
                    foreach (GameObject viking in vikings)
                    {
                        viking.SetActive(true);
                        viking.transform.SetParent(null);
                    }
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
