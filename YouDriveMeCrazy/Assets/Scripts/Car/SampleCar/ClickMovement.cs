using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMovement : MonoBehaviour
{
    public Camera camera;

    private Vector3 destination;
    private bool isMove;

    public bool leftBtn = false;
    public bool rightBtn = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1)){
            RaycastHit hit;
            if(Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                SetDestination(hit.point);
            }
        }
        Move();
    }

    void SetDestination(Vector3 dest){
        destination = dest;
        isMove = true;
    }

    void Move()
    {
        if(isMove){
            if(Vector3.Distance(destination, transform.position) <= 0.1f){
                isMove = false;
                return;
            }
            
            var dir = destination - transform.position;
            transform.position += dir.normalized * Time.deltaTime * 5f;

        }

    }
}
