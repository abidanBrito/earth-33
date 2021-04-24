using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerVFX : BaseGame
{
        void Update()
    {
        transform.position = playerTargetPosition;
    }
}
