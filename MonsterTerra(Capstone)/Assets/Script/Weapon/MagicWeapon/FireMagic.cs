using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMagic : WeaponComponent
{
    public static FireMagic i;
    [SerializeField] GameObject Circle; // ȸ��
    bool max =false;
    public int idxlv = 0;



    float dur = 3f;
    float amount = 5f;


    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        //�⺻�� ����
        weaponmulatk = 1f;
        weaponmulatkspd = 15.0f;
        debuffType = 3;
    }

    public override void Attack()
    {
        if (!canAttack)
        {
            return;
        }

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        //ICE Bullet ����
        GameObject fireCircle = Instantiate(Circle , GamePlayerMoveControl.i.playerPos, rotation, transform);
        fireCircle.GetComponentInChildren<FireBallCircle>().SetAttack(dmg, endCriDmg, endCri, max, dur, amount, debuffType);
        

        StartCoroutine(AttackRate());
    }

    public override void WeaponLevelUp()
    {
        lv++;
        if (lv == 1)
        {
            weaponmulatk *= 2.0f; //������ 2�� ����
        }
        else if (lv == 2)
        {
            amount *= 2f;//ȭ�󵥹��� 2�� ����
        }
        else if (lv == 3)
        {
            dur = 5; //��ȭ �ð� 5�ʷ� ����
        }
        else if (lv == 4)
        {
            idxlv++;//���� ���� ����
        }
        else if (lv == 5)
        {
            max = true;
        }
        else
        {
            Debug.Log("���̻� ��ȭ�� �� �����ϴ�.");
        }

        if (lv == 6)
        {
            lv--;
        }
    }
}
