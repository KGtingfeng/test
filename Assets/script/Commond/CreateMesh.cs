using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreateMesh : MonoBehaviour
{
    public Material m_HexMaterial;

    List<Vector3> m_CenterList;
    List<Mesh> m_MeshsList;

    void Awake()
    {

        Create(Vector3.zero, 5, 8, 1.0f, 0f);
    }

    void Create(Vector3 origin, int row, int col, float radius, float interval)
    {
        m_CenterList = new List<Vector3>();
        m_MeshsList = new List<Mesh>();

        for (var i = 0; i < row; i++)
        {
            for (var j = 0; j < col; j++)
            {
                var x = origin.x + j * ((radius * 2) + interval);
                var z = origin.z + i * ((radius * 2) + interval);
                m_CenterList.Add(new Vector3(x, origin.y, z));
            }
        }

        var a = radius / 2.0f;
        var b = Mathf.Sqrt(3.0f) / 2.0f * radius;
        var c = radius;
        var v1 = new Vector3(-a, 0.0f, b);
        var v2 = new Vector3(a, 0.0f, b);
        var v3 = new Vector3(c, 0.0f, 0.0f);
        var v4 = new Vector3(a, 0.0f, -b);
        var v5 = new Vector3(-a, 0.0f, -b);
        var v6 = new Vector3(-c, 0.0f, 0.0f);
        var vertices = new List<Vector3>();
        vertices.Add(Vector3.zero);
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        vertices.Add(v4);
        vertices.Add(v5);
        vertices.Add(v6);
        var triangles = new List<int>() {
            0, 1, 2,
            0, 2, 3,
            0, 3, 4,
            0, 4, 5,
            0, 5, 6,
            0, 6, 1
        };

        var mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();

        foreach (var t in m_CenterList)
        {
            var go = new GameObject();
            go.transform.position = t;
            go.transform.parent = this.transform;
            go.AddComponent<MeshFilter>().mesh = mesh;
            go.AddComponent<MeshRenderer>().material = GameObject.Instantiate(m_HexMaterial);
        }
    }

    void OnDrawGizmos()
    {
        //foreach (var t in m_CenterList)
        //{
        //    Gizmos.DrawSphere(t, 1);
        //}
    }
}