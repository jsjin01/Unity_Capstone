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

    public static bool canMove = true;
    HandControl handControl;

    void Awake()
    {
        i = this;
        handControl = GetComponentInChildren<HandControl>();
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
        if (canMove)
        {
            inputVec.x = Input.GetAxisRaw("Horizontal");
            inputVec.y = Input.GetAxisRaw("Vertical");

            //Set the Player Direction
            if (inputVec.x > 0)
            {
                dir = DIRECTION.RIGHT;
            }
            else if (inputVec.x < 0)
            {
                dir = DIRECTION.LEFT;

            }
            else if (inputVec.y > 0)
            {
                dir = DIRECTION.UP;
            }
            else if (inputVec.y < 0)
            {
                dir = DIRECTION.DOWN;
            }


            handControl.SetDirection(dir);
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


   
}
