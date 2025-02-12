/*Author: Jess Woodward
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose: the script allows for the settings panel to be opened and closed
 */
namespace menuPanels
{
    public class SettingsPanel : PanelBase
    {
        public void OpenSettingsPanel()
        {
            UIManager.Instance.OpenPanel(this);
        }
        public void CloseSettingsPanel()
        {
            UIManager.Instance.ClosePanel(this);
        }
    }
}
