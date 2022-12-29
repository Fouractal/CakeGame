using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    public Define.CubeState cubeState;
    public GameObject parent;

    [ContextMenu("BlinkAll")]
    public void BlinkAll()
    {
        // 무작위 n초동안 3번 깜빡인다. ( Blinkcake() )
        // 3번의 깜빡임 이후 포크는 낙하한다. 
        
        cubeState = Define.CubeState.Warning;
        
        float totalDuration = Random.Range(GameManager.Instance.balancingSO.attackDelayMin, GameManager.Instance.balancingSO.attackDelayMax);
        // 잠길 시간
        MapManager.Instance.totalDuration = totalDuration;
        float blinkDuration = totalDuration / 6;
        
        Blink[] blinkArray = parent.GetComponentsInChildren<Blink>();
        for (int i = 0; i < blinkArray.Length; i++)
        {
            blinkArray[i].Blinkcake(blinkDuration);
        }
    }

    [ContextMenu("DestroyCake")] 
    public void DestroyCake()
    {
        // 다시 채워 넣을 것을 대비 비활성화, Cake 프리팹 아래에 Cube 스크립트가 있고 다른 장식들이 있어서 부모 불러서 비활성화
        parent.SetActive(false);
    }
}
