using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurdle : MonoBehaviour
{
    Rigidbody hurdleRB;

    // Start is called before the first frame update
    void Start()
    {
        hurdleRB = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Dog")
        {
            hurdleRB.AddForceAtPosition(new Vector3(10,0,70), transform.position + new Vector3(0, 1f, 0));
        }
    }
}
