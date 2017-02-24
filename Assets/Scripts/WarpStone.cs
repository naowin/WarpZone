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
        vertices = new Vector3[5];
        float vStep = (2f * Mathf.PI) / warpings.warpSegmentCount;
        float uStep = warpings.ringDistance / warpings.curveSegmentCount; 

        Vector3 vertexA = GetPointOnTorus(warpings.depthIndex * uStep, warpings.startIndex * vStep);
        Vector3 vertexB = GetPointOnTorus((warpings.depthIndex + 1) * uStep, warpings.startIndex * vStep);

        // VertexA
        vertices[0] = vertexA;
        vertices[1] = vertexA = GetPointOnTorus(warpings.depthIndex * uStep, (warpings.startIndex + 1) * vStep);

        // VertexB
        vertices[2] = vertexB;
        vertices[3] = vertexB = GetPointOnTorus((warpings.depthIndex + 1) * uStep, (warpings.startIndex + 1) * vStep);
        vertices[4] = CreateEndpoint((warpings.depthIndex) * (uStep / 2), (warpings.startIndex + 2) * (vStep / 2)); //CreateEndpoint();
        mesh.vertices = vertices;
    }

    private void SetTriangles() 
    {
        triangles = new int[4 * 3];
        triangles[0] = 1;
        triangles[1] = 0;
        triangles[2] = 4;

        triangles[3] = 3;
        triangles[4] = 1;
        triangles[5] = 4;

        triangles[6] = 2;
        triangles[7] = 3;
        triangles[8] = 4;

        triangles[9] = 0;
        triangles[10] = 2;
        triangles[11] = 4;

        mesh.triangles = triangles;
    }

    private void SetUvs() 
    {
        var uvs = new Vector2[vertices.Length];
        for(int i = 0; i < uvs.Length; i++) 
        {
            if(i == 6 || i == 7 || i == 8)
            {
                continue;
            }   

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
        float r = (warpings.curveRadius + (warpings.warpRadius -0.3f) * Mathf.Cos(v));
        p.x = r * Mathf.Sin(u);
        p.y = r * Mathf.Cos(u);
        p.z = (warpings.warpRadius -0.3f) * Mathf.Sin(v);
        return p;
    }

    private Vector3 CreateEndpoint(float u, float v)
    {
        float vStep = (2f * Mathf.PI) / warpings.warpSegmentCount;
        float uStep = warpings.ringDistance / warpings.curveSegmentCount; 
        Vector3 vertexA = GetPointOnTorusSmallerRadius(warpings.depthIndex * uStep, warpings.startIndex * vStep);
        Vector3 vertexB = GetPointOnTorusSmallerRadius((warpings.depthIndex + 1) * uStep, warpings.startIndex * vStep);
        Vector3 vertexC = GetPointOnTorusSmallerRadius(warpings.depthIndex * uStep, (warpings.startIndex + 1) * vStep);
        Vector3 vertexD = GetPointOnTorusSmallerRadius((warpings.depthIndex + 1) * uStep, (warpings.startIndex + 1) * vStep);
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