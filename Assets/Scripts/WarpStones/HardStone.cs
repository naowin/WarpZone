using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardStone{

    public Warpings warpings { get; set;}
   

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private BaseStone baseStone = new BaseStone();

    public Mesh Create(Mesh mesh)
    {
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
        vertices = new Vector3[12];
        Vector3[] point = new Vector3[5];
        float vStep = (2f * Mathf.PI) / warpings.warpSegmentCount;
        float uStep = warpings.ringDistance / warpings.curveSegmentCount; 
        point[0] = this.baseStone.GetPointOnTorus(warpings.depthIndex * uStep, warpings.startIndex * vStep);
        point[1] = this.baseStone.GetPointOnTorus((warpings.depthIndex + warpings.depthFactor) * uStep, warpings.startIndex * vStep);
        point[2] = this.baseStone.GetPointOnTorus(warpings.depthIndex * uStep, (warpings.startIndex + warpings.warpStoneFactor) * vStep);
        point[3] = this.baseStone.GetPointOnTorus((warpings.depthIndex + warpings.depthFactor) * uStep, (warpings.startIndex + warpings.warpStoneFactor) * vStep);
        point[4] = this.baseStone.CreateTriangleEndpoint((warpings.depthIndex) * (uStep / 2), (warpings.startIndex + warpings.warpStoneFactor) * (vStep / 2)); 

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

        return vertices;
    }

    private int[] SetTriangles() 
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

        return triangles;
    }
}
