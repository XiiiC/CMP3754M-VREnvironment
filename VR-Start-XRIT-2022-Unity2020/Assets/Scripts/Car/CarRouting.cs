using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRouting : MonoBehaviour
{
    public List<Transform> wps;
    public List<Transform> route;
    public int routeNumber = 0;
    public int targetWP;
    public float dist;
    public bool go = false;
    public bool brake = false;
    public float initialDelay;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialDelay = Random.Range(8.0f, 20.0f);
        transform.position = new Vector3(0.0f, -5.0f, 0.0f);

        wps = new List<Transform>();
        GameObject wp;

        wp = GameObject.Find("carWP1");
        wps.Add(wp.transform);

        wp = GameObject.Find("carWP2");
        wps.Add(wp.transform);

        wp = GameObject.Find("carWP3");
        wps.Add(wp.transform);

        wp = GameObject.Find("carWP4");
        wps.Add(wp.transform);

        wp = GameObject.Find("carWP5");
        wps.Add(wp.transform);

        wp = GameObject.Find("carWP6");
        wps.Add(wp.transform);

        wp = GameObject.Find("carWP7");
        wps.Add(wp.transform);

        wp = GameObject.Find("carWP8");
        wps.Add(wp.transform);

        wp = GameObject.Find("carWP9");
        wps.Add(wp.transform);

        wp = GameObject.Find("carWP10");
        wps.Add(wp.transform);

        SetRoute();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!go)
        {
            initialDelay -= Time.deltaTime;
            if (initialDelay <= 0.0f)
            {
                go = true;
                SetRoute();
            }
            else return;
        }

        Vector3 displacement = route[targetWP].position - transform.position;
        displacement.y = 0;
        float dist = displacement.magnitude;

        if (dist < 0.1f)
        {
            targetWP++;
            if (targetWP >= route.Count)
            {
                SetRoute();
                return;
            }
        }

        if(!brake)
        {
            //calculate velocity for this frame
            Vector3 velocity = displacement;
            velocity.Normalize();
            velocity *= 10f;
            //apply velocity

            Vector3 newPosition = transform.position;
            newPosition += velocity * Time.deltaTime;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.MovePosition(newPosition);
        
            //align to velocity
            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, velocity, 10.0f * Time.deltaTime, 0f);
            Quaternion rotation = Quaternion.LookRotation(desiredForward);
            rb.MoveRotation(rotation);
        }
        
    }

    void SetRoute()
    {
        routeNumber = Random.Range(0, 5);
        if (routeNumber == 0) route = new List<Transform> { wps[0], wps[1], wps[2]};
        else if(routeNumber == 1) route = new List<Transform> { wps[0], wps[1], wps[3]};
        else if (routeNumber == 2) route = new List<Transform> { wps[5], wps[9], wps[2]};
        else if (routeNumber == 3) route = new List<Transform> { wps[5], wps[9], wps[4]};
        else if (routeNumber == 4) route = new List<Transform> { wps[6], wps[8], wps[3]};
        else if (routeNumber == 5) route = new List<Transform> { wps[6], wps[7],wps[4]};

        transform.position = new Vector3(route[0].position.x, 0.5f, route[0].position.z);
        targetWP = 1;
    }
}
