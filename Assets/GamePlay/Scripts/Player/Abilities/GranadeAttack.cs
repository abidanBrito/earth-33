using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeAttack : BaseGame
{
    private EnergyBall sphere;
    public float damage = 3.5f;
    public float radius = 5F;
    public float power = 600.0F;
    private Vector3 towardTarget;
    private float speed = 4f;
    private float rotationSpeed = 10f;
    private Vector3[] points;
    private int count = 0;
    private Vector3 nextPoint;
    private Vector3 startTarget;
    private Transform parent;
    private float time = 1.5f;

    void Start()
    {
        
        sphere = GetComponent<EnergyBall>();
        sphere.movements = 3;
        startTarget = transform.position;
        points = parabolicMovement(startTarget, hitGranade);
        nextPoint = points[0];
        parent = gameObject.transform.parent; 
        gameObject.transform.parent = null;
    }

    void Update()
    {   
        time = time + Time.deltaTime;
        towardTarget = nextPoint - transform.position;
        smoothMovement(transform, towardTarget, speed * time, rotationSpeed);

        if(towardTarget.magnitude < 0.1f)
        {
            if(count < 3)
            {
                count++;
                nextPoint = points[count]; 
            } else 
            {
                GameObject explosionPrefab = sphere.explosionVFX;
                ExplosionVFX(explosionPrefab);
                ExplosionAttack(transform, radius, damage, power);
                gameObject.transform.parent = parent;
                sphere.movements = -1;
                Destroy(GetComponent<GranadeAttack>());
            }
        }
    }

    public Vector3[] parabolicMovement(Vector3 startingPos, Vector3 arrivingPos)
    {
        int framesNum = (int)(2f * 2f);
        Vector3[] frames = new Vector3[framesNum];

        //PROJECTING ON Z AXIS
        Vector3 stP = new Vector3(0,startingPos.y,startingPos.z);
        Vector3 arP = new Vector3(0,arrivingPos.y,arrivingPos.z);

        Vector3 diff = new Vector3();

        Vector3 height = new Vector3(0,1,0);
        diff = ((arP-stP)/2) + height;
        Vector3 vertex = stP+diff;

        float x1 = startingPos.z;
        float y1 = startingPos.y;
        float x2 = arrivingPos.z;
        float y2 = arrivingPos.y;
        float x3 = vertex.z;
        float y3 = vertex.y;

        float denom = (x1 - x2)*(x1 - x3)*(x2 - x3);

        var z_dist = (arrivingPos.z - startingPos.z) / framesNum;
        var x_dist = (arrivingPos.x - startingPos.x) / framesNum;

        float A = (x3 * (y2 - y1) + x2 * (y1 - y3) + x1 * (y3 - y2)) / denom;
        float B = (float)(System.Math.Pow(x3, 2) * (y1 - y2) + System.Math.Pow(x2, 2) * (y3 - y1) + System.Math.Pow(x1, 2) * (y2 - y3)) / denom;
        float C = (x2 * x3 * (x2 - x3) * y1 + x3 * x1 * (x3 - x1) * y2 + x1 * x2 * (x1 - x2) * y3) / denom;

        float newX = startingPos.z;
        float newZ = startingPos.x;

        for(int i = 0; i < framesNum; i++)
        {
            newX += z_dist;
            newZ += x_dist;
            float yToBeFound = A*(newX*newX)+ B*newX + C;
            frames[i] = new Vector3(newZ, yToBeFound, newX);
        }
        
        return frames;
    }

    void OnDrawGizmos() 
    {
        Gizmos.color = Color.yellow;
        Vector3[] points = parabolicMovement(startTarget, hitGranade);

        foreach(Vector3 point in points)
        {
            //Gizmos.DrawSphere( point, .1f);
        }
    }
}