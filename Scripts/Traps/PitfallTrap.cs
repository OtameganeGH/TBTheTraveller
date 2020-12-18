using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PitfallTrap : MonoBehaviour
{
    public GameObject trapTop;
    bool breaking;
    public float timeBeforeFall, resetTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && breaking == false)
		{
            StartCoroutine(FallTimer());
            breaking = true;
		}
    }



    IEnumerator FallTimer()
	{
        yield return new WaitForSeconds(timeBeforeFall);
        //break obj
        trapTop.SetActive(false);
        StartCoroutine(ResetTimer());
    }

    IEnumerator ResetTimer() { 
        yield return new WaitForSeconds(resetTime);
        trapTop.SetActive(true);
        breaking = false;

    }

}
