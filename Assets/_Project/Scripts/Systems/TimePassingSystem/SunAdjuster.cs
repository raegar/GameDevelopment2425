using UnityEngine;

public class SunAdjuster : MonoBehaviour
{
    [Header("Sun Settings")]
    [SerializeField] private float sunIntensityMorning = 0.7f;
    [SerializeField] private float sunIntensityMidday = 1f;
    [SerializeField] private float sunIntensityAfternoon = 0.7f;
    [SerializeField] private float sunIntensityNight = 0f;
    [ReadOnly][SerializeField] private float currentSunIntensity;

    [Header("Transition Settings")]
    [SerializeField] private float transitionLength = 1f; // transition length in real seconds

    private TimeManager timeManager;
    private Light sunLight;

    [ReadOnly][SerializeField] private int internalTimeOfDay;

    private void Awake()
    {
        timeManager = FindObjectOfType<TimeManager>();

        if (timeManager == null)
        {
            Debug.LogError("TimeManager not found in scene. Disabling SunAdjuster.", this);
            enabled = false;
        }

        sunLight = GameObject.FindGameObjectWithTag("Sun").GetComponent<Light>();

        if (sunLight == null)
        {
            Debug.LogError("Sun Light not found in scene. Disabling SunAdjuster.", this);
            enabled = false;
        }
    }

    private void Start()
    {
        internalTimeOfDay = timeManager.GetTimeOfDay();
        sunLight.intensity = CalculateSunIntensity(internalTimeOfDay);
        currentSunIntensity = sunLight.intensity;
    }

    private void FixedUpdate()
    {
        UpdateSunIntensity();
    }

    private void UpdateSunIntensity()
    {
        if (internalTimeOfDay != timeManager.GetTimeOfDay())
        {
            internalTimeOfDay = timeManager.GetTimeOfDay();
        }

        if (CalculateSunIntensity(internalTimeOfDay) != sunLight.intensity)
        {
            SmoothTransitionSunIntensity(CalculateSunIntensity(internalTimeOfDay), transitionLength);
        }

        currentSunIntensity = sunLight.intensity;
    }



    private float CalculateSunIntensity(int timeOfDay)
    {
        if (timeOfDay >= 6 && timeOfDay < 12)
        {
            return sunIntensityMorning;
        }
        else if (timeOfDay >= 12 && timeOfDay < 18)
        {
            return sunIntensityMidday;
        }
        else if (timeOfDay >= 18 && timeOfDay < 24)
        {
            return sunIntensityAfternoon;
        }
        else
        {
            return sunIntensityNight;
        }
    }
    private void SmoothTransitionSunIntensity(float targetIntensity, float transitionLength)
    {
        float currentIntensity = sunLight.intensity;
        float transitionFactor = Time.deltaTime / transitionLength;
        sunLight.intensity = Mathf.Lerp(currentIntensity, targetIntensity, transitionFactor); // THIS NEEDS TO SCALE OFF OF THE TIME SCALE IN SOME WAY
    }
}
