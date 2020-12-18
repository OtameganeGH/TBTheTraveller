using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapdoorTrap : MonoBehaviour
{
    public GameObject trapTop;
    bool breaking;
    public float timeBeforeFall, resetTime, openSpeed, closeSpeed;
    private Vector3 rotation, defaultRotation;
    public Vector3 targetRot;
    
    // Start is called before the first frame update
    void Start()
    {
        defaultRotation = trapTop.transform.eulerAngles;
       
    }
   
    // Update is called once per frame
    void Update()
    {
        if (breaking == true)
        {
            rotation = trapTop.transform.eulerAngles;
           
                Debug.Log("Opening");
                rotation.x = Mathf.Lerp(rotation.x, targetRot.x, openSpeed * Time.deltaTime);
                rotation.y = Mathf.Lerp(rotation.y, targetRot.y, openSpeed * Time.deltaTime);
                rotation.z = Mathf.Lerp(rotation.z, targetRot.z, openSpeed * Time.deltaTime);
                trapTop.transform.eulerAngles = rotation;
			
			
        }
        else
        {
            rotation = trapTop.transform.eulerAngles;           
                Debug.Log("Closing");
                rotation.x = Mathf.Lerp(rotation.x, defaultRotation.x, closeSpeed * Time.deltaTime);
                rotation.y = Mathf.Lerp(rotation.y, defaultRotation.y, closeSpeed * Time.deltaTime);
                rotation.z = Mathf.Lerp(rotation.z, defaultRotation.z, closeSpeed * Time.deltaTime);
                trapTop.transform.eulerAngles = rotation;
            
            if (rotation != targetRot)
            {
                Debug.Log("StoppedClosing");
                breaking = false;
            }
        }
    }
   private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && breaking == false)
		{
            Debug.Log("TaggedPlayer");
            StartCoroutine(FallTimer());
            breaking = true;
		}
    }
    IEnumerator FallTimer()
    {
        yield return new WaitForSeconds(timeBeforeFall);
        //break obj       
        StartCoroutine(ResetTimer());
    }

    IEnumerator ResetTimer()
    {
        yield return new WaitForSeconds(resetTime);
        trapTop.SetActive(true);
        

    }
}
