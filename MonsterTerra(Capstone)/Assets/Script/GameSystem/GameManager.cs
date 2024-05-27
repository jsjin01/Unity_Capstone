using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager i;

    [Header("# Game Control")]
    public bool isLive; // ������ ���ۉ���� �ƴ��� �Ǵ��ϴ� ����
    public float gameTime;

    [Header("# Game Object")]
    public EnemyPoolManager Epool;
    public GameObject uiMain;
    public GameObject uiResult;

    private void Awake()
    {
        i = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLive) return;

        gameTime += Time.deltaTime;
    }

    public void GameStart()
    {
        isLive = true;
    }

    public void GameOver() //player ����� ȣ��
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine() //���� �� 0.5�� �� ���â �ߵ��� ����
    {
        isLive=false;

        yield return new WaitForSeconds(0.5f);

        uiMain.SetActive(false);
        uiResult.SetActive(true);
    }

    public void GameRetry() //�ٽ� â�� ��� �ʱ�ȭ
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame() // ���ӿ��� ������
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
