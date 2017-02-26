using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmothStone {

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
        vertices = new Vector3[(warpings.warpStoneFactor * 6) + 6];
        float vStep = (2f * Mathf.PI) / warpings.warpSegmentCount;
        float uStep = warpings.ringDistance / warpings.curveSegmentCount; 
        Vector3 vertexA = GetPointOnTorus(warpings.depthIndex * uStep, warpings.startIndex * vStep);
        Vector3 vertexB = GetPointOnTorus((warpings.depthIndex + warpings.depthFactor) * uStep, warpings.startIndex * vStep);

        Vector3 endPoint = CreateEndpoint((warpings.depthIndex) * (uStep / 2), (warpings.startIndex + warpings.warpStoneFactor) * (vStep / 2));
        int verticeIndex = 0;
        for(int sideStep = 1; sideStep <= warpings.warpStoneFactor; sideStep++)
        {
            // Front side
            vertices[verticeIndex] = vertexA;
            vertices[verticeIndex + 1] = vertexA = GetPointOnTorus(warpings.depthIndex * uStep, (warpings.startIndex + sideStep) * vStep);
            vertices[verticeIndex + 2] = endPoint;
            // Back side
            vertices[verticeIndex + 3] = vertexB;
            vertices[verticeIndex + 4] = vertexB = GetPointOnTorus((warpings.depthIndex + warpings.depthFactor) * uStep, (warpings.startIndex + sideStep) * vStep);
            vertices[verticeIndex + 5] = endPoint;
            // new set of trianglepoints
            verticeIndex += 6;
        }

        vertices[verticeIndex] = vertices[0];
        vertices[verticeIndex + 1] = vertices[3];
        vertices[verticeIndex + 2] = vertices[2];
        vertices[verticeIndex + 3] = vertices[verticeIndex - 5];
        vertices[verticeIndex + 4] = vertices[verticeIndex - 2];
        vertices[verticeIndex + 5] = vertices[verticeIndex - 1];


        mesh.vertices = vertices;
    }

    private void SetUvs() 
    {
        var uvs = new Vector2[vertices.Length];
        for(int i = 0; i < uvs.Length; i += 3) 
        {
            uvs[i] = new Vector2(0, 1);
            uvs[i + 1] = new Vector2(1, 0);
            uvs[i + 2] = new Vector2(0.5f, 0);
        }   

        mesh.uv = uvs;
    } 

    private void SetTriangles() 
    {        
        triangles = new int[(warpings.warpStoneFactor * 6) + 6];
        int verticeIndex = 0;
        for(int sideStep = 0; sideStep < warpings.warpStoneFactor; sideStep++)
        {
            triangles[verticeIndex] = verticeIndex + 1;
            triangles[verticeIndex + 1] = verticeIndex;
            triangles[verticeIndex + 2] = verticeIndex + 2;

            triangles[verticeIndex + 3] = verticeIndex + 4;
            triangles[verticeIndex + 4] = verticeIndex + 3;
            triangles[verticeIndex + 5] = verticeIndex + 5;

            verticeIndex += 6;
        }
            
        triangles[verticeIndex] = vertices.Length - 6;
        triangles[verticeIndex + 1] = vertices.Length - 5;
        triangles[verticeIndex + 2] = vertices.Length - 4;
        triangles[verticeIndex + 3] = vertices.Length - 3;
        triangles[verticeIndex + 4] = vertices.Length - 2;
        triangles[verticeIndex + 5] = vertices.Length - 1;

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
        if (reduceRadius > 3)
        {
            reduceRadius = 3;
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
