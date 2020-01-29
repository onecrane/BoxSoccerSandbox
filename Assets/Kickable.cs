using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kickable : MonoBehaviour
{
    public float kickFactor = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Task: Kick the ball harder based on the car's current speed.
        if (collision.gameObject.name == "Car")
        {
            GetComponent<Rigidbody>().AddForceAtPosition(collision.gameObject.transform.forward * collision.gameObject.GetComponent<DriveControls>().currentSpeed * kickFactor, collision.contacts[0].point);
        }
    }

}
