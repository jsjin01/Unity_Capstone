using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerMoveControl : MonoBehaviour
{
    public static GamePlayerMoveControl i;

    Rigidbody2D rb; 
    [SerializeField]Animator anit; 

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

    void Update()
    {
        playerPos = transform.position;
        Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetKey(KeyCode.Space))
        {
            WeaponManager.i.Attack();
        }
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



}
