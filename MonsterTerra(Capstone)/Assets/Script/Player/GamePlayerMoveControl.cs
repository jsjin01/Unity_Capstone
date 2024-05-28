using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerMoveControl : MonoBehaviour
{
    public static GamePlayerMoveControl i;

    Rigidbody2D rb;
    BoxCollider2D cd;
    [SerializeField]TrailRenderer tr;
    public Animator anit; 

    public Vector2 playerDir;//플레이어가 이동하는 방향을 받아옴
    public Vector2 playerPos;

    bool isMiss = true; //회피기를 사용할 수 있는지의 여부

    private void Awake()
    {
        i = this;
    }
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<BoxCollider2D>();
        tr = transform.GetChild(2).GetComponent<TrailRenderer>();
        anit = GamePlayerManager.i.Character.transform.GetChild(0).GetComponent<Animator>();
        rb.constraints = RigidbodyConstraints2D.None; //재시작할 때 고정되어있는 부분 풀기
        rb.freezeRotation = true; //회전 안하도록 설정
    }
    private void Update()
    {
        if (!GameManager.i.isLive) return;

        if (Input.GetKeyDown(KeyCode.Space)) //무기 공격하는 부분
        {
            WeaponManager.i.Attack();
        }
        if (Input.GetKeyDown(KeyCode.Q)) //무기 교체 부분
        {
            WeaponManager.i.changeWeapon();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))//회피기 작동
        {
            if(isMiss)
            {
                StartCoroutine("MissKey");
            }
        }
    }

    void FixedUpdate()
    {
        if (GamePlayerManager.i.isDead || !GameManager.i.isLive)
        {
            return;
        } //죽으면 끝나도록 설정
        playerPos = new Vector2(transform.position.x, transform.position.y + 0.5f) ;
        Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void Move(float x, float y)           //플레이어 이동
    {
        playerDir = new Vector2(x, y);
        playerDir = playerDir.normalized; // 플레이어 이동 방향 정규화
        rb.MovePosition(rb.position + playerDir*GamePlayerManager.i.speed*Time.deltaTime);
        
        if(x > 0) //방향에 따라 이미지 전환
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        anit.SetFloat("speed", playerDir.magnitude);
    }

    IEnumerator MissKey()//회피기 key
    {
        Debug.Log("miss");
        cd.enabled = false;
        tr.enabled = true;
        yield return new WaitForSeconds(0.1f);

        Vector2 startPosition = transform.position;
        Vector2 endPosition = startPosition + playerDir * GamePlayerManager.i.speed * 1;
        float elapsedTime = 0f;

        while (elapsedTime < 0.3f)
        {
            transform.position = Vector2.Lerp(startPosition, endPosition, elapsedTime / 0.3f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        StartCoroutine("MissCol");//대쉬 쿨타임 진입
        transform.position = endPosition; 
        cd.enabled = true;
        tr.enabled = false;
    }

    IEnumerator MissCol() //대쉬 쿨타임
    {
        isMiss = false;
        float elapsedTime = 0f;
        while (elapsedTime < 5f)
        {
            elapsedTime += Time.deltaTime;
            GamePlayerManager.i.mp = elapsedTime * 20;
            yield return null;
        }
        isMiss = true;
    }

    public void TakeDamage(int dmg) //데미지 받는 행위
    {
        if (GamePlayerManager.i.isDead)
        {
            return;
        } //죽으면 끝나도록 설정
        GamePlayerManager.i.hp -= dmg; //데미지 받음
        anit.SetTrigger("Hit");
        CameraControl.i.StartCameraShake(0.1f,0.2f,50f);
        if(GamePlayerManager.i.hp <= 0)
        {
            GamePlayerManager.i.isDead = true; //죽음 활성화
            rb.constraints = RigidbodyConstraints2D.FreezePosition;//몬스터에게 안 밀리도록 고정
            anit.SetTrigger("Dead");

            GameManager.i.GameOver();
        }
    }


}
