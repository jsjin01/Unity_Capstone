using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerManager : MonoBehaviour
{
    public static GamePlayerManager i;
    [Header("# Player Info")]
    public GameObject Character;
    public CHARACTERTYPE ctype;   //  ĳ���� Ÿ��
    public CharacterComponent CC; // ĳ���� ������Ʈ
    public float MaxHp = 100;
    public float hp = 100;
    public float MaxMp = 100;
    public float def = 0; // ��ȣ��
    public float mp = 100;
    public float atk = 20;
    public float atkSpd = 1f;
    public float cri = 0;
    public float criDmg = 1.5f;
    public float speed = 3f;
    
    public int lv = 1;
    public int exp = 0; // ���� ����ġ
    public int[] maxExp = { 100, 140, 196  ,100000000}; // Level UP �ϱ���� �ʿ��� ����ġ

    //�׾����� �Ǵ��ϴ� ����
    public bool isDead = false;
    
    private void Awake()
    {
        i = this;
    }

    public void  selectCharacter() // Ȱ��ȭ�� ĳ���� ã��
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
    public void GetExp() //������ �ý���
    {
        IncreaseExperience(10);

        if (exp >= maxExp[lv])
        {
            CC.LevelUp(); //ĳ���ͺ��� ������ �нú� Ư�� ����
            lv++;
            exp = 0;
        }
    }

    public void IncreaseExperience(int expAmount) //����ġ ����
    {
        exp += expAmount;
        Debug.Log("����ġ ȹ��");
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

