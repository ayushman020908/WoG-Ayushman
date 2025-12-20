using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour
{
    public float WalkSpeed=5f;
    public float RunSpeed=8f;

    private Rigidbody2D rb;
    private Vector2 moveinput;
    private Animator animator;

    public float dashDuration=0.2f;
    public float dashCooldown=0.5f;

    private bool IsDashing = false;
    private float dashTimer =0f;
    private float dashTimeLeft = 0f;  
    private Vector2 dashDirection;
     
    /*float minX = -50f;
    float maxX = 50f;
    float minY = -30f;
    float maxY = 30f;*/
    // Start is called before the first frame update

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame

    public void OnMove(InputAction.CallbackContext context)
    {

        moveinput=context.ReadValue<Vector2>();

    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        dash();
    }



    void dash()
    {  
        if (IsDashing)
            return;
        if (dashTimer>0f)
            return;

        if(moveinput.sqrMagnitude>0.0001f)
        {
            IsDashing=true;
            dashTimeLeft = dashDuration;
            dashTimer=dashCooldown;
            dashDirection= moveinput.normalized;
        }

    }

    void Update()
    {
        if (!IsDashing)
        {
            rb.velocity=moveinput*WalkSpeed;

            bool IsMoving=moveinput.sqrMagnitude>0.01f;
            animator.SetBool("IsMoving",IsMoving);

            if (IsMoving)
            {
                animator.SetFloat("X",moveinput.x);
                animator.SetFloat("Y",moveinput.y);
            }
        }
        /*bool IsRunning=Keyboard.current.leftShiftKey.isPressed;
        //animator.SetBool("IsRunning",IsRunning);
        if (IsRunning)
        {
            rb.velocity=moveinput*RunSpeed;
        }*/


    }
    void FixedUpdate()
{
    if (IsDashing)
    {
        rb.velocity = dashDirection * RunSpeed;
        dashTimeLeft -= Time.fixedDeltaTime;

        if (dashTimeLeft <= 0f)
        {
            IsDashing = false;
            dashTimer = dashCooldown; 
        }
    }
    else
    {
        rb.velocity = moveinput * WalkSpeed;

        if (dashTimer > 0f)
            dashTimer -= Time.fixedDeltaTime;
    }
}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("box"))
        {
            Debug.Log("Collided has started");
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("box"))
        {
            Debug.Log("Collided is continuing");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("box"))
        {
            Debug.Log("Collided has ended");
        }
    }
}


