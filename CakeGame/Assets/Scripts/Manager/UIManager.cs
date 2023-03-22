using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject replayButton;
    
    private static UIManager _instance;
    public static UIManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
            }

            return _instance;
        }
    }
    
    public Button GoMainButton;
    public Button GoPlayButton;

    private void Start()
    {
        if (GoMainButton != null)
        {
            GoMainButton.onClick.AddListener(GameManager.Instance.LoadMainScene);   
        }

        if (GoPlayButton != null)
        {
            GoPlayButton.onClick.AddListener(GameManager.Instance.LoadPlayScene);
        }
    }

    public void SetActiveReplayButton()
    {
        replayButton.SetActive(true);
    }
    
}
