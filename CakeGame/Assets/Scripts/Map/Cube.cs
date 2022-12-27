using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private GameObject cube;
    private void Start()
    {
        cube = GetComponent<GameObject>();
    }
    
    [ContextMenu("ChangeToRed")]
    public void ChangeColor()
    {
        var cubeRenderer = this.GetComponent<Renderer>();
        Debug.Log(cubeRenderer);
        cubeRenderer.material.SetColor("_Color",Color.red);
    }
    [ContextMenu("DestoryCake")] 
    public void DestroyCake()
    {
        // 다시 채워 넣을 것을 대비 비활성화, 
        this.gameObject.SetActive(false);
    }
}
