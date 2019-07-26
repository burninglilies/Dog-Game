using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    
    public float fowardForce = 325f;
    float time = 0;
    bool running;
    public string dog;
    float timer = 0;
    
    // Applies a force to move the object foward in the z direction
    public void run() 
    {
        if (running == true)
        {
            GetComponent<Rigidbody>().AddForce(0, 0, fowardForce * Time.deltaTime);
        }
        time += Time.deltaTime;
        if (time >= 60)
        {
            running = false;
            System.Console.WriteLine(time);
        }
    }
    // Destroys object after it hits a Trigger
    void OnTriggerEnter(Collider collisionInfo)
    {
        Destroy(gameObject);
        print("Time: " + time);
    }

    void Start()
    {
        running = true;
        print("Running");
    }
    // Allows you to only jump every 3 seconds
    void TimerCap()
    {
        timer += Time.deltaTime;

    }

    void Update()
    {
        run();
        TimerCap();

        if (Input.GetKey("d"))
        {
            GetComponent<Rigidbody>().AddForce(200 * Time.deltaTime, 0, 0);
        }
        if(Input.GetKey("a"))
        {
            GetComponent<Rigidbody>().AddForce(-200 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("w") && timer >= 2)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 200 * Time.deltaTime, 0), ForceMode.Impulse);
            timer = 0;
        }
        if (Input.GetKey("s"))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0,-1, 0), ForceMode.Impulse);
        }
        if (GetComponent<Rigidbody>().position.y < -5f)
        {
            gameObject.transform.position.Set(0,0,0);
            time = 0;
        }
    }
}
