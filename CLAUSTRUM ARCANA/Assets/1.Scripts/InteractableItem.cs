using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class InteractableItem : MonoBehaviour, IInteractable
{
    public GameObject interactableItem;
    public GameObject uiElements;
    public GameObject buttonRotate;

    private bool isSelected = false;
    private bool canRotate = true;

    private bool isRotating = false;
    public float rotationSpeed = 200f;

    private float targetRotationAngle = 0f; 
    private float rotationProgress = 0f;

    private static InteractableItem currentSelectedItem;

    public void Interact()
       {
        if (!isSelected)
        {
            SelectItem();
        }

       
    }

    public void Deselect()  
    {
        if (isSelected)
        {
            DeselectItem();
        }
    }

    public bool IsSelected() 
    {
        return isSelected;
    }


    private void SelectItem()
    {
        if (currentSelectedItem != null)
        {
            currentSelectedItem.Deselect(); 
        }

        isSelected = true;
        currentSelectedItem = this;

        if (uiElements != null && buttonRotate != null)
        {
            uiElements.SetActive(true); 
            buttonRotate.SetActive(true);
        }

        Debug.Log("Item Selecionado");
    }

    private void DeselectItem()
    {
        isSelected = false;
        currentSelectedItem = null;

        if (uiElements != null && buttonRotate != null)
        {
            uiElements.SetActive(false);
            buttonRotate.SetActive(false);
        }

        Debug.Log("Item Deselecionado");
    }

    public void Rotate()
    {
     
        if (canRotate && !isRotating && isSelected)
        {
            targetRotationAngle += 90f;
            isRotating = true;
        }
    }

    private void Update()
    {
        if (isRotating)
        {
            
            rotationProgress = Mathf.MoveTowards(rotationProgress, targetRotationAngle, rotationSpeed * Time.deltaTime);

            
            interactableItem.transform.rotation = Quaternion.Euler(0f, rotationProgress, 0f);

            
            if (Mathf.Abs(rotationProgress - targetRotationAngle) < 0.1f)
            {
                
                interactableItem.transform.rotation = Quaternion.Euler(0f, targetRotationAngle, 0f);
                isRotating = false; 
                Debug.Log("Item rotacionado suavemente para " + targetRotationAngle + " graus");
            }
        }
    }
}