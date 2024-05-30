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
    public float mp = 100;
    public int atk = 20;
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
}

