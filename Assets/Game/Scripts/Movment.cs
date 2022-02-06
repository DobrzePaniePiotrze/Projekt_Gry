using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    bool SpaceWasPressed; // checking if space was pressed
    public Vector2 jumpHeight; // how high we want to jump
    private  float HorizontalInput; // Move during jump
    private Rigidbody2D Rigidbody2Dcomponent; // body
    private Animator anim;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask wallLayerMask;
    private bool grounded;
    private BoxCollider2D boxCollider2d;
    private float wallJumpCooldown;

    // Reference
    void Start()
    {
        Rigidbody2Dcomponent = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider2d =  transform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
         // check if space was pressed jumping 
        if(Input.GetKeyDown(KeyCode.Space))
        {
           SpaceWasPressed = true; 
        }

        // END

        HorizontalInput = Input.GetAxis("Horizontal"); // Get Horizontal Direction
         
         // look towards direction of movment
         if (HorizontalInput > 0.01F)
         {
             transform.localScale = Vector3.one;  
        } else if (HorizontalInput < -0.01F)
        {
            transform.localScale = new Vector3(-1,1,1);  
        }

        // END
        
        //Set Animation
        anim.SetBool("walk", HorizontalInput !=0);
        anim.SetBool("grounded", IsGrounded() );

        
        }

     private void FixedUpdate()
    {
        // walljumps
         if (wallJumpCooldown > 0.2f)
         {

            // Walk 
            Rigidbody2Dcomponent.velocity = new Vector2(HorizontalInput*5, Rigidbody2Dcomponent.velocity.y);

           
            if(onWall() && !IsGrounded())
            { 
                  Rigidbody2Dcomponent.gravityScale = 0;
                  Rigidbody2Dcomponent.velocity = Vector2.zero;

            }else
            Rigidbody2Dcomponent.gravityScale = 3;
            
             // Jump
            if(SpaceWasPressed)
            {
                Jump();
            
             SpaceWasPressed = false;
            }

         }else
         wallJumpCooldown += Time.deltaTime;
       
    }   

    private void Jump()
    {
        if(IsGrounded())
        {
            Rigidbody2Dcomponent.AddForce(jumpHeight * 5, ForceMode2D.Impulse); 
            anim.SetTrigger("jump");
        }else if(onWall() && IsGrounded())
        {
            if(HorizontalInput == 0)
            {
                Rigidbody2Dcomponent.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*10,0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x)*3,transform.localScale.y, transform.localScale.z);
            }else
                Rigidbody2Dcomponent.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*3,6);

            wallJumpCooldown = 0;
            
        }
       
        

    }

    private bool IsGrounded()  
     {

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0, Vector2.down, 0.5f , groundLayerMask);

        return raycastHit.collider != null;
     
     }
    
     private bool onWall() 
     {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.5f , wallLayerMask);

        return raycastHit.collider != null;
     }

       
       public bool canAttack(){
           return HorizontalInput == 0 && IsGrounded() && !onWall();
       }
 }

