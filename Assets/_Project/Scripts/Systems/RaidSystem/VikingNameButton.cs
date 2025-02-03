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
