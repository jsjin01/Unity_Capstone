using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    private Vector3 originalPosition; //original position of camera
    private float shakeMagnitude = 100.0f; 
    private float shakeTime = 1.5f; //time of shaking
    private float shakeTimer; //time of continuous shaking

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }


    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if(shakeTimer > 0) //if shaking effect is activated
        {
            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude; //change local position of camera
            shakeTimer -= Time.deltaTime;
        }
        else //if shaking effect is not activated
        {
            shakeTimer = 0f;
            transform.localPosition = originalPosition;
        }
    }

    public void StartShake()
    {
        shakeTimer = shakeTime;
    }
}
