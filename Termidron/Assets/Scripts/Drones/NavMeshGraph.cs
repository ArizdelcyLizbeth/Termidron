using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshGraph
{
    public List<Vector3> areaAVertices = new List<Vector3>();
    public List<Vector3> areaBVertices = new List<Vector3>();

    private Vector3[] areaA;
    private Vector3[] areaB;

    public NavMeshGraph()
    {
        areaA = new Vector3[]
        {
            new Vector3(-130, 0, -130),     // bottomLeft
            new Vector3(5, 0, -130),    // bottomRight
            new Vector3(5, 0, 5),   // topRight
            new Vector3(-130, 0, 5)     // topLeft
        };

        areaB = new Vector3[]
        {
            new Vector3(5, 0, -130),    // bottomLeft
            new Vector3(140, 0, -130),    // bottomRight
            new Vector3(140, 0, 5),   // topRight
            new Vector3(5, 0, 5)    // topLeft
        };

        BuildGraph();
    }

    private void BuildGraph()
    {
        UnityEngine.AI.NavMeshTriangulation triangulation = UnityEngine.AI.NavMesh.CalculateTriangulation();
        Vector3[] vertices = triangulation.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 v = vertices[i];

            if (IsPointInQuad(v, areaA[0], areaA[1], areaA[2], areaA[3]))
            {
                areaAVertices.Add(v);
            }

            if (IsPointInQuad(v, areaB[0], areaB[1], areaB[2], areaB[3]))
            {
                areaBVertices.Add(v); 
            }
        }
    }

    private bool IsPointInQuad(Vector3 point, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        Vector2 p = new Vector2(point.x, point.z);
        Vector2 A = new Vector2(a.x, a.z);
        Vector2 B = new Vector2(b.x, b.z);
        Vector2 C = new Vector2(c.x, c.z);
        Vector2 D = new Vector2(d.x, d.z);

        return IsPointInTriangle(p, A, B, C) || IsPointInTriangle(p, A, C, D);
    }

    private bool IsPointInTriangle(Vector2 p, Vector2 a, Vector2 b, Vector2 c)
    {
        float s = a.y * c.x - a.x * c.y + (c.y - a.y) * p.x + (a.x - c.x) * p.y;
        float t = a.x * b.y - a.y * b.x + (a.y - b.y) * p.x + (b.x - a.x) * p.y;

        if ((s < 0) != (t < 0)) return false;

        float A = -b.y * c.x + a.y * (c.x - b.x) + a.x * (b.y - c.y) + b.x * c.y;
        return A < 0 ? (s <= 0 && s + t >= A) : (s >= 0 && s + t <= A);
    }

    public bool IsPointInArea(Vector3 point, string area)
    {
        if (area == "A")
            return IsPointInQuad(point, areaA[0], areaA[1], areaA[2], areaA[3]);
        else if (area == "B")
            return IsPointInQuad(point, areaB[0], areaB[1], areaB[2], areaB[3]);

        return false;
    }
}