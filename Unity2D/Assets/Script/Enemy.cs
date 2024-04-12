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
        Charge_1,
        Charge_2,
        Charge_3,
        Shoot_1,
        Shoot_2,
        Shoot_3,
        Shoot_4,
        Boss_1,
        Boss_2,
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

        Wtype_E = WeaponType_E.Charge_3;

        // monster type
        switch (Wtype_E)
        {
            case WeaponType_E.Charge_1: //wait 2sec + charge
                if (Time.time > nextChargeTime && Vector2.Distance(transform.position, target.position) <= chargeRange)
                {
                    Charge(target.position);
                    nextChargeTime = Time.time + 5f; // 5-2sec
                }
                else if (!Weapon_E.isCharging)
                {
                    WalkToTarget();
                }
                break;
            case WeaponType_E.Charge_2: //2sec follow
                if (Time.time > nextChargeTime && Vector2.Distance(transform.position, target.position) <= chargeRange)
                {
                    StartCoroutine(RunToTarget(3f, 2f)); //speed, duration
                    nextChargeTime = Time.time + 5f; // 5-2sec
                }
                else
                {
                    WalkToTarget();
                }
                break;
            case WeaponType_E.Charge_3: //2sec follow stop * 3
                if (Time.time > nextChargeTime && Vector2.Distance(transform.position, target.position) <= chargeRange)
                {
                    StartCoroutine(RunToTarget_R(3f, 2f)); //speed, duration
                    nextChargeTime = Time.time + 5f; // 5-2sec
                }
                else
                {
                    WalkToTarget();
                }
                break;
            case WeaponType_E.Shoot_1: // shoot and stop
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
            case WeaponType_E.Shoot_2: // shoot and move
                if (Time.time > nextFireTime && Vector2.Distance(transform.position, target.position) <= fireRange)
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
                break;
            case WeaponType_E.Shoot_3: // shoot and stop
                if (Time.time > nextFireTime)
                {
                    if (Vector2.Distance(transform.position, target.position) <= fireRange)
                    {
                        HomingMissile(target.position);
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
            case WeaponType_E.Shoot_4: // shoot and move
                if (Time.time > nextFireTime && Vector2.Distance(transform.position, target.position) <= fireRange)
                {
                    HomingMissile(target.position);
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

        Wtype_P = WeaponType_P.P1;

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

    private void WalkToTarget()
    {
        Vector2 dirVec = target.position - rigid.position; // target Direction
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // next pos
        rigid.MovePosition(rigid.position + nextVec); // next pos move
        rigid.velocity = Vector2.zero; // velocity init
    }

    IEnumerator RunToTarget(float newSpeed, float duration)
    {
        float originalSpeed = speed;
        speed = newSpeed;
        yield return new WaitForSeconds(duration);
        speed = originalSpeed;
    }

    IEnumerator RunToTarget_R(float newSpeed, float duration)
    {
        float originalSpeed = speed;
        speed = newSpeed;
        yield return new WaitForSeconds(duration);
        speed = originalSpeed;
        yield return new WaitForSeconds(duration);

        speed = newSpeed;
        yield return new WaitForSeconds(duration);
        speed = originalSpeed;
        yield return new WaitForSeconds(duration);

        speed = newSpeed;
        yield return new WaitForSeconds(duration);
        speed = originalSpeed;

        StartCoroutine(Stun(2f)); //sturn
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
        Weapon_E.Shoot(targetPosition);
    }

    void HomingMissile(Vector2 targetPosition)
    {
        Weapon_E.ShootHoming(targetPosition);
    }

    void Charge(Vector2 targetPosition)
    {
        StartCoroutine(Weapon_E.Charging(targetPosition, 3f));
    }

    void Dead() // Animation-enemy-Dead
    {
        gameObject.SetActive(false);
    }
}
