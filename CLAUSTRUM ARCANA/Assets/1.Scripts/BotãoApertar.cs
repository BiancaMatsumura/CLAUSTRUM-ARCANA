using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class TrocadorDeCamera : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera cam1;
    public Cinemachine.CinemachineVirtualCamera cam2;
    public AudioSource audion;
    public Image fillImage;
    public Toggle button;
    public float fillDuration = 2f;
    public DialogueController dialogueController;

    private Coroutine fillingCoroutine;
    private float fillProgress = 1f; 

    private Scene cenaAtiva;

    void Start()
    {
        cenaAtiva = SceneManager.GetActiveScene();

    }
    public void SwitchCamera()
    {
        if (cam1.Priority > cam2.Priority)
        {
            cam2.Priority = 10;
            cam1.Priority = 0;
            audion.Play();
            ToggleFilling();
        }
        else
        {
            cam1.Priority = 10;
            cam2.Priority = 0;
            audion.Play();
            ToggleFilling();
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
        cam1.Priority = 10;
        dialogueController.Dialogue03();
        button.interactable = false;
        fillingCoroutine = null;
    }
}
