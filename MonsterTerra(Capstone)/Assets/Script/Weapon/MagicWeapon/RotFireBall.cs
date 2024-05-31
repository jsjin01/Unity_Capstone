using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotFireBall : MonoBehaviour
{
    float atk;
    float cridmg;
    float cri;
    int etype = 0;
    float dur = 0;
    float amount = 0;

    float parAngle = 0;

    [SerializeField] GameObject firebullet;

    private void Start()
    {
        Invoke("FireDestroy", 1.05f);
    }

    private void FireDestroy() //파괴될 때 파이어볼 불릿이 생성되도록 설정
    {
        float angle = Mathf.Atan2(GamePlayerMoveControl.i.playerDir.y, GamePlayerMoveControl.i.playerDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        gameObject.transform.parent.parent.rotation = Quaternion.Euler(0f, 0f, parAngle);
        GameObject fire = Instantiate(firebullet, transform.position, rotation);
        fire.GetComponentInChildren<MagicBulletFire>().SetAttack(atk, cridmg, cri, dur, amount, etype);
        fire.GetComponentInChildren<MagicBulletFire>().Move(GamePlayerMoveControl.i.playerDir);

        Destroy(gameObject.transform.parent.parent.gameObject);
    }

    public void SetAttack(float _atk, float _cridmg, float _cri, float _dur = 0, float _amount = 0, int _etype = 0, GameObject obj = null) //데미지 설정
    {
        atk = _atk;
        cridmg = _cridmg;
        cri = _cri;

        etype = _etype;
        dur = _dur;
        amount = _amount;
    }

    public void setCircleRot(float angle)
    {
        parAngle = angle - 50f;
    }

}
