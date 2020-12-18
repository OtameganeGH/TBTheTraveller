using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class FallingPillar : MonoBehaviour
{
    public float fallTime, fallAngle, raiseTime, timeBeforeReturn, timeBeforeFall, autoResetInitailDelayTime;
    public bool IsMoving = false, GoingUp = false, willReset, autoReset;
    public GameObject pillar, hitBox, pillar2;
    Vector3 currentRotation, defaultRotation, targetRotation;
    // Start is called before the first frame update
    void Start()
    {
        defaultRotation = pillar.transform.eulerAngles;
        targetRotation = new Vector3(currentRotation.x, currentRotation.y, fallAngle);

        if (autoReset == true)
            StartCoroutine(AutoPillarStartDelay());
    }

    // Update is called once per frame
    void Update()
    {
        currentRotation = pillar.transform.eulerAngles;
       
        if (autoReset == false && currentRotation.z > targetRotation.z - 3)
        {
          
            hitBox.SetActive(false);
            pillar2.tag  = "Floor";
            
        }
        if (IsMoving == true && GoingUp == false)
        {
                      
            currentRotation.z = Mathf.Lerp(currentRotation.z, targetRotation.z, fallTime * Time.deltaTime);
            pillar.transform.eulerAngles = currentRotation;
          
			
        }
        else if (IsMoving == true && GoingUp == true)
        {
               
            currentRotation.z = Mathf.Lerp(currentRotation.z, defaultRotation.z, raiseTime * Time.deltaTime);        
            pillar.transform.eulerAngles = currentRotation;
        }

        if (IsMoving == true && GoingUp == true && pillar.transform.eulerAngles.z <= defaultRotation.z + 0.1 && pillar.transform.eulerAngles.z >= defaultRotation.z - 0.1)
        {
            if (autoReset == true)
            {
                IsMoving = false;
                StartCoroutine(FallDelay());
            }
            else
            {
                IsMoving = false;
            }
        }
        else if (IsMoving == true && GoingUp == false&& pillar.transform.eulerAngles.z <= fallAngle + 0.1 && pillar.transform.eulerAngles.z >= fallAngle - 0.1)
        {
            if (willReset == true)
            {
                StartCoroutine(PillarRaiseTimer());
            } else
			{
                IsMoving = false;
			}
          
            

        }

     


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && IsMoving == false)
            StartCoroutine(FallDelay());
    }

    void MakePillarFall()
    {
       // Debug.Log("Falling");
        IsMoving = true;
        GoingUp = false;
    }
    IEnumerator PillarRaiseTimer()
    {
        yield return new WaitForSeconds(timeBeforeReturn);
        GoingUp = true;
       // Debug.Log("Raise");
    }
    IEnumerator FallDelay()
    {
       
        yield return new WaitForSeconds(timeBeforeFall);
        MakePillarFall();
    }

    IEnumerator AutoPillarStartDelay()
    {
        yield return new WaitForSeconds(autoResetInitailDelayTime);
        StartCoroutine(FallDelay());
    }
    }
