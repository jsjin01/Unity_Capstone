using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRain : CharacterComponent
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
        GamePlayerManager.i.atk += 1; //�������ÿ� ������ 1����
    }

}
