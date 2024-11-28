using GameProjectManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteButton : MonoBehaviour
{
    private string deleteName;
    public void SetDeleteName(string name)
    {
        string[] tmp = name.Split('/');
        deleteName = tmp[tmp.Length - 1];
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }
    void OnClick()
    {
        GameManager.Instance.DeleteData(deleteName);
    }
}
