using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Friend : MonoBehaviour
{
    public Define.FriendType friendType = Define.FriendType.None;
    public Define.FriendState friendState = Define.FriendState.Idle;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Start()
    {
        InitPos();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ChangeState(Define.FriendState.Idle);
        }
        if(Input.GetKey(KeyCode.S))
        {
            ChangeState(Define.FriendState.Walk);
        }
        if(Input.GetKey(KeyCode.D))
        {
            ChangeState(Define.FriendState.Dance);
        }
        if(Input.GetKey(KeyCode.F))
        {
            ChangeState(Define.FriendState.Die);
        }
    }

    private void InitPos()
    {
        switch (friendType)
        {
            case Define.FriendType.None:
                break;
            case Define.FriendType.Cherryman:
                transform.localPosition = Vector3.up;
                break;
            case Define.FriendType.Strawberryman:
                transform.localPosition = Vector3.up;
                break;
        }
    }
    
    public void ChangeState(Define.FriendState targetState)
    {
        friendState = targetState;
        _animator.SetInteger("State", (int)targetState);
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
