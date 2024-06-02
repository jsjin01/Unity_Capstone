using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderMagic : WeaponComponent
{
    public static ThunderMagic i;
    [SerializeField] GameObject thunder; // ����ü
    float dur = 0.5f;
    float amount = 0;


    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        //�⺻�� ����
        weaponmulatk = 1f;
        weaponmulatkspd = 15.0f;
        debuffType = 1;
    }

    public override void Attack()
    {
        if (!canAttack)
        {
            return;
        }
        //thunder ����
        StartCoroutine(ThunderAttack());

        if(lv >= 5)
        {
            StartCoroutine(ThunderMax());
        }
        StartCoroutine(AttackRate());
    }

    public override void WeaponLevelUp()
    {
        lv++;
        if (lv == 1)
        {
           //���� ���� ����(5����)
        }
        else if (lv == 2)
        {
            weaponmulatk += 1.5f; //���ݷ� 150%����
        }
        else if (lv == 3)
        {
            dur = 1.5f; //���� ���� �ð� 1.5�ʷ� ����
        }
        else if (lv == 4)
        {
            //���� ���� ����(7��)
        }
        else if (lv == 5)
        {
            //�� ��ü�� ���� ������
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

    IEnumerator ThunderAttack()
    {
        Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
        Vector2 dir = GamePlayerMoveControl.i.playerDir;

        SoundManger.i.PlaySound(8);
        GameObject thunder1 = Instantiate(thunder, GamePlayerMoveControl.i.playerPos + dir, rotation, transform);
        thunder1.transform.GetComponentInChildren<MagicVFX>().SetAttack(dmg, endCriDmg, endCri, dur, amount, debuffType);
        yield return new WaitForSeconds(0.1f);

        SoundManger.i.PlaySound(8);
        GameObject thunder2 = Instantiate(thunder, GamePlayerMoveControl.i.playerPos + 2 * dir, rotation, transform);
        thunder2.transform.GetComponentInChildren<MagicVFX>().SetAttack(dmg, endCriDmg, endCri, dur, amount, debuffType);
        yield return new WaitForSeconds(0.1f);

        SoundManger.i.PlaySound(8);
        GameObject thunder3 = Instantiate(thunder, GamePlayerMoveControl.i.playerPos + 3 * dir, rotation, transform);
        thunder3.transform.GetComponentInChildren<MagicVFX>().SetAttack(dmg, endCriDmg, endCri, dur, amount, debuffType);
        yield return new WaitForSeconds(0.1f);

        if(lv >= 1)
        {
            SoundManger.i.PlaySound(8);
            GameObject thunder4 = Instantiate(thunder, GamePlayerMoveControl.i.playerPos + 4 * dir, rotation, transform);
            thunder4.transform.GetComponentInChildren<MagicVFX>().SetAttack(dmg, endCriDmg, endCri, dur, amount, debuffType);
            yield return new WaitForSeconds(0.1f);

            SoundManger.i.PlaySound(8);
            GameObject thunder5 = Instantiate(thunder, GamePlayerMoveControl.i.playerPos + 5 * dir, rotation, transform);
            thunder5.transform.GetComponentInChildren<MagicVFX>().SetAttack(dmg, endCriDmg, endCri, dur, amount, debuffType);
            yield return new WaitForSeconds(0.1f);
        }

        if (lv >= 4)
        {
            SoundManger.i.PlaySound(8);
            GameObject thunder6 = Instantiate(thunder, GamePlayerMoveControl.i.playerPos + 6 * dir, rotation, transform);
            thunder6.transform.GetComponentInChildren<MagicVFX>().SetAttack(dmg, endCriDmg, endCri, dur, amount, debuffType);
            yield return new WaitForSeconds(0.1f);

            SoundManger.i.PlaySound(8);
            GameObject thunder7 = Instantiate(thunder, GamePlayerMoveControl.i.playerPos + 7 * dir, rotation, transform);
            thunder7.transform.GetComponentInChildren<MagicVFX>().SetAttack(dmg, endCriDmg, endCri, dur, amount, debuffType);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator ThunderMax()
    {
        Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);

        for (int i = 0; i < 25; i++)
        {
            float x = Random.Range(-9, 9);
            float y = Random.Range(-5, 5);
            Vector2 randomPos = GamePlayerMoveControl.i.playerPos + new Vector2(x, y);

            SoundManger.i.PlaySound(8);
            GameObject thunderMax = Instantiate(thunder, randomPos, rotation, transform);
            thunderMax.transform.GetComponentInChildren<MagicVFX>().SetAttack(dmg, endCriDmg, endCri, dur, amount, debuffType);

            yield return new WaitForSeconds(0.1f);
        }

    }
}
