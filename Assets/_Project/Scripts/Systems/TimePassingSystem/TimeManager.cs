using System;
using UnityEngine;
using TimeConversion;

public class TimeManager : MonoBehaviour
{
    [Header("Time Settings")]
    [SerializeField] private int dayTimeScale = 60; // 1 minute in real time = 1 hour in game time
    [SerializeField] private int nightTimeScale = 120; // 1 minute in real time = 2 hours in game time
    private int localTimeScale;

    [Header("Day Settings")]
    [SerializeField] private int hourOfDay = 12; // 0 = midnight, 12 = midday
    [SerializeField] private int dayLength = 24; // 24 hours in a day
    [SerializeField] private int gameDay = 1; // start at day 1
    [SerializeField] private int nightTimeStart = 18; // 6 PM
    [SerializeField] private int nightTimeEnd = 6; // 6 AM
    private float gameTimePassed = 0;
    private float lastGameTimePassed = 0; // used to check if time has changed

    [Header("Debug Information")]
    [ReadOnly][SerializeField] private string timeScaleRatio;
    [ReadOnly][SerializeField] private string currentTime;

    // Actions
    public static Action onDayChanged;
    public static Action onTimeChanged;

    private void Update()
    {
        UpdateTime();
    }

    private void Awake()
    {
        localTimeScale = IsNight() ? nightTimeScale : dayTimeScale;
        gameTimePassed = TimeConverter.ConvertToSeconds(hourOfDay, 0, 0);
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

        int previousTimeOfDay = hourOfDay;

        // this line converts seconds to hours and wraps around dayLength, resulting in integer hourOfDay
        hourOfDay = (int)TimeConverter.ConvertToHours(0, 0, gameTimePassed, false);

        if (previousTimeOfDay != hourOfDay)
        {
            previousTimeOfDay = hourOfDay;
            onTimeChanged?.Invoke();
        }

        if (gameTimePassed >= TimeConverter.ConvertToSeconds(dayLength, 0, 0))
        {
            IncrementDay();
        }

        timeScaleRatio = $"1 real minute equals {localTimeScale} game minutes";
        currentTime = $"Day {gameDay}, Time {GetTimeOfDayWithMinutes().Item1}:{GetTimeOfDayWithMinutes().Item2}";
    }

    private bool IsNight()
    {
        return hourOfDay >= nightTimeStart || hourOfDay < nightTimeEnd;
    }


    // public methods
    public int GetTimeOfDay() // returns time of day in hours
    {
        return hourOfDay;
    }

    public (int, int) GetTimeOfDayWithMinutes() // returns time of day in hours and minutes
    {
        float hours = TimeConverter.ConvertToHours(0, 0, gameTimePassed, false);
        float leftOverTime = hours % 1;
        float minutes = TimeConverter.ConvertToMinutes(leftOverTime, 0, 0);
        return ((int)Mathf.Floor(hours), (int)Mathf.Floor(minutes));
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

    // Test/Utility method
    public void FastForwardTime(int hours)
    {
        gameTimePassed += TimeConverter.ConvertToSeconds(hours, 0, 0);
        UpdateTime();
    }
}