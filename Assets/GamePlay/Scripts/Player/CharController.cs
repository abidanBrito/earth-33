using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{

    //Movement
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 3.5f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    public float runSpeed = 7f;

    // animator
    public Animator animator;
    private float speed;
    
    // Spheres
    private Collider[] _collidersDetected;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        GameConstants._sphereModes = 0; // se pone en modo ataque
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.25f);
    }

    //Comprueba todos los Colliders dentro de _detecteds[]; si el tag es de 'Tornillo'
    //Mueve el objeto hacia el 'Player' y muestra un mensaje por consola 
    public void Recolection()
    {
        // Recoleting Tornillos to the player position
        GameConstants.PlayerTargetPosition = transform.position;
        _collidersDetected = Physics.OverlapSphere(transform.position, 3f);

        foreach(Collider c in _collidersDetected)
        {
            if(c.gameObject.tag == "Tornillo") c.gameObject.GetComponent<Tornillos>()._Mode = 2;
        }
    }

    public void Disparar()
    {
            if(GameConstants._usingBall == false && GameConstants.Esferas[0]._Mode == -2)
            {
                GameConstants.Esferas[0]._Mode = 0;
                GameConstants._usingBall = true;
            } 
            else if(GameConstants._usingBall == false && GameConstants.Esferas[1]._Mode == -2)
            {
                GameConstants.Esferas[1]._Mode = 0;
                GameConstants._usingBall = true;
            } 
            else if(GameConstants._usingBall == false && GameConstants.Esferas[2]._Mode == -2)
            {
                GameConstants.Esferas[2]._Mode = 0;
                GameConstants._usingBall = true;
            } 
    }
    void Update()
    {
        Recolection();

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

        if(Input.GetKeyDown(KeyCode.Mouse1)){
            Disparar();
        }
        if(Input.GetKeyDown(KeyCode.Tab)){
            //cambia de modo
            Debug.Log(GameConstants._sphereModes);
            GameConstants._sphereModes++;
            if(GameConstants._sphereModes > 2) GameConstants._sphereModes = 0;//vuelve al modo ataque
        }
        
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        //animation
        animator.SetFloat("speed",speed);
    }
}
