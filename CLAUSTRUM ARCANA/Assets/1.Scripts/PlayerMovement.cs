using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;

    private Vector3 direction;
    
    private FixedJoystick joystick;

    private CharacterController characterController;

    private Animator animator;
    void Start()
    {
        joystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>(); 
    }

    
    void Update()
    {
        
        MovementeMobile();
        Rotation();
    }

    void MovementeMobile()
    {
        direction = (Vector3.forward * joystick.Vertical) + (Vector3.right * joystick.Horizontal);
        direction = direction.normalized;
        characterController.Move(direction * (playerSpeed * Time.deltaTime));

        if(direction != Vector3.zero)
        {
            animator.SetBool("walking",true);
            
        }
        else
        {
            animator.SetBool("walking",false);
        }
    }

    void Rotation()
    {

        if(direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
            
        }

        transform.Translate(direction * (playerSpeed * Time.deltaTime),Space.World);
    }

}
