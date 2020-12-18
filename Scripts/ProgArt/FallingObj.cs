using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class FallingObj : MonoBehaviour
{

    public float xDirection, yDirection, zDirection;
    public Rigidbody body;
    public bool objMovingPositive, objMovingOnX, objMovingOnY, objMovingOnZ;
    public GameObject fallOffMarker, startingMarker;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
      
           body.velocity = new Vector3(xDirection, yDirection, zDirection);
       
        if (objMovingOnX)
        {
            FallingX();
        }
        if (objMovingOnY)
        {
            FallingY();
        }
        if (objMovingOnZ)
        {
            FallingZ();
        }
    }

    void FallingX()
	{
        if (objMovingPositive == false && this.transform.position.x < fallOffMarker.transform.position.x)
		{
            ReturnPosition();
		}else if (objMovingPositive == true && this.transform.position.x > fallOffMarker.transform.position.x)
		{
            ReturnPosition();
        }
	}
    void FallingY()
	{
        if (objMovingPositive == false && this.transform.position.y < fallOffMarker.transform.position.y)
        {
            ReturnPosition();
        }
        else if (objMovingPositive == true && this.transform.position.y > fallOffMarker.transform.position.y)
        {
            ReturnPosition();
        }
    }
	void FallingZ()
	{
        if (objMovingPositive == false && this.transform.position.z < fallOffMarker.transform.position.z)
        {
            ReturnPosition();
        }
        else if (objMovingPositive == true && this.transform.position.z > fallOffMarker.transform.position.z)
        {
            ReturnPosition();
        }
    }
    
    void ReturnPosition()
	{
        this.transform.position = startingMarker.transform.position;
    }
  




}
