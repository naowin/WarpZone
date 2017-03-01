using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStone {

    public Warpings warpings { get; set;}

    public Vector2[] SetTriangleUvs(Mesh mesh) 
    {
        var uvs = new Vector2[mesh.vertices.Length];
        for(int i = 0; i < uvs.Length; i += 3) 
        {
            uvs[i] = new Vector2(0.1f, 0);
            uvs[i + 1] = new Vector2(0.1f, 1);
            uvs[i + 2] = new Vector2(0.5f, 0.5f);
        }  

        return uvs;
    } 

    public Vector3 GetPointOnTorus (float u, float v) 
    {
        Vector3 p;
        float r = (warpings.curveRadius + warpings.warpRadius * Mathf.Cos(v));
        p.x = r * Mathf.Sin(u);
        p.y = r * Mathf.Cos(u);
        p.z = warpings.warpRadius * Mathf.Sin(v);
        return p;
    }

    public Vector3 GetPointOnTorusSmallerRadius (float u, float v) 
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

    public Vector3 CreateTriangleEndpoint(float u, float v)
    {
        float vStep = (2f * Mathf.PI) / warpings.warpSegmentCount;
        float uStep = warpings.ringDistance / warpings.curveSegmentCount; 
        Vector3 verticeA = GetPointOnTorusSmallerRadius(warpings.depthIndex * uStep, warpings.startIndex * vStep);
        Vector3 verticeB = GetPointOnTorusSmallerRadius((warpings.depthIndex + 1) * uStep, warpings.startIndex * vStep);
        Vector3 verticeC = GetPointOnTorusSmallerRadius(warpings.depthIndex * uStep, (warpings.startIndex + warpings.warpStoneFactor) * vStep);
        Vector3 verticeD = GetPointOnTorusSmallerRadius((warpings.depthIndex + 1) * uStep, (warpings.startIndex + warpings.warpStoneFactor) * vStep);
        Vector3 endPoint = (verticeA + verticeB + verticeC + verticeD) / 4;

        return endPoint;
    }
}
