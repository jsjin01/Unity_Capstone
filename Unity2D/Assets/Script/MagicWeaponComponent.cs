using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MAGIC
{
    ICE,
    FIRE,
    THUNDER
}
public class MagicWeaponComponent : MonoBehaviour
{

    [SerializeField] GameObject magicSkill;
    [SerializeField] SpriteRenderer magicSkillSr;
    [SerializeField] SpriteRenderer magicSr;
    int atk;
    float atkSpd = 5f;
    public MAGIC type;
    bool isAttack = true;

    private void OnEnable()
    {
        WeaponComponent.i.mEvt += () =>
        {
            StopAllCoroutines();
            isAttack = true;
            magicSkill.SetActive(false);
        };
    }

    // Update is called once per frame
    void Update()
    {
        type = WeaponComponent.i.m;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isAttack)
            {
                Attack();
            }
        }
        ChangeSprite(type);
    }

    void Attack()
    {
        StartCoroutine(StartAttack());
    }
    IEnumerator AttackCol()
    {
        isAttack = false;
        yield return new WaitForSeconds(atkSpd);
        isAttack = true;
    }

    IEnumerator StartAttack()//Atk Rate
    {
        magicSkill.SetActive(true);
        yield return new WaitForSeconds(1f);
        magicSkill.SetActive(false);
        StartCoroutine(AttackCol());
    }

    void ChangeSprite(MAGIC m)
    {
        if(m == MAGIC.ICE)
        {
            magicSr.color = Color.blue;
            magicSkillSr.color = Color.blue;
        }
        else if(m == MAGIC.FIRE)
        {
            magicSr.color = Color.red;
            magicSkillSr.color = Color.red;
        }
        else if(m == MAGIC.THUNDER)
        {
            magicSr.color = Color.yellow;
            magicSkillSr.color = Color.yellow;
        }

    }
}
