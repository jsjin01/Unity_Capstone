using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerVFX : MonoBehaviour
{
    float atk = 5;
    float cridmg = 0;
    float cri = 0;
    int etype = 0; // Ư�� ȿ��
    int lv = 0;    //������ ����

    float addatk = 0;//�߰�Ÿ ������
    float addcri = 0;//�߰�Ÿ ũ��Ƽ�� Ȯ��

    Enemy enemy; //���� �޴� ���� =>  Invoke �ȿ��� ����ϱ� ���ؼ� ����
    
    bool addATk1 = false;
    bool addATk2 = false;

    [SerializeField] GameObject parent;
    [SerializeField] Vector2 target;
    [SerializeField] float dTime = 1.0f;

    private void Start()
    {
        Destroy(parent, dTime);
    }

    private void Update()
    {
        target = GamePlayerMoveControl.i.playerPos;
        parent.transform.position = target;
    }

    public void SetAttack(float _atk, float _cridmg, float _cri, int lv, GameObject obj = null) //������ ����
    {
        atk = _atk;
        cridmg = _cridmg;
        cri = _cri;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RushMonster1") || collision.CompareTag("RushMonster2") || collision.CompareTag("RushMonster3") || collision.CompareTag("RushMonster4") ||
            collision.CompareTag("ShootMonster1")|| collision.CompareTag("ShootMonster2")|| collision.CompareTag("ShootMonster3")|| collision.CompareTag("ShootMonster4")||
            collision.CompareTag("BossMonster1")|| collision.CompareTag("BossMonster2"))
        {
            //���Ϳ��� ������ �ִ� �κ�
            collision.GetComponent<Enemy>().TakeDamage(atk,cridmg, cri, etype);
            enemy = collision.GetComponent<Enemy>();
            if(lv == 1)
            {
                addatk = atk * 0.3f; //�߰� ������ 30% ����
            }
            else if(lv > 1)
            {
                addatk = atk * 0.6f; //�߰� ������ 60% ����
            }
            
            if(lv >= 4)
            {
                addcri = cri;       //�߰�Ÿ���� ġ��Ÿ ����
            }

            if(lv > 1)
            {
                Invoke("AddAtk1", 0.1f); //�߰�Ÿ ����
            }
            Debug.Log("takeDamage");
        }
    }

    public void DestroyWeapon(float dt = 0)
    {
        Destroy(parent, dt);
    }

    void AddAtk1() //�߰� 1Ÿ
    {
        enemy.TakeDamage(addatk, cridmg, addcri, etype);
        if (lv == 3)
        {
            Invoke("AddAtk2", 0.1f);
        }
    }

    void AddAtk2() //�߰� 2Ÿ
    {
        enemy.TakeDamage(addatk, cridmg, addcri, etype);
    }

}
