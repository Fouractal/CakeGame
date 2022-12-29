using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CameraHolder : MonoBehaviour
{
    private Animator _CherryMananimator;
    [SerializeField] private CinemachineVirtualCamera vCam1;
    [SerializeField] private CinemachineVirtualCamera vCam2;

    [SerializeField] private Transform P1;
    [SerializeField] private Transform P2;

    [SerializeField] private Text text;
    [SerializeField] private Button btn1;
    [SerializeField] private Button btn2;
    [SerializeField] private Transform playSprite;
    private void OnEnable()
    {
        TitleScemeCameraController.Register(vCam1);
        TitleScemeCameraController.Register(vCam2);
        TitleScemeCameraController.SwitchCamera(vCam1);
    }

    private void OnDisable()
    {
        TitleScemeCameraController.Unregister(vCam1);
        TitleScemeCameraController.Unregister(vCam2);
    }

    public IEnumerator Start()
    {
        _CherryMananimator = GetComponent<Animator>();
        btn1.onClick.AddListener(SetActivePlayBtn);
        
        yield return new WaitForSeconds(2f);
        MovetoP2();
        yield return new WaitForSeconds(5f);
        P1.DORotate(new Vector3(0, 0, 0), 2);
        // 10초 동안 케이크 위까지 걸어간다.
        yield return new WaitForSeconds(3f);
        //카메라 전환, 숨은 딸기클릭하면 Play UI 통통 팅겨나옴
        ChageCam();
        yield return new WaitForSeconds(1f);
        text.gameObject.SetActive(true);
    
        
    }

    private void ChageCam()
    {
        if (TitleScemeCameraController.IsActiveCamera(vCam1))
        {
            TitleScemeCameraController.SwitchCamera(vCam2);
        }
        else if (TitleScemeCameraController.IsActiveCamera(vCam2))
        {
            TitleScemeCameraController.SwitchCamera(vCam1);
        }
    }

    private void MovetoP2()
    {
        P1.DORotate(new Vector3(0,-100,0), 1).SetEase(Ease.InCirc);
        P1.DOMove(P2.position, 5).SetEase(Ease.InOutQuad);
    }

    public void SetActivePlayBtn()
    {
        btn2.gameObject.SetActive(true);
        playSprite.gameObject.SetActive(true);
        playSprite.DOJump(new Vector3(-1.5f, 5, -13), 10, 2, 2).SetEase(Ease.InBounce);
        _CherryMananimator.SetBool("isDancing",true);
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(1);
    }
}
