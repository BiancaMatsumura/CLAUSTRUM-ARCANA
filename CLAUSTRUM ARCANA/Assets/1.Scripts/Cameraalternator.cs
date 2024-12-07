using System.Collections;
using UnityEngine;

public class cameraALternator : MonoBehaviour
{
    public GameObject[] cameras; 
    private int[] dialogos; 
    public int cameraIndex;       
    public PlayerMovement playerMovement;  

    public DialogueController dialogueController;

    private void Start()
    {
        dialogos = new int[3];
        dialogos[0] = 5;
        dialogos[1] = 6;
        dialogos[2] = 7;
    }
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
            // Inicia o diálogo associado ao índice da câmera
            if (dialogueController != null && cameraIndex >= 0 && cameraIndex < dialogos.Length)
            {
                dialogueController.StartDialogue(dialogos[cameraIndex]);
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
