using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Collectables : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject canvas;
    public int collectableValue;
    void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
	{
        if(other.tag == "Player" && gameObject.tag=="Collectable")
		{
            canvas.GetComponent<InGameUI>().IncreaseCollectables(collectableValue);
            GameObject.Destroy(gameObject);
		}else if (other.tag == "Player" && gameObject.tag == "ExtraLife")
		{
            canvas.GetComponent<InGameUI>().IncreaseLives();
            GameObject.Destroy(gameObject);
        }
        else if (other.tag == "Player" && gameObject.tag == "ShipPart")
        {
            canvas.GetComponent<InGameUI>().currentSceneShipPartCollected = true;
            canvas.GetComponent<InGameUI>().SaveHighScore();
            StartCoroutine(DelayForSave());
            canvas.GetComponent<InGameUI>().currentSceneShipPartCollected = false;
            SceneManager.LoadScene(0);        
        }
        else if (other.tag == "Player" && gameObject.tag == "Hat")
        {
            if (other.GetComponent<PlayerBehaviour>().hasHat == false)
            {                
                other.GetComponent<PlayerBehaviour>().hasHat = true;
			}
			else if(other.GetComponent<PlayerBehaviour>().hasHat == true)
			{
                canvas.GetComponent<InGameUI>().IncreaseLives();
            }
            GameObject.Destroy(gameObject);
        }


    }

	IEnumerator DelayForSave() 
    {
        yield return new WaitForSeconds(1f);
    }

}
