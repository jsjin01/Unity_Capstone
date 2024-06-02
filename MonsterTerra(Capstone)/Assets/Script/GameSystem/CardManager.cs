using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    public static CardManager i;

    [SerializeField] GameObject[] Card;  //�Ϲ� ī�� => 6�� / 0 1 2 3 4 5
                                         //���� ��ȭ ī�� => 4�� / 6 7 8 9

    //1,2,3��° ī�尡  ���� ��ȭ���� �ƴ��� ���
    int[] cardisWeapon = { 0, 0, 0 };
    int[] cardiskind = { 0, 0, 0 };


    private void OnEnable() //Ȱ��ȭ �� ��
    {
        if(i == null)
        {
            i = this;
        }

        Time.timeScale = 0f;  //���� ����
        CardSelect(GamePlayerManager.i.lv);
        for(int i = 0; i < 3; i++)
        {
            Card[cardiskind[i]].SetActive(true); //Ȱ��ȭ
        }
    }

    void CardSelect(int lv)
    {
        if(lv%5 == 0) //5�� ����� ��
        {
            while (cardisWeapon[0] == 0 || cardisWeapon[1] == 0 || cardisWeapon[2] == 0)
            {
                //������ ���� ��ȭ ī�尡 ���� ���� �� ���� ����
                cardisWeapon[0] = KindOfCard();
                cardisWeapon[1] = KindOfCard();
                cardisWeapon[2] = KindOfCard();
            }
        }
        else //�ƴ� ��
        {
            cardisWeapon[0] = KindOfCard();
            cardisWeapon[1] = KindOfCard();
            cardisWeapon[2] = KindOfCard();
        }

        while (!(cardiskind[0] != cardiskind[1] && cardiskind[1] != cardiskind[2] && cardiskind[0] != cardiskind[2]) ) //ī�尡 ��� ���� ������ ���� ������
        {
            for (int i = 0; i < 3; i++)
            {
                if (cardisWeapon[i] == 0)
                {
                    cardiskind[i] = Random.Range(0, 6); //�Ϲ� ī�� �̱�
                }
                else
                {
                    cardiskind[i] = Random.Range(6, 10); //���� ī�� �̱�
                }
            }
        }
    }

    int KindOfCard() //���� ��ȭ���� �ƴ��� ���
    {
        if(Random.Range(0, 100) < 10)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    //�Ϲ� ī�� ���
    public void AttackUp() //���ݷ� +2
    {
        GamePlayerManager.i.atk += 2;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void AttackSpeedUp() //���ݼӵ� + 5%
    {
        GamePlayerManager.i.atkSpd *= 0.95f;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void SpeedUp() //�̵��ӵ� +10%
    {
        GamePlayerManager.i.speed *= 1.1f;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void MaxHpUp()  //�ִ� ü�� +10
    {
        GamePlayerManager.i.MaxHp += 10;
        GamePlayerManager.i.hp += 10;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void CriDmgUp()  //ũ��Ƽ�� ���ط� +5%
    {
        GamePlayerManager.i.criDmg += 0.05f;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void criUp() //ũ��Ƽ�� Ȯ�� + 3%
    {
        GamePlayerManager.i.cri += 0.03f;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void CRLevelUp() //���� ���� ��
    {
        WeaponManager.i.WeaponUpgrade(0);
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void SOLevelUp() //���Ÿ� ���� ��
    {
        WeaponManager.i.WeaponUpgrade(1);
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void MWLevelUp() //���� ���� ��
    {
        WeaponManager.i.WeaponUpgrade(2);
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void SPLevelUp() //����Ʈ ���� ��
    {
        WeaponManager.i.WeaponUpgrade(3);
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

}
