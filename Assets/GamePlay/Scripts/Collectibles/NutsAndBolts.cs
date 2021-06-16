using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutsAndBolts : BaseGame
{
    // For Load and Save Status
    private DataSaveLoad saveLoad;
    private InventoryStatus inventoryStatus;
    private const string KEYNAME_NUTS_AND_BOLTS = "inventory_status";

    [SerializeField] private float speed = 6f;
    [SerializeField] private float rotationSpeed = 10f;
    private Vector3 targetPlayer;
    private Vector3 towardsTarget;
    private Vector3 targetPosition;
    private int mode = 0;
    public int Mode
    {
        get => mode;
        set => mode = value;
    }
    
    void Start()
    {
        // For Load and Save Status
        inventoryStatus = new InventoryStatus();
        saveLoad = new DataSaveLoad();

        if (gameObject.tag == GameConstants.APARECER_TORNILLO_TAG)
        {
            var rg = gameObject.AddComponent<Rigidbody>();
            rg.useGravity = false;
            targetPosition = transform.position + Random.insideUnitSphere * 2f;
            transform.position= new Vector3(targetPosition.x,  transform.position.y,targetPosition.z);
            mode = 1;
        }
    }

    void Update()
    {
        if(mode == 1 || mode == 2)
        {
            buscarPunto();
        }

        // For Load and Save Status
        if (GameManager.Instance.ItsTimeToSave())
        {
            SaveStatus();
        }
        
    }

    //Si se detecta un collider, se comprueba el tag; si es tipo 'Player',
    //se elimina el Tornillo (ha sido recolectado)
    public void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == GameConstants.PLAYER_TAG)
        {
            mode = 2;
        }
    }

    //Si aparece por muerte de enemigo, buscará un punto aleatorio
    //Si detecta un 'Player', ira a por él
    //Si el tornillo ha llegado al punto aleatorio, se convierte al tag 'Tornillo'
    public void buscarPunto()
    {
        Vector3 positionPlayerHead = GameObject.Find("Neck").transform.position;
        
        if(mode == 1){
             towardsTarget = targetPosition - transform.position;
            smoothMovement(transform, towardsTarget, speed, rotationSpeed);

        }
        else if(mode == 2){
            targetPlayer = positionPlayerHead - transform.position;
            smoothMovement(transform, targetPlayer, speed, rotationSpeed);

            if(targetPlayer.magnitude < 0.5f){
                Inventory.nutsQuantity++;
                GameManager.Instance.Nuts += 1;
                Destroy(gameObject);
            }
        }

        if(mode == 1 && towardsTarget.magnitude < 0.1f) mode = 0; 
        gameObject.tag = GameConstants.TORNILLO_TAG;
    }

    // For Save Status
    public void SaveStatus()
    {
        inventoryStatus.nutsQuantity = Inventory.nutsQuantity;
        saveLoad.Save(KEYNAME_NUTS_AND_BOLTS, inventoryStatus);
        Debug.Log(KEYNAME_NUTS_AND_BOLTS + " guardado");
    }
}