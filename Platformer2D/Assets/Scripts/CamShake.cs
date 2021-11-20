using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamShake : MonoBehaviour{

    private CinemachineVirtualCamera cam;
    private float shakeTimer;

    private void Awake() {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCam(float intensity, float time){
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void Update() {
        if (shakeTimer > 0f){
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f){
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                    cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }

}
