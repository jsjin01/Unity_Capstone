using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl i;
    Transform player;

    [SerializeField] float lerpSpeed; //따라오는 속도

    IEnumerator camShake;  //카메라 흔들림 설정
    Vector2 oripos;        //카메라 흔들리기 전의 위치
    bool isShack = false; //흔들리는상태인지 아닌지 계산

    private void Awake()
    {
        i = this; 
    }

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }
    void FollowPlayer()
    {
        if (isShack)
        {
            return;
        }
        Vector3 playerPos = new Vector3(player.position.x, player.position.y, -1);
        transform.position = playerPos;
    }

    public void StartCameraShake(float dur, float amount, float speed)
    {
        if (camShake != null)
        {
            StopCoroutine(camShake);
            Camera.main.transform.localPosition = oripos;
        }

        camShake = Shake(dur, amount, speed);
        StartCoroutine(camShake);
    }

    IEnumerator Shake(float dur, float amount, float speed)
    {
        isShack = true;
        float t = dur;                         //흔들릴 시간
        Transform cam = transform;             // 메인 카메라가 있는 위치

        oripos = (Vector2)player.position;           // 흔들리기 전의 위치
        Vector2 targerPos = transform.position; //흔들리는 위치 설정

        while(t > 0)
        {
            if(targerPos == (Vector2)transform.position)
            {
                targerPos = oripos + (Vector2)(Random.insideUnitCircle * amount);
                //지름이 1인 내부의 임의의 한 점을 반환
                
            }

            cam.localPosition = Vector2.Lerp(cam.localPosition, targerPos, speed * Time.deltaTime);
            cam.position = new Vector3(cam.localPosition.x, cam.localPosition.y , -1); //카메라가 Z축으로 가까워지는거 방지

            if(Vector2.Distance(cam.localPosition,targerPos) <= 0.02f ) //두 지점의 거리의 값이 0.02보다 가깝다면
            {
                targerPos = transform.position;                               //타겟 위치 초기화
            }

            t-= Time.deltaTime;  //시간 감소
            yield return null; 
        }

       
        cam.localPosition = player.position;        //흔들리기 전 위치로 돌림
        isShack = false;
    }
}
