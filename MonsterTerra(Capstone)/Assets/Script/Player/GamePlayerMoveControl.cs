using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerMoveControl : MonoBehaviour
{
    public static GamePlayerMoveControl i;

    Rigidbody2D rb; 
    public Animator anit; 

    public Vector2 playerDir;//플레이어가 이동하는 방향을 받아옴
    public Vector2 playerPos;

    private void Awake()
    {
        i = this;
    }
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anit = GamePlayerManager.i.Character.transform.GetChild(0).GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //무기 공격하는 부분
        {
            WeaponManager.i.Attack();
        }
        if (Input.GetKeyDown(KeyCode.Q)) //무기 교체 부분
        {
            WeaponManager.i.changeWeapon();
        }
    }

    void FixedUpdate()
    {
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

    public void TakeDamage(int dmg) //데미지 받는 행위
    {
        GamePlayerManager.i.hp -= dmg; //데미지 받음
        anit.SetTrigger("Hit");
        if(GamePlayerManager.i.hp <= 0)
        {
            GamePlayerManager.i.isDead = true; //죽음 활성화
            anit.ResetTrigger("Dead");
        }
    }


}
