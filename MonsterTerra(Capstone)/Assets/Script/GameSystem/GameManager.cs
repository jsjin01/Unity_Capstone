using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager i;

    [Header("# Game Control")]
    public bool isLive; // 게임이 시작됬는지 아닌지 판단하는 변수
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

    public void GameOver() //player 사망시 호출
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine() //죽은 후 0.5초 후 결과창 뜨도록 설정
    {
        isLive=false;

        yield return new WaitForSeconds(0.5f);

        uiMain.SetActive(false);
        uiResult.SetActive(true);
    }

    public void GameRetry() //다시 창을 띄워 초기화
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame() // 게임에서 나가기
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
