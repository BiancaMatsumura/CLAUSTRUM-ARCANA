using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private InteractableItem currentItem;

    public void SetCurrentItem(InteractableItem item)
    {
        currentItem = item;
    }

    public void OnRotateButtonPressed()
    {
        if (currentItem != null)
        {
            currentItem.Rotate();
        }
    }
}