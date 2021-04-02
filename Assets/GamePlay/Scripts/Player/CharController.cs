using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 3.5f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    public float runSpeed = 7f;
    public Animator animator;
    private float speed;
    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            animator.SetBool("jumping", false);
            playerVelocity.y = -1f;                        
        }

        //gets controller direction.
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //if shit is pressed and is grounded
        if (Input.GetButton("Fire3") && groundedPlayer)
        {
            controller.Move(move * Time.deltaTime * runSpeed);
            speed =  runSpeed;
            
        }else{
            controller.Move(move * Time.deltaTime * playerSpeed);
            speed = playerSpeed;
        }

        if (move != Vector3.zero)
        {
            transform.forward = move;                
        }else{
            if(move.magnitude < 1)
            speed = 0;
        }


        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            animator.SetBool("jumping", true);
            
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);


        //animation
        animator.SetFloat("speed",speed);
    }
}
