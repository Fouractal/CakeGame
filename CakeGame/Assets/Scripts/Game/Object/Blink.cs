using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Blink : MonoBehaviour
{
    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }
    
    [ContextMenu("Blink")]
    public void Blinkcake(float blinkDuration)
    {
        mat.DOColor(Color.red, blinkDuration).SetLoops(6, LoopType.Yoyo);
    }

}
