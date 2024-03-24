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

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!isLive) return; // Live Check

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
        hp = maxHp;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHp = data.hp;
        hp = data.hp;
    }
}
