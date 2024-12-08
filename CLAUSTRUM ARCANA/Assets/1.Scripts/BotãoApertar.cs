using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class TrocadorDeCamera : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera cam2;
    public AudioSource audion;
    public Image fillImage;
    public Toggle button;
    public float fillDuration = 2f;
    public DialogueController dialogueController;

    public Coroutine fillingCoroutine;
    public float fillProgress = 1f; 

    private Scene cenaAtiva;
    private bool acabou = false;

    void Start()
    {
        cenaAtiva = SceneManager.GetActiveScene();

    }
    public void SwitchCamera()
    {
        if (cam2.Priority == 0)
        {
            cam2.Priority = 10;
            audion.Play();
            ToggleFilling();
        }
        else
        {
            cam2.Priority = 0;
            audion.Play();
            ToggleFilling();
        }
    }

    public void ResetVision() 
    {
        fillImage.fillAmount = 1;
        fillProgress = 1;
        button.interactable = true;
        if (acabou)
        {
            ToggleFilling();
            acabou = false;
        }
        


    }

    void ToggleFilling()
    {
        if (cenaAtiva.name != "Tutorial" )
        {
            if (fillingCoroutine != null)
            {
                StopCoroutine(fillingCoroutine);
                fillingCoroutine = null; 
            }
            else
            {
                fillingCoroutine = StartCoroutine(FillOverTime()); 
            }


        }

    }

    private IEnumerator FillOverTime()
    {
        while (fillProgress > 0f)
        {
            fillProgress -= Time.deltaTime / fillDuration;
            fillImage.fillAmount = fillProgress;
            yield return null;
        }

        fillProgress = 0f;
        fillImage.fillAmount = 0f;
        cam2.Priority = 0;
        dialogueController.StartDialogue(2);
        button.interactable = false;
        acabou = true;
        
    }


}
