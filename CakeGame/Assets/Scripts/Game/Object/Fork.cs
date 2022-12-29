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
    public Material forkMaterial;
    public Transform forkFront;
    public float totalDuration;
    public int pickedCakeIndexI;
    public int pickedCakeIndexJ;

    
    private IEnumerator Start()
    {
        forkMaterial = GetComponent<MeshRenderer>().material;
        // 포크가 생성된 후 totalDuration초 기다렸다가 FallDown 실행
        yield return new WaitForSeconds(totalDuration);
        FallDown(_pickedCake.position); 
        
        // 3초 뒤 포크뽑히면서 FadeOut
        yield return new WaitForSeconds(2f);
        yield return PickUp();
        forkMaterial.DOFade(0, 3f);
        // FaedOut된 후 3초뒤에 setParent 원상복구
        yield return new WaitForSeconds(3f);
        MapManager.Instance.mapInfo[pickedCakeIndexI, pickedCakeIndexJ].cube.cubeState = Define.CubeState.Destroyed;
        this.transform.SetParent(GameObject.Find("Object").transform);
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false); // 포크 비활성화
        
    }

    // 깜빡임 시간 끝나고 찌름 
    [ContextMenu("FallDown")]
    public void FallDown(Vector3 cakePosition) 
    {
        // 포크 앞 부분, 케이크 까지의 거리 만큼 이동
        // 포크가 생성되는 위치에서 포크 앞부분이 모두 잠길 정도로 찌름 
        _Fork.DOMove(cakePosition + Vector3.up * 3f, Random.Range(2, 3)).SetEase(Ease.OutBack);
    }


    [ContextMenu("PickUp")]
    public IEnumerator PickUp()
    {
        // 케이크랑 같이 들어올리면서 케이크는 FadeOut
        // 케이크 흔들리고 끌려 올려옴
        /*_Fork.DOPunchPosition(
            Vector3.down * 3,
            2f,
            0,
            0);*/
        this.transform.SetParent(_pickedCake);
        _pickedCake.DOShakePosition(
            0.5f,
            0.5f,
            10);

        float pickingTime = Random.Range(2, 3);
        _pickedCake.DOMoveY(3f, pickingTime ).SetEase(Ease.InOutBounce);
        
        // Fade Out
        //Debug.Log($"{_pickedCake.name} ({pickedCakeIndexI}, {pickedCakeIndexJ})가 FadeOut");
        MapManager.Instance.CakeFadeOut(pickedCakeIndexI, pickedCakeIndexJ);
        yield return null;
    }
}
