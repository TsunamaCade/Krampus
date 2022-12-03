using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] private CharacterController controller;

    [SerializeField] private float speed = 6f;
    [SerializeField] private float normSpeed = 6f;
    [SerializeField] private float gravity = -19.62f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float crouchSpeed = 3;

    [SerializeField] private float normalHeight = 3f;
    [SerializeField] private float crouchHeight = 1.5f;
    [SerializeField] private bool isCrouched = false;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.04f;
    [SerializeField] private LayerMask groundMask;
    

    Vector3 velocity;
    bool isGrounded;

    public Transform cam;

    void Update()
    {
        //Character Movement
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


        //Run
        if(Input.GetButtonDown("Run"))
        {
            speed = sprintSpeed;
            isCrouched = false;
        }

        if(Input.GetButtonUp("Run"))
        {
            speed = normSpeed;
        }


        //Crouch
        if(Input.GetButtonDown("Crouch") && isCrouched == false)
        {
            controller.height = Mathf.Lerp(controller.height, crouchHeight, Time.deltaTime);
            speed = crouchSpeed;
            StartCoroutine(Crouch());
        }

        
        if(Input.GetButtonDown("Crouch") && isCrouched == true)
        {
            controller.height = normalHeight;
            speed = normSpeed;
            isCrouched = false;
        }
    }

    IEnumerator Crouch()
    {
        yield return new WaitForSeconds(0.1f);
        isCrouched = true;
    }
}
