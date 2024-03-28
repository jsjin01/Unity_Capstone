using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed; // Monster Speed Setting
    public float hp; // current hp
    public float maxHp; // max hp
    public RuntimeAnimatorController[] animCon; // status
    public Rigidbody2D target; // target

    bool isLive; //Live or Dead

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    WaitForFixedUpdate wait;
    Collider2D coll;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
        coll = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return; // Live Check

        Vector2 dirVec = target.position - rigid.position; // target Direction
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // nex pos
        rigid.MovePosition(rigid.position + nextVec); // nex pos move
        rigid.velocity = Vector2.zero; // velocity init
    }

    void LateUpdate()
    {
        if (!isLive) return;

        spriter.flipX = target.position.x < rigid.position.x; // monster Direction
    }

    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>(); // target init
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
        hp = maxHp;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHp = data.hp;
        hp = data.hp;
    }

    void OnTriggerEnter2D(Collider2D collision) //Damage or Dead
    {
        if (!collision.CompareTag("Bullet") || !isLive) return;

        hp -= collision.GetComponent<BulletComponet>().dmg; // hp - damage
        StartCoroutine(KnockBack()); //knockback

        if (hp > 0)
        {
            anim.SetTrigger("Hit");
        }
        else //die setting
        {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
            GameManager.instance.GetExp();
        }
    }

    IEnumerator KnockBack() // knockback
    {
        yield return wait;
        Vector3 playerPos = GameManager.instance.player.transform.position; //player pos
        Vector3 dirVec = transform.position - playerPos; // direct
        rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse); // 5px
    }

    void Dead() // Animation-enemy-Dead
    {
        gameObject.SetActive(false);
    }
}
