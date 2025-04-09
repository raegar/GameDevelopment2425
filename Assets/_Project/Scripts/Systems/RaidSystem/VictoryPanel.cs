/* Author Jess
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose: This script handles the vicory panel for when the raids over
*/

using Inventory2;
using TMPro;

namespace raidSystem
{
    public class VictoryPanel : PanelBase
    {
        public int silver;
        public int prestige;

        public TextMeshProUGUI silverText;
        public TextMeshProUGUI prestigeText;
        public RaidPanel raidPanel;

        public ItemSO silverSO;
        public ItemSO prestigeSO;
        public void OpenPanel()
        {
            UIManager.Instance.OpenPanel(this);
            SetResults();
        }
        public void ClosePanel()
        {
            UIManager.Instance.ClosePanel(this);
        }
        public void SetResults()
        {
            silverText.SetText("Silver: " + silver.ToString());
            prestigeText.SetText("Prestige: " + prestige.ToString());
            SettlementInventory.Instance.AddItem(silverSO, silver);
            SettlementInventory.Instance.AddItem(prestigeSO, prestige);
        }
    }
}
