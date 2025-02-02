using TMPro;

namespace raidSystem
{
    public class VictoryPanel : PanelBase
    {
        public int silverGained;
        public int prestigeGained;

        public TextMeshProUGUI silverText;
        public TextMeshProUGUI prestigeText;
        public void OpenPanel()
        {
            UIManager.Instance.OpenPanel(this);
        }
        public void ClosePanel()
        {
            UIManager.Instance.ClosePanel(this);
        }
        public void SetResults()
        {
            silverText.SetText("Silver Gained: " + silverGained.ToString());
            prestigeText.SetText("Prestige Gained: " + prestigeGained.ToString());
        }
    }
}
