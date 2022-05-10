using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update

    //! 이거 래퍼런스로 넣을때 hierachy말고 Prefabs에 있는거 넣어야 반복되는거다 ㅋㅋㅋㅋ 위계에 있는거 넣으면 재생산 안되자나 바보야 ㅋ
    public GameObject[] animalPrefabs;
    private float spawnRangeX = 20;
    private float spawnPosz = 20;
    private float startDelay = 2;
    private float spawnInterval = 1.5f;
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal",startDelay,spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
       
       
    }
     void SpawnRandomAnimal(){
            int animalIndex = Random.Range(0,animalPrefabs.Length);
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX,spawnRangeX),0,spawnPosz);
            Instantiate(animalPrefabs[animalIndex],spawnPos, animalPrefabs[animalIndex].transform.rotation);

        }
}
