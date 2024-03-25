using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    GunWeaponComponent gunWeaponComponent;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gunWeaponComponent = GetComponent<GunWeaponComponent>();
    }


    void Start()
    {
        
    }

    
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        //gunWeaponComponent.Update();
    }

    void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec); //Player move
    }

    void LateUpdate()
    {
        anim.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)  //flip left and right
        {
            spriter.flipX = inputVec.x < 0;
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive)
            return;

        GameManager.instance.hp -= Time.deltaTime *  10;

        if (GameManager.instance.hp < 0)
        {
            for (int index=2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }

            anim.SetTrigger("Dead");
        }
    }


}
