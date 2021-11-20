using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour{

    private PlayerMovement mov;
    private Vector3 launchDirection;
    public float JPadNormalForce = 35f;
    public float JPadJumpForce = 45f;

    private void Start() {
        mov = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update() {
        launchDirection = 
            new Vector3(Mathf.Sin(-(Mathf.Deg2Rad * transform.rotation.eulerAngles.z)),Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.z), 0);
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "Player"){

            mov.rb.velocity = Vector2.zero;

            mov.dashJump = true;
            mov.dashCount = 2;

            if (mov.jumpBufferTimeCounter > 0){
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(launchDirection * JPadJumpForce, ForceMode2D.Impulse );
                //Debug.Log("jp");
            }else{
                //Debug.Log("oki");
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(launchDirection * JPadNormalForce, ForceMode2D.Impulse );
            }
        }
    }
}
