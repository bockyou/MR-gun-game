using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast : MonoBehaviour
{
    public float rayLength = 5.0f; // 레이 길이
    private LineRenderer[] lineRenderers; // 여러 개의 Line Renderer

    void Start()
    {
        // Line Renderer 컴포넌트 3개 추가 및 초기화
        lineRenderers = new LineRenderer[3];
        for (int i = 0; i < 3; i++)
        {
            GameObject lineObj = new GameObject("LineRenderer" + i);
            lineObj.transform.parent = transform; // 현재 오브젝트의 자식으로 설정
            lineRenderers[i] = lineObj.AddComponent<LineRenderer>();
            lineRenderers[i].positionCount = 2; // 시작점과 끝점 두 개의 점으로 구성
            lineRenderers[i].startWidth = 0.01f; // 레이 시작 부분 두께
            lineRenderers[i].endWidth = 0.01f; // 레이 끝 부분 두께
        }
       
    }

    void Update()
    {
        // 레이 시작점 (현재 오브젝트 위치)
        Vector3 rayOrigin = transform.position + new Vector3(-0.07f, 0.04f, 0.07f);

        // 각 축 방향의 레이 방향 설정
        Vector3[] rayDirections = new Vector3[]
        {
            -transform.right,   // X축 방향
            -transform.up,      // Y축 방향
            -transform.forward  // Z축 방향
        };

        // 각 방향으로 레이 발사
        for (int i = 0; i < 3; i++)
        {
            Vector3 rayDirection = rayDirections[i];
            Vector3 rayEnd = rayOrigin + rayDirection * rayLength;

            // Line Renderer 업데이트
            lineRenderers[i].SetPosition(0, rayOrigin);
            lineRenderers[i].SetPosition(1, rayEnd);

            // Raycast 실행
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayLength))
            {
                // Raycast가 충돌한 경우, Line Renderer 길이 조절하여 충돌 지점까지 표시
                lineRenderers[i].SetPosition(1, hit.point);
            }
        }
    }
}
