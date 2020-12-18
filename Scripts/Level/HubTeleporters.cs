using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HubTeleporters : MonoBehaviour
{
    // Start is called before the first frame update
    public int LevelBuildNo;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
        if(other.tag == "Player")
		{
            SceneManager.LoadScene(LevelBuildNo);
		}
	}

    }
