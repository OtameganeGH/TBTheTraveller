using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditorInternal;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public bool isTriggered = false, spikesAtMaxHeight, raiseSpikes, lowerSpikes, autoSpike;
    public float spikeDelay, spikeSpeed, spikeHeight, waitTime, autoInitialSpikeDelayTime;
    public GameObject spikeObject;
    Vector3 spikePos;

    // Start is called before the first frame update
    void Start()
    {
         spikePos = spikeObject.transform.position;

        if(autoSpike == true)
            StartCoroutine(AutoSpikeStartDelay());
    }

    // Update is called once per frame
    void Update()
    {

        if (raiseSpikes == true && SpikeCloseToHeight(new Vector3(spikePos.x, spikePos.y + spikeHeight, spikePos.z)))
        {
            spikesAtMaxHeight = true;
        }
        if (lowerSpikes == true && autoSpike == true && SpikeCloseToHeight(new Vector3(spikePos.x, spikePos.y, spikePos.z)))
        {
            DeploySpikes();
        }


        if (SpikeCloseToHeight(spikePos) && lowerSpikes == true)
        {
            isTriggered = false;
            lowerSpikes = false;
        }

        if (spikesAtMaxHeight == true)
        {
            StartCoroutine(SpikeLowerTimer());
        }

        if (raiseSpikes == true)
        {
            RaiseSpikes();
        }
        if (lowerSpikes == true)
        {
            LowerSpikes();
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isTriggered == false && autoSpike == false)
        {
           // Debug.Log("Hit");
            DeploySpikes();
        }
    }


	
    void DeploySpikes()
	{
      //  Debug.Log("Deploy the spikes");
        isTriggered = true;
        StartCoroutine(SpikeRaiseTimer());
	}
    void RaiseSpikes()
	{
        float interpolation = spikeSpeed * Time.deltaTime;
        Vector3 position = spikeObject.transform.position;
        Vector3 newPos = new Vector3(spikePos.x, spikePos.y + spikeHeight, spikePos.z);
       // Debug.Log("Spikes Going Up");
        position.y = Mathf.Lerp(spikeObject.transform.position.y, newPos.y, interpolation);
        spikeObject.transform.position = position;

    }

    void LowerSpikes()
	{
        float interpolation = spikeSpeed * Time.deltaTime;
        Vector3 position = spikeObject.transform.position;
       // Debug.Log("Spikes Going Down");
        position.y = Mathf.Lerp(spikeObject.transform.position.y, spikePos.y, interpolation);
        spikeObject.transform.position = position;
    }

    IEnumerator SpikeRaiseTimer()
    {
        yield return new WaitForSeconds(spikeDelay);
        raiseSpikes = true;
    }

    IEnumerator SpikeLowerTimer()
	{
        spikesAtMaxHeight = false;
        raiseSpikes = false;
        yield return new WaitForSeconds(waitTime);
        lowerSpikes = true;

    }
    bool SpikeCloseToHeight(Vector3 position)
	{
        if (spikeObject.transform.position.y <= (position.y + 0.05f) && spikeObject.transform.position.y >= (position.y - 0.05f) )
        {
            return true;
        }
        else return false;
	}
    IEnumerator AutoSpikeStartDelay()
    {
        yield return new WaitForSeconds(autoInitialSpikeDelayTime);
        DeploySpikes();
    }



    }
