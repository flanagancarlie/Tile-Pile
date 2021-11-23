using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public Camera mainCam;

    float shakeAmount = 0;

    void Awake() {

        if(mainCam == null)
            mainCam = Camera.main;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {

            Shake(0.1f, 0.2f);
        }
    }

    public void Shake(float amt, float length) {

        shakeAmount = amt;
        InvokeRepeating("BeginShake", 0, 0.01f);
        Invoke("StopShake", length);

    }

    void BeginShake() {
        if(shakeAmount > 0) {

            Vector3 camPos = mainCam.transform.position;

            float offsetx = Random.value * shakeAmount * 2 - shakeAmount;
            float offsety = Random.value * shakeAmount * 2 - shakeAmount;
            camPos.x += offsetx;
            camPos.y += offsety;

            mainCam.transform.position = camPos;
        }

    }

    void StopShake() {
        Vector3 camPos = mainCam.transform.position;

        CancelInvoke("BeginShake");
        camPos.x = 0;
        camPos.y = 0;
        mainCam.transform.position = camPos;

    }

}

// source: https://www.youtube.com/watch?v=Y8nOgEpnnXo //