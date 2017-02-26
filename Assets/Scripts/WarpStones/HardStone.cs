using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardStone {

    public Warpings warpings { get; set;}
   
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    public Mesh Create(Mesh mesh)
    {
        this.mesh = mesh;
        SetVertices();
        if(warpings.useUvs)
        {
            SetUvs();
        }

        SetTriangles();
        return mesh;
    }

    public void SetVertices()
    {
        vertices = new Vector3[12];
        Vector3[] point = new Vector3[5];
        float vStep = (2f * Mathf.PI) / warpings.warpSegmentCount;
        float uStep = warpings.ringDistance / warpings.curveSegmentCount; 
        point[0] = GetPointOnTorus(warpings.depthIndex * uStep, warpings.startIndex * vStep);
        point[1] = GetPointOnTorus((warpings.depthIndex + warpings.depthFactor) * uStep, warpings.startIndex * vStep);
        point[2] = GetPointOnTorus(warpings.depthIndex * uStep, (warpings.startIndex + warpings.warpStoneFactor) * vStep);
        point[3] = GetPointOnTorus((warpings.depthIndex + warpings.depthFactor) * uStep, (warpings.startIndex + warpings.warpStoneFactor) * vStep);
        point[4] = CreateEndpoint((warpings.depthIndex) * (uStep / 2), (warpings.startIndex + warpings.warpStoneFactor) * (vStep / 2)); 

        // first side
        vertices[0] = point[0];
        vertices[1] = point[1];
        vertices[2] = point[4];
        // second side
        vertices[3] = point[3];
        vertices[4] = point[2];
        vertices[5] = point[4];
        // third side
        vertices[6] = point[1];
        vertices[7] = point[3];
        vertices[8] = point[4];
        // fourth side
        vertices[9] = point[2];
        vertices[10] = point[0];
        vertices[11] = point[4];

        mesh.vertices = vertices;
    }

    private void SetUvs() 
    {
        var uvs = new Vector2[vertices.Length];
        for(int i = 0; i < uvs.Length; i += 3) 
        {
            uvs[i] = new Vector2(0.1f, 0);
            uvs[i + 1] = new Vector2(0.1f, 1);
            uvs[i + 2] = new Vector2(0.5f, 0.5f);
        }  

        mesh.uv = uvs;
    } 

    private void SetTriangles() 
    {
        triangles = new int[warpings.warpStoneFactor * 6 + 6];

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        triangles[3] = 3;
        triangles[4] = 4;
        triangles[5] = 5;

        triangles[6] = 6;
        triangles[7] = 7;
        triangles[8] = 8;

        triangles[9] = 9;
        triangles[10] = 10;
        triangles[11] = 11;

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
