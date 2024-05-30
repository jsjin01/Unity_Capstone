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
    public float mp = 100;
    public int atk = 20;
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
}

