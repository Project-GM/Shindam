using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 카메라 움직임 관련 스크립트
/// </summary>

public class CameraMove : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 1;
    public Vector2 offset;//offset.y만큼 카메라가 캐릭터 y좌표랑 완전히 같은게 아닌 조금 더 위에 있게 설정해준다.
    public float limitMinX, limitMaxX, limitMinY, limitMaxY;
    float cameraHalfWidth, cameraHalfHeight;

    private void Start()
    {
        //카메라 사이즈 설정
        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        cameraHalfHeight = Camera.main.orthographicSize;
    }

    private void Update()
    {
        //카메라 위치 제한
        Vector3 desiredPosition = new Vector3(
            //
            Mathf.Clamp(target.position.x, limitMinX + cameraHalfWidth, limitMaxX - cameraHalfWidth),   // X
            Mathf.Clamp(target.position.y + offset.y, limitMinY + cameraHalfHeight, limitMaxY - cameraHalfHeight), // Y
            -10);                                                                                                  // Z
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);// 카메라 부드럽게 이동
    }
}
