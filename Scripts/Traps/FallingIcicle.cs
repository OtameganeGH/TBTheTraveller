using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class FallingIcicle : MonoBehaviour
{
    public GameObject icicle;
    Rigidbody rb;
    private bool triggered = false, resetTimer = false;
    public float fallSpeed, fallDelay, resetTime;
    Vector3 icicleInitialLocation;
    // Start is called before the first frame update
    void Start()
    {
        rb = icicle.GetComponent<Rigidbody>();
        icicleInitialLocation = icicle.transform.position;
    }

   
    void Update()
    {
        if(triggered == true)
		{
            rb.velocity= new Vector3(0, fallSpeed, 0);          
        }
        if (resetTimer == true)
        {

            StartCoroutine(IcicleResetTimer());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Track if player moves under ice
        if (other.tag == "Player" && triggered == false)
        {
           // Debug.Log("Triggered Ice");
            StartCoroutine(IcicleDropDelay());          
        }    
    }

     private void OnTriggerExit(Collider other)
	{
        //Icicle reset
        if (other.tag == "Icicle" && triggered == true)
        {
           // Debug.Log("reset ice");
            resetTimer = true;
        }
    }



	IEnumerator IcicleDropDelay()
	{
        yield return new WaitForSeconds(fallDelay);
        rb.isKinematic = false;
        triggered = true;
    }
    IEnumerator IcicleResetTimer()
	{
        //Debug.Log("Reset timer Go");
        resetTimer = false;      
        rb.velocity = new Vector3(0,0,0);
        rb.isKinematic =true;
       // Debug.Log(rb.velocity);
       yield return new WaitForSeconds(resetTime);       
        icicle.transform.position = icicleInitialLocation;        
        triggered = false;
    }
}
