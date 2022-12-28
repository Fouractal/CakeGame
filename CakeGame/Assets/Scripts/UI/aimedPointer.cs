using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimedPointer : MonoBehaviour
{
    private Vector3 targetPosition;
    private RectTransform pointerRectTransform;
    public Transform playerTransform;
    public Transform targetTransform;
    private void Awake()
    {
        //targetPosition = new Vector3(200, 45);
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector3 toPosition = Camera.main.transform.position - targetTransform.position;
        Vector3 fromPosition = Camera.main.transform.position - playerTransform.position;
        Vector3 dir = toPosition - fromPosition;
        Debug.Log($"Camera : {Camera.main.transform.position}, player : {playerTransform.position}, fromPosition :{fromPosition}");
        //playerTransform.gameObject.transform.position.normalized;

        float angle = Vector3.Angle(dir , Vector3.right);
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
        Debug.Log($"target : {toPosition}, player : {fromPosition}, angle : {angle}");
       //Debug.Log();
    }
}
