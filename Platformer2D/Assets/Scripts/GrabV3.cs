using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabV3 : MonoBehaviour{

    public Transform holdPos;
    public Rigidbody2D rb;
    public bool isGrabbing;
    public Vector2 throwDirection;
    public float throwForce;
    public float multiplier;

    public bool bonk = false;
    public LayerMask isGround;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        holdPos = GameObject.FindGameObjectWithTag("Hold").GetComponent<Transform>();
    }

    void Update() {
        if(isGrabbing){
            Grabbing();
        }

        if(isGrabbing && Input.GetKeyUp(KeyCode.P)){
            Throwing();
            isGrabbing = false;
        }

        bonk = Physics2D.OverlapBox(transform.position, new Vector2(0.75f,0.75f),0, isGround);

        if (bonk){
            isGrabbing = false;
            //rb.isKinematic = false;
            rb.gravityScale = 1.5f;
        }
    }

    void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.P)){
            isGrabbing = true;
            Debug.Log("grab");
        }
    }

    void Grabbing(){
        transform.position = holdPos.position;
        rb.gravityScale = 0;
        //rb.isKinematic = true;
    }

    void Throwing(){
        //rb.isKinematic = false;
        rb.gravityScale = 1.25f;
        throwDirection.x = Input.GetAxisRaw("Horizontal");
        throwDirection.y = Input.GetAxisRaw("Vertical");

        if (throwDirection != Vector2.zero){
            Debug.Log("yeet");
            //rb.AddForce(throwDirection.normalized * throwForce, ForceMode2D.Impulse);
            rb.velocity = new Vector2(throwDirection.x,throwDirection.y) *throwForce;
        }else{
            Debug.Log("saas");
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireCube(transform.position, new Vector3(0.75f, 0.75f, 1));
    }

}
