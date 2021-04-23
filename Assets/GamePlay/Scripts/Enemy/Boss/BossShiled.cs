using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShiled : BaseGame
{
    public Transform pilarsController;
    private List<GameObject> crystals = new List<GameObject>();
    void Start()
    {
        crystals = pilarsController.GetComponent<PilarsController>().crystals;
    }

    void Update()
    {
        if(crystals.Count == 3)
        {
            gameObject.transform.localScale = new Vector3(6f, 6f, 6f);
        }
        else if(crystals.Count == 2)
        {
            gameObject.transform.localScale = new Vector3(4f, 4f, 4f);

        }
        else if(crystals.Count == 1)
        {
            gameObject.transform.localScale = new Vector3(3f, 3f, 3f);
        }
        else if(crystals.Count == 0)
        {
            Destroy(gameObject);
        }
    }
}
