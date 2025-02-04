/* Author Jess
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose: This script lets the button remove itslef from the raid panel
*/

using UnityEngine;

namespace raidSystem
{
    public class VikingNameButton : MonoBehaviour
    {
        public void OnClick()
        {
            FindObjectOfType<RaidPanel>().RemoveVikingFromRaid(gameObject);
        }
    }
}
