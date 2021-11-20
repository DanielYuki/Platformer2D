using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour{

    private PlatformEffector2D effector;
    public float waitTime;

    void Start(){
        effector = GetComponent<PlatformEffector2D>();
    }

    void Update(){

        if (Input.GetAxisRaw("Vertical") < 0){
            if (waitTime <= 0){
                effector.rotationalOffset = 180f;
            }else{
                waitTime -= Time.deltaTime;
            }
        }else{
            effector.rotationalOffset = 0f;
            waitTime = 0.5f;
        }
    }
}
