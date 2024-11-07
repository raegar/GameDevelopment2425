/*Author: Jess Woodward
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose: the script allows for the Main menu panel to be opened and closed
 */
namespace menuPanels
{
    public class MainMenuPanel : PanelBase
    {
        private void Awake()
        {
            base.Awake();
            OpenMainMenuPanel();
        }
        public void OpenMainMenuPanel()
        {
            UIManager.Instance.CloseAllPanels();
            UIManager.Instance.OpenPanel(this);
        }
        public void CloseMainMenuPanel()
        {
            UIManager.Instance.ClosePanel(this);
        }
    }
}
