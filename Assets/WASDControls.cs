using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDControls : MonoBehaviour
{

    // Speed of the car in units/s
    [Range(0, 100)]
    public float maxSpeed = 20f;
    private float currentSpeed = 0f;

    public float accelerationRate = 20;



    // Rotation rate in unknown units (possibly degrees/s?)
    [Range(10, 200)]
    public float turnRate = 10;


    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //            transform.position += transform.forward * speed * Time.fixedDeltaTime;
            currentSpeed += accelerationRate * Time.fixedDeltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            //transform.position -= transform.forward * speed * Time.fixedDeltaTime;
            currentSpeed -= accelerationRate * Time.fixedDeltaTime;
        }

        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);

        GetComponent<Rigidbody>().velocity = transform.forward * currentSpeed;



        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(transform.up, -turnRate * Time.fixedDeltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(transform.up, turnRate * Time.fixedDeltaTime);
        }


    }


}
