using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SaveSystem;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    public TMP_InputField inputField;

    private void Awake()
    {
        if (inputField == null) { inputField = GetComponentInParent<TMP_InputField>(); }
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }
    public void OnClick()
    {
        Debug.Log("SaveButton.OnClick()");
        if (inputField.text == "") { SaveManager.Instance.SaveData(); }
        else { SaveManager.Instance.SaveData(inputField.text); }
    }
}
