using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider : MonoBehaviour
{
    [SerializeField, Header("最大最新奥偏移量")] private Vector2 maxDistanceOffset;

    [SerializeField, Header("层级检测")] private LayerMask wall;
}
