using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public GameObject player;
    private Transform playerTransform;
   
    [SerializeField] float offset;
    [SerializeField] float lerpSpeed;

    IEnumerator camShake;
    Vector3 originalPos;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    void Start()
    {
        player = GameObject.Find("Player");

        if(player != null)
        {
            playerTransform = player.transform;
        }
    }


    void LateUpdate()
    {
        FollowPlayer();
    }


    void FollowPlayer()
    {
        if(playerTransform != null)
        {
            transform.position = Vector3.Lerp(transform.position, playerTransform.position * offset, lerpSpeed * Time.deltaTime);
        }
    }


    public void StartCameraShake (float dur, float amount, float speed)
    {
        if (camShake != null)
        {
            StopCoroutine(camShake);
            Camera.main.transform.localPosition = originalPos;
        }

        camShake = Shake(dur, amount, speed);
        StartCoroutine(camShake);
    }


    IEnumerator Shake(float dur, float amount, float speed)
    {
        float t = dur;
        Transform cam = Camera.main.transform;
        originalPos = cam.localPosition;
        Vector3 targetPos = Vector3.zero;

        while(t > 0)
        {
            if(targetPos == Vector3.zero)
            {
                targetPos = originalPos + (Vector3)(Random.insideUnitCircle * Time.deltaTime);
            }

            cam.localPosition = Vector3.Lerp(cam.localPosition, targetPos, speed * Time.deltaTime);

            if (Vector2.Distance(cam.localPosition, targetPos) <= 0.02f)
            {
                targetPos = Vector3.zero;
            }

            t -= Time.deltaTime;
            yield return null;
        }

        cam.localPosition = originalPos;
    }
}
