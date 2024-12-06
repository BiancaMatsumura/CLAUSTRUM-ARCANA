using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class LayerVisao : MonoBehaviour
{
    public CinemachineVirtualCamera cameraVisao; 
    public LayerMask maskVisao;         
    public LayerMask maskPadrao;         

    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        
        if (cameraVisao != null && cameraVisao.Priority > 0)
        {
            mainCamera.cullingMask = maskVisao.value;
        }
        else
        {
            mainCamera.cullingMask = maskPadrao.value;
        }
    }
}
