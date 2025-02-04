using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] IInteractable interactable;

    public void Interact()
    {
        interactable.Interact();
    }
}
