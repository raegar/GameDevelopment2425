using GameProjectManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadButton : MonoBehaviour
{
    private string loadName;
    public void SetLoadName(string name)
    {
        loadName = name;
        string[] tmp = name.Split('/');
        loadName = tmp[tmp.Length - 1];
        Button button = GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("Button is null");
        }
        else
        {
            button.onClick.AddListener(OnClick);          
            button.GetComponentInChildren<TMP_Text>().text = loadName;
        }
    }
    void OnClick()
    {
        GameManager.Instance.LoadData(loadName);
    }
}
