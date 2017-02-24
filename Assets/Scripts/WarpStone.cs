using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpStone : MonoBehaviour {

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    public Warpings warpings { get; set;}

    void Awake() 
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "WarpStone";
    }

    public void Generate() 
    {        
        mesh.Clear();
        SetVertices();
        SetUvs();
        SetTriangles();
        mesh.RecalculateNormals();
    }

    private void SetVertices()
    {
        vertices = new Vector3[(warpings.warpStoneFactor * 4) + 1];
        float vStep = (2f * Mathf.PI) / warpings.warpSegmentCount;
        float uStep = warpings.ringDistance / warpings.curveSegmentCount; 
        Vector3 vertexA = GetPointOnTorus(warpings.depthIndex * uStep, warpings.startIndex * vStep);
        Vector3 vertexB = GetPointOnTorus((warpings.depthIndex + 1) * uStep, warpings.startIndex * vStep);

        for(int v = 1, i = 0; v < warpings.warpStoneFactor + 1; v++)
        {
            // VertexA
            vertices[i] = vertexA;
            vertices[i + 1] = vertexA = GetPointOnTorus(warpings.depthIndex * uStep, (warpings.startIndex + v) * vStep);

            // VertexB
            vertices[i + 2] = vertexB;
            vertices[i + 3] = vertexB = GetPointOnTorus((warpings.depthIndex + 1) * uStep, (warpings.startIndex + v) * vStep);
            i += 4;
        }

        vertices[vertices.Length - 1] = CreateEndpoint((warpings.depthIndex) * (uStep / 2), (warpings.startIndex + warpings.warpStoneFactor) * (vStep / 2)); //CreateEndpoint();
        mesh.vertices = vertices;
    }

    private void SetTriangles() 
    {
        triangles = new int[warpings.warpStoneFactor * 6 + 6];
        int t = 0;
       
        for(int i = 0; i < (warpings.warpStoneFactor * 4) - 1;  i+=4)
        {
            triangles[t] = i + 1;
            triangles[t + 1] = i;
            triangles[t + 2] = vertices.Length - 1;

            triangles[t + 3] = i + 3;
            triangles[t + 4] = i + 2;
            triangles[t + 5] = vertices.Length - 1;

            t += 6;
        }

        triangles[t] = 0;
        triangles[t + 1] = 2;
        triangles[t + 2] = vertices.Length -1;

        triangles[t + 3] = vertices.Length - 2;
        triangles[t + 4] = vertices.Length - 4;
        triangles[t + 5] = vertices.Length -1;

        mesh.triangles = triangles;
    }

    /*
    private void SetTriangles() 
    {
        triangles = new int[warpings.warpStoneFactor * 6 + 6];

        triangles[0] = 1;
        triangles[1] = 0;
        triangles[2] = 4;

        triangles[3] = 2;
        triangles[4] = 3;
        triangles[5] = 4;

        triangles[6] = 3;
        triangles[7] = 1;
        triangles[8] = 4;

        triangles[9] = 0;
        triangles[10] = 2;
        triangles[11] = 4;

        mesh.triangles = triangles;
    }
    */

    private void SetUvs() 
    {
        var uvs = new Vector2[vertices.Length];
        for(int i = 0; i < uvs.Length; i++) 
        {
           uvs[i] = new Vector2(0, 1);
        }   

        mesh.uv = uvs;
    }   

    private Vector3 GetPointOnTorus (float u, float v) 
    {
        Vector3 p;
        float r = (warpings.curveRadius + warpings.warpRadius * Mathf.Cos(v));
        p.x = r * Mathf.Sin(u);
        p.y = r * Mathf.Cos(u);
        p.z = warpings.warpRadius * Mathf.Sin(v);
        return p;
    }

    private Vector3 GetPointOnTorusSmallerRadius (float u, float v) 
    {
        Vector3 p;
        float r = (warpings.curveRadius + (warpings.warpRadius - (warpings.warpStoneFactor * 0.3f)) * Mathf.Cos(v));
        p.x = r * Mathf.Sin(u);
        p.y = r * Mathf.Cos(u);
        p.z = (warpings.warpRadius - (warpings.warpStoneFactor * 0.3f)) * Mathf.Sin(v);
        return p;
    }

    private Vector3 CreateEndpoint(float u, float v)
    {
        float vStep = (2f * Mathf.PI) / warpings.warpSegmentCount;
        float uStep = warpings.ringDistance / warpings.curveSegmentCount; 
        Vector3 vertexA = GetPointOnTorusSmallerRadius(warpings.depthIndex * uStep, warpings.startIndex * vStep);
        Vector3 vertexB = GetPointOnTorusSmallerRadius((warpings.depthIndex + 1) * uStep, warpings.startIndex * vStep);
        Vector3 vertexC = GetPointOnTorusSmallerRadius(warpings.depthIndex * uStep, (warpings.startIndex + warpings.warpStoneFactor) * vStep);
        Vector3 vertexD = GetPointOnTorusSmallerRadius((warpings.depthIndex + 1) * uStep, (warpings.startIndex + warpings.warpStoneFactor) * vStep);
        Vector3 endPoint = (vertexA + vertexB + vertexC + vertexD) / 4;

        return endPoint;
    }

    public void AlignWith(Warp warp)
    {
        transform.SetParent(warp.transform, false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0f, 0f, -warp.CurveAngle);
        transform.Translate(0f, warp.CurveRadius, 0f);
        transform.Rotate(warpings.relativeRotation, 0f, 0f);
        transform.Translate(0f, -warpings.curveRadius, 0f);
        transform.SetParent(warp.transform.parent);
        transform.localScale = Vector3.one;
    }

    // {}
        
}