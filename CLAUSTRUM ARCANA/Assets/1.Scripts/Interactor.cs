using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    
    public string InteractionPrompt { get; }

    public bool Interact(Interactor interactor);

    void Deselect();

}

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRaious = 0.5f;
    [SerializeField] private LayerMask _interactableMask;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;

    private IInteractable currentInteractable;

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRaious, _colliders, _interactableMask);

        if (_numFound > 0)
        {
            var interactable = _colliders[0].GetComponent<IInteractable>();

            if (interactable != null)
            {
                currentInteractable = interactable; 

                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Began)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(touch.position);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _interactableMask))
                        {
                            if (hit.collider == _colliders[0])
                            {
                                interactable.Interact(this);
                            }
                        }
                    }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRaious);
    }

}
