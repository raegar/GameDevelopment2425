/* Author Jess
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose: This script handles the vicory panel for when the raids over
*/

using TMPro;

namespace raidSystem
{
    public class VictoryPanel : PanelBase
    {
        public TextMeshProUGUI silverText;
        public TextMeshProUGUI prestigeText;
        public RaidPanel raidPanel;
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
            silverText.SetText("Silver: " + raidPanel.silver.ToString());
            prestigeText.SetText("Prestige: " + raidPanel.prestige.ToString());
        }
    }
}
