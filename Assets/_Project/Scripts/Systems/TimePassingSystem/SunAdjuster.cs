using UnityEngine;
using TimeConversion;

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

    [Header("Transition")]
    [ReadOnly][SerializeField] private AnimationCurve sunlightAnimationCurve;

    private TimeManager timeManager;
    private Light sunLight;

    [Header("Readonly Debug")]
    [ReadOnly][SerializeField] private float currentSunIntensity;
    [ReadOnly][SerializeField] private (int, int) internalTimeOfDay;

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
        CreateSunlightAnimationCurve();
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
        internalTimeOfDay = timeManager.GetTimeOfDayWithMinutes();
        currentSunIntensity = sunLight.intensity;
    }

    private void FixedUpdate()
    {
        UpdateSunIntensity();
    }

    private void UpdateSunIntensity()
    {
        if (internalTimeOfDay != timeManager.GetTimeOfDayWithMinutes())
        {
            internalTimeOfDay = timeManager.GetTimeOfDayWithMinutes();
        }

        Debug.Log($"Time of day: {internalTimeOfDay.Item1}:{internalTimeOfDay.Item2} - Sun intensity: {currentSunIntensity}");

        TransitionSunIntensity();

        currentSunIntensity = sunLight.intensity;
    }

    private void TransitionSunIntensity()
    {
        float currentIntensity = sunLight.intensity;

        float normalizedTime = (internalTimeOfDay.Item1 + (internalTimeOfDay.Item2 / 60f)) / 24f;
        float curveValue = sunlightAnimationCurve.Evaluate(normalizedTime);
        sunLight.intensity = Mathf.Lerp(currentIntensity, curveValue, Time.deltaTime);
    }

    private void CreateSunlightAnimationCurve()
    {
        sunlightAnimationCurve = new AnimationCurve();

        // Key frames for the sunlightAnimationCurve, representing sun intensity at certain times of day
        sunlightAnimationCurve.AddKey(new Keyframe(0f, sunIntensityNight)); // midnight
        sunlightAnimationCurve.AddKey(new Keyframe(nightSunEnd / 24f, sunIntensityNight)); // end of night
        sunlightAnimationCurve.AddKey(new Keyframe(morningSunStart / 24f, sunIntensityMorning)); // start of morning
        sunlightAnimationCurve.AddKey(new Keyframe(middaySunStart / 24f, sunIntensityMidday)); // start of midday
        sunlightAnimationCurve.AddKey(new Keyframe(eveningSunStart / 24f, sunIntensityEvening)); // start of evening
        sunlightAnimationCurve.AddKey(new Keyframe(nightSunStart / 24f, sunIntensityNight)); // start of night
        sunlightAnimationCurve.AddKey(new Keyframe(1f, sunIntensityNight)); // next midnight
    }
}
