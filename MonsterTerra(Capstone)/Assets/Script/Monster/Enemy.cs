using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //�ִϸ��̼� �κ�,��������Ʈ �κ� �� �ּ�ó�� 
    public float speed; // Monster Speed Setting
    public float hp; // current hp
    public float maxHp; // max hp
    public float damage; // max hp
    public float defense; // max hp

    //public RuntimeAnimatorController[] animCon; // status
    public Rigidbody2D target; // target
    public GameObject expPrefab;
    private Exp expScript;

    bool isLive; //Live or Dead
    bool isStunned;
    float stunDuration;

    Rigidbody2D rigid;
    [SerializeField]Animator anit;
    //SpriteRenderer spriter;
    //Animator anim;
    WaitForFixedUpdate wait;
    [SerializeField] BoxCollider2D coll;
    EnemyBehavior enemyBehavior;

    private float nextFireTime;
    private float nextChargeTime;
    public float fireRange = 5f; // distance
    public float chargeRange = 5f; // distance
    WeaponType_P Wtype_P;
    WeaponType_E Wtype_E;
    bool isWaitingToFire = false;


    float animationLength;
    bool startani = true;
    public enum WeaponType_P
    {
        Status_KnockBack,
        Status_Stun,
        Status_Burn,
        Status_Frostbite,
        Status_Poison,
        Status_Weaken,
        Status_Bleed,
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

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        //spriter = GetComponent<SpriteRenderer>();
        //anim = GetComponent<Animator>();
        anit = transform.Find("z_MonsterRush1").GetChild(0).GetComponent<Animator>();
        AnitTime();
        wait = new WaitForFixedUpdate();
        coll = GetComponent<BoxCollider2D>();
        enemyBehavior = GetComponent<EnemyBehavior>();
    }
    private void Update()
    {

    }
    void FixedUpdate()
    {
        if (!isLive || isStunned) return; // Live Check

        Wtype_E = WeaponType_E.Charge_1;

        // monster type
        switch (Wtype_E)
        {
            case WeaponType_E.Charge_1: //wait 2sec + charge
                if (Time.time > nextChargeTime && Vector2.Distance(transform.position, target.position) <= chargeRange)
                {
                    Charge(target.position);
                    nextChargeTime = Time.time + 5f; // 5-2sec
                }
                else if (enemyBehavior.isCharging == false)
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
                        WalkMaintain();
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
                    WalkMaintain();
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
                        WalkMaintain();
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
                    WalkMaintain();
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
        if (target.position.x > rigid.position.x) //���⿡ ���� �̹��� ��ȯ
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (target.position.x < rigid.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        //spriter.flipX = target.position.x < rigid.position.x; // monster Direction
    }

    void OnEnable()
    {
        target = GameObject.Find("Player").GetComponent<Rigidbody2D>(); // target init
        isLive = true;
        if (coll != null)
        {
            coll.enabled = true;
        }
        if (rigid != null)
        {
            rigid.simulated = true;
        }
        //spriter.sortingOrder = 2;
        //anim.SetBool("Dead", false);
        hp = maxHp;
    }

    public void Init(SpawnData data)
    {
        //anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHp = data.hp;
        hp = data.hp;
        damage = data.damage;
        defense = data.defense;
        isStunned = false;
        stunDuration = 3f;
        nextFireTime = Time.time;
        nextChargeTime = Time.time;
    }

    void OnTriggerEnter2D(Collider2D collision) //Damage or Dead
    {

        if (!collision.CompareTag("Bullet") || !isLive) return;

        hp -= collision.GetComponent<BulletComponent>().dmg; // hp - damage

        Wtype_P = WeaponType_P.Status_KnockBack;

        switch (Wtype_P)
        {
            case WeaponType_P.Status_KnockBack:
                StartCoroutine(KnockBack()); //knockback
                break;
            case WeaponType_P.Status_Stun:
                StartCoroutine(Stun(stunDuration)); //stun
                break;
            case WeaponType_P.Status_Burn:
                StartCoroutine(Burn(stunDuration, 1f, 1f)); //burn dot-dmg
                break;
            case WeaponType_P.Status_Frostbite:
                StartCoroutine(Frostbite(stunDuration, 1f)); //frostbite -spd
                break;
            case WeaponType_P.Status_Poison:
                StartCoroutine(Poison(stunDuration, 1f)); //poison -dmg
                break;
            case WeaponType_P.Status_Weaken:
                StartCoroutine(Weaken(stunDuration, 1f)); //weaken -def
                break;
            case WeaponType_P.Status_Bleed:
                StartCoroutine(Bleed(stunDuration, 1f, 1f)); //bleed -all
                break;
            default:
                //more
                break;
        }
        if (hp > 0)
        {
            //anim.SetTrigger("Hit");
        }
        else //die setting
        {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            //spriter.sortingOrder = 1;
            //anim.SetBool("Dead", true);
            GamePlayerManager.i.GetExp();
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
    //Move
    private void WalkToTarget()
    {
        Vector2 dirVec = target.position - rigid.position; // target Direction
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // next pos
        rigid.MovePosition(rigid.position + nextVec); // next pos move
        rigid.velocity = Vector2.zero; // velocity init
    }

    private void WalkMaintain()
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

    void Fire(Vector2 targetPosition)
    {
        enemyBehavior.Shoot(targetPosition);
    }

    void HomingMissile(Vector2 targetPosition)
    {
        enemyBehavior.ShootHoming(targetPosition);
    }

    void Charge(Vector2 targetPosition)
    {
        StartCoroutine(enemyBehavior.Charging(targetPosition, speed * 2));
    }

    /// <summary>
    /// Status abnormality
    /// </summary>
    /// <param name="Status"></param>
    /// 
    IEnumerator KnockBack() // knockback
    {
        yield return wait;
        Vector3 playerPos = target.position; //player pos
        Vector3 dirVec = transform.position - playerPos; // direct
        rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse); // 5px
    }

    IEnumerator Stun(float duration)
    {
        isStunned = true;   //stun
        yield return new WaitForSeconds(duration); //stuntime
        isStunned = false;  //stun fin
    }

    IEnumerator Burn(float duration, float damage, float interval)
    {
        float timer = 0f;

        while (timer < duration)
        {
            if (hp < 0)
                yield break;

            yield return new WaitForSeconds(interval);
            // Apply damage to rigid's health
            hp -= damage;
            timer += interval;
        }
    }

    IEnumerator Frostbite(float duration, float slowAmount)
    {
        float orgSpd = speed;

        // Reduce player's movement speed
        speed *= slowAmount;

        yield return new WaitForSeconds(duration);

        // Restore player's movement speed
        speed = orgSpd;
    }

    IEnumerator Poison(float duration, float damageReduction)
    {
        float orgAtk = damage;

        // Reduce rigid's attack power
        damage -= damageReduction;
        yield return new WaitForSeconds(duration);
        // Restore rigid's attack power
        damage = orgAtk;
    }

    IEnumerator Weaken(float duration, float defenseReduction)
    {
        float orgDef = defense;

        // Reduce rigid's defense
        defense -= defenseReduction;
        yield return new WaitForSeconds(duration);
        // Restore rigid's defense
        defense = orgDef;
    }

    IEnumerator Bleed(float duration, float damageReduction, float defenseReduction)
    {
        float orgAtk = damage;
        float orgDef = defense;

        // Reduce both attack power and defense
        damage -= damageReduction;
        defense -= defenseReduction;

        yield return new WaitForSeconds(duration);

        // Restore both attack power and defense
        damage = orgAtk;
        defense = orgDef;
    }

    void Dead() // Animation-enemy-Dead
    {
        gameObject.SetActive(false);
    }

    //default ���� 0���� ȿ���� �ְ� ũ��Ƽ�� �������� Ȯ���� ���� �� �ְ� �����ϱ�!
    public void TakeDamage(float dmg)
    {
        hp -= dmg;
        if (hp > 0)
        {
            anit.SetTrigger("Hit");
            //hit ����
        }
        else //die setting
        {
            StartCoroutine(Die(animationLength));
            //isLive = false;
            //coll.enabled = false;
            //rigid.simulated = false;
            if (expPrefab != null)
            {
                // random position
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

    IEnumerator Die(float anitTime)
    {
        isLive = false;
        coll.enabled = false;
        rigid.simulated = false;
        anit.SetTrigger("Dead");
        yield return new WaitForSeconds(anitTime);
        gameObject.SetActive(false);
    }

    void AnitTime()
    {
        // �ִϸ��̼� Ŭ���� ���̸� ������
        AnimationClip[] clips = anit.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "4_Death") // �ִϸ��̼� Ŭ���� �̸��� ����
            {
                animationLength = clip.length;
                break;
            }
        }
    }
}