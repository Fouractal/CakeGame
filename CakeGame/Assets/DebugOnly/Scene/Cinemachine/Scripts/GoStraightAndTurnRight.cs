using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GoStraightAndTurnRight : MonoBehaviour
{
    private Transform _playerTransform;

    public Transform[] positions; // position 1 ~ 4의 Transform
    // Start is called before the first frame update
    IEnumerator Start()
    {
        _playerTransform = GetComponent<Transform>();
        float duration = 0f;

        for (int i = 0; i < positions.Length; i++)
        {
            duration = MoveForward(i);
            yield return new WaitForSeconds(duration);
        }
        //yield return null; // 이거 확인
    }

    private float MoveForward(int positionNum)
    {
        float duration = 3f;
        _playerTransform.DOMove(positions[positionNum].position, duration);
        return duration;
    }
}
