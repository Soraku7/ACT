using System;
using System.Collections;
using System.Collections.Generic;
using Unilts.Tools.DevelopmentTool;
using UnityEngine;

public class CameraCollider : MonoBehaviour
{
    [SerializeField, Header("最大最新奥偏移量")] private Vector2 maxDistanceOffset;

    [SerializeField, Header("层级检测")] private LayerMask wallLayer;

    [SerializeField , Header("射线长度")] private float detectionDistance;

    [SerializeField, Header("相机碰撞移动时间")] private float colliderSmoothTime;
    private Vector3 _originPosition;
    private float _originOffsetDistance;

    private Transform _mainCamera;


    private void Awake()
    {
        if (Camera.main != null) _mainCamera = Camera.main.transform;
    }

    private void Start()
    {
        _originPosition = transform.localPosition.normalized;
        _originOffsetDistance = maxDistanceOffset.y;
    }

    private void Update()
    {
        UpdateCameraCollider();
    }

    private void UpdateCameraCollider()
    {
        var detectionDirection = transform.TransformPoint(_originPosition * detectionDistance);
        if (Physics.Linecast(transform.position, detectionDirection, out var hit, wallLayer))
        {
            _originOffsetDistance = Mathf.Clamp(hit.distance * 0.8f, maxDistanceOffset.x, maxDistanceOffset.y);
        }
        else
        {
            detectionDistance = maxDistanceOffset.y;
        }

        _mainCamera.localPosition = Vector3.Lerp(_mainCamera.localPosition, _originPosition * (_originOffsetDistance - 0.1f) ,
            DevelopmentToos.UnTetheredLerp(colliderSmoothTime));
    }
}
