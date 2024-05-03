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
    [Header("# Weapon Sprite")]
    [SerializeField] Sprite[] crweapons;
    [SerializeField] Sprite[] soweapons;
    [SerializeField] Sprite[] mwweapons; 
    [SerializeField] Sprite[] spweapons;
    /// <summary>
    /// �̹��� ũ��� ���� ������ �ʿ���
    /// </summary>
    int weaponAttackNum = 4; // � ����� Attack�� ������ �� ����
    bool startchange = true; //������ �� �⺻ ����� �ٲ�
    
    private void Awake()
    {
        i = this;
    }
    void Start()
    {
        weaponSR = GamePlayerManager.i.Character.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<SpriteRenderer>();
        //���⸦ �����ϰ� �ִ� ��������Ʈ�������� ������
    }

    private void Update()
    {
        if (startchange)
        {
            changeWeapon();
            startchange = false;
        }
    }


    public void changeWeapon() //���� �ٲٱ� 
    {
        if(idx == 1) //���� -> ���Ÿ�
        {
            weaponSR.sprite = soweapons[(int)sotype];
            weaponAttackNum++;
            idx++;
        }
        else if(idx ==2) // ���Ÿ� -> ���� �����
        {
            weaponSR.sprite = mwweapons[(int)mwtype];
            weaponAttackNum++;
            idx++;
        }
        else if (idx == 3) // ���� ����� ->  ���� �����
        {
            weaponSR.sprite = spweapons[(int)sptype];
            weaponAttackNum++;
            idx++;
        }
        else if (idx == 4) // ���� ����� -> ����
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
