/*Author: Jess Woodward
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose: the script allows for the credits panel to be opend and closed
 */
namespace menuPanels
{
    public class CreditsPanel : PanelBase
    {
        private void Awake()
        {
            base.Awake();
        }
        public void OpenCreditsPanel()
        {
            UIManager.Instance.OpenPanel(this);
            GetComponentInChildren<AutoScroll>().ResetCredits();
        }
        public void CloseCreditsPanel()
        {
            UIManager.Instance.ClosePanel(this);
        }
    }
}
