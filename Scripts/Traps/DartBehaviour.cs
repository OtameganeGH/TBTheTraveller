using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartBehaviour : MonoBehaviour
{

    private bool canFire, timerOn;
    //Editable values to indicate direction and cutoff points
    public bool movingX, movingY, movingZ, movingPositive;
    public float xDirection, yDirection, zDirection, delayTimer;
    public GameObject fallOffMarker, startingMarker;

    private Rigidbody body;
   

    void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
        canFire = true;
        timerOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(canFire == true)
		{
            body.velocity = new Vector3(xDirection, yDirection, zDirection);
		}
		else
		{
            body.velocity = new Vector3(0, 0, 0);
		}
        if (movingX)
        {
            FallingX();
        }
        if (movingY)
        {
            FallingY();
        }
        if (movingZ)
        {
            FallingZ();
        }

    }
    void FallingX()
    {
        if (movingPositive == false && this.transform.position.x < fallOffMarker.transform.position.x)
        {
            ReturnPosition();
        }
        else if (movingPositive == true && this.transform.position.x > fallOffMarker.transform.position.x)
        {
            ReturnPosition();
        }
    }
    void FallingY()
    {
        if (movingPositive == false && this.transform.position.y < fallOffMarker.transform.position.y)
        {
            ReturnPosition();
        }
        else if (movingPositive == true && this.transform.position.y > fallOffMarker.transform.position.y)
        {
            ReturnPosition();
        }
    }
    void FallingZ()
    {
        if (movingPositive == false && this.transform.position.z < fallOffMarker.transform.position.z)
        {
            ReturnPosition();
        }
        else if (movingPositive == true && this.transform.position.z > fallOffMarker.transform.position.z)
        {
            ReturnPosition();
        }
    }

    void ReturnPosition()
    {
        this.transform.position = startingMarker.transform.position;
		if (timerOn == false)
		{
         
          
            StartCoroutine(WaitTime());
            canFire = false;
            timerOn = true;


        }
    }

    IEnumerator WaitTime()
	{
       // Debug.Log("Pausing");
        
        yield return new WaitForSeconds(delayTimer);
       // Debug.Log("TimerOver");
        canFire = true;
        timerOn = false;
    }
}
