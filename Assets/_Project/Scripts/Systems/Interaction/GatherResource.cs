using SettlerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GatherResource : MonoBehaviour, IInteractable
{
    public bool availableToGather = true;
    public GameObject selectedByViking = null;
    public int resourceAmount = 30;
    public float gatherTime = 5f;
    public string skillRequired;
    public int skillLevel;
    private Settler settler;
    public float coolDownDuration = 60f;
    public float cooldownTimer;
    // problem with Jess's Item class - public Item item;
    public bool isGathering = false;
    void Awake()
    {
       cooldownTimer = coolDownDuration;
    }

    void OnTriggerEnter(Collider other)
    {
     if (other.gameObject == selectedByViking)
        {
            Gather(selectedByViking);
        }
    }

    public void Interact()
    {
        selectedByViking = UnitControlManager.Instance.selectedUnit.gameObject;
    }

    void Update()
    {
        // cooldown for gathering
        if (!availableToGather)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                availableToGather = true;
                cooldownTimer = coolDownDuration;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == selectedByViking)
        {
            isGathering = false;
            selectedByViking = null;
            CancelInvoke();
        }
    }

    public void Gather(GameObject viking)
    {
        Debug.Log("Gathering");
        isGathering = true;
        settler = selectedByViking.GetComponentInChildren<Settler>();
        if (settler.skills.ContainsKey(skillRequired))
        {
            skillLevel = settler.skills[skillRequired];
            // play animation

            // when time is up , call GatherComplete
            Invoke("GatherComplete", gatherTime/skillLevel);
        }
        else {return; }
    }

    public void GatherComplete()
    {
        // problem with Jess's Item class vikingInventory inventory = selectedByViking.GetComponentInChildren<vikingInventory>();
        // problem with Jess's Item class inventory.AddResource(item, resourceAmount/10 * skillLevel );
        Debug.Log("Gathered " + resourceAmount / 10 * skillLevel); 
        selectedByViking = null;
        // stop animation

        // cooldown if applicable
        availableToGather = false;
    }
}
