using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractor : MonoBehaviour , IInteractable
{
    public void Interact()
    {
        Debug.Log("Interacting with TestInteractor");
    }
}
