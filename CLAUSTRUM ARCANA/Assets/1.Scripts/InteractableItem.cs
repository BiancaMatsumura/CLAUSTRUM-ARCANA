using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractable
{
    public void Interact()
    {

        Debug.Log("Interagindo com o Item");

    }

}
