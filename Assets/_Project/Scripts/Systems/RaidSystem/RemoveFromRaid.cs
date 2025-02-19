using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace raidSystem
{
    public class RemoveFromRaid : MonoBehaviour
    {
        RaidPanel raidPanel;
        private void Awake()
        {
            raidPanel = FindAnyObjectByType<RaidPanel>();
        }
        public void OnClick()
        {
            raidPanel.RemoveVikingFromRaid(gameObject.name);
        }
    }
}
