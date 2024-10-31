
public class LoadGamePanel : PanelBase
{
    public void OpenLoadgamePanel()
    {
        UIManager.Instance.OpenPanel(this);
    }
    public void CloseLoadgamePanel()
    { 
        UIManager.Instance.ClosePanel(this);
    }
}
