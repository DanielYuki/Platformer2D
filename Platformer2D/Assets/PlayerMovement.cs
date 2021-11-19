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

    private bool canMove = true;
    private bool faceRight = true;

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

    [Header("Wall Jump")]
    public Transform wallCheck;
    public Vector2 wallJumpDirection;
    private bool onWall;
    private bool wallSliding;
    public float wallCheckRadius = 0.3f;
    public float wallSlidingSpeed = 1.5f;
    public float wallJumpForce;
    public float wallJumpTime;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        moveInput.x = Input.GetAxisRaw("Horizontal");   //Directional Inputs
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (!faceRight && moveInput.x > 0){
            Flip();
        }else if (faceRight && moveInput.x < 0){
            Flip();
        }

        //onGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, isGround);
        onGround = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, isGround);

        onWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, isGround);

        if (onGround){
            coyoteTimeCounter = coyoteTime;
        }else{
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (onWall && !onGround && rb.velocity.y < 0 && moveInput != Vector2.zero){
            wallSliding = true;
        }else{
            wallSliding = false;
        }

        if (Input.GetButtonDown("Jump")){
            jumpBufferTimeCounter = jumpBufferTime;
        }else{
            jumpBufferTimeCounter -= Time.deltaTime;
        }

        if(jumpBufferTimeCounter > 0f && (onGround || onWall || coyoteTimeCounter > 0f)){
            isJumping = true;
            coyoteTimeCounter = 0;
            jumpBufferTimeCounter = 0;
        }
    }

    void FixedUpdate(){
        if (canMove){
            Move();
        }

        Jump();

        if (wallSliding && rb.velocity.y < -wallSlidingSpeed){
            rb.velocity = new Vector2(rb.velocity.x , -wallSlidingSpeed);
        }

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

    void Flip(){
        faceRight = !faceRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void Jump(){
        if (isJumping && !wallSliding && onGround){
            rb.velocity = new Vector2(rb.velocity.x , 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }else if (isJumping && wallSliding){
            Vector2 wjForce = new Vector2(wallJumpForce * wallJumpDirection.x * -moveInput.x, wallJumpForce * wallJumpDirection.y);
        
            rb.velocity = Vector2.zero;
            rb.AddForce(wjForce, ForceMode2D.Impulse);
            StartCoroutine("StopMove");
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

    IEnumerator StopMove(){
        canMove = false;

        yield return new WaitForSeconds(wallJumpTime);

        canMove = true;
    }

}