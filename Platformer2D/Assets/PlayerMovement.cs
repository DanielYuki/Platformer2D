using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    private Rigidbody2D rb;
    public Vector2 moveInput;

    [Header("Movement")]
    public float moveSpeed;
    public float acceleration;
    public float deceleration;
    public float velocityPower;
    public float friction;

    [Header("Jump Check")]
    public Transform groundCheck;
    public LayerMask isGround;
    //public float checkRadius = 0.3f;
    public Vector2 groundCheckSize;
    private bool onGround;

    [Header("Jump Properties")]
    public float jumpForce;
    private bool isJumping = false;
    public float fullHop = 2f;
    public float shortHop = 10f;
    
    public float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    public float jumpBufferTime = 0.2f;
    private float jumpBufferTimeCounter;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        moveInput.x = Input.GetAxisRaw("Horizontal");   //Directional Inputs
        moveInput.y = Input.GetAxisRaw("Vertical");

        //onGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, isGround);
        onGround = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, isGround);

        if (onGround){
            coyoteTimeCounter = coyoteTime;
        }else{
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump")){
            jumpBufferTimeCounter = jumpBufferTime;
        }else{
            jumpBufferTimeCounter -= Time.deltaTime;
        }

        if(jumpBufferTimeCounter > 0f && (onGround || coyoteTimeCounter > 0f)){
            isJumping = true;
            coyoteTimeCounter = 0;
            jumpBufferTimeCounter = 0;
        }
    }

    void FixedUpdate(){
        Move();
        Jump();

        //apply friction if grounded
        if (onGround && Mathf.Abs(moveInput.x) < 0.01f){
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(friction));
            amount *= Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right * -amount , ForceMode2D.Impulse);
        }
    }

    void Move(){
        float desiredVelocity = moveInput.x * moveSpeed;
        float speedDif = desiredVelocity - rb.velocity.x;

        float accelRate = (Mathf.Abs(desiredVelocity) > 0.01f) ? acceleration : deceleration;  //set accelerationRate to acceleration or deceleration
        //acceleration increases with higher speeds
        float moveForce = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velocityPower) * Mathf.Sign(speedDif);

        rb.AddForce(moveForce * Vector2.right);
    }

    void Jump(){
        if (isJumping){
            rb.velocity = new Vector2(rb.velocity.x , 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        isJumping = false;

        FastFall();
    }

    void FastFall(){
        if (rb.velocity.y < 0){
            rb.velocity += Vector2.up * Physics2D.gravity.y * fullHop * Time.deltaTime;
        }else if (rb.velocity.y > 0 && !Input.GetButton("Jump")){
            rb.velocity += Vector2.up * Physics2D.gravity.y * shortHop * Time.deltaTime;
        }
    }

}