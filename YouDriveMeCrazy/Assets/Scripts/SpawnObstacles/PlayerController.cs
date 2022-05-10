using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    private float xboundary = -10.0f;
    public float speed = 10.0f;
    public GameObject prefabObjects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right*Time.deltaTime*speed*horizontalInput);
        if(transform.position.x <xboundary){
            transform.position = new Vector3(xboundary,transform.position.y,transform.position.z);
        }
        if(transform.position.x > -xboundary){

 transform.position = new Vector3(-xboundary,transform.position.y,transform.position.z);
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            Instantiate(prefabObjects, transform.position, prefabObjects.transform.rotation);
        }
    }

}
