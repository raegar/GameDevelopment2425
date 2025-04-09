using TMPro;
using UnityEngine;

public class DayCounterUI : MonoBehaviour
{
    [SerializeField] private bool showHours = true;

    [SerializeField] TextMeshProUGUI dayText;
    [SerializeField] GameObject hoursTextHolder; // gameobject to ensure that the right reference is assigned

    [SerializeField] TimeManager timeManager;

    private TextMeshProUGUI hoursText; 

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

        if (hoursText == null)
        {

            hoursText = hoursTextHolder.GetComponent<TextMeshProUGUI>();
            if (hoursText == null)
            {
                Debug.LogError("DayCounterUI: Missing TextMeshProUGUI reference.", this);
                showHours = false;
            }
        }
    }

    private void Start()
    {
        UpdateDayText();
        if (showHours)
        {
            hoursTextHolder.SetActive(true);
            UpdateHoursText();
        }
        else
        {
            hoursTextHolder.SetActive(false);
        }
    }

    private void OnEnable()
    {
        TimeManager.onDayChanged += UpdateDayText;
        TimeManager.onTimeChanged += UpdateHoursText;
    }

    private void OnDisable()
    {
        TimeManager.onDayChanged -= UpdateDayText;
        TimeManager.onTimeChanged -= UpdateHoursText;
    }

    private void UpdateDayText()
    {
        dayText.text = "Day " + timeManager.GetDay();
    }

    private void UpdateHoursText()
    {
        int timeOfDay = timeManager.GetTimeOfDay();
        hoursText.text = ConvertTo12HourClock(timeOfDay) + " " + CalculateHourSuffix(timeOfDay);
    }

    private int ConvertTo12HourClock(int timeOfDay)
    {
        if (timeOfDay > 12)
        {
            return timeOfDay - 12;
        }
        else
        {
            return timeOfDay;
        }
    }

    private string CalculateHourSuffix(int timeOfDay)
    {
        if (timeOfDay >= 12)
        {
            return "PM";
        }
        else
        {
            return "AM";
        }
    }
}
