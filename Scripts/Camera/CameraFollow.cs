using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player, cam, camTargetobj;
    private Transform defaultCameraRotation;
    Quaternion defaultRotation;
    public Transform target, newCameraRotation;
    public float smoothing, fastcam, pauseDistance, reverseDistance;
    public bool camRotating = false, camReturning = false;
    public bool camXToggle, camYToggle, camZToggle, OnPlayer;
    private bool DXToggle, DYToggle, DZToggle;
    Vector3 camTarget;
	
	private void Start()
	{
        OnPlayer = true;
        // target = player.transform;      
        player = GameObject.Find("Player");
        camTargetobj = GameObject.Find("CamFollow");
        defaultCameraRotation = cam.transform;
        defaultRotation = defaultCameraRotation.rotation;
        DXToggle = camXToggle;
        DYToggle = camYToggle;
        DZToggle = camZToggle;
       

    }
	void Update()
    {
       
  //      if (target.tag == "Player")
  //      {
  //          if (this.transform.position.z >= target.position.z + reverseDistance)
  //          {
  //              MoveCam(fastcam);
  //          }
  //          else if (target.position.z >= this.transform.position.z)
  //          {
  //              MoveCam(smoothing);
  //          }
		//}
		//else {
            MoveCam(smoothing);
      //  }


        if (camRotating == true && camReturning == false)
        {
            RotateCam();
        }else if(camRotating == false && camReturning == true)
		{
            ReturnCam();
		}
       
    }




    void MoveCam(float smoothtime)
	{

        if(camXToggle == true)
		{
            if (OnPlayer == true)
            {
                camTarget.x = camTargetobj.transform.position.x;
			}
			else
			{
                camTarget.x = player.transform.position.x;
            }
		}
		else
		{
            if (OnPlayer != true)
            {
                camTarget.x = target.transform.position.x;
            }
			else
			{
                 camTarget.x = camTargetobj.transform.position.x;
            }
		}


        if (camYToggle == true)
        {
           // camTarget.y = camTargetobj.transform.position.y;
            if (OnPlayer == true)
            {
                camTarget.y = camTargetobj.transform.position.y;
            }
            else
            {
                camTarget.y = player.transform.position.y;
            }
        }
        else
        {
            // camTarget.y = target.position.y;
            if (OnPlayer != true)
            {
                camTarget.y = target.transform.position.y;
            }
            else
            {
                camTarget.y = camTargetobj.transform.position.y;
            }
        }

        if (camZToggle == true)
        {
            if (OnPlayer == true)
            {
                camTarget.z = camTargetobj.transform.position.z;
            }
            else
            {
                camTarget.z = player.transform.position.z;
            }
        }
        else
        {
            if (OnPlayer != true)
            {
                camTarget.z = target.transform.position.z;
            }
            else
            {
                camTarget.z = camTargetobj.transform.position.z;
            }
        }


        float interpolation = smoothtime * Time.deltaTime;
        Vector3 position = this.transform.position;

        //if (camXToggle == true && position.x < camTarget.x - 0.2f || position.x > camTarget.x + 0.2f)
        //    position.x = Mathf.Lerp(this.transform.position.x, camTarget.x, interpolation);
        //if (camZToggle == true && position.z < camTarget.z - 0.2f || position.z > camTarget.z + 0.2f)
        //    position.z = Mathf.Lerp(this.transform.position.z, camTarget.z, interpolation);
        //if (camYToggle == true && position.y < camTarget.y - 0.2f || position.y > camTarget.y + 0.2f)
        //    position.y = Mathf.Lerp(this.transform.position.y, camTarget.y +1, interpolation);


        if (position.x < camTarget.x - 0.2f || position.x > camTarget.x + 0.2f)
            position.x = Mathf.Lerp(this.transform.position.x, camTarget.x, interpolation);
        if (position.z < camTarget.z - 0.2f || position.z > camTarget.z + 0.2f)
            position.z = Mathf.Lerp(this.transform.position.z, camTarget.z, interpolation);
        if (position.y < camTarget.y - 0.2f || position.y > camTarget.y + 0.2f)
            position.y = Mathf.Lerp(this.transform.position.y, camTarget.y, interpolation);

        this.transform.position = position;
    }
    public void SetTarget(Transform newTarget)
	{
        target = newTarget;
	}
    public void SetCameraRotation(Transform newCamRotation)
	{
        newCameraRotation = newCamRotation;
        camRotating = true;
        camReturning = false;
      //  Debug.Log(cam.transform.rotation + " / " + defaultCameraRotation.rotation);
    }
    void RotateCam()
	{ 
     

        float camInterpolation = 4 * Time.deltaTime;
        cam.transform.rotation = Quaternion.Slerp(defaultCameraRotation.rotation, newCameraRotation.rotation, camInterpolation);      

    }
    void ReturnCam()
	{
        float camInterpolation = 4 * Time.deltaTime;
        cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, defaultRotation, camInterpolation);
        
    }
    
    public void StartCamReturn()
	{
        camRotating = false;
        camReturning = true;
        camXToggle = DXToggle;
        camYToggle = DYToggle;
        camZToggle = DZToggle;
        // Debug.Log(cam.transform.rotation + " / " + defaultCameraRotation.rotation);

    }
}

