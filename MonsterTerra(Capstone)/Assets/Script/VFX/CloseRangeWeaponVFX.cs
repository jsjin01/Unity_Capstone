using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRangeWeaponVFX : MonoBehaviour
{
    float atk = 5;
    float cridmg = 0;
    float cri = 0;
    int etype = 0;
    [SerializeField] GameObject parent;
    [SerializeField] Vector2 target;
    [SerializeField] float dTime = 1.0f;

    private void Start()
    {
        Destroy(parent, dTime);
    }

    private void Update()
    {
        target = GamePlayerMoveControl.i.playerPos;
        parent.transform.position = target;
    }

    public void SetAttack(float _atk, float _cridmg, float _cri, int _etype = 0, GameObject obj = null) //데미지 설정
    {
        atk = _atk;
        cridmg = _cridmg;
        cri = _cri;
        etype = _etype;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            //몬스터에게 데미지 주는 부분
            collision.GetComponent<Enemy>().TakeDamage(atk,cridmg,cri,etype);
            Debug.Log("takeDamage");
        }
    }

    public void DestroyWeapon(float dt = 0)
    {
        Destroy(parent, dt);
    }
}
