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
    public void Blinkcake()
    {
        float totalDuration = Random.Range(GameManager.instance.balancingSO.attackDelayMin, GameManager.instance.balancingSO.attackDelayMax);
        float blinkDuration = totalDuration / 6;
        
        mat.DOColor(Color.red, blinkDuration).SetLoops(6, LoopType.Yoyo);
    }

}
