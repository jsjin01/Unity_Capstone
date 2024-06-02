using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    public float destroyDelay = 10f;
    public int amount = 10;
    // ExpPrefab�� ������ �� ȣ��Ǵ� �Լ�
    void Start()
    {
        Debug.Log("Exp ��ũ��Ʈ�� ����Ǿ����ϴ�.");
        // ���� �� ���� �ð� �Ŀ� ����
        Destroy(gameObject, destroyDelay);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ������Ʈ�� "Player" �±׸� ���� �÷��̾����� Ȯ���մϴ�.
        if (collision.CompareTag("Player"))
        {
            GamePlayerManager.i.GetExp(amount);
            Destroy(gameObject);
        }
    }
}
