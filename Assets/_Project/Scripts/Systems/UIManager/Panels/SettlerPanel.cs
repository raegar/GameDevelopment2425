/* Author(s)    : Don MacSween & Jess Woodward
 * email(s)     : dm1200@student.aru.ac.uk & jw1519@student.aru.ac.uk
 * License      : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Last Modified: 26/10/2024
 * Purpose      :A simple script to demonstrate inheritance from the PanelBase.cs script
 */
using SettlerSystem;
using System.Collections;
using TMPro;
using UnityEngine;
public class SettlerPanel : PanelBase
{

    private Settler selectedUnit;

    [SerializeField] private TextMeshProUGUI nameText, ageText, genderText, birthdayText;

    [SerializeField] private TextMeshProUGUI healthText, socialStatusText, hungerText, fatigueText, happinessText, attackText, defenceText, moneyOwnedText;

    private bool staticAttributesUpdated = false;

    [Header("Values")]
    [SerializeField] private string nameValue, genderValue, socialStatusValue, ageValue, worldDayBornValue, healthValue, hungerValue, fatigueValue, happinessValue, attackValue, defenceValue, moneyOwnedValue;
    protected override void Awake()
    {
        // Dont remove this. This is required to register the panel with the UIManager
        base.Awake();

        // do anything else that you want to do when your script wakes up here
    }

    private void OnEnable()
    {
        Debug.Log("Settler Panel is enabled");
        if (!EnsureSettlerReference())
        {
            return;
        }
        LoadSettler();
    }

    private void OnDisable()
    {
        selectedUnit = null;
        staticAttributesUpdated = false;
        Debug.Log("Settler Panel is disabled");
    }
    // do other funky stuff here

    public bool EnsureSettlerReference()
    {
        //link to UnitControlManager for reference to the selected settler
        selectedUnit = UnitControlManager.Instance.selectedUnit.GetComponentInChildren<Settler>();

        if (selectedUnit == null)
        {
            Debug.LogError("No settler selected");
            return false;
        }
        else
        {
            return true;
        }
    }

    public void LoadSettlerDataStatic(Settler values)
    {
        nameValue = values.forename + " " + values.surname;
        genderValue = values.gender.ToString();
        worldDayBornValue = values.worldDayBorn.ToString();
        Debug.Log(values.stats);
        Debug.Log(values.stats.attack);
        attackValue = values.stats.attack.ToString();
        defenceValue = values.stats.defence.ToString();
        socialStatusValue = values.stats.socialStatus.ToString();
    }

    public void LoadSettlerDataOther(Settler values)
    {
        ageValue = values.age.ToString();
        healthValue = values.stats.health + "/" + values.stats.maxHealth;
        hungerValue = values.stats.hunger.ToString("0.00");
        fatigueValue = values.stats.fatigue.ToString("0.00");
        happinessValue = values.stats.happiness.ToString("0.00");
        moneyOwnedValue = values.stats.moneyOwned.ToString();
    }

    public void DisplaySettlerDataStatic()
    {
        nameText.text = nameValue;
        genderText.text = genderValue;
        birthdayText.text = "Born: Day " + worldDayBornValue;
        attackText.text = "Attack: " + attackValue;
        defenceText.text = "Defence: " + defenceValue;
        socialStatusText.text = socialStatusValue;
    }

    public void DisplaySettlerDataOther()
    {
        ageText.text = "Age: " + ageValue;
        healthText.text = "Health: " + healthValue;
        hungerText.text = "Hunger: " + hungerValue;
        fatigueText.text = "Fatigue: " + fatigueValue;
        happinessText.text = "Happiness: " + happinessValue;
        moneyOwnedText.text = "Money: " + moneyOwnedValue;
    }

    public void LoadSettler()
    {
        LoadSettlerDataStatic(selectedUnit);
        LoadSettlerDataOther(selectedUnit);

        DisplaySettlerDataStatic();

        staticAttributesUpdated = true;
    }

    private void FixedUpdate()
    {
        UpdateAttributes();
    }

    private void UpdateAttributes()
    {
        if (selectedUnit == null || !staticAttributesUpdated)
        {
            return;
        }
        DisplaySettlerDataOther();
    }
}
