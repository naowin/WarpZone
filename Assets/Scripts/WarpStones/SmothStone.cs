using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmothStone {

    public Warpings warpings { get; set;}

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private BaseStone baseStone = new BaseStone();

    public Mesh Create(Mesh mesh){
        this.mesh = mesh;
        this.baseStone.warpings = warpings;
        this.mesh.vertices = SetVertices();
        if(warpings.useUvs)
        {
            this.mesh.uv = this.baseStone.SetTriangleUvs(mesh);
        }

        this.mesh.triangles = SetTriangles();
        return mesh;
    }

    private Vector3[] SetVertices()
    {
        vertices = new Vector3[(warpings.warpStoneFactor * 6) + 6];
        float vStep = (2f * Mathf.PI) / warpings.warpSegmentCount;
        float uStep = warpings.ringDistance / warpings.curveSegmentCount; 
        Vector3 vertexA = this.baseStone.GetPointOnTorus(warpings.depthIndex * uStep, warpings.startIndex * vStep);
        Vector3 vertexB = this.baseStone.GetPointOnTorus((warpings.depthIndex + warpings.depthFactor) * uStep, warpings.startIndex * vStep);

        Vector3 endPoint = this.baseStone.CreateTriangleEndpoint((warpings.depthIndex) * (uStep / 2), (warpings.startIndex + warpings.warpStoneFactor) * (vStep / 2));
        int verticeIndex = 0;
        for(int sideStep = 1; sideStep <= warpings.warpStoneFactor; sideStep++)
        {
            // Front side
            vertices[verticeIndex] = vertexA;
            vertices[verticeIndex + 1] = vertexA = this.baseStone.GetPointOnTorus(warpings.depthIndex * uStep, (warpings.startIndex + sideStep) * vStep);
            vertices[verticeIndex + 2] = endPoint;
            // Back side
            vertices[verticeIndex + 3] = vertexB;
            vertices[verticeIndex + 4] = vertexB = this.baseStone.GetPointOnTorus((warpings.depthIndex + warpings.depthFactor) * uStep, (warpings.startIndex + sideStep) * vStep);
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


        return vertices;
    }

    private int[] SetTriangles() 
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

        return triangles;
    }
}
