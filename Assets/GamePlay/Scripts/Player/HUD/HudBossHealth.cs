using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudBossHealth : BaseGame
{
    private float MaxHit = 100f;
    private float currentHits = 100f;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)){
            currentHits -=5f;
            transform.localScale = new Vector3(currentHits/MaxHit,1f,1f);
        }
        
    }
}
