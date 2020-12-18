using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingAxeTrap : MonoBehaviour
{
    public float initialVelocity, leftSwingMaxAngle, rightSwingMaxAngle;
    public bool xRotation, zRotation;
    private bool goingLeft, goingRight;
    Vector3 rotSpeed;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        if (xRotation)
        {
            rotSpeed = new Vector3(initialVelocity, 0, 0);
        }else if (zRotation)
		{
            rotSpeed = new Vector3(0, 0, initialVelocity);
        }
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = rotSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.rotation.x);
        if (xRotation)
        {
          
            if (transform.rotation.x > rightSwingMaxAngle)
            {
               // Debug.Log("switching to left");
                rb.angularVelocity = rotSpeed *-1;
            }
             if (transform.rotation.x < leftSwingMaxAngle)
            {
              //  Debug.Log("switching to right");
                rb.angularVelocity = rotSpeed;
            }
        }
        if (zRotation)
        {
            if (transform.rotation.z > rightSwingMaxAngle)
            {
                rb.angularVelocity = rotSpeed * -1;
            }
            if (transform.rotation.z < leftSwingMaxAngle)
            {
                rb.angularVelocity = rotSpeed;
            }
        }
    }
}
