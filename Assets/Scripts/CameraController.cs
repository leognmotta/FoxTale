using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform farBackground;
    [SerializeField]
    private Transform middleBackground;
    [SerializeField]
    private float minHeight;
    [SerializeField]
    private float maxHeight;

    private const float CameraZIndex = -10f;

    private Vector2 _lastPosition;
    private Vector2 _amountToMove;
    private float _clampedY;

    private void Awake()
    {
        _lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        var targetPosition = target.position;
        var cameraPosition = transform.position;
        _amountToMove = new Vector2(cameraPosition.x - _lastPosition.x, cameraPosition.y - _lastPosition.y);
        _clampedY = Mathf.Clamp(targetPosition.y, minHeight, maxHeight);

        transform.position = new Vector3(targetPosition.x, _clampedY, CameraZIndex);

        farBackground.position += new Vector3(_amountToMove.x, _amountToMove.y * .2f, 0f);
        middleBackground.position += new Vector3(_amountToMove.x, 0f, 0f) * .5f;

        _lastPosition = cameraPosition;
    }
}
