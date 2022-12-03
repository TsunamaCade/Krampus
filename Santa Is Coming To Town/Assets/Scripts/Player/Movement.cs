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

    private Vector3 lastPos;

    [SerializeField] private float normalHeight = 3f;
    [SerializeField] private float crouchHeight = 1.5f;
    [SerializeField] private bool isCrouched = false;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.04f;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private Animator anim;
    

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

        if(this.transform.position != lastPos)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }


        //Run
        if(Input.GetButtonDown("Run"))
        {
            speed = sprintSpeed;
            controller.height = normalHeight;
            isCrouched = false;
            anim.SetBool("isRunning", true);
        }

        if(Input.GetButtonUp("Run"))
        {
            speed = normSpeed;
            anim.SetBool("isRunning", false);
        }


        //Crouch
        if(Input.GetButtonDown("Crouch") && isCrouched == false)
        {
            controller.height = crouchHeight;
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.75f, this.transform.position.z);
            groundCheck.position = new Vector3(groundCheck.position.x, groundCheck.position.y + 0.75f, groundCheck.position.z);
            speed = crouchSpeed;
            StartCoroutine(Crouch());
        }

        
        if(Input.GetButtonDown("Crouch") && isCrouched == true)
        {
            controller.height = normalHeight;
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.75f, this.transform.position.z);
            groundCheck.position = new Vector3(groundCheck.position.x, groundCheck.position.y - 0.75f, groundCheck.position.z);
            speed = normSpeed;
            isCrouched = false;
        }

        lastPos = this.transform.position;
    }

    IEnumerator Crouch()
    {
        yield return new WaitForSeconds(0.05f);
        isCrouched = true;
    }
}
