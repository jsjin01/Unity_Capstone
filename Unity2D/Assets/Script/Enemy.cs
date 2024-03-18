using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed; // Monster Speed Setting
    public Rigidbody2D target; // target

    bool isLive = true; //Live or Dead

    Rigidbody2D rigid;
    SpriteRenderer spriter;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void fixedUpdate()
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
}
