using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public enum InfoType { Exp, Lv, Hp }
    public InfoType type;

    void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[GameManager.instance.lv];
                break;

            case InfoType.Lv:

                break;

            case InfoType.Hp:

                break;
        }
    }
}
