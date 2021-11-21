using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour{

    public ParticleSystem dust, wallDust, wallJumpDust, landingDust, dashDust, death;
    public bool spawnLandingDust = false;

    public void CreateDust(){
        dust.Play();
    }

    public void CreateWallDust(){
        wallDust.Play();
    }

    public void CreateWallJumpDust(){
        wallJumpDust.Play();
    }

    public void CreateLandingDust(){
        landingDust.Play();
    }

    public void CreateDashDust(){
        dashDust.Play();
    }

    public void DeathPS(){
        death.Play();
    }
}
