using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    private Rigidbody2D rb;
    public Vector2 moveInput;

    public float moveSpeed;

    public Transform groundCheck;
    public bool onGround;
    public LayerMask isGround;
    public float checkRadius = 0.3f;

    public float jumpForce;
    public bool isJumping = false;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        onGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, isGround);

        if(onGround && Input.GetButtonDown("Jump")){
            isJumping = true;
        }
    }

    void FixedUpdate(){
        Move();
        
        if (isJumping){
            Jump();
        }
    }

    void Move(){
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }

    void Jump(){
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isJumping = false;
    }

}