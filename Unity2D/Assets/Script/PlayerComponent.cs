using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    public static PlayerComponent instance;

    Animator anim;
    SpriteRenderer sr;


    private void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Die()
    {
        for (int index = 1; index < transform.childCount; index++)
        {
            transform.GetChild(index).gameObject.SetActive(false);
        }

        anim.SetTrigger("Dead");

        Player.canMove = false; 
    }

    public void TakeDamage()
        {
            if (!GameManager.instance.isLive)
                return;

            GameManager.instance.hp -= Time.deltaTime * 10;

            if (GameManager.instance.hp < 0)
            {
                Die();
            }
        }

    public IEnumerator SetColorCoroutine()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }

    IEnumerator CameraShakeCoroutine()
    {
        CameraController.instance.StartCameraShake(10.0f, 5.0f, 30);
        yield return null;
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
            StartCoroutine(SetColorCoroutine());
            StartCoroutine(CameraShakeCoroutine());
        }
    }

    public void RecoveryHp(int val)
    {
        Debug.Log("현재 체력 " + GameManager.instance.hp);

        GameManager.instance.hp += val;

        if(GameManager.instance.hp >= GameManager.instance.maxHp)
        {
            GameManager.instance.hp = GameManager.instance.maxHp;
        }

        Debug.Log("포션 회득 후 회복된 체력 " + GameManager.instance.hp);
    }

    
}
