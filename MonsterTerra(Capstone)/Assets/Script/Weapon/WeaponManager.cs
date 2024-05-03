using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager i;

    [Header("# Weapon Info")]
    [SerializeField] int idx = 4; //현재 무기 1=> 근접, 2 => 원거리, 3 => 마법봉, 4 => 지원 무기 
    [SerializeField] CRTYPE crtype; // 선택한 근접무기
    [SerializeField] SOTYPE sotype; // 선택한 원거리무기
    [SerializeField] MWTYPE mwtype; // 선택한 마법무기
    [SerializeField] SPTYPE sptype; // 선택한 지원무기
    SpriteRenderer weaponSR;        // 플레이어가 하고 있는 캐릭터의 손의 무기 
    [Header("# Weapon Sprite")]
    [SerializeField] Sprite[] crweapons;
    [SerializeField] Sprite[] soweapons;
    [SerializeField] Sprite[] mwweapons; 
    [SerializeField] Sprite[] spweapons;
    /// <summary>
    /// 이미지 크기랑 각도 수정이 필요함
    /// </summary>
    int weaponAttackNum = 4; // 어떤 무기로 Attack을 시전할 것 인지
    bool startchange = true; //시작할 때 기본 무기로 바꿈
    
    private void Awake()
    {
        i = this;
    }
    void Start()
    {
        weaponSR = GamePlayerManager.i.Character.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<SpriteRenderer>();
        //무기를 적용하고 있는 스프라이트랜더러를 가져옴
    }

    private void Update()
    {
        if (startchange)
        {
            changeWeapon();
            startchange = false;
        }
    }


    public void changeWeapon() //무기 바꾸기 
    {
        if(idx == 1) //근접 -> 원거리
        {
            weaponSR.sprite = soweapons[(int)sotype];
            weaponAttackNum++;
            idx++;
        }
        else if(idx ==2) // 원거리 -> 마법 무기류
        {
            weaponSR.sprite = mwweapons[(int)mwtype];
            weaponAttackNum++;
            idx++;
        }
        else if (idx == 3) // 마법 무기류 ->  지원 무기류
        {
            weaponSR.sprite = spweapons[(int)sptype];
            weaponAttackNum++;
            idx++;
        }
        else if (idx == 4) // 지원 무기류 -> 근접
        {
            weaponSR.sprite = crweapons[(int)crtype];
            weaponAttackNum = 1;
            idx = 1;
        }
    }

    public void Attack()
    {
        LongSword.i.Attack();
    }
}
