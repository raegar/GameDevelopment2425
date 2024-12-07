using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameProjectManager;

public class SaveInputField : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button savebutton;
    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onValueChanged.AddListener(CheckForEmpty);
        inputField.onEndEdit.AddListener(CheckForEmpty);
    }

    public void CheckForEmpty(string input)
    {
        Debug.Log(input);
        if (input == "")
        {
            savebutton.interactable = false;
        }
        else { savebutton.interactable = true; }
    }
}
