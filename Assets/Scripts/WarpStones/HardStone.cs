using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardWarpStone{

    public WarpSettings warpSettings { get; set;}
   

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private BaseWarpStone baseWarpStone = new BaseWarpStone();

    public Mesh Create(Mesh mesh)
    {
        this.mesh = mesh;
        this.baseWarpStone.warpSettings = warpSettings;
        this.mesh.vertices = SetVertices();
        if(warpSettings.useUvs)
        {
            this.mesh.uv = this.baseWarpStone.SetTriangleUvs(mesh);
        }

        this.mesh.triangles = SetTriangles();
        return mesh;
    }

    private Vector3[] SetVertices()
    {
        vertices = new Vector3[12];
        Vector3[] point = new Vector3[5];
        float vStep = (2f * Mathf.PI) / warpSettings.warpSegmentCount;
        float uStep = warpSettings.ringDistance / warpSettings.curveSegmentCount; 
        point[0] = this.baseWarpStone.GetPointOnTorus(warpSettings.depthIndex * uStep, warpSettings.startIndex * vStep);
        point[1] = this.baseWarpStone.GetPointOnTorus((warpSettings.depthIndex + warpSettings.depthFactor) * uStep, warpSettings.startIndex * vStep);
        point[2] = this.baseWarpStone.GetPointOnTorus(warpSettings.depthIndex * uStep, (warpSettings.startIndex + warpSettings.warpStoneFactor) * vStep);
        point[3] = this.baseWarpStone.GetPointOnTorus((warpSettings.depthIndex + warpSettings.depthFactor) * uStep, (warpSettings.startIndex + warpSettings.warpStoneFactor) * vStep);
        point[4] = this.baseWarpStone.CreateTriangleEndpoint((warpSettings.depthIndex) * (uStep / 2), (warpSettings.startIndex + warpSettings.warpStoneFactor) * (vStep / 2)); 

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
        triangles = new int[warpSettings.warpStoneFactor * 6 + 6];

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
