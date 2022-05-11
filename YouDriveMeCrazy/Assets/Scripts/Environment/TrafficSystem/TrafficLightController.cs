using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightController : MonoBehaviour
{

    [Header("Traffic Light")]
    [SerializeField] private GameObject redLight;
    [SerializeField] private GameObject yellowLight;
    [SerializeField] private GameObject greenLight;
    

    [Header("Cycle Time")]
    [SerializeField] private float redLightTime = 10f;
    [SerializeField] private float yellowLightTime = 5f;
    [SerializeField] private float greenLightTime = 20f;

    private BoxCollider sensingArea;

    private bool isRedLight = false;
    private bool isWorking = true;

    private float timer;
    private float totalTime;

    void Start()
    {
        sensingArea = GetComponent<BoxCollider>();
        sensingArea.enabled = false;

        redLight.SetActive(false);
        yellowLight.SetActive(false);
        greenLight.SetActive(false);

        totalTime = redLightTime + yellowLightTime + greenLightTime;

        StartCoroutine(Timer());
    }


    // by 상민, If car cross the reference line on a red light, colliding is detected    
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Car" && isRedLight){
            isWorking = false;
            GameManager.Instance.GameOver();
        }
    }

    // by 상민, red,yellow,greenLightTime에 따라 신호등 동작, green->yellow->red 순으로 바뀜 
    IEnumerator Timer(){
        timer = 0f;

        //turn on green light
        redLight.SetActive(false);
        greenLight.SetActive(true);
        isRedLight = false;
        sensingArea.enabled = false;

        while(timer < totalTime){
            timer += Time.deltaTime;
            
            if(timer > greenLightTime && timer < greenLightTime+yellowLightTime) {
                // turn on yellow light
                greenLight.SetActive(false);
                yellowLight.SetActive(true);
            }
            else if (timer > greenLightTime+yellowLightTime){
                //turn on red light
                yellowLight.SetActive(false);
                redLight.SetActive(true);
                isRedLight = true;
                sensingArea.enabled = true;
            }

            yield return null;  
        }
        if(isWorking) StartCoroutine(Timer());
    }


}
