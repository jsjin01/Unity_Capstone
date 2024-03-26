using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DIRECTION
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}
public class Player : MonoBehaviour
{
    public static Player i;
    public Vector2 inputVec;
    public float speed;
    public DIRECTION dir = DIRECTION.RIGHT;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    GunWeaponComponent gunWeaponComponent;

    void Awake()
    {
        i = this;
    }


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gunWeaponComponent = GetComponent<GunWeaponComponent>();
    }

    
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        //Set the Player Direction
        if (Input.GetKeyDown(KeyCode.D))
        {
            dir = DIRECTION.RIGHT;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        { 
            dir = DIRECTION.LEFT; 
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            dir = DIRECTION.UP;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            dir = DIRECTION.DOWN;
        }
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
