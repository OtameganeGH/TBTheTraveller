using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSpin : MonoBehaviour
{
    public float spinSpeed;
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    { 
  
        transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
    }
}

