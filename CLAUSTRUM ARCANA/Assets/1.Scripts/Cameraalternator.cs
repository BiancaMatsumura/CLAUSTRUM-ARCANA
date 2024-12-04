using System.Collections;
using UnityEngine;

public class cameraALternator : MonoBehaviour
{
    public GameObject[] cameras;  
    public int cameraIndex;       
    public PlayerMovement playerMovement;  
    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
                playerMovement.StopWalkingAnimation();
            }

            // Desativa todas as câmeras
            foreach (var cam in cameras)
            {
                cam.SetActive(false);
            }

            // Ativa a câmera associada a este trigger
            if (cameraIndex >= 0 && cameraIndex < cameras.Length)
            {
                cameras[cameraIndex].SetActive(true);
            }

            
            StartCoroutine(EnablePlayerMovementAfterDelay(1f));  
        }
    }


    private IEnumerator EnablePlayerMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
            playerMovement.ResumeWalkingAnimation();   
        }
    }
}
