using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class RingMesh : MonoBehaviour
{
    public float outerRadius = 1f; // Outer radius of the ring
    public float innerRadius = 0.5f; // Inner radius of the ring
    public int segments = 32; // Number of segments to approximate the circle

    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = CreateRingMesh();
    }

    Mesh CreateRingMesh()
    {
        Mesh mesh = new Mesh();

        // Total number of vertices (each segment has 2 vertices, one for outer and one for inner)
        Vector3[] vertices = new Vector3[segments * 2];
        int[] triangles = new int[segments * 6]; // Each quad is made of 2 triangles (6 indices)
        Vector2[] uvs = new Vector2[segments * 2];

        float angleStep = 2 * Mathf.PI / segments; // Angle between each segment

        // Generate vertices for the outer and inner circle
        for (int i = 0; i < segments; i++)
        {
            float angle = i * angleStep;
            float cos = Mathf.Cos(angle);
            float sin = Mathf.Sin(angle);

            // Outer vertex
            vertices[i * 2] = new Vector3(cos * outerRadius, sin * outerRadius, 0f);
            uvs[i * 2] = new Vector2(cos * 0.5f + 0.5f, sin * 0.5f + 0.5f); // Basic UV mapping

            // Inner vertex
            vertices[i * 2 + 1] = new Vector3(cos * innerRadius, sin * innerRadius, 0f);
            uvs[i * 2 + 1] = new Vector2(cos * 0.5f + 0.5f, sin * 0.5f + 0.5f); // Basic UV mapping
        }

        // Generate triangles
        for (int i = 0; i < segments; i++)
        {
            int nextIndex = (i + 1) % segments;

            // First triangle (outer vertex, inner vertex, next outer vertex)
            triangles[i * 6] = i * 2;
            triangles[i * 6 + 1] = i * 2 + 1;
            triangles[i * 6 + 2] = nextIndex * 2;

            // Second triangle (next outer vertex, inner vertex, next inner vertex)
            triangles[i * 6 + 3] = nextIndex * 2;
            triangles[i * 6 + 4] = i * 2 + 1;
            triangles[i * 6 + 5] = nextIndex * 2 + 1;
        }

        // Assign vertices, triangles, and UVs to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        // Recalculate normals for lighting
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }
}
