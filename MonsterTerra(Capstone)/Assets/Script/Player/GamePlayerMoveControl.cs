using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerMoveControl : MonoBehaviour
{
    public static GamePlayerMoveControl i;

    Rigidbody2D rb; 
    public Animator anit; 

    public Vector2 playerDir;//�÷��̾ �̵��ϴ� ������ �޾ƿ�
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
        if (Input.GetKeyDown(KeyCode.Space)) //���� �����ϴ� �κ�
        {
            WeaponManager.i.Attack();
        }
        if (Input.GetKeyDown(KeyCode.Q)) //���� ��ü �κ�
        {
            WeaponManager.i.changeWeapon();
        }
    }

    void FixedUpdate()
    {
        playerPos = new Vector2(transform.position.x, transform.position.y + 0.5f) ;
        Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void Move(float x, float y)           //�÷��̾� �̵�
    {
        playerDir = new Vector2(x, y);
        playerDir = playerDir.normalized; // �÷��̾� �̵� ���� ����ȭ
        rb.MovePosition(rb.position + playerDir*GamePlayerManager.i.speed*Time.deltaTime);
        
        if(x > 0) //���⿡ ���� �̹��� ��ȯ
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        anit.SetFloat("speed", playerDir.magnitude);
    }

    public void TakeDamage(int dmg) //������ �޴� ����
    {
        GamePlayerManager.i.hp -= dmg; //������ ����
        anit.SetTrigger("Hit");
        if(GamePlayerManager.i.hp <= 0)
        {
            GamePlayerManager.i.isDead = true; //���� Ȱ��ȭ
            anit.ResetTrigger("Dead");
        }
    }


}
