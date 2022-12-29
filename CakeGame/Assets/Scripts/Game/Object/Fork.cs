using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class Fork : MonoBehaviour
{
    // 서서히 다가오면서 푹 찌름, 다시 케이크 들고 위로 올라감
    [SerializeField] private Transform _Fork,_pickedCake;
    public Vector2 target;


    private IEnumerator Start()
    {
        // 포크가 생성된 후 2초 기다렸다가 FallDown 실행
        yield return new WaitForSeconds(2f);
        FallDown();
    }
    
    

    [ContextMenu("FallDown")]
    public void FallDown()
    {
        _Fork.DOMoveY(1.5f,
                Random.Range(2,3)) 
            .SetEase(Ease.OutBack);
        //Random.Range(GameManager.instance.balancingSO.attackDelayMin,GameManager.instance.balancingSO.attackDelayMax)
    }


    [ContextMenu("PickUp")]
    public void PickUp()
    {
        _Fork.DOPunchPosition(
            Vector3.down * 3,
            2f,
            0,
            0);
        _pickedCake.DOShakePosition(
            2f,
            1f,
            10);
    }
    /*
    public IEnumerator FallDown()
    {
        float fallDownDelay = Random.Range(GameManager.instance.balancingSO.attackDelayMin, GameManager.instance.balancingSO.attackDelayMax);
        yield return new WaitForSeconds(fallDownDelay);
        
        gameObject.AddComponent<Rigidbody>();
    }
    */
}
