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

    bool isMiss = true; //ȸ�Ǳ⸦ ����� �� �ִ����� ����

    private void Awake()
    {
        i = this;
    }
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<BoxCollider2D>();
        tr = transform.GetChild(2).GetComponent<TrailRenderer>();
        rb.constraints = RigidbodyConstraints2D.None; //������� �� �����Ǿ��ִ� �κ� Ǯ��
        rb.freezeRotation = true; //ȸ�� ���ϵ��� ����
    }
    private void Update()
    {
        if (!GameManager.i.isLive) return;

        if (Input.GetKey(KeyCode.Space)) //���� �����ϴ� �κ�
        {
            WeaponManager.i.Attack();
        }
        if (Input.GetKeyDown(KeyCode.Q)) //���� ��ü �κ�
        {
            WeaponManager.i.changeWeapon();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))//ȸ�Ǳ� �۵�
        {
            if(isMiss)
            {
                StartCoroutine("MissKey");
            }
        }

        //������ ���
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseItem(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseItem(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseItem(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UseItem(3);
        }
    }

    void FixedUpdate()
    {
        if (GamePlayerManager.i.isDead || !GameManager.i.isLive)
        {
            return;
        } //������ �������� ����
        playerPos = new Vector2(transform.position.x, transform.position.y + 0.5f) ;
        Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void Move(float x, float y)           //�÷��̾� �̵�
    {
        Vector2 movedir = new Vector2(x, y);
        movedir = movedir.normalized; // �÷��̾� �̵� ���� ����ȭ

        if (movedir == Vector2.zero)
        {
            if(playerDir == Vector2.zero)
            {
                playerDir = Vector2.right;
            }
        }
        else
        {
            playerDir = movedir;
        }

        rb.MovePosition(rb.position + movedir *GamePlayerManager.i.speed*Time.deltaTime);
        
        if(x > 0) //���⿡ ���� �̹��� ��ȯ
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        anit.SetFloat("speed", movedir.magnitude);
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

        StartCoroutine("MissCol");//�뽬 ��Ÿ�� ����
        transform.position = endPosition; 
        cd.enabled = true;
        tr.enabled = false;
    }

    IEnumerator MissCol() //�뽬 ��Ÿ��
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

    public void TakeDamage(float dmg) //������ �޴� ����
    {
        dmg *= GamePlayerManager.i.DmgAdd;
        if (GamePlayerManager.i.isDead)
        {
            return;
        } //������ �������� ����
        if(GamePlayerManager.i.def > 0)
        {
            if(dmg - GamePlayerManager.i.def > 0)
            {
                dmg -= GamePlayerManager.i.def;
                StopCoroutine(Shield.i.ShieldCor);
                UIManager.i.BuffOff(0);
            }
            else
            {
                GamePlayerManager.i.def -= dmg;
                dmg = 0;
            }
        }

        GamePlayerManager.i.hp -= dmg; //������ ����
        anit.SetTrigger("Hit");
        CameraControl.i.StartCameraShake(0.1f,0.2f,50f);
        if(GamePlayerManager.i.hp <= 0)
        {
            GamePlayerManager.i.isDead = true; //���� Ȱ��ȭ
            rb.constraints = RigidbodyConstraints2D.FreezePosition;//���Ϳ��� �� �и����� ����
            anit.SetTrigger("Dead");

            GameManager.i.GameOver();
        }
    }

    void UseItem(int i)
    {
        GamePlayerManager.i.UseItme(GamePlayerManager.i.item[i]);
        GamePlayerManager.i.item[i] = 0;// ������ ����� �ش� �ε��� 0
        UIManager.i.ItemImgChange(i, GamePlayerManager.i.item[i]);
    }

}
