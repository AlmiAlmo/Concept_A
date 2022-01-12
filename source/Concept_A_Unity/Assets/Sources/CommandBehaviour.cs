using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandBehaviour : MonoBehaviour
{
    SelectionBehaviour selection;
    MovingBehaviour moving;

    // Start is called before the first frame update
    void Start()
    {
        selection = this.gameObject.GetComponent<SelectionBehaviour>();
        moving = this.gameObject.GetComponent<MovingBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector3 movPos)
    {
        moving.target = movPos;
    }

    public void BeginCommand()
    {
        selection.SetSelected(true);
    }

    public void EndCommand()
    {
        selection.SetSelected(false);
    }
}

