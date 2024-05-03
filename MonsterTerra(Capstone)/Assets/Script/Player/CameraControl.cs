using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl i;
    Transform player;

    [SerializeField] float lerpSpeed; //������� �ӵ�

    IEnumerator camShake;  //ī�޶� ��鸲 ����
    Vector2 oripos;        //ī�޶� ��鸮�� ���� ��ġ


    private void Awake()
    {
        i = this; 
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    StartCameraShake(0.5f, 1, 20);
        //}
    }
    void FollowPlayer()
    {
        Vector3 playerPos = new Vector3(player.position.x, player.position.y, -1);
        transform.position = Vector3.Lerp(transform.position, playerPos, lerpSpeed * Time.deltaTime);
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
        float t = dur;                         //��鸱 �ð�
        Transform cam = transform;             // ���� ī�޶� �ִ� ��ġ

        oripos = (Vector2)player.position;           // ��鸮�� ���� ��ġ
        Debug.Log(oripos);
        Vector2 targerPos = transform.position; //��鸮�� ��ġ ����

        while(t > 0)
        {
            if(targerPos == (Vector2)transform.position)
            {
                targerPos = oripos + (Vector2)(Random.insideUnitCircle * amount);
                //������ 1�� ������ ������ �� ���� ��ȯ
                
            }

            cam.localPosition = Vector2.Lerp(cam.localPosition, targerPos, speed * Time.deltaTime);
            cam.position = new Vector3(cam.localPosition.x, cam.localPosition.y , -1); //ī�޶� Z������ ��������°� ����

            if(Vector2.Distance(cam.localPosition,targerPos) <= 0.02f ) //�� ������ �Ÿ��� ���� 0.02���� �����ٸ�
            {
                targerPos = transform.position;                               //Ÿ�� ��ġ �ʱ�ȭ
            }

            t-= Time.deltaTime;  //�ð� ����
            yield return null; 
        }

       
        cam.localPosition = player.position;        //��鸮�� �� ��ġ�� ����

    }
}
