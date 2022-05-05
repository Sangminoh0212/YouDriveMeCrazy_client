using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    // Start is called before the first frame update
    public float boundary = 30.0f;
    public float lowerbound = -10.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z > boundary){
            Destroy(gameObject);
        }else if(transform.position.z < lowerbound){
            Destroy(gameObject);
        }
        
    }
}
