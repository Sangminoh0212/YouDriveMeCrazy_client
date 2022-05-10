using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceSpawnManager : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        Instantiate(prefab);
        print("스폰 선 밟았음");
    }
}
