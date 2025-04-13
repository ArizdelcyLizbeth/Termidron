using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Clase que genera un gráfo de NavMesh, extrayendo los vértices dentro de dos áreas definidas (área A y área B).
/// Proporciona funciones para verificar si un punto está dentro de una de las áreas y almacena los vértices de las áreas.
/// </summary>
public class NavMeshGraph
{
    public List<Vector3> areaAVertices = new List<Vector3>();
    public List<Vector3> areaBVertices = new List<Vector3>();

    private Vector3[] areaA;
    private Vector3[] areaB;

    /// <summary>
    /// Constructor que inicializa las áreas A y B con sus vértices predeterminados y luego construye el gráfico de NavMesh.
    /// </summary>
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

    /// <summary>
    /// Construye el gráfico de NavMesh, extrayendo los vértices de NavMesh que pertenecen a las áreas definidas.
    /// </summary>
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

    /// <summary>
    /// Verifica si un punto 3D está dentro de un cuadrado definido por cuatro vértices.
    /// </summary>
    /// <param name="point">Punto a verificar.</param>
    /// <param name="a">Vértice A del cuadrado.</param>
    /// <param name="b">Vértice B del cuadrado.</param>
    /// <param name="c">Vértice C del cuadrado.</param>
    /// <param name="d">Vértice D del cuadrado.</param>
    /// <returns>True si el punto está dentro del cuadrado, false si no lo está.</returns>
    private bool IsPointInQuad(Vector3 point, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        Vector2 p = new Vector2(point.x, point.z);
        Vector2 A = new Vector2(a.x, a.z);
        Vector2 B = new Vector2(b.x, b.z);
        Vector2 C = new Vector2(c.x, c.z);
        Vector2 D = new Vector2(d.x, d.z);

        return IsPointInTriangle(p, A, B, C) || IsPointInTriangle(p, A, C, D);
    }

    /// <summary>
    /// Verifica si un punto está dentro de un triángulo.
    /// </summary>
    /// <param name="p">Punto a verificar.</param>
    /// <param name="a">Vértice A del triángulo.</param>
    /// <param name="b">Vértice B del triángulo.</param>
    /// <param name="c">Vértice C del triángulo.</param>
    /// <returns>True si el punto está dentro del triángulo, false si no lo está.</returns>
    private bool IsPointInTriangle(Vector2 p, Vector2 a, Vector2 b, Vector2 c)
    {
        float s = a.y * c.x - a.x * c.y + (c.y - a.y) * p.x + (a.x - c.x) * p.y;
        float t = a.x * b.y - a.y * b.x + (a.y - b.y) * p.x + (b.x - a.x) * p.y;

        if ((s < 0) != (t < 0)) return false;

        float A = -b.y * c.x + a.y * (c.x - b.x) + a.x * (b.y - c.y) + b.x * c.y;
        return A < 0 ? (s <= 0 && s + t >= A) : (s >= 0 && s + t <= A);
    }

    /// <summary>
    /// Verifica si un punto está dentro de una de las áreas definidas (A o B).
    /// </summary>
    /// <param name="point">Punto a verificar.</param>
    /// <param name="area">Área en la que se desea verificar el punto ("A" o "B").</param>
    /// <returns>True si el punto está dentro del área especificada, false si no lo está.</returns>
    public bool IsPointInArea(Vector3 point, string area)
    {
        if (area == "A")
            return IsPointInQuad(point, areaA[0], areaA[1], areaA[2], areaA[3]);
        else if (area == "B")
            return IsPointInQuad(point, areaB[0], areaB[1], areaB[2], areaB[3]);

        return false;
    }
}