using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionController : MonoBehaviour
{

    public CarRouting car;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {   
        car.brake = true;
        
    }
    void OnTriggerExit(Collider col)
    {
        car.brake = false;
        
    }
}