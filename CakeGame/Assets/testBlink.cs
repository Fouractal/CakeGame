using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class testBlink : MonoBehaviour
{
    [SerializeField]
    private Transform cake;
    private Material mat;
    private Material mat2;
    public float duration;
    private void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        mat2= GetComponent<MeshRenderer>().material;
    }


    [ContextMenu("Blink/blink2222222")]
    public void Blink2()
    {
        mat.DOFade(0.0f, duration).SetLoops(-1, LoopType.Yoyo);
    }
}
