using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    public float destroyDelay = 10f;

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
            // GameManager�� �ν��Ͻ��� �����ɴϴ�.
            GameManager gameManager = GameManager.instance;

            // GameManager �ν��Ͻ��� �����ϴ��� Ȯ���մϴ�.
            if (gameManager != null)
            {
                // ����ġ�� ������ŵ�ϴ�.
                gameManager.GetExp();

                // �� ������Ʈ�� �����մϴ�.
                Destroy(gameObject);
            }
        }
    }
}
