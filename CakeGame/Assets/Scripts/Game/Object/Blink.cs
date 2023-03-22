using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Blink : MonoBehaviour
{
    public Material mat;
    
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

    public void FadeOut()
    {
        mat.DOFade(0, 3f);
    }

    public void FadeIn()
    {
        mat.DOFade(1, 1f);
    }
    
}
