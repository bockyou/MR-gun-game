using UnityEngine;

public class LineMeshVisualizer : MonoBehaviour
{
    public float rayLength = 5.0f; // ���� ����
    public float lineWidth = 0.1f; // �� �β�

    private GameObject[] lines;

    void Start()
    {
        // ���� ���� GameObject ����
        lines = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            lines[i] = new GameObject("Line" + i);
            lines[i].transform.parent = transform; // ���� ������Ʈ�� �ڽ����� ����
            CreateLineMesh(lines[i].transform, Color.white);
        }
    }

    void Update()
    {
        // ���� ������ (���� ������Ʈ ��ġ)
        Vector3 rayOrigin = transform.position + new Vector3(-0.02f, 0.05f, 0.1f);

        // �� �� ������ ���� ���� ����
        Vector3[] rayDirections = new Vector3[]
        {
            transform.right,   // X�� ����
            transform.up,      // Y�� ����
            transform.forward  // Z�� ����
        };

        // �� �������� ���� ����
        Color[] colors = new Color[]
        {
            Color.red,   // ������
            Color.green, // �ʷϻ�
            Color.blue   // �Ķ���
        };

        for (int i = 0; i < 3; i++)
        {
            Vector3 rayDirection = rayDirections[i];
            Vector3 rayEnd = rayOrigin + rayDirection * rayLength;

            // Line Mesh�� ��ġ ������Ʈ
            UpdateLineMesh(lines[i].transform, rayOrigin, rayEnd, colors[i]);
        }
    }

    void CreateLineMesh(Transform lineTransform, Color color)
    {
        MeshFilter meshFilter = lineTransform.gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = lineTransform.gameObject.AddComponent<MeshRenderer>();

        meshRenderer.material = new Material(Shader.Find("Unlit/Color"))
        {
            color = color
        };

        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;

        Vector3[] vertices = new Vector3[4];
        int[] triangles = new int[6];

        // Vertices ����
        vertices[0] = new Vector3(-lineWidth / 2, 0, 0);
        vertices[1] = new Vector3(lineWidth / 2, 0, 0);
        vertices[2] = new Vector3(-lineWidth / 2, 0, 1);
        vertices[3] = new Vector3(lineWidth / 2, 0, 1);

        // Triangles ����
        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;
        triangles[3] = 1;
        triangles[4] = 2;
        triangles[5] = 3;

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    void UpdateLineMesh(Transform lineTransform, Vector3 start, Vector3 end, Color color)
    {
        lineTransform.position = (start + end) / 2;
        lineTransform.LookAt(end);
        lineTransform.localScale = new Vector3(lineWidth, lineWidth, Vector3.Distance(start, end));
    }
}
