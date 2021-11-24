using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDash : MonoBehaviour{

    public Animator anim;
    public PlayerMovement mov;
 
    private GameMaster gm;

    void Start(){
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        
        if (gm.dashUnlockTriggered){
            this.gameObject.SetActive(false);
            mov.canDash = true;
            
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")){
            anim.SetTrigger("ShowTxt");
            this.gameObject.SetActive(false);
            mov.canDash = true;
            gm.dashUnlockTriggered = true;
        }
    }
}
