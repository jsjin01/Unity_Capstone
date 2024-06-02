using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    public static CardManager i;

    [SerializeField] GameObject[] Card;  //일반 카드 => 6장 / 0 1 2 3 4 5
                                         //무기 강화 카드 => 4장 / 6 7 8 9

    //1,2,3번째 카드가  무기 강화인지 아닌지 계산
    int[] cardisWeapon = { 0, 0, 0 };
    int[] cardiskind = { 0, 0, 0 };


    private void OnEnable() //활성화 될 때
    {
        if(i == null)
        {
            i = this;
        }

        Time.timeScale = 0f;  //일정 정지
        CardSelect(GamePlayerManager.i.lv);
        for(int i = 0; i < 3; i++)
        {
            Card[cardiskind[i]].SetActive(true); //활성화
        }
    }

    void CardSelect(int lv)
    {
        if(lv%5 == 0) //5의 배수일 때
        {
            while (cardisWeapon[0] == 0 || cardisWeapon[1] == 0 || cardisWeapon[2] == 0)
            {
                //무조건 무기 강화 카드가 한장 나올 때 까지 돌림
                cardisWeapon[0] = KindOfCard();
                cardisWeapon[1] = KindOfCard();
                cardisWeapon[2] = KindOfCard();
            }
        }
        else //아닐 때
        {
            cardisWeapon[0] = KindOfCard();
            cardisWeapon[1] = KindOfCard();
            cardisWeapon[2] = KindOfCard();
        }

        while (!(cardiskind[0] != cardiskind[1] && cardiskind[1] != cardiskind[2] && cardiskind[0] != cardiskind[2]) ) //카드가 모두 같지 않을떄 까지 돌리기
        {
            for (int i = 0; i < 3; i++)
            {
                if (cardisWeapon[i] == 0)
                {
                    cardiskind[i] = Random.Range(0, 6); //일반 카드 뽑기
                }
                else
                {
                    cardiskind[i] = Random.Range(6, 10); //무기 카드 뽑기
                }
            }
        }
    }

    int KindOfCard() //무기 강화인지 아닌지 계산
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

    //일반 카드 기능
    public void AttackUp() //공격력 +2
    {
        GamePlayerManager.i.atk += 2;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void AttackSpeedUp() //공격속도 + 5%
    {
        GamePlayerManager.i.atkSpd *= 0.95f;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void SpeedUp() //이동속도 +10%
    {
        GamePlayerManager.i.speed *= 1.1f;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void MaxHpUp()  //최대 체력 +10
    {
        GamePlayerManager.i.MaxHp += 10;
        GamePlayerManager.i.hp += 10;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void CriDmgUp()  //크리티컬 피해량 +5%
    {
        GamePlayerManager.i.criDmg += 0.05f;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void criUp() //크리티컬 확률 + 3%
    {
        GamePlayerManager.i.cri += 0.03f;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void CRLevelUp() //근접 무기 업
    {
        WeaponManager.i.WeaponUpgrade(0);
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void SOLevelUp() //원거리 무기 업
    {
        WeaponManager.i.WeaponUpgrade(1);
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void MWLevelUp() //마법 무기 업
    {
        WeaponManager.i.WeaponUpgrade(2);
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void SPLevelUp() //서포트 무기 업
    {
        WeaponManager.i.WeaponUpgrade(3);
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

}
