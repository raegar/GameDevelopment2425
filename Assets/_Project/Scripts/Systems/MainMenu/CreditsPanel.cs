
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
