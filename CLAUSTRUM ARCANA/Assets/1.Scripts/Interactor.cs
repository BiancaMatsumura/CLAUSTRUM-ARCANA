using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public void Interact();
    public void Deselect();
    public bool IsSelected();  
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;

    private IInteractable currentInteractable;  

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = new Ray(InteractorSource.position, InteractorSource.forward);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, InteractRange))
                {
                   
                    if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                    {
                        
                        if (currentInteractable != null && currentInteractable != interactObj)
                        {
                            currentInteractable.Deselect();
                        }

                        
                        interactObj.Interact();
                        currentInteractable = interactObj;
                    }
                    else
                    {
                        
                        if (currentInteractable != null)
                        {
                            currentInteractable.Deselect();
                            currentInteractable = null; 
                        }
                    }
                }
                else
                {
                    
                    if (currentInteractable != null)
                    {
                        currentInteractable.Deselect();
                        currentInteractable = null;
                    }
                }
            }
        }
    }
}
