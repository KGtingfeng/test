using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

    public int x;
    public int y;

    public MeshRenderer meshRenderer;
    public Material material0;
    public Material material1;

    public void Init(int x,int y)
    {
        gameObject.GetComponent<MeshCollider>().sharedMesh = CreateMesh();
        this.x = x;
        this.y = y;
        if (x % 2 == 0)
        {
            this.transform.localPosition = new Vector3(y * 0.18f, 0, x * 0.16f);
        }
        else
        {
            this.transform.localPosition = new Vector3(y * 0.18f + 0.09f, 0, x * 0.16f );
        }
        
    }

    public void ChangeMaterial()
    {       
        meshRenderer.materials[0].CopyPropertiesFromMaterial(material1);
    }

    public void ReturnMaterial()
    {
        meshRenderer.materials[0].CopyPropertiesFromMaterial(material0);
    }

    /// <summary>
    /// 六边形碰撞网格
    /// </summary>
    /// <returns></returns>
    Mesh CreateMesh() {
        float radius = 0.1f;
        var a = radius / 2.0f;
        var b = Mathf.Sqrt(3.0f) / 2.0f * radius;
        var c = radius;
        var v1 = new Vector3(0.0f, 0.0f, c);
        var v2 = new Vector3(b, 0.0f, a);
        var v3 = new Vector3(b, 0.0f, -a);
        var v4 = new Vector3(0, 0.0f, -c);
        var v5 = new Vector3(-b, 0.0f, -a);
        var v6 = new Vector3(-b, 0.0f, a);
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
        return mesh;


    }



}
