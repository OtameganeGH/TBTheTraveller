using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodySpin : MonoBehaviour
{
    public float turnSmoothingTime;
    private float turnVeliocity, angleOffset;
    private bool movingForward, movingHorizontalRight, movingHorizontalLeft, movingReverse;
    PlayerMovement pMove;
    // Start is called before the first frame update
    void Start()
    {
        pMove = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        movingForward = pMove.movingForward;
        movingHorizontalLeft = pMove.movingHorizontalLeft;
        movingHorizontalRight = pMove.movingHorizontalRight;
        movingReverse = pMove.movingReverse;

        if (movingForward)
		{
            angleOffset = 0;
		}
        else if (movingHorizontalRight)
		{
            angleOffset = 90;
		}
        else if (movingHorizontalLeft)
		{
            angleOffset = -90;
        }else if (movingReverse)
		{
            angleOffset = 180;
		}

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if(direction.magnitude >= 0.1f)
		{
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle - angleOffset, ref turnVeliocity, turnSmoothingTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
		}
    }
}
