using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadowBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject shadow;
    private float shadowRayCastDistance;
    [SerializeField]
    private bool onGround;
    void Start()
    {
        shadowRayCastDistance = gameObject.GetComponent<PlayerMovement>().rayCastDistance;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit groundCheck;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out groundCheck, shadowRayCastDistance))
        {
            if ((groundCheck.collider.tag != "Floor")&& (groundCheck.collider.tag != "Player") && (groundCheck.collider.tag != "Shadow"))
            {
                Debug.Log("Off Ground");
                onGround = false;
            }
        }
        else
        {
            Debug.Log("OnGround");
            onGround = true;
        }
        RaycastHit shadowRay;
        Vector3 shadowPos;

        if (onGround == false)
		{
            shadow.SetActive(true);
            Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out shadowRay, 2000f);
            shadowPos = shadowRay.collider.transform.position;
            shadow.transform.position = new Vector3(shadow.transform.position.x, shadowPos.y +0.1f, shadow.transform.position.z);
		}
		else
		{
            shadow.SetActive(false);
		}
    }
}
