using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowTrail : MonoBehaviour{

    public float trailDelay = 0.04f;
    private float trailDelayTime;
    public GameObject dashTrail;
    public bool makeTrail = false;

    void Start(){
        trailDelayTime = trailDelay;
    }


    void Update(){
        if (makeTrail){
            if (trailDelayTime > 0){
                trailDelayTime -= Time.deltaTime;
            }else{
                GameObject currentTrail = Instantiate(dashTrail, transform.position, transform.rotation);
                trailDelayTime = trailDelay;
                Destroy(currentTrail, 0.3f);
            }
        }
    }

}
