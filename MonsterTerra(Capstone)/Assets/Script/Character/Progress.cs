using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : CharacterComponent
{
    private void OnEnable()
    {
        //��� �ɷ�ġ �ٿ�
        GamePlayerManager.i.atk = (int)((float)GamePlayerManager.i.atk * 0.7f);
        GamePlayerManager.i.atkSpd *= 1.3f;
        GamePlayerManager.i.speed *= 0.7f;
        GamePlayerManager.i.MaxHp *= 0.7f;
        GamePlayerManager.i.hp = GamePlayerManager.i.MaxHp;

        Debug.Log("ĳ���Ͱ� �Ҵ�Ǿ����ϴ�.");
    }
    public override void CharaterSkill()
    {
        throw new System.NotImplementedException();
    }

    public override void LevelUp()
    {
        Debug.Log("�нú� �۵� ��");
    }

}
