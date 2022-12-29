using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class Fork : MonoBehaviour
{
    // 서서히 다가오면서 푹 찌름, 다시 케이크 들고 위로 올라감
    [SerializeField] public Transform _Fork,_pickedCake;
    public Transform forkFront;
    public float totalDuration;
    

    private IEnumerator Start()
    {
        // 포크가 생성된 후 2초 기다렸다가 FallDown 실행
        yield return new WaitForSeconds(totalDuration);
        FallDown(_pickedCake.position); // 찍을 cakePosition? 
    }

    // 깜빡임 시간 끝나고 찌름 
    [ContextMenu("FallDown")]
    public void FallDown(Vector3 cakePosition) // 매개변수로 찍을 CakePosition값 받음
    {
        // 포크 앞 부분, 케이크 까지의 거리 만큼 이동
        // 포크가 생성되는 위치에서 포크 앞부분이 모두 잠길 정도로 찌름 
        _Fork.DOMove(cakePosition + Vector3.up * 3f, Random.Range(2, 3)).SetEase(Ease.OutBack);
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
