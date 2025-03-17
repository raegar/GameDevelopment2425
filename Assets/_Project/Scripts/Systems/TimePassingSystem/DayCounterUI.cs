using TMPro;
using UnityEngine;

public class DayCounterUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dayText;
    [SerializeField] TimeManager timeManager;

    private void Awake()
    {
        if (timeManager == null)
        {
            timeManager = FindObjectOfType<TimeManager>();
            if (timeManager == null)
            {
                Debug.LogError("DayCounterUI: Missing TimeManager reference.", this);
                enabled = false;
            }
        }

        if (dayText == null)
        {
            dayText = GetComponent<TextMeshProUGUI>();
            if (dayText == null)
            {
                Debug.LogError("DayCounterUI: Missing TextMeshProUGUI reference.", this);
                enabled = false;
            }
        }
    }

    private void Start()
    {
        UpdateDayText();
    }

    private void OnEnable()
    {
        TimeManager.onDayChanged += UpdateDayText;
    }

    private void OnDisable()
    {
        TimeManager.onDayChanged -= UpdateDayText;
    }

    private void UpdateDayText()
    {
        dayText.text = "Day " + timeManager.GetDay();
    }
}
