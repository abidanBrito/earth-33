using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : BaseGame
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
    private Animator animator;
    private float speed;
    
    // Spheres
    EnergyBall sphereController;

    private Collider[] _collidersDetected;

    //new movement with Camera
    Vector3 move;
    private Vector3 nextPosition;
    private Quaternion nextRotation;
    public float rotationPowerCam = 5f;
    public float rotationLerpCam = 0.5f;
    // Neck Follow for camera
    private GameObject followGameObject;

    // Mirillas
    private Transform rotatorEngine;
    public float rotationSpeedSpheres = 3;

    private void Awake()
    {
        followGameObject = GameObject.FindWithTag(GameConstants.PLAYER_NECK_TAG);
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        sphereModes = 0; // se pone en modo ataque
        sphereController = GetComponentInChildren<EnergyBall>();
        Cursor.visible = false;
    }
    
    //Comprueba todos los Colliders dentro de _detecteds[]; si el tag es de 'Tornillo'
    //Mueve el objeto hacia el 'Player' y muestra un mensaje por consola 
    public void Recolection()
    {
        // Recoleting Tornillos to the player position
        foreach(Collider c in _collidersDetected)
        {
            if(c.gameObject.tag == GameConstants.TORNILLO_TAG) c.gameObject.GetComponent<NutsAndBolts>().Mode = 2;
        }
    }

    
    
    void Update()
    {
        rotatorEngine = GameObject.Find("Rotator").transform;
        rotatorEngine.Rotate(new Vector3(0f,rotationSpeedSpheres,0f)*Time.deltaTime*10);

        
        groundedPlayer = controller.isGrounded;
        _collidersDetected = Physics.OverlapSphere(transform.position, 3f);

        if(_collidersDetected.Length > 0)
        {
            Recolection();
        } 

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            animator.SetBool("jumping", true);
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        if(Input.GetKeyDown(KeyCode.Mouse0)){
            sphereController.Disparar();
        }
        if(Input.GetKeyDown(KeyCode.Tab)){
            sphereController.CambiarModo();
        }
        if(pet != null){
            if(Input.GetKeyDown(KeyCode.Mouse1)){
               Pet petControl = pet.GetComponent<Pet>();
               petControl.StopControlingEnemy();
            }
        }
        if(collectedObject != null){
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
              if (collectedObject.tag == GameConstants.MOVABLE_OBJECTS_TAG)
                {
                    MovableObjects movableObject = collectedObject.GetComponent<MovableObjects>();
                    movableObject.stopControlingObject();
                }
                else if (collectedObject.tag == GameConstants.CRYSTAL_TAG)
                {
                    Crystal crystal = collectedObject.GetComponent<Crystal>();
                    crystal.StopControlingObject();
                }
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (collectedObject.tag == GameConstants.MOVABLE_OBJECTS_TAG)
                {
                    MovableObjects movableObject = collectedObject.GetComponent<MovableObjects>();
                    movableObject.shootControlledObject();
                }
            }
        }

        if (groundedPlayer && playerVelocity.y < 0)
        {
            animator.SetBool("jumping", false);
            playerVelocity.y = -1f;                        
        }
        // camara 
        followGameObject.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationPowerCam, Vector3.up);

        #region Vertical Rotation
        followGameObject.transform.rotation *= Quaternion.AngleAxis(-Input.GetAxis("Mouse Y") * rotationPowerCam, Vector3.right);

        var angles = followGameObject.transform.localEulerAngles;
        angles.z = 0;

        var angle = followGameObject.transform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < 280)
        {
            angles.x = 280;
        }
        else if(angle < 180 && angle > 40)
        {
            angles.x = 40;
        }

        followGameObject.transform.localEulerAngles = angles;
        #endregion

        //gets controller direction.
        // Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        nextRotation = Quaternion.Lerp(followGameObject.transform.rotation, nextRotation, Time.deltaTime * rotationLerpCam); //guarda la posicion de giro

        if (move.x == 0 && move.z == 0) 
        {   
            nextPosition = transform.position; // cuando esta quieto no se gira
            ComprobarMovimiento(); 
            return; 
        }
        ComprobarMovimiento();

        Vector3 position = (transform.forward * move.x * 5f) + (transform.right * move.z * 5f);
        nextPosition = transform.position + position;  

        transform.rotation = Quaternion.Euler(0, followGameObject.transform.rotation.eulerAngles.y, 0);
        //reset the y rotation of the look transform
        followGameObject.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
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
