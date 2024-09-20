
using Unity.VisualScripting;
using UnityEngine;

public class TrocadorDeCamera : MonoBehaviour
{
    // troca as cameras
    public Cinemachine.CinemachineVirtualCamera cam1;

    public Cinemachine.CinemachineVirtualCamera cam2;
    //public GameObject Ui;
    public void SwitchCamera()
    {
        if (cam1.Priority > cam2.Priority)
        {
            cam2.Priority = 10;
            cam1.Priority = 0;
            //Ui.SetActive(true);
        }
        else 
        {
            cam1.Priority = 10;
            cam2.Priority = 0;
           // Ui.SetActive(false);
        }
    }
    }
