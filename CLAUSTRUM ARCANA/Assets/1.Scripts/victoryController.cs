using System.Collections;
using Cinemachine;
using UnityEngine;

public class victoryController : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;
    public GameObject creditsPanel;
    public GameObject luz;
    public GameObject logo;

    private bool camMenu = true;
    private bool camCredits = false;

    public CinemachineBrain cinemachineBrain;

    void Start()
    {
        cinemachineBrain.m_CameraActivatedEvent.AddListener(OnCameraTransitionComplete);
    }

    private void OnCameraTransitionComplete(ICinemachineCamera fromCam, ICinemachineCamera toCam)
    {
        StartCoroutine(WaitForBlendComplete());
    }

    private IEnumerator WaitForBlendComplete()
    {
        
        while (cinemachineBrain.IsBlending)
        {
            yield return null; 
        }

        
        if (!camMenu)
        {
            creditsPanel.SetActive(true);
            luz.SetActive(true);
            logo.SetActive(true);
            
        }

    }

    public void TrocadorDeCamera()
    {
        if (camMenu && !camCredits)
        {
            camMenu = false;
            camCredits = true;
            cam2.Priority = 10;
            cam1.Priority = 0;
           
        }
        else if (!camMenu && camCredits)
        {
            camMenu = true;
            camCredits = false;
            cam1.Priority = 10;
            cam2.Priority = 0;
            creditsPanel.SetActive(false);
            luz.SetActive(false);
            logo.SetActive(false);

            
        }
    }
}
