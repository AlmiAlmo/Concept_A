using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBehaviour : MonoBehaviour
{
    public Vector3 target;
    public float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        target = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        Debug.Log(step);
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }
}
