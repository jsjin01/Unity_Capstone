using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    //���ݷ�, ũ��Ƽ�� ������, ũ��Ƽ�� Ȯ��, ȿ�� �ο�
    float atk;
    float cridmg;
    float cri;
    int etype = 0;
    Rigidbody2D rb;

    [SerializeField] GameObject parent;
    [SerializeField] float dTime = 1.0f;

    [SerializeField]SOTYPE Type;     //������ �Ѿ����� ����
    [SerializeField] bool isguided = false;  //�����Ǵ��� ����

    public void Move(Vector3 p)      //�� �������� �� ����      
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        rb.velocity = p.normalized * speed; //���������� ������ �κ�
    }

    public void SetAttack(float _atk, float _cridmg, float _cri, int _etype = 0, GameObject obj = null) //������ ����
    {
        atk = _atk;
        cridmg = _cridmg;
        cri = _cri;
        etype = _etype;
    }
    private void Start()
    {
        Invoke("DestroyBullet", dTime); //���� �ð� ���� ����
    }

    void DestroyBullet()
    {
        Destroy(parent); //�����ð� ���� ����
    }

    private void OnTriggerEnter2D(Collider2D collision) //���Ͷ� �浹�ϸ� ����
    {
        if (collision.CompareTag("Monster"))
        {
            collision.GetComponent<Enemy>().TakeDamage(atk, cridmg, cri, etype); // ���Ϳ��� ������ �ִ� �κ�
            CancelInvoke("DestroyBullet");
            DestroyBullet();
        }
    }

    private void GuidedBullet() //�����Ǵ� �κ�
    {
        //���͵� �� �Ѿ˰� ���� ����� �κп� �ִ� ���Ϳ��� ����
    }
}
