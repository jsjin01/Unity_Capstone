using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallCircle : MonoBehaviour
{
    //-50도
    float atk;
    float cridmg;
    float cri;
    int etype = 0;
    float dur = 0;
    float amount = 0;
    bool Max =false;

    [SerializeField]GameObject fireball; //파이어볼 객체

    private void Start()
    {
        Destroy(gameObject, 1.1f);
    }
    private void Update()
    {
        transform.position = GamePlayerMoveControl.i.playerPos;
    }
    public void SetAttack(float _atk, float _cridmg, float _cri, bool _Max, float _dur = 0, float _amount = 0, int _etype = 0, GameObject obj = null) //데미지 설정
    {
        atk = _atk;
        cridmg = _cridmg;
        cri = _cri;

        etype = _etype;
        dur = _dur;
        amount = _amount;
        Max = _Max;

        CreatFireBall(Max);
    }

    public void CreatFireBall(bool Max)
    {
        float angle;
        if (!Max)
        {
            angle = 360 / 5f;
            for (int i = 0; i < 5; i++)
            {
                Quaternion rot = Quaternion.Euler(0, 0, (-i)*angle);
                GameObject fireballcircle = Instantiate(fireball, GamePlayerMoveControl.i.playerPos, rot , transform);
                fireballcircle.GetComponentInChildren<RotFireBall>().SetAttack(atk, cridmg, cri, dur, amount, etype);
                fireballcircle.GetComponentInChildren<RotFireBall>().setCircleRot((-i) * angle);
            }
        }
        else if (Max)
        {
            angle = 360 / 25f;
            for (int i = 0; i < 25; i++)
            {
                Quaternion rot = Quaternion.Euler(0, 0, (-i) * angle);
                GameObject fireballcircle = Instantiate(fireball, GamePlayerMoveControl.i.playerPos, rot, transform);
                fireballcircle.GetComponentInChildren<RotFireBall>().SetAttack(atk, cridmg, cri, dur, amount, etype);
                fireballcircle.GetComponentInChildren<RotFireBall>().setCircleRot((-i) * angle);
            }
        }


    }
}
