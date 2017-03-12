using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardBlockStone {

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
            this.mesh.uv = SetBlockUvs(mesh);
        }

        this.mesh.triangles = SetTriangles();
        return mesh;
    }

    public Vector3[] SetVertices()
    {
        vertices = new Vector3[4 * 6];
        Vector3[] point = new Vector3[8];
        float vStep = (2f * Mathf.PI) / warpings.warpSegmentCount;
        float uStep = warpings.ringDistance / warpings.curveSegmentCount; 
        int startIndex = warpings.startIndex;
        int endIndex = warpings.startIndex + (warpings.warpSegmentCount / 2);
        if(endIndex > warpings.warpSegmentCount)
        {
            startIndex -= (warpings.warpSegmentCount / 2);
            endIndex -= (warpings.warpSegmentCount / 2);                
        }

        point[0] = this.baseStone.GetPointOnTorus(warpings.depthIndex * uStep, startIndex * vStep);
        point[1] = this.baseStone.GetPointOnTorus(warpings.depthIndex * uStep, (startIndex + warpings.warpStoneFactor) * vStep);
        point[2] = this.baseStone.GetPointOnTorus((warpings.depthIndex + warpings.depthFactor) * uStep, (startIndex + warpings.warpStoneFactor) * vStep);
        point[3] = this.baseStone.GetPointOnTorus((warpings.depthIndex + warpings.depthFactor) * uStep, startIndex * vStep);

        point[4] = this.baseStone.GetPointOnTorus(warpings.depthIndex * uStep, endIndex * vStep);
        point[5] = this.baseStone.GetPointOnTorus((warpings.depthIndex + warpings.depthFactor) * uStep, endIndex * vStep);
        point[6] = this.baseStone.GetPointOnTorus((warpings.depthIndex + warpings.depthFactor) * uStep, (endIndex + warpings.warpStoneFactor) * vStep);
        point[7] = this.baseStone.GetPointOnTorus(warpings.depthIndex * uStep, (endIndex + warpings.warpStoneFactor) * vStep);


        int verticeIndex = 0;
        for(int cubeSides = 0; cubeSides < 3; cubeSides++)
        {
            
            vertices[verticeIndex] = point[cubeSides + 1];
            vertices[verticeIndex + 1] = point[cubeSides];
            vertices[verticeIndex + 2] = point[cubeSides + 4];
            vertices[verticeIndex + 3] = point[cubeSides + 1];
            vertices[verticeIndex + 4] = point[cubeSides + 4];
            vertices[verticeIndex + 5] = point[cubeSides + 5];
            verticeIndex += 6;
        }


        vertices[verticeIndex] = point[0];
        vertices[verticeIndex + 1] = point[3];
        vertices[verticeIndex + 2] = point[7];
        vertices[verticeIndex + 3] = point[7];
        vertices[verticeIndex + 4] = point[0];
        vertices[verticeIndex + 5] = point[4];

        return vertices;
    }

    private Vector2[] SetBlockUvs(Mesh mesh) 
    {
        var uvs = new Vector2[mesh.vertices.Length];
        for(int i = 0; i < uvs.Length; i++) 
        {
            uvs[i] = new Vector2(0, 1);
        }   
        return uvs;
    } 

    private int[] SetTriangles() 
    {
        triangles = new int[4 * 6];

        int triangleIndex = 0;
        for(int cubeSides = 0; cubeSides < 3; cubeSides++)
        {
            triangles[triangleIndex] = triangleIndex;
            triangles[triangleIndex + 1] = triangleIndex + 1;
            triangles[triangleIndex + 2] = triangleIndex + 2;

            triangles[triangleIndex + 3] = triangleIndex + 3;
            triangles[triangleIndex + 4] = triangleIndex + 4;
            triangles[triangleIndex + 5] = triangleIndex + 5;
            triangleIndex += 6;
        }

        triangles[triangleIndex] = vertices.Length - 6;
        triangles[triangleIndex + 1] = vertices.Length - 5;
        triangles[triangleIndex + 2] = vertices.Length - 4;
        triangles[triangleIndex + 3] = vertices.Length - 3;
        triangles[triangleIndex + 4] = vertices.Length - 2;
        triangles[triangleIndex + 5] = vertices.Length - 1;

        return triangles;
    }

    // {} [] 
}
