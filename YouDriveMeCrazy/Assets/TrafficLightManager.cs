using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightManager : MonoBehaviour
{

    [Header("Traffic Light")]
    [SerializeField] private GameObject redLight;
    [SerializeField] private GameObject yellowLight;
    [SerializeField] private GameObject greenLight;
    

    [Header("Cycle Time")]
    [SerializeField] private float redLightTime = 0.5f;
    [SerializeField] private float yellowLightTime = 3f;
    [SerializeField] private float greenLightTime = 3f;

    private bool isRedLight = false;
    private bool isYellowLight = false;
    private bool isTrafficLightWork = true;

    private int testNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        redLight.SetActive(false);
        yellowLight.SetActive(false);
        greenLight.SetActive(false);

        StartCoroutine(Timer(redLightTime));
    }

    private void TrafficLightCycle(){
        if(isTrafficLightWork){
            print("green light on");
            //StartCoroutine(Timer(greenLightTime));
            print("yellow light on");
            //StartCoroutine(Timer(yellowLightTime));
            print("red light on");
            //StartCoroutine(Timer(redLightTime));
        }
    }

    // If car cross the reference line on a red light, colliding is detected    
    private void OnTriggerEnter(Collider other) {
        if(other!=null && redLight){
            //print(other.gameObject.name);
        }
    }

    IEnumerator Timer(float time){
        
        var waitTime = new WaitForSeconds(time);
        yield return waitTime;
        testNum += 1;
        print(testNum);
        yield return waitTime;
        testNum += 3;
        print(testNum);
        StartCoroutine(Timer(redLightTime));
    }



    /*
    IEnumerator RedLight()
    {
        print("red on");
        yellowLight.SetActive(false);
        redLight.SetActive(true);
        yield return new WaitForSeconds(redLightTime);
        isRedLight = false;
    }


    IEnumerator YellowLight()
    {
        print("yellow on");
        redLight.SetActive(false);
        greenLight.SetActive(true);
        yield return new WaitForSeconds(greenLightTime);
        isYellowLight = false;
        isRedLight = true;


    }


    IEnumerator GreenLight(){
        print("green on");
        redLight.SetActive(false);
        greenLight.SetActive(true);
        yield return new WaitForSeconds(greenLightTime);
        isYellowLight = true;
        print("green on2");
    }
    */

    /*
    IEnumerator TrafficLightCycle(){
        while(true){
            redLight.SetActive(false);
            greenLight.SetActive(true);
            isRedLight = false;
            print("green on");
            yield return new WaitForSeconds(greenLightTime);

            print("yellow on");
            greenLight.SetActive(false);
            yellowLight.SetActive(true);
            yield return new WaitForSeconds(yellowLightTime);

            print("red on");
            yellowLight.SetActive(false);
            redLight.SetActive(true);
            isRedLight = true;
            yield return new WaitForSeconds(redLightTime);
        }
    }
    */


    // greenTime=20초 후 ->  yellowLight.SetActive(true) && greenLight.SetActive(false); 
    // yellowTime=5초 후 -> redLight.SetActive(true) && yellowLight.SetActive(False); 
    // redTime=10초 후 -> greenLight.SetActive(true) && redLight.SetActive(False); 


}
