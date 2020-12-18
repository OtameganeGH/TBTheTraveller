using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    GameObject lastCheck, canvas;
    public GameObject player, hat, spotLight;
    Vector3 checkPos;
    [SerializeField]
    private string checkname;
    public int playerHealth, playerHealthStartingAmount;
    public CharacterController cont;
    PlayerMovement pMove;
    public bool hasHat, spotlightOn;
    
    // Start is called before the first frame update
    void Start()
    {
        cont = gameObject.GetComponent<CharacterController>();
        pMove = gameObject.GetComponent<PlayerMovement>();
        
        canvas = GameObject.Find("Canvas");
        playerHealth = playerHealthStartingAmount;
        spotlightOn = false;
        hasHat = false;
        lastCheck = gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (lastCheck)
        checkname = lastCheck.name;

        if(hasHat == true)
		{
            hat.SetActive(true);
		}
		else
		{
            hat.SetActive(false);
		}
    }
    private void OnTriggerEnter(Collider other)
	{
        if(other.tag == "Checkpoint")
		{           
            lastCheck = other.gameObject;
            checkPos = lastCheck.transform.position;           
        }
        if (other.tag == "CheckTest")
        {
            ReturnToLastCheckpoint();
        }
        if (other.tag == "ExitLevel")
        {
            Debug.Log("Exit");
            StartCoroutine(WaitBeforeExit());
        }

 
        if( other.tag == "Deadly")
		{
            if(hasHat == false)
			{
                canvas.GetComponent<InGameUI>().ReduceLives();
                ReturnToLastCheckpoint();
			}else if(hasHat == true)
			{
                hasHat = false;
                pMove.PushPlayer();
			}
		}

       
    if (other.tag == "FallPlane")
        {
            if (hasHat == false)
            {
                canvas.GetComponent<InGameUI>().ReduceLives();
                ReturnToLastCheckpoint();
            }
            else if (hasHat == true)
            {
                hasHat = false;
                ReturnToLastCheckpoint();
            }
        }


    }
	private void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Deadly")
		{
			if (hasHat)
			{
               // pMove.PushPlayer();
                hasHat = false;
			}
			else
			{
                canvas.GetComponent<InGameUI>().ReduceLives();
                ReturnToLastCheckpoint();
            }

		}
	}
   

	public void ReturnToLastCheckpoint()
	{
        Debug.Log("Return to Check");      
        cont.enabled = false;
        player.transform.position = checkPos;
        cont.enabled = true;
    }
    public void SetCContEnable(bool set)
	{
        cont.enabled = set;
           
        
    }

    IEnumerator WaitBeforeExit()
	{
        canvas.GetComponent<InGameUI>().SaveHighScore();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);
    }

}
