using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager i;

    [Header("# Weapon Info")]
    [SerializeField] int idx = 4; //���� ���� 1=> ����, 2 => ���Ÿ�, 3 => ������, 4 => ���� ���� 
    [SerializeField] CRTYPE crtype; // ������ ��������
    [SerializeField] SOTYPE sotype; // ������ ���Ÿ�����
    [SerializeField] MWTYPE mwtype; // ������ ��������
    [SerializeField] SPTYPE sptype; // ������ ��������
    SpriteRenderer weaponSR;        // �÷��̾ �ϰ� �ִ� ĳ������ ���� ���� 

    //�ش� ������ weapon component�� ������
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
    /// �̹��� ũ��� ���� ������ �ʿ���
    /// </summary>
    int weaponAttackNum = 4; // � ����� Attack�� ������ �� ����
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


    public void changeWeapon() //���� �ٲٱ� 
    {
        if(idx == 1) //���� -> ���Ÿ�
        {
            weaponSR.sprite = soweapons[(int)sotype];
            weaponAttackNum++;
            idx++;
            UIManager.i.SetWeaponImg(idx, (int)sotype);
        }
        else if(idx ==2) // ���Ÿ� -> ���� �����
        {
            weaponSR.sprite = mwweapons[(int)mwtype];
            weaponAttackNum++;
            idx++;
            UIManager.i.SetWeaponImg(idx, (int)mwtype);
        }
        else if (idx == 3) // ���� ����� ->  ���� �����
        {
            weaponSR.sprite = spweapons[(int)sptype];
            weaponAttackNum++;
            idx++;
            UIManager.i.SetWeaponImg(idx, (int)sptype);
        }
        else if (idx == 4) // ���� ����� -> ����
        {
            weaponSR.sprite = crweapons[(int)crtype];
            weaponAttackNum = 1;
            idx = 1;
            UIManager.i.SetWeaponImg(idx, (int)crtype);
        }
    }

    public void Attack()
    {
        //���� �տ� ��� �ִ� ���⿡ ���� ���� ����� ����
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

    public void SetWeapon(int cr, int so, int mw, int sp, CharacterComponent cc) //������ ���⸸ Ȱ��ȭ
    {
        weaponSR = GamePlayerManager.i.Character.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<SpriteRenderer>();
        //���⸦ �����ϰ� �ִ� ��������Ʈ�������� ������

        //���� ���� ��ȭ
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

        //ĳ���� ��� �߰�
        crWeapon.SetCdamage(cc);
        soWeapon.SetCdamage(cc);
        mwWeapon.SetCdamage(cc);    
        //spWeapon.SetCdamage(cc);
    }
}
