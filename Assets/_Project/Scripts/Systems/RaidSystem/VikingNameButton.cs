/* Author Jess
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose: This script lets the button remove itslef from the raid panel
*/
using UnityEngine;

namespace raidSystem
{
    public class VikingNameButton : MonoBehaviour
    {
        RaidPanel raidPanel;
        private void Awake()
        {
            raidPanel = FindAnyObjectByType<RaidPanel>();
        }
        public void OnClick()
        {
            raidPanel.AddVikingToRaid("VikingPrefab (Clone)");
            Destroy(gameObject);
        }
    }
}
