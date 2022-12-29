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
    public float colorChangeSpeed = 0.5f;
    public Color startColor;
    public Color endColor;
    private float startTime;
    public Material InitMaterial;
    private Renderer initRender;

    private void Start()
    {
        startTime = Time.time;
    }

    public void StartBlink()
    {
        IEnumerator Blink()
        {
            cubeState = Define.CubeState.Warning;

            yield return null;
            
            MeshRenderer curRenderer = GetComponent<MeshRenderer>();
            Color curColor = curRenderer.material.color;
            
            // 무작위 n초동안 3번 깜빡인다.
            // 3번의 깜빡임 이후 포크는 낙하한다. 

            float totalBlinkTime = Random.Range(GameManager.instance.balancingSO.attackDelayMin, GameManager.instance.balancingSO.attackDelayMax); 
            
            Sequence sequence = DOTween.Sequence();
            sequence
                .Append(curRenderer.material.DOColor(Color.red, totalBlinkTime / 6).From(curRenderer).SetEase(Ease.InCirc))
                .Append(curRenderer.material.DOColor(curColor, totalBlinkTime / 6).From(Color.red).SetEase(Ease.InCirc))
                .Append(curRenderer.material.DOColor(Color.red, totalBlinkTime / 6).From(curRenderer).SetEase(Ease.InCirc))
                .Append(curRenderer.material.DOColor(curColor, totalBlinkTime / 6).From(Color.red).SetEase(Ease.InCirc))
                .Append(curRenderer.material.DOColor(Color.red, totalBlinkTime / 6).From(curRenderer).SetEase(Ease.InCirc))
                .Append(curRenderer.material.DOColor(curColor, totalBlinkTime / 6).From(Color.red).SetEase(Ease.InCirc))
                .AppendCallback( ()=> this.DestroyCake() );
            // 케이크 비활성화
            
        }

        StartCoroutine(Blink());
    }

    [ContextMenu("ChangeOwnColor")]
    public void ChangeOwnColor()
    {
        /*
        var cubeRenderer = this.GetComponent<Renderer>();
        Debug.Log(cubeRenderer);
        cubeRenderer.material.SetColor("_Color",Color.red);
        */

        GetComponent<Renderer>().material = InitMaterial;
    }

    [ContextMenu("BlinkAll")]
    public void BlinkAll()
    {
        Blink[] blinkArray = parent.GetComponentsInChildren<Blink>();
        for (int i = 0; i < blinkArray.Length; i++)
        {
            blinkArray[i].Blinkcake();
        }
    }

    [ContextMenu("DestroyCake")] 
    public void DestroyCake()
    {
        // 다시 채워 넣을 것을 대비 비활성화, Cake 프리팹 아래에 Cube 스크립트가 있고 다른 장식들이 있어서 부모 불러서 비활성화
        parent.SetActive(false);
    }
}
