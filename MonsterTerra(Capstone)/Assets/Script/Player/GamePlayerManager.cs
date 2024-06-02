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
    public float def = 0; // ��ȣ��
    public float MaxMp = 100;
    public float mp = 100;
    public float MaxDash = 100;
    public float dash = 100;
    public float atk = 20;
    public float atkSpd = 1f;
    public float cri = 0;
    public float criDmg = 1.5f;
    public float speed = 3f;
    public float DmgAdd = 1f;
    public float Cbuff = 1f; // ĳ���Ϳ��� ����Ǵ� ����
    public GameObject uiCard;

    //Item â ����
    public int[] item = {0, 0, 0, 0 };

    public int lv = 1;
    public int exp = 0; // ���� ����ġ
    public int MaxExp = 50; //ó���� ������ �ϴµ� �ʿ��� ����ġ

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
    public void GetExp(int expAmount) //������ �ý���
    {
        IncreaseExperience(expAmount);

        if (exp >= MaxExp)
        {
            CC.LevelUp(); //ĳ���ͺ��� ������ �нú� Ư�� ����
            lv++;
            exp = 0;
            MaxExp = (int)(MaxExp*1.4f);
            SoundManger.i.PlaySound(3);
            uiCard.SetActive(true);
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

    //Item
    public void setItme(int idx)
    {
        for (int i = 0; i < 4; i++)
        {
            if (item[i] == 0)
            {
                //������ �迭�� �ƹ��͵� ������ �ش� ��ȣ�� �������� ����
                item[i] = idx;
                UIManager.i.ItemImgChange(i,idx);
                Debug.Log("�������� " + i + "�ڸ��� �����ϴ�."+ idx);
                break;
            }
        }
    }

    public void UseItme(int idx)
    {
        SoundManger.i.PlaySound(12);
        if (idx == 1)
        {
            StartCoroutine(Anger());
        }
        else if(idx == 2)
        {
            StartCoroutine(Concentration());
        }
        else if(idx == 3)
        {
            StartCoroutine(Rapture());
        }
        else if(idx == 4)
        {
            StartCoroutine(Challenge());
        }
        else if(idx == 5)
        {
            float healing = MaxHp * 0.3f * Cbuff;
            hp += healing;
            if(hp > MaxHp)
            {
                hp = MaxHp;
            }
        }

    }

    //������ �ڷ�ƾ
    IEnumerator Anger() //�г��� ����
    {
        UIManager.i.BuffOn(5);
        atk += (5 * Cbuff);
        yield return new WaitForSeconds(10f * Cbuff);
        atk -= (5 * Cbuff);
        UIManager.i.BuffOff(5);
    }

    IEnumerator Concentration() //������ ����
    {
        UIManager.i.BuffOn(6);
        float atkspdup = atkSpd * 0.2f * Cbuff;
        atkSpd -= atkspdup;
        yield return new WaitForSeconds(10f * Cbuff);
        atkSpd += atkspdup;
        UIManager.i.BuffOff(6);
    }

    IEnumerator Rapture()
    {
        UIManager.i.BuffOn(7);
        float spd = speed * 0.1f * Cbuff;
        speed += spd;
        yield return new WaitForSeconds(10f * Cbuff);
        speed -= spd;
        UIManager.i.BuffOff(7);
    }

    IEnumerator Challenge()
    {
        UIManager.i.BuffOn(8);
        DmgAdd += 0.3f;
        yield return new WaitForSeconds(10f * Cbuff);
        DmgAdd -= 0.3f;
        UIManager.i.BuffOff(8);
    }

}

