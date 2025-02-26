using SettlerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using InventorySystem;
using Inventory2;


public class GatherResource : MonoBehaviour, IInteractable
{
    public bool availableToGather = true;
    public GameObject selectedByViking = null;
    public int resourceAmount = 30;
    public float gatherTime = 5f;
    public string skillRequired;
    private Animator vikingAnimator;
    public int skillLevel;
    private Settler settler;
    public float coolDownDuration = 60f;
    public float cooldownTimer;
    public Inventory2.Item item;
    public ItemSO templateItem;
    public AudioClip gatherSound;
    public bool isGathering = false;
    public GameObject beforeHarvest;
    public GameObject afterHarvest;
    private HandInteractable handInteractable;
    private AnimationEvents animationEvent;
    public string animationName;

    void Awake()
    {
        cooldownTimer = coolDownDuration;
        beforeHarvest.SetActive(true);
        afterHarvest.SetActive(false);
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
                beforeHarvest.SetActive(true);
                afterHarvest.SetActive(false);
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
            vikingAnimator.SetBool("isWoodcutting", false);
            CancelInvoke();
        }
    }

    public void Gather(GameObject viking)
    {
        Debug.Log("Gathering");
        isGathering = true;
        settler = selectedByViking.GetComponentInChildren<Settler>();
        handInteractable = selectedByViking.GetComponentInChildren<HandInteractable>();
        animationEvent = selectedByViking.GetComponent<AnimationEvents>();
        animationEvent.sound = gatherSound;
        if (settler.skills.ContainsKey(skillRequired))
        {
            skillLevel = settler.skills[skillRequired];
            // play animation
            if (handInteractable != null)
            {
                handInteractable.Equip();
            }
            vikingAnimator = selectedByViking.GetComponent<Animator>();
            vikingAnimator.SetBool("isWalking", false);
            vikingAnimator.SetBool("isWoodcutting", true);

            // when time is up , call GatherComplete
            Invoke("GatherComplete", gatherTime / skillLevel);
        }
        else { return; }
    }

    public void GatherComplete()
    {
        
        SettlementInventory.Instance.AddItem(templateItem, resourceAmount);
        Debug.Log("Gathered " + resourceAmount);
        animationEvent.sound = null;
        selectedByViking = null;
        // stop animation

        if (handInteractable != null)
        {
            handInteractable.Unequip();
        }
        vikingAnimator.SetBool("isWoodcutting", false);
        // cooldown if applicable
        availableToGather = false;
        beforeHarvest.SetActive(false);
        afterHarvest.SetActive(true);
    }
}
