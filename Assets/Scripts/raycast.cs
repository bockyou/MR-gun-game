using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast : MonoBehaviour
{
    public float rayLength = 5.0f; // ���� ����
    private LineRenderer[] lineRenderers; // ���� ���� Line Renderer

    void Start()
    {
        // Line Renderer ������Ʈ 3�� �߰� �� �ʱ�ȭ
        lineRenderers = new LineRenderer[3];
        for (int i = 0; i < 3; i++)
        {
            GameObject lineObj = new GameObject("LineRenderer" + i);
            lineObj.transform.parent = transform; // ���� ������Ʈ�� �ڽ����� ����
            lineRenderers[i] = lineObj.AddComponent<LineRenderer>();
            lineRenderers[i].positionCount = 2; // �������� ���� �� ���� ������ ����
            lineRenderers[i].startWidth = 0.01f; // ���� ���� �κ� �β�
            lineRenderers[i].endWidth = 0.01f; // ���� �� �κ� �β�
        }
       
    }

    void Update()
    {
        // ���� ������ (���� ������Ʈ ��ġ)
        Vector3 rayOrigin = transform.position + new Vector3(-0.07f, 0.04f, 0.07f);

        // �� �� ������ ���� ���� ����
        Vector3[] rayDirections = new Vector3[]
        {
            -transform.right,   // X�� ����
            -transform.up,      // Y�� ����
            -transform.forward  // Z�� ����
        };

        // �� �������� ���� �߻�
        for (int i = 0; i < 3; i++)
        {
            Vector3 rayDirection = rayDirections[i];
            Vector3 rayEnd = rayOrigin + rayDirection * rayLength;

            // Line Renderer ������Ʈ
            lineRenderers[i].SetPosition(0, rayOrigin);
            lineRenderers[i].SetPosition(1, rayEnd);

            // Raycast ����
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayLength))
            {
                // Raycast�� �浹�� ���, Line Renderer ���� �����Ͽ� �浹 �������� ǥ��
                lineRenderers[i].SetPosition(1, hit.point);
            }
        }
    }
}
