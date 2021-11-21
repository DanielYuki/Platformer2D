using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHandler : MonoBehaviour{

    public PlayerParticles particles;
    public PlayerMovement mov;
    public Animator anim;
    public float deathTime;

    private GameMaster gm;

    void Start(){
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        
        transform.position = gm.lastCheckPoint;
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.CompareTag("Death")){
            Death();
        }
    }

    void Death(){
        StartCoroutine("DeathTimer");
        mov.canMove = false;
        mov.enabled = false;
    }

    IEnumerator DeathTimer(){
        particles.DeathPS();
        //anim.SetTrigger("Death");
        anim.SetBool("Dead",true);
        yield return new WaitForSeconds(deathTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
