using UnityEngine;
using UnityEngine.UI;
using SaveSystem;
using UnityEngine.EventSystems;

public class MainMenuPanel : PanelBase
{
    [SerializeField] private GameObject NewGamePanel;
    [SerializeField] private GameObject HasSavesPanel;
    [SerializeField] private EventSystem eventsystem;
    [SerializeField] private Canvas canvas;


    private void OnEnable()
    {
        if (eventsystem == null) { eventsystem = GetComponentInParent<EventSystem>(); }
        Canvas.ForceUpdateCanvases();
        if (canvas == null)
        {
            canvas = GetComponentInParent<Canvas>();

            // check if there are any saves
            if (SaveManager.Instance.saveCounter > 0)
            {
                HasSavesPanel.SetActive(true);
                NewGamePanel.SetActive(false);
            }
            else
            {
                HasSavesPanel.SetActive(false);
                NewGamePanel.SetActive(true);
            }
            // TODO select firstGameObject 
        }
    }
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
