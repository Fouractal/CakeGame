using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;

public class aimedPointer : MonoBehaviour
{
    private Camera Cam;
    private float rotation=90;
    private float borderSize = 100f;
    private Vector3 targetPosition;
    private RectTransform pointerRectTransform;
    public Transform playerTransform;
    public Transform targetTransform;

    private int _reverse;
    public int reverse
    {
        get
        {
            if (playerTransform.position.z > targetTransform.position.z)
            {
                _reverse = -1;
            }
            else
                _reverse = 1;
            return _reverse;
        }

    }
    private void Awake()
    {
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
    }

    private void Update()
    {
        // target 방향 표시
        Vector3 toPosition = targetTransform.position - Camera.main.transform.position;
        Vector3 fromPosition = playerTransform.position - Camera.main.transform.position;
        Vector3 dir = toPosition - fromPosition;

        float angle = Vector3.Angle(dir , Vector3.right);
        pointerRectTransform.localEulerAngles = Vector3.forward*rotation+new Vector3(0, 0, angle * reverse);

        // Screen에 target있는가
        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetTransform.position);
        bool isOffScreen = targetPositionScreenPoint.x <= 0 || targetPositionScreenPoint.x >= Screen.width ||
                           targetPositionScreenPoint.y <= 0 || targetPositionScreenPoint.y >= Screen.height;

        if (isOffScreen)
        {
            
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            if (cappedTargetScreenPosition.x <= borderSize) cappedTargetScreenPosition.x = borderSize; 
            if (cappedTargetScreenPosition.x >= Screen.width - borderSize) cappedTargetScreenPosition.x = Screen.width - borderSize;
            if (cappedTargetScreenPosition.y <= borderSize) cappedTargetScreenPosition.y = borderSize;
            if (cappedTargetScreenPosition.y >= Screen.height - borderSize) cappedTargetScreenPosition.y = Screen.height - borderSize;
            Vector3 pointWorldPosition = Camera.main.ScreenToWorldPoint(cappedTargetScreenPosition);
            
            pointerRectTransform.position = new Vector3(cappedTargetScreenPosition.x,
                cappedTargetScreenPosition.y, 0f);
        }
        else
        {
            pointerRectTransform.position = new Vector3(targetPositionScreenPoint.x, targetPositionScreenPoint.y, 0f);
        }
    }

}
