using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : MonoBehaviour
{
    public Define.FriendState friendState = Define.FriendState.Idle;

    public void ChangeState(Define.FriendState targetState)
    {
        
    }

    public IEnumerator FollowRoutine()
    {
        yield return null;
    }

    public IEnumerator Die()
    {
        yield return null;
    }
}
