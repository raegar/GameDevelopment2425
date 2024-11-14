using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameProjectManager;

public class SaveInputField : MonoBehaviour
{
    public TMP_InputField inputField;

    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onEndEdit.AddListener(OnEndedInputField);
    }

    public void OnEndedInputField(string input)
    {
        if (input == "")
        {
            SaveManager.Instance.SaveData();
        }
        else { SaveManager.Instance.SaveData(input); }
    }
}
