using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private float playTimeMax = 30f;
    private float time_start;
    private float time_current;
    public bool onPlay;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        onPlay = false;
    }

    
    void Update()
    {
        if (!onPlay) return;
        
        // .... on Playing
        check_Time();
    }
    [ContextMenu("StartGame")]
    public void GameStart()
    {
        // 게임 시작 : 제한 시간 측정,  
        Time.timeScale = 1;
        onPlay = true;
        time_start = Time.time;
    }
    [ContextMenu("GameOver")]
    public void GameOver()
    {
        // 플레이 중 캐릭터 사망, 일시정지, 점수 집계 UI, Main 씬 이동 버튼 활성화
        onPlay = false;
        Time.timeScale = 0;
        // Main 
        UIManager.instance.SetActiveGoMain();
    }
    private void check_Time()
    {
        time_current = Time.time - time_start;
        if (time_current > playTimeMax)
        {
            // 제한 시간 종료 = 게임 클리어, 점수 집계 UI, Main 씬 이동 버튼 활성화
            onPlay = false;
            Time.timeScale = 0;
            UIManager.instance.SetActiveGoMain();
        }
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadPlayScene()
    {
        SceneManager.LoadScene(1);
        GameStart();
    }
}
