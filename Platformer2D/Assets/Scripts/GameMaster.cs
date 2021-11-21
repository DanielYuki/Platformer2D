using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour{

    private static GameMaster instance;
    public Vector2 lastCheckPoint;

    void Awake() {
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(this);
        }else{
            Destroy(gameObject);
        }
    }
}
