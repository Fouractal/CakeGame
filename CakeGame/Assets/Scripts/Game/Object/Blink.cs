using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Blink : MonoBehaviour
{
    private Material mat;
    public Material ownMat;
    public float duration =1f;
    
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;

    }
    [ContextMenu("Blink")]
    public void Blinkcake()
    {
        mat.DOColor(Color.red, duration).SetLoops(4, LoopType.Yoyo);
    }

}
