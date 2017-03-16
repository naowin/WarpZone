using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStone {

    public WarpSettings warpSettings { get; set;}

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private BaseStone baseStone = new BaseStone();

    public Mesh Create(Mesh mesh){
        this.mesh = mesh;
        this.baseStone.warpSettings = warpSettings;
        this.mesh.vertices = SetVertices();
        if(warpSettings.useUvs)
        {
            this.mesh.uv = SetBlockUvs(mesh);
        }

        this.mesh.triangles = SetTriangles();
        return mesh;
    }

    public Vector3[] SetVertices()
    {
        vertices = new Vector3[(warpSettings.warpStoneFactor * 12) + 12];
        float vStep = (2f * Mathf.PI) / warpSettings.warpSegmentCount;
        float uStep = warpSettings.ringDistance / warpSettings.curveSegmentCount; 
        int startIndex = warpSettings.startIndex;
        int endIndex = warpSettings.startIndex + (warpSettings.warpSegmentCount / 2);
        if(endIndex > warpSettings.warpSegmentCount)
        {
            startIndex -= (warpSettings.warpSegmentCount / 2);
            endIndex -= (warpSettings.warpSegmentCount / 2);                
        }

        endIndex += warpSettings.warpStoneFactor;
            

        Vector3 verticeA = this.baseStone.GetPointOnTorus(warpSettings.depthIndex * uStep, startIndex * vStep);
        Vector3 verticeB = this.baseStone.GetPointOnTorus(warpSettings.depthIndex * uStep, endIndex * vStep);

        Vector3 verticeC = this.baseStone.GetPointOnTorus((warpSettings.depthIndex + warpSettings.depthFactor) * uStep, startIndex * vStep);
        Vector3 verticeD = this.baseStone.GetPointOnTorus((warpSettings.depthIndex + warpSettings.depthFactor) * uStep, endIndex * vStep);

        int verticeIndex = 0;
        for(int sideStep = 1; sideStep <= warpSettings.warpStoneFactor; sideStep++)
        {
            // front top triangle
            vertices[verticeIndex] = verticeA;
            vertices[verticeIndex + 1] = verticeA = this.baseStone.GetPointOnTorus(warpSettings.depthIndex * uStep, (startIndex + sideStep) * vStep);
            vertices[verticeIndex + 2] = verticeB;

            // front bottom triangle
            vertices[verticeIndex + 3] = verticeB;
            vertices[verticeIndex + 4] = verticeA;
            vertices[verticeIndex + 5] = verticeB = this.baseStone.GetPointOnTorus(warpSettings.depthIndex * uStep, (endIndex - sideStep) * vStep);

            // back top triangle
            vertices[verticeIndex + 6] = verticeC;
            vertices[verticeIndex + 7] = verticeC = this.baseStone.GetPointOnTorus((warpSettings.depthIndex + warpSettings.depthFactor) * uStep, (startIndex + sideStep) * vStep);;
            vertices[verticeIndex + 8] = verticeD;

            // back bottom triangle
            vertices[verticeIndex + 9] = verticeD;
            vertices[verticeIndex + 10] = verticeC;
            vertices[verticeIndex + 11] = verticeD = this.baseStone.GetPointOnTorus((warpSettings.depthIndex + warpSettings.depthFactor) * uStep, (endIndex - sideStep) * vStep);
            verticeIndex += 12;
        }
            
        // left side;
        vertices[verticeIndex] = vertices[6];
        vertices[verticeIndex + 1] = vertices[0];
        vertices[verticeIndex + 2] = vertices[2];

        vertices[verticeIndex + 3] = vertices[3];
        vertices[verticeIndex + 4] = vertices[6];
        vertices[verticeIndex + 5] = vertices[9];

        // right side;
        vertices[verticeIndex + 6] = vertices[verticeIndex - 11];
        vertices[verticeIndex + 7] = vertices[verticeIndex - 7];
        vertices[verticeIndex + 8] = vertices[verticeIndex - 2];

        vertices[verticeIndex + 9] = vertices[verticeIndex - 7];
        vertices[verticeIndex + 10] = vertices[verticeIndex - 2];
        vertices[verticeIndex + 11] = vertices[verticeIndex - 1];
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
        triangles = new int[(warpSettings.warpStoneFactor * 12) + 12];

        int verticeIndex = 0;
        for(int sideStep = 0; sideStep < warpSettings.warpStoneFactor; sideStep++)
        {
            // front top triangle
            triangles[verticeIndex] = verticeIndex + 1;
            triangles[verticeIndex + 1] = verticeIndex;
            triangles[verticeIndex + 2] = verticeIndex + 2;

            // front bottom triangle
            triangles[verticeIndex + 3] = verticeIndex + 4;
            triangles[verticeIndex + 4] = verticeIndex + 5;
            triangles[verticeIndex + 5] = verticeIndex + 3;

            // back top triangle
            triangles[verticeIndex + 6] = verticeIndex + 7;
            triangles[verticeIndex + 7] = verticeIndex + 6;
            triangles[verticeIndex + 8] = verticeIndex + 8;

            // back bottom triangle
            triangles[verticeIndex + 9] = verticeIndex + 10;
            triangles[verticeIndex + 10] = verticeIndex + 11;
            triangles[verticeIndex + 11] = verticeIndex + 9;
            verticeIndex += 12;
        }

        // left side;
        triangles[verticeIndex] = verticeIndex;
        triangles[verticeIndex + 1] = verticeIndex + 1;
        triangles[verticeIndex + 2] = verticeIndex + 2;

        triangles[verticeIndex + 3] = verticeIndex + 3;
        triangles[verticeIndex + 4] = verticeIndex + 4;
        triangles[verticeIndex + 5] = verticeIndex + 5;

        // right side;
        triangles[verticeIndex + 6] = verticeIndex + 6;
        triangles[verticeIndex + 7] = verticeIndex + 7;
        triangles[verticeIndex + 8] = verticeIndex + 8;

        triangles[verticeIndex + 9] = verticeIndex + 9;
        triangles[verticeIndex + 10] = verticeIndex + 10;
        triangles[verticeIndex + 11] = verticeIndex + 11;
     
        return triangles;
    }
   
    // {} [] 
}
