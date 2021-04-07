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

    //new movement with Camera
    Vector3 move;
    public Vector3 nextPosition;
    public Quaternion nextRotation;
    public float rotationPower = 5f;
    public float rotationLerp = 0.5f;
    // Neck Follow for camera
    public GameObject followTransform;

    // Control Object
    

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        GameConstants._sphereModes = 0; // se pone en modo ataque
        Debug.Log(GameConstants._sphereModes);
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
        foreach(Collider c in _collidersDetected)
        {
            if(c.gameObject.tag == "Tornillo") c.gameObject.GetComponent<Tornillos>()._Mode = 2;
        }
    }

    public void Disparar()
    {
        int mode = GameConstants._sphereModes;
        
            if(GameConstants._usingBall == false && GameConstants.Esferas[0]._Movements == -2)
            {
                GameConstants.Esferas[0]._Movements = 0; //modo de movimiento de la esfera
                GameConstants.Esferas[0]._Mode = mode; //Modo de la esfera
                GameConstants._usingBall = true;
            } 
            else if(GameConstants._usingBall == false && GameConstants.Esferas[1]._Movements == -2)
            {
                GameConstants.Esferas[1]._Movements = 0;
                GameConstants.Esferas[1]._Mode = mode; //Modo de la esfera

                GameConstants._usingBall = true;
            } 
            else if(GameConstants._usingBall == false && GameConstants.Esferas[2]._Movements == -2)
            {
                GameConstants.Esferas[2]._Movements = 0;
                GameConstants.Esferas[2]._Mode = mode; //Modo de la esfera
                GameConstants._usingBall = true;
            } 
    }
    private void CambiarModo()
    {
        //cambia de modo
        GameConstants._sphereModes++;
        Debug.Log(GameConstants._sphereModes);
        if(GameConstants._sphereModes > 2) GameConstants._sphereModes = 0;//vuelve al modo ataque
    }
    void Update()
    {
        groundedPlayer = controller.isGrounded;
        _collidersDetected = Physics.OverlapSphere(transform.position, 3f);
        if(_collidersDetected.Length > 0) Recolection();

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            animator.SetBool("jumping", true);
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        if(Input.GetKeyDown(KeyCode.Mouse0)){
            Disparar();
        }
        if(Input.GetKeyDown(KeyCode.Tab)){
            CambiarModo();
        }
        if(GameConstants._HasPet){
            if(Input.GetKeyDown(KeyCode.Mouse1)){
               Pet petControl = GameConstants.Pet.GetComponent<Pet>();
               petControl.StopControlingEnemy();
            }
        }
        if(GameConstants._collectedObject != null){
            if(Input.GetKeyDown(KeyCode.R)){
               MovableObject movableObject = GameConstants._collectedObject.GetComponent<MovableObject>();
               movableObject.StopControlingObject();
            }
        }

        if (groundedPlayer && playerVelocity.y < 0)
        {
            animator.SetBool("jumping", false);
            playerVelocity.y = -1f;                        
        }

        //transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationPower, Vector3.up);
        followTransform.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationPower, Vector3.up);

        #region Vertical Rotation
        followTransform.transform.rotation *= Quaternion.AngleAxis(-Input.GetAxis("Mouse Y") * rotationPower, Vector3.right);

        var angles = followTransform.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTransform.transform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < 280)
        {
            angles.x = 280;
        }
        else if(angle < 180 && angle > 40)
        {
            angles.x = 40;
        }

        followTransform.transform.localEulerAngles = angles;
        #endregion

        //gets controller direction.
        // Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        nextRotation = Quaternion.Lerp(followTransform.transform.rotation, nextRotation, Time.deltaTime * rotationLerp); //guarda la posicion de giro

        if (move.x == 0 && move.z == 0) 
        {   
            nextPosition = transform.position; // cuando esta quieto no se gira
            ComprobarMovimiento(); 
            return; 
        }
        ComprobarMovimiento();

        Vector3 position = (transform.forward * move.x * 5f) + (transform.right * move.z * 5f);
        nextPosition = transform.position + position;  

        transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
        //reset the y rotation of the look transform
        followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
    }

    private void ComprobarMovimiento(){
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
            //transform.forward = move;          
        }else{
            if(move.magnitude < 1)
            speed = 0;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        //animation
        animator.SetFloat("speed",speed);
    }
}
