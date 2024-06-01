using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerManager : MonoBehaviour
{
    public static GamePlayerManager i;
    [Header("# Player Info")]
    public GameObject Character;
    public CHARACTERTYPE ctype;   //  캐릭터 타입
    public CharacterComponent CC; // 캐릭터 컴포넌트
    public float MaxHp = 100;
    public float hp = 100;
    public float MaxMp = 100;
    public float def = 0; // 보호막
    public float mp = 100;
    public float atk = 20;
    public float atkSpd = 1f;
    public float cri = 0;
    public float criDmg = 1.5f;
    public float speed = 3f;
    
    public int lv = 1;
    public int exp = 0; // 현재 경험치
    public int[] maxExp = { 100, 140, 196  ,100000000}; // Level UP 하기까지 필요한 경험치

    //죽었는지 판단하는 변수
    public bool isDead = false;
    
    private void Awake()
    {
        i = this;
    }

    public void  selectCharacter() // 활성화된 캐릭터 찾기
    {
        Transform c = transform.Find("Character").transform;
        Transform activeChild = null;
        foreach (Transform child in c)
        {
            if (child.gameObject.activeSelf)
            {
                activeChild = child;
                break;
            }
        }

        if (activeChild != null)
        {
            Character = activeChild.gameObject;
        } 
        CC = Character.GetComponent<CharacterComponent>();
    }
    public void GetExp() //레벨업 시스템
    {
        IncreaseExperience(10);

        if (exp >= maxExp[lv])
        {
            CC.LevelUp(); //캐릭터별로 고유한 패시브 특성 적용
            lv++;
            exp = 0;
        }
    }

    public void IncreaseExperience(int expAmount) //경험치 증가
    {
        exp += expAmount;
        Debug.Log("경험치 획득");
    }

    //SupportWeapon 
    public IEnumerator Shield(float df, float dur) 
    {
        UIManager.i.BuffOn(0);
        def += df;
        yield return new WaitForSeconds(dur);
        def = 0;
        UIManager.i.BuffOff(0);
    }

    public IEnumerator Brunch(float heal, float dur, bool max)
    {
        UIManager.i.BuffOn(1);
        if (!max)
        {
            float healinterval = heal / (dur / 0.5f);
            while (dur <= 0)
            {
                hp += healinterval;
                if (hp > MaxHp)
                {
                    hp = MaxHp;
                }
                dur -= 0.5f;
                yield return new WaitForSeconds(0.5f);
            }
        }
        else
        {
            hp += heal;
            if (hp > MaxHp)
            {
                hp = MaxHp;
            }
        }
        UIManager.i.BuffOff(1);
    }

    public IEnumerator MagicTool(float atkup, float dur)
    {
        UIManager.i.BuffOn(2);
        atk += atkup;
        yield return new WaitForSeconds(dur);
        atk -= atkup;
        UIManager.i.BuffOff(2);
    }

    public IEnumerator Coffee(float atkspdup, float dur)
    {
        UIManager.i.BuffOn(3);
        float amount = atkSpd * (atkspdup / 100);
        atkSpd -= amount;
        yield return new WaitForSeconds(dur);
        atkSpd += amount;
        UIManager.i.BuffOff(3);
    }

    public IEnumerator Feather(float spd, float dur)
    {
        UIManager.i.BuffOn(4);
        float amount =  speed * (spd / 100);
        speed += amount;
        yield return new WaitForSeconds(dur);
        speed -= amount;
        UIManager.i.BuffOff(4);
    }
}

