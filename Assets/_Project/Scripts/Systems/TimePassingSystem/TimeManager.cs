using System;
using UnityEngine;
using TimeConversion;

public class TimeManager : MonoBehaviour
{
    [Header("Time Settings")]
    [SerializeField] private int dayTimeScale = 60; // 1 minute in real time = 1 hour in game time
    [SerializeField] private int nightTimeScale = 120; // 1 minute in real time = 2 hours in game time
    [ReadOnly] [SerializeField] private int localTimeScale;

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
        gameTimePassed = TimeConverter.ConvertToSeconds(timeOfDay, 0, 0);
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

        // this line converts seconds to hours and wraps around dayLength, resulting in integer timeOfDay
        timeOfDay = TimeConverter.ConvertToHours(0, 0, (int)gameTimePassed) % dayLength;

        if (timeOfDay != previousTimeOfDay)
        {
            onTimeChanged?.Invoke();
        }

        if (gameTimePassed >= TimeConverter.ConvertToSeconds(dayLength, 0, 0))
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
}