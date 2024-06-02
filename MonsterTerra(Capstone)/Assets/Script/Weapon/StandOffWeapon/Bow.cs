using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : WeaponComponent
{
    public static Bow i;
    [SerializeField] GameObject bullet; // ����ü
    bool lvMax = false;


    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        //�⺻�� ����
        weaponmulatk = 0.8f;
        weaponmulatkspd = 1f;
    }

    public override void Attack()
    {
        if (!canAttack)
        {
            return;
        }

        SoundManger.i.PlaySound(6);
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        //ȭ�� ����
        GameObject arrow = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, rotation, transform);
        arrow.GetComponentInChildren<BulletComponent>().SetAttack(dmg, endCriDmg, endCri);
        arrow.GetComponentInChildren<BulletComponent>().Move(GamePlayerMoveControl.i.playerDir); //�÷��̾� �̵� �������� �߻�
        if (lvMax)
        {
            //�߰� ȭ�� ���� �� ���� ���
            Quaternion leftRotation = Quaternion.Euler(0f, 0f, angle+ 30);
            Quaternion rightRotation = Quaternion.Euler(0f, 0f, angle - 30);
            Vector2 leftDir = RotateVector2(Vector2.right, angle + 30);
            Vector2 rightDir = RotateVector2(Vector2.right, angle - 30);

            //���� ȭ�� && ������ ȭ�� ����
            GameObject leftArrow = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, leftRotation, transform);
            GameObject rightArrow = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, rightRotation, transform);
            leftArrow.GetComponentInChildren<BulletComponent>().SetAttack(dmg, endCriDmg, endCri);
            leftArrow.GetComponentInChildren<BulletComponent>().Move(leftDir); 
            rightArrow.GetComponentInChildren<BulletComponent>().SetAttack(dmg, endCriDmg, endCri);
            rightArrow.GetComponentInChildren<BulletComponent>().Move(rightDir); 
        }
        StartCoroutine(AttackRate());
    }
    Vector2 RotateVector2(Vector2 dir, float angle) //������ ���� playDir ��ȭ ���
    {
        // ������ �������� ��ȯ
        float rad = angle * Mathf.Deg2Rad;

        // ȸ�� ��ȯ ����
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);

        float newX = dir.x * cos - dir.y * sin;
        float newY = dir.x * sin + dir.y * cos;

        return new Vector2(newX, newY).normalized;
    }

    public override void WeaponLevelUp()
    {
        lv++;
        if (lv == 1)
        {
            weaponmulatkspd -= 0.2f;// ���ݼӵ� 20% ����
        }
        else if (lv == 2)
        {
            weaponmulatk += 0.1f; //���� �߰� ���ݷ� 10% ����
        }
        else if (lv == 3)
        {
            addCridmg += 0.15f; // ġ��Ÿ ������ 15% ����
        }
        else if (lv == 4)
        {
            addCri += 0.1f; //ġ��Ÿ Ȯ�� 10�� ����
        }
        else if (lv == 5)
        {
            lvMax = true;
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
