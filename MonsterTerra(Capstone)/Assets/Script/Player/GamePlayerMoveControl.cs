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

    public Vector2 playerDir;//�÷��̾ �̵��ϴ� ������ �޾ƿ�
    public Vector2 playerPos;

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
        if (Input.GetKeyDown(KeyCode.LeftControl))//ȸ�Ǳ� �۵�
        {
            StartCoroutine("MissKey");
        }
    }

    void FixedUpdate()
    {
        if (GamePlayerManager.i.isDead)
        {
            return;
        } //������ �������� ����
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

    IEnumerator MissKey()//ȸ�Ǳ� key
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

        transform.position = endPosition; 
        cd.enabled = true;
        tr.enabled = false;
    }

    public void TakeDamage(int dmg) //������ �޴� ����
    {
        if (GamePlayerManager.i.isDead)
        {
            return;
        } //������ �������� ����
        GamePlayerManager.i.hp -= dmg; //������ ����
        anit.SetTrigger("Hit");
        CameraControl.i.StartCameraShake(0.1f,0.3f,20f);
        if(GamePlayerManager.i.hp <= 0)
        {
            GamePlayerManager.i.isDead = true; //���� Ȱ��ȭ
            anit.SetTrigger("Dead");
        }
    }


}
