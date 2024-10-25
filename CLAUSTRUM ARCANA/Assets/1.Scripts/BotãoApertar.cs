
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TrocadorDeCamera : MonoBehaviour
{
    // troca as cameras
    public Cinemachine.CinemachineVirtualCamera cam1;

    public Cinemachine.CinemachineVirtualCamera cam2;

    public AudioSource audion;

    public Image fillImage;
    public Toggle button;
    public float fillDuration = 2f;

    public DialogueController dialogueController;

    private bool isFilling = false;



    //public GameObject Ui;

    public void SwitchCamera()
    {
        if (cam1.Priority > cam2.Priority)
        {
            cam2.Priority = 10;
            cam1.Priority = 0;
            //Ui.SetActive(true);
            audion.Play();
            StartFilling();
        }
        else
        {
            cam1.Priority = 10;
            cam2.Priority = 0;
            // Ui.SetActive(false);
            audion.Play();
            dialogueController.Dialogue03();

        }
    }

    void StartFilling()
    {
        if (!isFilling)
        {
            isFilling = true;
            StartCoroutine(FillOverTime(fillDuration));
        }

    }
    private IEnumerator FillOverTime(float duration)
    {
        float time = 0f;
        fillImage.fillAmount = 1f;

        while (time < duration)
        {
            time += Time.deltaTime;
            fillImage.fillAmount = Mathf.Lerp(1f, 0f, time / duration);
            yield return null;
        }

        fillImage.fillAmount = 0f;
        isFilling = false;
        SwitchCamera();
        button.interactable = false;

    }
}
