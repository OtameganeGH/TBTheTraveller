using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] movePoints;
    public GameObject targetObj, player;
    int nodeNo;
    public float moveSpeed, distanceBeforeNextNode, finalNodeDropTime;
    Vector3 currentTarget, startPos;
     bool moving, XToggle = false, YToggle = false, ZToggle = false, playerPos;
    PlayerBehaviour pb;
    PlayerMovement pm;
    void Start()
    {
        startPos = gameObject.transform.position;
        moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            if (nodeNo <= movePoints.Length -1)
            {
                targetObj = movePoints[nodeNo];
                currentTarget = targetObj.transform.position;
            }
        }
        else
        {
            currentTarget = startPos;
            nodeNo = -1;
        }

        if (currentTarget != startPos)
        {
            float interpolation = moveSpeed * Time.deltaTime;
            Vector3 position = this.transform.position;
			if (position.x < currentTarget.x - distanceBeforeNextNode || position.x > currentTarget.x + distanceBeforeNextNode)
			{
				position.x = Mathf.Lerp(this.transform.position.x, currentTarget.x, interpolation);
			}
			else
			{
                XToggle = true;
			}


            if (position.z < currentTarget.z - distanceBeforeNextNode || position.z > currentTarget.z + distanceBeforeNextNode)
            {
                position.z = Mathf.Lerp(this.transform.position.z, currentTarget.z, interpolation);
            }
            else
            {
                ZToggle = true;
            }
            if (position.y < currentTarget.y - distanceBeforeNextNode || position.y > currentTarget.y + distanceBeforeNextNode)
            {
                position.y = Mathf.Lerp(this.transform.position.y, currentTarget.y, interpolation);
            }
            else
            {
                YToggle = true;
            }
            this.transform.position = position;
        }
        if(XToggle == true && YToggle == true && ZToggle == true)
		{

            if (movePoints[nodeNo].name == "Final Node")
			{
                Debug.Log("DroppingPlayer");
                StartCoroutine(FinalNodeTimer());
                XToggle = false;
                YToggle = false;
                ZToggle = false;
            }
			else
			{
                nodeNo += 1;
                XToggle = false;
                YToggle = false;
                ZToggle = false;
            }
           
		}
		if (playerPos)
		{
            player.transform.position = (new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z));
		}
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            if (moving == false)
            {               
                pb = player.GetComponent<PlayerBehaviour>();
                pb.SetCContEnable(false);
                pm = player.GetComponent<PlayerMovement>();
                pm.canJump = false;
                pm.canMove = false;
                player.transform.parent = this.transform;
                playerPos = true;
                nodeNo = 0;
                moving = true;
            }        
          
        }
    }

    IEnumerator FinalNodeTimer()
	{
        yield return new WaitForSeconds(finalNodeDropTime);
        playerPos = false;
        pm.canJump = true;
        pm.canMove = true;
        pb.SetCContEnable(true);
        player.transform.parent = null;       
        moving = false;
        this.gameObject.transform.position = startPos;
    }

}