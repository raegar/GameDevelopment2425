using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitCommandScript))]
public abstract class BaseUnitControls : MonoBehaviour
{
    protected UnitCommandScript unitCommandScript;

    protected virtual void Awake()
    {
        SetReferences();
    }
    protected virtual void SetReferences()
    {
        unitCommandScript = GetComponent<UnitCommandScript>();
    }
}
