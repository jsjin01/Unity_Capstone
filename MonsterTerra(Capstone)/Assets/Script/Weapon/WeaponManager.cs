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

    //해당 무기의 weapon component를 가져옴
    WeaponComponent crWeapon;
    WeaponComponent soWeapon;
    WeaponComponent mwWeapon;
    WeaponComponent spWeapon;

    [Header("# Weapon Sprite")]
    [SerializeField] Sprite[] crweapons;
    [SerializeField] Sprite[] soweapons;
    [SerializeField] Sprite[] mwweapons; 
    [SerializeField] Sprite[] spweapons;
    /// <summary>
    /// 이미지 크기랑 각도 수정이 필요함
    /// </summary>
    int weaponAttackNum = 4; // 어떤 무기로 Attack을 시전할 것 인지
    private void Awake()
    {
        i = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {

            if (idx == 1)
            {
                crWeapon.WeaponLevelUp();
            }
            else if (idx == 2)
            {
                soWeapon.WeaponLevelUp();
            }
            else if (idx == 3) 
            {
                mwWeapon.WeaponLevelUp();
            }
            else if(idx == 4)
            {
                spWeapon.WeaponLevelUp();
            }
        }
    }


    public void changeWeapon() //무기 바꾸기 
    {
        if(idx == 1) //근접 -> 원거리
        {
            weaponSR.sprite = soweapons[(int)sotype];
            weaponAttackNum++;
            idx++;
            UIManager.i.SetWeaponImg(idx, (int)sotype);
        }
        else if(idx ==2) // 원거리 -> 마법 무기류
        {
            weaponSR.sprite = mwweapons[(int)mwtype];
            weaponAttackNum++;
            idx++;
            UIManager.i.SetWeaponImg(idx, (int)mwtype);
        }
        else if (idx == 3) // 마법 무기류 ->  지원 무기류
        {
            weaponSR.sprite = spweapons[(int)sptype];
            weaponAttackNum++;
            idx++;
            UIManager.i.SetWeaponImg(idx, (int)sptype);
        }
        else if (idx == 4) // 지원 무기류 -> 근접
        {
            weaponSR.sprite = crweapons[(int)crtype];
            weaponAttackNum = 1;
            idx = 1;
            UIManager.i.SetWeaponImg(idx, (int)crtype);
        }
    }

    public void Attack()
    {
        //지금 손에 들고 있는 무기에 따라 공격 방식이 변함
        if (idx == 1)
        {
            crWeapon.Attack();
        }
        else if(idx == 2)
        {
            soWeapon.Attack();
        }
        else if(idx == 3)
        {
            mwWeapon.Attack();
        }
        else if(idx == 4)
        {
            spWeapon.Attack();
        }
    }

    public void SetWeapon(int cr, int so, int mw, int sp, CharacterComponent cc) //선택한 무기만 활성화
    {
        weaponSR = GamePlayerManager.i.Character.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<SpriteRenderer>();
        //무기를 적용하고 있는 스프라이트랜더러를 가져옴

        //무기 설정 변화
        crtype = (CRTYPE)cr;
        sotype = (SOTYPE)so;
        mwtype = (MWTYPE)mw;
        sptype = (SPTYPE)sp;

        transform.GetChild(0).GetChild(cr).gameObject.SetActive(true);
        transform.GetChild(1).GetChild(so).gameObject.SetActive(true);
        transform.GetChild(2).GetChild(mw).gameObject.SetActive(true);
        transform.GetChild(3).GetChild(sp).gameObject.SetActive(true);
        crWeapon = transform.GetChild(0).GetChild(cr).GetComponent<WeaponComponent>();
        soWeapon = transform.GetChild(1).GetChild(so).GetComponent<WeaponComponent>();
        mwWeapon = transform.GetChild(2).GetChild(mw).GetComponent<WeaponComponent>();
        spWeapon = transform.GetChild(3).GetChild(sp).GetComponent<WeaponComponent>();

        //캐릭터 계수 추가
        crWeapon.SetCdamage(cc);
        soWeapon.SetCdamage(cc);
        mwWeapon.SetCdamage(cc);    
        //spWeapon.SetCdamage(cc);
    }
}
