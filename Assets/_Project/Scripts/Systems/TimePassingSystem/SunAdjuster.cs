using UnityEngine;

public class SunAdjuster : MonoBehaviour
{
    [Header("Morning")]
    [SerializeField] private float sunIntensityMorning = 0.7f;
    [SerializeField] private int morningSunStart = 6;
    [SerializeField] private int morningSunEnd = 12;
    [Header("Midday")]
    [SerializeField] private float sunIntensityMidday = 1f;
    [SerializeField] private int middaySunStart = 12;
    [SerializeField] private int middaySunEnd = 18;
    [Header("Evening")]
    [SerializeField] private float sunIntensityEvening = 0.7f;
    [SerializeField] private int eveningSunStart = 18;
    [SerializeField] private int eveningSunEnd = 20;
    [Header("Night")]
    [SerializeField] private float sunIntensityNight = 0f;
    [SerializeField] private int nightSunStart = 20;
    [SerializeField] private int nightSunEnd = 6;

    [Header("Transition Settings")]
    [SerializeField] private float transitionLength = 1f; // transition length in real seconds

    private TimeManager timeManager;
    private Light sunLight;

    [Header("Readonly Debug")]
    [ReadOnly][SerializeField] private float currentSunIntensity;
    [ReadOnly][SerializeField] private int internalTimeOfDay;

    private void Awake()
    {
        timeManager = FindObjectOfType<TimeManager>();

        if (timeManager == null)
        {
            Debug.LogError("SunAdjuster: TimeManager not found in scene. Disabling SunAdjuster.", this);
            enabled = false;
        }

        sunLight = GameObject.FindGameObjectWithTag("Sun").GetComponent<Light>();

        if (sunLight == null)
        {
            Debug.LogError("SunAdjuster: Sun Light not found in scene. Disabling SunAdjuster.", this);
            enabled = false;
        }

        EnsureAppropriateSunTimings();
    }

    private void EnsureAppropriateSunTimings()
    {
        middaySunStart = morningSunEnd;
        eveningSunStart = middaySunEnd;
        nightSunStart = eveningSunEnd;
        nightSunEnd = morningSunStart;
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
            TransitionSunIntensity(CalculateSunIntensity(internalTimeOfDay), transitionLength, true);
        }

        currentSunIntensity = sunLight.intensity;
    }



    private float CalculateSunIntensity(int timeOfDay)
    {
        if (timeOfDay >= morningSunStart && timeOfDay < morningSunEnd)
        {
            return sunIntensityMorning;
        }
        else if (timeOfDay >= middaySunStart && timeOfDay < middaySunEnd)
        {
            return sunIntensityMidday;
        }
        else if (timeOfDay >= eveningSunStart && timeOfDay < eveningSunEnd)
        {
            return sunIntensityEvening;
        }
        else if (timeOfDay >= nightSunStart || timeOfDay < nightSunEnd)
        {
            return sunIntensityNight;
        }
        else
        {
            Debug.LogWarning("SunAdjuster: Time of day not within any range. Defaulting to night.", this);
            return 0; // default to night, although this shouldn't happen
        }
    }
    private void TransitionSunIntensity(float targetIntensity, float transitionLength, bool scaleWithTimeScale)
    {
        float currentIntensity = sunLight.intensity;
        float transitionFactor = 1f;


        if (!scaleWithTimeScale)
        {
            transitionFactor = Time.deltaTime / transitionLength;
        }
        else
        {
            transitionFactor = Time.deltaTime * timeManager.GetTimeScale() / transitionLength;

        }
        sunLight.intensity = Mathf.Lerp(currentIntensity, targetIntensity, transitionFactor);
    }
}
