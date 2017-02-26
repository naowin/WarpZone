using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStone {

    public Warpings warpings { get; set;}

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    public Mesh Create(Mesh mesh){
        this.mesh = mesh;
        SetVertices();
        SetUvs();
        SetTriangles();
        return mesh;
    }

    public void SetVertices()
    {
        vertices = new Vector3[8];
        float vStep = (2f * Mathf.PI) / warpings.warpSegmentCount;
        float uStep = warpings.ringDistance / warpings.curveSegmentCount; 
        Vector3 vertexA = GetPointOnTorus(warpings.depthIndex * uStep, warpings.startIndex * vStep);
        Vector3 vertexB = GetPointOnTorus((warpings.depthIndex + warpings.depthFactor) * uStep, warpings.startIndex * vStep);

        // VertexA
        vertices[0] = vertexA;
        vertices[1] = vertexA = GetPointOnTorus(warpings.depthIndex * uStep, (warpings.startIndex + warpings.warpStoneFactor) * vStep);

        // VertexB
        vertices[2] = vertexB;
        vertices[3] = vertexB = GetPointOnTorus((warpings.depthIndex + warpings.depthFactor) * uStep, (warpings.startIndex + warpings.warpStoneFactor) * vStep);
        vertices[4] = CreateEndpoint((warpings.depthIndex) * (uStep / 2), (warpings.startIndex + warpings.warpStoneFactor) * (vStep / 2)); 
        mesh.vertices = vertices;
    }

    private void SetUvs() 
    {
        var uvs = new Vector2[vertices.Length];
        for(int i = 0; i < uvs.Length; i++) 
        {
            uvs[i] = new Vector2(0, 1);
        }   

        mesh.uv = uvs;
    } 

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
        float reduceRadius = warpings.warpStoneFactor;
        if (reduceRadius > 2)
        {
            reduceRadius = 2;
        }

        Vector3 p;
        float r = (warpings.curveRadius + (warpings.warpRadius - (reduceRadius * 0.3f)) * Mathf.Cos(v));
        p.x = r * Mathf.Sin(u);
        p.y = r * Mathf.Cos(u);
        p.z = (warpings.warpRadius - (reduceRadius * 0.3f)) * Mathf.Sin(v);
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
}
