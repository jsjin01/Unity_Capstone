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
    public GameObject expPrefab;
    private Exp expScript;

    bool isLive; //Live or Dead
    bool isStunned;
    float stunDuration;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    WaitForFixedUpdate wait;
    Collider2D coll;
    EnemyWeapon Weapon_E;

    private float nextFireTime;
    private float nextChargeTime;
    public float fireRange = 5f; // distance
    public float chargeRange = 5f; // distance
    WeaponType_P Wtype_P;
    WeaponType_E Wtype_E;
    bool isWaitingToFire = false;


    public enum WeaponType_P
    {
        P1, P2, P3,
    }

    public enum WeaponType_E
    {
        E1, E2, E3,
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
        coll = GetComponent<Collider2D>();
        Weapon_E = GetComponent<EnemyWeapon>();
    }

    void FixedUpdate()
    {
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit") || isStunned) return; // Live Check

        Wtype_E = WeaponType_E.E1;

        // monster type
        switch (Wtype_E)
        {
            case WeaponType_E.E1:
                if (Time.time > nextFireTime)
                {
                    if (Vector2.Distance(transform.position, target.position) <= fireRange)
                    {
                        Fire(target.position);
                        nextFireTime = Time.time + 2f;
                    }
                    else
                    {
                        // calc relative
                        Vector2 relativePositionToPlayer = target.position - (Vector2)transform.position;
                        Vector2 targetPosition;

                        // distance maintain
                        if (relativePositionToPlayer.magnitude > fireRange)
                        {
                            targetPosition = target.position;
                        }
                        else //move
                        {
                            targetPosition = (Vector2)transform.position - relativePositionToPlayer.normalized * fireRange;
                        }

                        // move direction
                        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

                        // move
                        Vector2 nextVec = direction * speed * Time.fixedDeltaTime;
                        rigid.MovePosition((Vector2)rigid.position + nextVec);
                    }
                }
                break;
            case WeaponType_E.E2:
                if (Time.time > nextChargeTime)
                {
                    if (Vector2.Distance(transform.position, target.position) <= chargeRange)
                    {
                        Charge(target.position);
                        nextChargeTime = Time.time + 2f;
                    }
                    else
                    {
                        Vector2 dirVec = target.position - rigid.position; // target Direction
                        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // nex pos
                        rigid.MovePosition(rigid.position + nextVec); // nex pos move
                        rigid.velocity = Vector2.zero; // velocity init
                    }
                }
                break;
            default:
                //more
                break;
        }
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
        isStunned = false;
        stunDuration = 3f;
        nextFireTime = Time.time;
        nextChargeTime = Time.time;
    }

    void OnTriggerEnter2D(Collider2D collision) //Damage or Dead
    {
        if (!collision.CompareTag("Bullet") || !isLive) return;

        hp -= collision.GetComponent<BulletComponet>().dmg; // hp - damage

        Wtype_P = WeaponType_P.P2;

        switch (Wtype_P)
        {
            case WeaponType_P.P1:
                StartCoroutine(KnockBack()); //knockback
                break;
            case WeaponType_P.P2:
                StartCoroutine(Stun(stunDuration)); //sturn
                break;
            default:
                //more
                break;
        }
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
            //GameManager.instance.GetExp();
            if (expPrefab != null)
            {
                // randdom position
                Vector2 currentPosition = transform.position;
                float randomX = Random.Range(-0.5f, 0.5f);
                float randomY = Random.Range(-0.5f, 0.5f);

                // 1px = 0.01f
                Vector2 randomOffset = new Vector2(randomX, randomY) * 0.01f;

                // ExpPrefab spawn
                GameObject expObj = Instantiate(expPrefab, currentPosition + randomOffset, Quaternion.identity);
            }
        }
    }

    IEnumerator KnockBack() // knockback
    {
        yield return wait;
        Vector3 playerPos = GameManager.instance.player.transform.position; //player pos
        Vector3 dirVec = transform.position - playerPos; // direct
        rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse); // 5px
    }

    IEnumerator Stun(float duration)
    {
        isStunned = true;   //stun
        yield return new WaitForSeconds(duration); //stuntime
        isStunned = false;  //stun fin
    }

    void Fire(Vector2 targetPosition)
    {
        Weapon_E.ShootAtPlayer(targetPosition);
    }

    void Charge(Vector2 targetPosition)
    {
        StartCoroutine(Weapon_E.Charging(targetPosition, 5f));
    }

    void Dead() // Animation-enemy-Dead
    {
        gameObject.SetActive(false);
    }
}
