/*Author: Jess Woodward
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose: the script allows for the load game panel to be opened and closed
 */
namespace menuPanels
{
    public class LoadGamePanel : PanelBase
    {
        public void OpenLoadgamePanel()
        {
            UIManager.Instance.CloseAllPanels();
            UIManager.Instance.OpenPanel(this);
        }
        public void CloseLoadgamePanel()
        {
            UIManager.Instance.ClosePanel(this);
        }
    }
}
