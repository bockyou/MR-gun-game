
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast3 : MonoBehaviour
{
    public float rayLength2 = 5.0f; // ���� ����
    private LineRenderer lineRender;

    void Start()
    {
        // Line Renderer ������Ʈ �߰� �� �ʱ�ȭ
        lineRender = gameObject.AddComponent<LineRenderer>();
        lineRender.positionCount = 2; // �������� ���� �� ���� ������ ����
        lineRender.startWidth = 0.01f; // ���� ���� �κ� �β�
        lineRender.endWidth = 0.01f; // ���� �� �κ� �β�
    }

    void Update()
    {
        // ���� ������ (���� ������Ʈ ��ġ)
        Vector3 rayOrigin2 = transform.position;

        // ���� ���� (���� ������Ʈ�� ���� ����)
        Vector3 rayDirection2 = transform.forward;
        // ���� ���� ���
        Vector3 rayEnd2 = rayOrigin2 + rayDirection2 * rayLength2;

        // Line Renderer ������Ʈ
        lineRender.SetPosition(0, rayOrigin2);
        lineRender.SetPosition(1, rayEnd2);

        // Raycast ����
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin2, rayDirection2, out hit, rayLength2))
        {
            // Raycast�� �浹�� ���, Line Renderer ���� �����Ͽ� �浹 �������� ǥ��
            lineRender.SetPosition(1, hit.point);
        }
    }
}