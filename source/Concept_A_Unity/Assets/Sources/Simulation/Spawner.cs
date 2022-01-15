using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject target;
    public int count = 1;
    bool isDestroySelf = true;


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(target, this.gameObject.transform.position, Quaternion.identity);
        if (isDestroySelf) { Object.Destroy(this.gameObject); }
    }
}
