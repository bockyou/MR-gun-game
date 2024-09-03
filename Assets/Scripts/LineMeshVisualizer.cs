using UnityEngine;

public class LineMeshVisualizer : MonoBehaviour
{
    public float rayLength = 5.0f; // 레이 길이
    public float lineWidth = 0.1f; // 선 두께

    private GameObject[] lines;

    void Start()
    {
        // 선을 위한 GameObject 생성
        lines = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            lines[i] = new GameObject("Line" + i);
            lines[i].transform.parent = transform; // 현재 오브젝트의 자식으로 설정
            CreateLineMesh(lines[i].transform, Color.white);
        }
    }

    void Update()
    {
        // 레이 시작점 (현재 오브젝트 위치)
        Vector3 rayOrigin = transform.position + new Vector3(-0.02f, 0.05f, 0.1f);

        // 각 축 방향의 레이 방향 설정
        Vector3[] rayDirections = new Vector3[]
        {
            transform.right,   // X축 방향
            transform.up,      // Y축 방향
            transform.forward  // Z축 방향
        };

        // 각 방향으로 선을 설정
        Color[] colors = new Color[]
        {
            Color.red,   // 빨간색
            Color.green, // 초록색
            Color.blue   // 파란색
        };

        for (int i = 0; i < 3; i++)
        {
            Vector3 rayDirection = rayDirections[i];
            Vector3 rayEnd = rayOrigin + rayDirection * rayLength;

            // Line Mesh의 위치 업데이트
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

        // Vertices 설정
        vertices[0] = new Vector3(-lineWidth / 2, 0, 0);
        vertices[1] = new Vector3(lineWidth / 2, 0, 0);
        vertices[2] = new Vector3(-lineWidth / 2, 0, 1);
        vertices[3] = new Vector3(lineWidth / 2, 0, 1);

        // Triangles 설정
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
