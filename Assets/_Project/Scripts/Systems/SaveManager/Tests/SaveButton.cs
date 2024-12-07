using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameProjectManager;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
                     private Button button;

    private void Awake()
    {
        if (inputField == null) { inputField = GetComponentInParent<TMP_InputField>(); }
        button = GetComponent<Button>();
    }
    private void OnEnable()
    {
        button.onClick.AddListener(OnClick);
    }
    private void OnDisable()
    {
        button.onClick.RemoveListener(OnClick);
    }
    public void OnClick()
    {
      
            Debug.Log("Save clicked with name");
            SaveManager.Instance.SaveData(inputField.text);

    }
}
