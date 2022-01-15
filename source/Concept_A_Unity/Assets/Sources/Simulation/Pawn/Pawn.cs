using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : InterractableObject
{
    public MovingBehaviour movingBehaviour { get; private set; }

    void Awake()
    {
        movingBehaviour = this.gameObject.GetComponent<MovingBehaviour>();
    }
}
