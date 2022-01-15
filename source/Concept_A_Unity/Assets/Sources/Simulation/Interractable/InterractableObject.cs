using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterractableObject : MonoBehaviour
{
    public SelectionBehaviour selection { get; private set; }

    private void Awake()
    {
        selection = this.gameObject.GetComponent<SelectionBehaviour>();
    }
}