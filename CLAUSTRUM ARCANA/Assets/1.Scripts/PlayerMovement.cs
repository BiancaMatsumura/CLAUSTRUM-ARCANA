using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;

    private Vector3 direction;
    
    private FixedJoystick joystick;

    private CharacterController characterController;

    public Animator animator;

    public TrocadorDeCamera vision;

    public bool direcao01 = false;
    public bool direcao02 = false;

    private Scene cenaAtiva;
    private CinemachineBrain cinemachineBrain;

    private bool isChangingCamera = false;  

    //public GameObject[] cameras;  
    //private int currentCameraIndex = 0;  
    public Animator stairsAnim;

    void Start()
    {
        joystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
        characterController = GetComponent<CharacterController>();
        cenaAtiva = SceneManager.GetActiveScene();
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();

        
        //cameras[currentCameraIndex].SetActive(true);  
    }

    void Update()
    {
        
        if (!isChangingCamera)
        {
            MovementeMobile();
            Rotation();
        }

        CheckCameraAndUpdateDirection();

    }

    void MovementeMobile()
    {
        MoveJoystick();
        characterController.Move(direction * (playerSpeed * Time.deltaTime));

        if (direction != Vector3.zero)
        {
            animator.SetBool("walking", true); 
        }
        else
        {
            animator.SetBool("walking", false);
        }
    }

    void Rotation()
    {
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        transform.Translate(direction * (playerSpeed * Time.deltaTime), Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            vision.fillImage.fillAmount = 1;
            vision.fillProgress = 1;
            vision.button.interactable = true;
            Destroy(other.gameObject);
        }
    }

    public void MoveJoystick()
    {
        if (cenaAtiva.name == "Tutorial")
        {
            direction = (Vector3.right * -joystick.Vertical) + (Vector3.back * -joystick.Horizontal);
        }
        else
        {
            if (!direcao01 && !direcao02)
            {
                direction = (Vector3.right * -joystick.Vertical) + (Vector3.back * -joystick.Horizontal);
            }
            else if (direcao01 && !direcao02)
            {
                direction = (Vector3.right * joystick.Horizontal) + (Vector3.back * -joystick.Vertical);
            }
            else if (!direcao01 && direcao02)
            {
                direction = (Vector3.right * joystick.Vertical) + (Vector3.back * joystick.Horizontal);
            }
        }
    }

    
    public void StopWalkingAnimation()
    {
        animator.SetBool("walking", false);  
    }

    
    public void ResumeWalkingAnimation()
    {
        if (direction != Vector3.zero)
        {
            animator.SetBool("walking", true);
        }
    }

    void CheckCameraAndUpdateDirection()
    {
        if (cinemachineBrain != null)
        {
            CinemachineVirtualCamera activeCam = cinemachineBrain.ActiveVirtualCamera as CinemachineVirtualCamera;

            if (activeCam != null)
            {
                if (activeCam.name == "Cam3") 
                {
                    direcao01 = true;
                    direcao02 = false;
                }
                else if (activeCam.name == "Cam4") 
                {
                    stairsAnim.SetBool("descerEscada",true);
                    direcao01 = false;
                    direcao02 = true;
                }
            }
        }
    }
}
