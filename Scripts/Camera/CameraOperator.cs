using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraOperator : MonoBehaviour
{

    public bool swapCam, returnCam, playerForwardMovement, playerHorizontalRightMovement,playerHorizontalLeftMovement, playerReverseMovement;
    public GameObject newCamTarget, cam;
    CameraFollow cameraController;
    private GameObject player;
    public bool camXFollow, camYFollow, camZFollow;
    
  
    void Start()
    {
        cam = GameObject.Find("DynamicCam");
        cameraController = cam.GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            ChangeCam();
            player = null;
            
        }
    }

    void ChangeCam()
    {
        PlayerMovement pMove = player.GetComponent<PlayerMovement>();
		if (playerForwardMovement)
		{
            pMove.movingForward = true;
            pMove.movingHorizontalLeft = false;
            pMove.movingHorizontalRight = false;
            pMove.movingReverse = false;
		}
        else if (playerHorizontalRightMovement)
        {
            pMove.movingForward = false;
            pMove.movingHorizontalLeft = false;
            pMove.movingHorizontalRight = true;
            pMove.movingReverse = false;
        }
        else if (playerHorizontalLeftMovement)
        {
            pMove.movingForward = false;
            pMove.movingHorizontalLeft = true;
            pMove.movingHorizontalRight = false;
            pMove.movingReverse = false;
        }
        else if (playerReverseMovement)
        {
            pMove.movingForward = false;
            pMove.movingHorizontalLeft = false;
            pMove.movingHorizontalRight = false;
            pMove.movingReverse = true;
        }
        //SENDS THE CAMERA TO DESIGNATED LOCATION AND SETS ROTATION 
        if (swapCam == true && returnCam == false)
        {
            cameraController.OnPlayer = false;
            cameraController.SetTarget(newCamTarget.transform);           
            cameraController.SetCameraRotation(newCamTarget.transform);
            cameraController.camXToggle = camXFollow;
            cameraController.camYToggle = camYFollow;
            cameraController.camZToggle = camZFollow;

        }

        //RETURNS CAMERA TO PREVIOUS LOCATION AND ROTATION 
        else if (returnCam == true && swapCam == false)
        {
            cameraController.OnPlayer = true;
            cameraController.SetTarget(cameraController.target);
            cameraController.StartCamReturn(); 
        }
		else
		{
            Debug.Log("You either haven't selected what this trigger is for or haven't setup the new cam position for obj named: " + gameObject.name);
		}
    }
}
	
