using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public Define.CubeState cubeState;

    public float colorChangeSpeed = 5f;
    public Color startColor;
    public Color endColor;
    private float startTime;
    public Material InitMaterial;
    private Renderer initRender;
    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if (cubeState == Define.CubeState.beAimed)
        {
            float t = (Mathf.Sin(Time.time - startTime) * colorChangeSpeed);
            GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
        }
        
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
        // 다시 채워 넣을 것을 대비 비활성화, 
        this.gameObject.SetActive(false);
    }
}
