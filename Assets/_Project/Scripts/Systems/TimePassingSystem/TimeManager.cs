using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("Time Settings")]
    [SerializeField] private int dayTimeScale = 60; // 1 minute in real time = 1 hour in game time
    [SerializeField] private int nightTimeScale = 120; // 1 minute in real time = 2 hours in game time
    private int localTimeScale;

    [Header("Time Data")]
    [SerializeField] private int timeOfDay = 12; // 0 = midnight, 12 = midday
    [SerializeField] private int dayLength = 24; // 24 hours in a day
    [SerializeField] private int gameDay = 1; // start at day 1
    private float gameTimePassed = 0;

    // Events
    public static Action onDayChanged;
    public static Action onTimeChanged;

    private void Update()
    {
        UpdateTime();
    }

    private void Start()
    {
        localTimeScale = IsNight() ? nightTimeScale : dayTimeScale;
        gameTimePassed = ConvertHoursSeconds(timeOfDay, true);
    }

    private void IncrementDay()
    {
        gameTimePassed = 0;
        gameDay++;
        onDayChanged?.Invoke();
    }

    private void UpdateTime()
    {
        localTimeScale = IsNight() ? nightTimeScale : dayTimeScale;
        gameTimePassed += Time.deltaTime * localTimeScale;

        int previousTimeOfDay = timeOfDay;
        timeOfDay = ConvertHoursSeconds((int)gameTimePassed, false) % dayLength;

        if (timeOfDay != previousTimeOfDay)
        {
            onTimeChanged?.Invoke();
        }

        if (gameTimePassed >= ConvertHoursSeconds(dayLength, true))
        {
            IncrementDay();
        }
    }

    private bool IsNight()
    {
        return timeOfDay >= 18 || timeOfDay < 6;
    }


    // Public methods
    public int GetTimeOfDay() // returns time of day in hours
    {
        return timeOfDay;
    }

    public int GetDay() // returns current day
    {
        return gameDay;
    }

    public int GetTimeScale() // returns current time scale
    {
        return localTimeScale;
    }

    public int GetDayLength() // returns day length in hours
    {
        return dayLength;
    }

    // Helper methods
    private int ConvertHoursSeconds(int hours, bool hoursToSeconds)
    {
        if (hoursToSeconds)
        {
            return hours * 3600;
        }
        else
        {
            return hours / 3600;
        }
    }
}