/* Author  : Don MacSween
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose : This script contains the common enumerators for the settler system.
 *           It is generally considered to be good practice to keep all cross script 
 *           enumerators in one place to avoid duplication and confusion.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Please ensure that if the stats in SettlerStats.cs are changed, the the StatType emum is updated to reflect the changes
public enum StatType // The different types of stats that a settler can have
{
    Health,
    MaxHealth,
    Hunger,
    Gender,
    AgeInDays,
    Sleep,
    Attack,
    Defence
}

public enum Gender // The gender of the settler - only used cosmetically & for family procreation
{
    Male, Female
}

public enum SocialStatus
{
    Unassigned, // A settler that has not been assigned a social status
    Thrall, // A slave or servant
    Karl,   // A free middle class settler
    Warrior,// A warrior or soldier
    Jarl,   // A noble or leader
}