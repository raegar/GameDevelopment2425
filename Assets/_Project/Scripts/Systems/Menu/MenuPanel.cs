/* Author: Jess
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 *  Purpose: This script controls the opening and closing of the menu panel. 
 */
public class MenuPanel : PanelBase
{
    void Awake()
    {
        base.Awake();
    }
    public void OpenMenu()
    {
        UIManager.Instance.OpenPanel(this);
    }
    public void CloseMenu()
    {
        UIManager.Instance.ClosePanel(this);
    }
}
