using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STICK
{
    SHOVEL,
    SCYCHE,
    TRIDENT
}
public class StickWeapon : MonoBehaviour
{
    int atk;
    float atkSpd = 0.1f;
    public STICK type;
    bool isAttack = true;

    [SerializeField] Rigidbody2D rd;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Animator anit;
    [SerializeField] Sprite[] StickSprite;
    IEnumerator atkcolCor;

    private void OnEnable()
    {
        WeaponComponent.i.sEvt += () =>
        {
            StopAllCoroutines();
            isAttack = true;
            anit.StopPlayback();
        };
    }

    // Update is called once per frame
    void Update()
    {
        type = WeaponComponent.i.s;
        ChangeSprite(type);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isAttack)
            {
                Attack();
            }
        }
    }
    void Attack()
    {
        anit.SetTrigger("attack");
        if(atkcolCor == null)
        {
            atkcolCor = AttackCol();
        }
        StartCoroutine(atkcolCor);
    }

    IEnumerator AttackCol()//Atk Rate
    {
        isAttack = false;
        yield return new WaitForSeconds(atkSpd);
        isAttack = true;
    }

    void ChangeSprite(STICK s)
    {
        if (s == STICK.SHOVEL)
        {
            sr.sprite = StickSprite[0];
        }
        else if (s == STICK.SCYCHE)
        {
            sr.sprite = StickSprite[1];
        }
        else if (s == STICK.TRIDENT)
        {
            sr.sprite= StickSprite[2];
        }
    }



}
