
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast3 : MonoBehaviour
{
    public float rayLength2 = 5.0f; // 레이 길이
    private LineRenderer lineRender;

    void Start()
    {
        // Line Renderer 컴포넌트 추가 및 초기화
        lineRender = gameObject.AddComponent<LineRenderer>();
        lineRender.positionCount = 2; // 시작점과 끝점 두 개의 점으로 구성
        lineRender.startWidth = 0.01f; // 레이 시작 부분 두께
        lineRender.endWidth = 0.01f; // 레이 끝 부분 두께
    }

    void Update()
    {
        // 레이 시작점 (현재 오브젝트 위치)
        Vector3 rayOrigin2 = transform.position;

        // 레이 방향 (현재 오브젝트의 앞쪽 방향)
        Vector3 rayDirection2 = transform.forward;
        // 레이 끝점 계산
        Vector3 rayEnd2 = rayOrigin2 + rayDirection2 * rayLength2;

        // Line Renderer 업데이트
        lineRender.SetPosition(0, rayOrigin2);
        lineRender.SetPosition(1, rayEnd2);

        // Raycast 실행
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin2, rayDirection2, out hit, rayLength2))
        {
            // Raycast가 충돌한 경우, Line Renderer 길이 조절하여 충돌 지점까지 표시
            lineRender.SetPosition(1, hit.point);
        }
    }
}