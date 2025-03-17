using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("Time Settings")]
    [SerializeField] private int timeScale = 60; // 1 minute in real time = 1 hour in game time
    [SerializeField] private int nightTimeScale = 120; // 1 minute in real time = 2 hours in game time
    private int localTimeScale;

    [Header("Time Data")]
    [SerializeField] private int timeOfDay = 12; // 0 = midnight, 12 = midday
    [SerializeField] private int dayLength = 24; // 24 hours in a day
    [SerializeField] private int gameDay = 1; // start at day 1
    private float gameTimePassed = 0;

    // Events
    public static Action onDayChanged;

    private void Update()
    {
        UpdateTime();
    }

    private void Start()
    {
        localTimeScale = timeScale;
        gameTimePassed = timeOfDay * 3600; // Corrected initialization
    }

    private void IncrementDay()
    {
        gameTimePassed = 0;
        gameDay++;
        onDayChanged?.Invoke();
    }

    private void UpdateTime()
    {
        localTimeScale = IsNight() ? nightTimeScale : timeScale;
        gameTimePassed += Time.deltaTime * localTimeScale;
        timeOfDay = ConvertSecondsToHours((int)gameTimePassed) % dayLength;
        if (gameTimePassed >= ConvertHoursToSeconds(dayLength))
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
    private int ConvertHoursToSeconds(int hours)
    {
        return hours * 3600;
    }

    private int ConvertSecondsToHours(int seconds)
    {
        return seconds / 3600;
    }
}