using System.Collections;
using System.Collections.Generic;
using Game.Gimmick;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public BalancingSO balancingSO;
    
    private float time_start;
    private float time_current;
    private float time_end;
    public bool onPlay;

    private Coroutine gameFramework;
    public delegate void GameEventHandler();
    public event GameEventHandler OnGameOver;
    
    void Start()
    {
        onPlay = false;
        GameStart();
    }

    private IEnumerator GameFramework() //게임 규칙 및 흐름
    {
        StartCoroutine(ForkFactory.Instance.ForkSpawnRoutine());
        StartCoroutine(ItemSpawnManager.Instance.CreamSpawnRoutine());
        StartCoroutine(FriendFactory.Instance.ForkSpawnRoutine());
        
        yield return new WaitUntil(() => TimeCheck() > balancingSO.playTime);
        //yield return new WaitForSecondsRealtime(balancingSO.playTime);
        
        GameOver();
    }
    
    [ContextMenu("StartGame")]
    public void GameStart()
    {
        // 게임 시작 : 제한 시간 측정,
        Debug.Log("GameStart");
        
        Time.timeScale = 1;
        time_start = Time.time;
        onPlay = true;
        
        if (gameFramework != null) StopCoroutine(gameFramework);
        gameFramework = StartCoroutine(GameFramework());
    }
    
    [ContextMenu("GameOver")]
    public void GameOver()
    {
        // 플레이 중 캐릭터 사망, 일시정지, 점수 집계 UI, Main 씬 이동 버튼 활성화
        Debug.Log("GameOver");   
        
        Time.timeScale = 0;
        time_end = Time.time;
        onPlay = false;
        
        if (gameFramework != null) StopCoroutine(gameFramework);
        gameFramework = null;

        if (OnGameOver != null) OnGameOver();
        
        // Main
        UIManager.instance.SetActiveGoMain();
    }
    
    private float TimeCheck()
    {
        time_current = Time.time - time_start;
        return time_current;
    }

    
    
    public void LoadMainScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadPlayScene()
    {
        SceneManager.LoadScene(1);
    }
}
