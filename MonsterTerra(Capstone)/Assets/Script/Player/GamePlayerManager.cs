using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerManager : MonoBehaviour
{
    public static GamePlayerManager i;
    [Header("# Player Info")]
    public GameObject Character;
    public float MaxHp = 100;
    public float hp = 100;
    public int atk = 20;
    public float atkSpd = 1f;
    public float cri = 0;
    public float criDmg = 1.5f;
    public float speed = 3f;
    public int lv = 1;
    public int exp = 0; // ���� ����ġ
    public int[] maxExp = { 100, 140, 196  ,100000000}; // Level UP �ϱ���� �ʿ��� ����ġ
    
    private void Awake()
    {
        i = this;
        selectCharacter();// ������ ĳ���� ����
    }

    void  selectCharacter()
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
        }  // Ȱ��ȭ�� ĳ���� ã�� 
    }
    public void GetExp() //������ �ý���
    {
        IncreaseExperience(10);

        if (exp >= maxExp[lv])
        {
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

