using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : PanelBase
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;

    public void NewGame() 
    {
        
        //GameManager.Instance.NewGame();
    }
    public void ContinueGame() 
    {
        // get playerprefs last save
        //GameManager.Instance.LoadGame();
    }
    public void LoadGame() 
    {
        //UIManager.Instance.ShowPanel()
    }
    public void Settings() 
    {
        //UIManager.Instance.ShowPanel()
    }
    public void Exit()
    {
        //GameManager.Instance.RequestGameExit();
    }
}
