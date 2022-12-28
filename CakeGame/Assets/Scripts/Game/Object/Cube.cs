using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

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
            // 깜빡임 구현
            float t = (Mathf.Sin(Time.time - startTime) * colorChangeSpeed);
            GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
        
            yield return new WaitForSeconds(t);
        
            // 깜빡임 종료 후 머테리얼 교체
            ChangeOwnColor();    
        }

        StartCoroutine(Blink());
    }

    [ContextMenu("ChangeColor")]
    public void ChangeOwnColor()
    {
        /*
        var cubeRenderer = this.GetComponent<Renderer>();
        Debug.Log(cubeRenderer);
        cubeRenderer.material.SetColor("_Color",Color.red);
        */

        GetComponent<Renderer>().material = InitMaterial;
    }
    [ContextMenu("DestoryCake")] 
    public void DestroyCake()
    {
        // 다시 채워 넣을 것을 대비 비활성화, Cake 프리팹 아래에 Cube 스크립트가 있고 다른 장식들이 있어서 부모 불러서 비활성화
        parent.SetActive(false);
    }
}
