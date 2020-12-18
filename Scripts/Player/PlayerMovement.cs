using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject shadow, model;
    public float movementSpeed, jumpHeight, rayCastDistance, damageKnockbackStrength, damageKnockbackTiming;
    public int airJumps;
    [SerializeField]
    private int airJumpsRemain;
    private Vector3 jumpVelcocity;
    [SerializeField]
    private bool rayGrounded, animWalking, animJumping, animAirJump;
    private Rigidbody rb;
    public bool canMove, movingForward, movingHorizontalRight, movingHorizontalLeft, movingReverse, canJump;
    private float defaultMoveSpeed;
    
   
    CharacterController controller;
    public float gravityValue;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        defaultMoveSpeed = movementSpeed;
        anim = model.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        controller = gameObject.GetComponent<CharacterController>();
        canMove = true;
        canJump = true;
        movingForward = true;
        
    }

    private void Movement()
    {
      
        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");
        Vector3 motion;

        if (canMove)
        {
            if((HorizontalInput ==1 && VerticalInput == 1) ||(HorizontalInput == -1 && VerticalInput == -1) || (HorizontalInput == 1 && VerticalInput == -1) || (HorizontalInput == -1 && VerticalInput == 1))
			{
                movementSpeed = defaultMoveSpeed *0.6f;

            }
			else
			{
                movementSpeed = defaultMoveSpeed;
			}
            if (movingForward)
            {               
                 motion = new Vector3(HorizontalInput, 0.0f, VerticalInput);
                controller.Move(motion * movementSpeed * Time.deltaTime);
            }else if (movingHorizontalRight)
			{               
                motion = new Vector3(VerticalInput*-1, 0.0f, HorizontalInput);
                controller.Move(motion * movementSpeed * Time.deltaTime);
            }
            else if(movingHorizontalLeft)
            {              
                motion = new Vector3(VerticalInput, 0.0f, HorizontalInput*-1);               
                controller.Move(motion * movementSpeed * Time.deltaTime);
            }
            else if (movingReverse)
            {               
                motion = new Vector3(HorizontalInput * -1 , 0.0f, VerticalInput * -1);
                controller.Move(motion * movementSpeed * Time.deltaTime);
            }
        }

        if(HorizontalInput != 0 || VerticalInput != 0)
		{
			if (!animWalking && !animJumping)
			{
                anim.SetBool("Jogging", true);

            }
           
		}
		else if(HorizontalInput == 0 && VerticalInput == 0)
		{
            if (animWalking)
			{
                anim.SetBool("Jogging", false);
			}
                
        }
		
  


		
      
        
    }
    
 

    void Update()
    {

        animWalking = anim.GetBool("Jogging");
        animJumping = anim.GetBool("Jumping");
        animAirJump = anim.GetBool("AirJump");
    
        Movement();
        if (rayGrounded==true)
        {
            airJumpsRemain = airJumps;
            rb.velocity = Vector3.zero;
           
        } 

        if (rayGrounded && jumpVelcocity.y < 0)
       {
           jumpVelcocity.y = 0; 
       }
       
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, rayCastDistance);
   
            if (hit.collider != null && hit.collider.tag == "Floor" )
            {
                        
                rayGrounded = true;
          
            }
            else if(hit.collider == null || hit.collider.tag != "Floor")
            {
          
                rayGrounded = false;
            }


       

        if (Input.GetKeyDown(KeyCode.Space) && canMove == true)
        {
            if (rayGrounded == true && canJump == true)
            {
				
                canJump = false;
                StartCoroutine(JumpCooldown());
                jumpVelcocity.y += Mathf.Sqrt(jumpHeight  *-3.0f * gravityValue);
                if (!animJumping)
                {
                    StartCoroutine(AnimCooldown());
                    anim.SetBool("Jumping", true);
                }

            }
             if (rayGrounded == false && airJumpsRemain > 0 && canJump == true)
            {
             
                airJumpsRemain -= 1;
                jumpVelcocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                if (!animJumping)
                {
                    StartCoroutine(AnimCooldown());
                    anim.SetBool("Jumping", true);
                }
                else if (animJumping && !animAirJump)
                {
                   
                    StartCoroutine(AnimCooldown());
                    anim.SetBool("AirJump", true);
                }
            }
        }
    
       

        if (rayGrounded == false)
        {
           
            RaycastHit shadowRay;
            
            Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out shadowRay, Mathf.Infinity);
            if(shadowRay.collider != null && shadowRay.collider.tag != "Player" && shadowRay.collider.tag != "Spikes" && shadowRay.collider.tag != "Deadly" && shadowRay.collider.tag != "IcicleTrigger" && shadowRay.collider.tag != "Icicle" && shadowRay.collider.tag != "Slow" && shadowRay.collider.tag != "ExtraLife" && shadowRay.collider.tag != "Collectable" && shadowRay.collider.tag != "Checkpoint" && shadowRay.collider.tag != "ShipPart" && shadowRay.collider.tag != "CheckTest" && shadowRay.collider.tag != "ExitLevel" && shadowRay.collider.tag != "Hat" && shadowRay.collider.tag != "FallPlane" && shadowRay.collider.tag != "Shadow")
			{
                shadow.SetActive(true);
                
                shadow.transform.position = new Vector3(shadow.transform.position.x, shadowRay.point.y + 0.05f, shadow.transform.position.z);
			}
			else if(shadowRay.collider == null)
			{
               shadow.SetActive(false);
			}
        }
        else
        {
            if (animJumping)
            {
                anim.SetBool("Jumping", false);
            }
			if (animAirJump)
			{
                anim.SetBool("AirJump", false);
            }

            shadow.SetActive(false);
        }
        jumpVelcocity.y += gravityValue * Time.deltaTime;
        controller.Move(jumpVelcocity * Time.deltaTime);
    }

    public void PushPlayer()
    {

        //knockback

        if (canMove)
        {
            if (movingForward)
            {
                jumpVelcocity.z -= Mathf.Sqrt(damageKnockbackStrength);
                jumpVelcocity.y += Mathf.Sqrt(damageKnockbackStrength / 2);
                StartCoroutine(PushBacktimer());
                canMove = false;
            }

            else if (movingHorizontalRight)
            {
                jumpVelcocity.z -= Mathf.Sqrt(damageKnockbackStrength);
                jumpVelcocity.y += Mathf.Sqrt(damageKnockbackStrength / 2);
                StartCoroutine(PushBacktimer());
                canMove = false;
            }
            else if (movingHorizontalLeft)
            {
                jumpVelcocity.z -= Mathf.Sqrt(damageKnockbackStrength);
                jumpVelcocity.y += Mathf.Sqrt(damageKnockbackStrength / 2);
                StartCoroutine(PushBacktimer());
                canMove = false;
            }
            else if (movingReverse)
            {
                jumpVelcocity.z += Mathf.Sqrt(damageKnockbackStrength);
                jumpVelcocity.y += Mathf.Sqrt(damageKnockbackStrength / 2);
                StartCoroutine(PushBacktimer());
                canMove = false;
            }
            controller.Move(jumpVelcocity * Time.deltaTime);



        }
    }
   



    IEnumerator JumpCooldown()
	{
        yield return new WaitForSeconds(0.1f);
        canJump = true;
    }
    IEnumerator AnimCooldown()
    {
        yield return new WaitForSeconds(0.5f);
       
    }
    IEnumerator PushBacktimer()
	{
        yield return new WaitForSeconds(damageKnockbackTiming);
        jumpVelcocity.x = 0;
        jumpVelcocity.y = 0;
        jumpVelcocity.z = 0;
        canMove = true;
    }
}
