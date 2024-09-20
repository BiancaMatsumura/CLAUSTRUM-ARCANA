using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class InteractableItem : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public GameObject uiElements;
    public GameObject interatableUi;

    private UIManager uiManager;

    [Header("Girar")]
    public GameObject interactableItem;  
    public float rotationSpeed = 200f;   

    private bool isRotating = false;     
    private float targetRotationAngle = 0f;  
    private float rotationProgress = 0f;

    public void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        
        uiElements.SetActive(true);
        interatableUi.SetActive(true);
        uiManager.SetCurrentItem(this);

        Debug.Log("interagindo com o item");
        return true;
    }

    public void Deselect()
    {
        uiElements.SetActive(false);
        interatableUi.SetActive(false);
        uiManager.SetCurrentItem(null);
    }


    public void Rotate()
    {
        if (!isRotating)  
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
            }
        }
    }
}