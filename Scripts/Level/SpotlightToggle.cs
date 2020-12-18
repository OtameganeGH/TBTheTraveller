using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightToggle : MonoBehaviour
{
    PlayerBehaviour pBev;
	PlayerMovement pMove;
	GameObject player;
    public GameObject worldLight;
	public bool toggleCantJump;
	// Start is called before the first frame update
	private void Start()
	{
        player = GameObject.Find("Player");
		pBev = player.GetComponent<PlayerBehaviour>();
		pMove = player.GetComponent<PlayerMovement>();
		worldLight = GameObject.Find("Directional Light");
	}

	// Update is called once per frame

	private void OnTriggerEnter(Collider other)
	{
        if(other.tag == "Player"&& pBev.spotlightOn == false)
		{          
            pBev.spotLight.SetActive(true);
            worldLight.SetActive(false);
			pBev.spotlightOn = true;
			if(toggleCantJump)
			pMove.canJump = false;
			//toggle door?
		}
		else if(other.tag == "Player" && pBev.spotlightOn == true)
		{
            pBev.spotLight.SetActive(false);
            worldLight.SetActive(true);
            pBev.spotlightOn = false;
			if(toggleCantJump)
			pMove.canJump = true;
		}
    }

    

}
