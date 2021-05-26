using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollezionabileCrystal : GameManager
{
    // RESUMEN: 

    // Después de solucionar este error, debo de crear una variable que mantenga
    // la cantidad de piezas de cristales obtenidos.
    //
    // Finalmente, veré como afecta al jugador el que haya recolectado todos los
    // cristaales repartidos por el mapa.

    GameObject crystal;
    public MeshFilter meshFilter;
    public Mesh crystalFull;
    public Mesh crystalBroken1;
    public Mesh crystalBroken2;
    public Mesh crystalCollect;
    private float cooldownTrhowObjects = 1.5f;
    public bool canUseTrhowObjects = true;
    public BoxCollider m_Collider;


    void Start() 
    {
       meshFilter = gameObject.GetComponent<MeshFilter>();
       m_Collider = gameObject.GetComponent<BoxCollider>();
    }

    void Update() 
    {
        if(canUseTrhowObjects == false) TimerCooldownTrhowObjects();
    }

    public void OnTriggerEnter(Collider other) 
    {
        // Cuando el objeto recibe un impacto de la esfera del jugador
        // cambia el Prefav dependiaendo del estado del coleccionable
        if(canUseTrhowObjects == true && other.gameObject.tag == GameConstants.PLAYER_SPHERE)
        {
            if (meshFilter.sharedMesh == crystalFull) meshFilter.sharedMesh = crystalBroken1;
            else if (meshFilter.sharedMesh == crystalBroken1) meshFilter.sharedMesh = crystalBroken2;
            else if (meshFilter.sharedMesh == crystalBroken2) meshFilter.sharedMesh = crystalCollect;
            else 
            {
                //Ruido de obtenido
                Crystals = Crystals + 1;
                Destroy(gameObject);
            } 
            
            canUseTrhowObjects = false;

            // Adapta el tamaño de los cristales
            transform.localScale = new Vector3(0.38f, 0.38f, 0.38f);
            m_Collider.size = new Vector3(5f, 10f, 4f);
        }       
    }

    private void TimerCooldownTrhowObjects()
    {   
       if(cooldownTrhowObjects >= 0)
        {
            cooldownTrhowObjects = cooldownTrhowObjects-Time.deltaTime;
        }
        else
        {
            canUseTrhowObjects = true;
            cooldownTrhowObjects = 0;
        }
    }
}
