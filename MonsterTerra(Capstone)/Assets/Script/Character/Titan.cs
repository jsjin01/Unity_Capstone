using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titan : CharacterComponent
{
    private void OnEnable()
    {
        Debug.Log("ĳ���Ͱ� �Ҵ�Ǿ����ϴ�.");
    }
    public override void CharaterSkill()
    {
        throw new System.NotImplementedException();
    }

    public override void LevelUp()
    {
        Debug.Log("�нú� �۵� ��");
        GamePlayerManager.i.MaxHp += 20; //�������ÿ� �ִ� ü�� ����
        GamePlayerManager.i.hp += 20;  //�ִ� ü�� ������ ��ŭ ����
    }

}
