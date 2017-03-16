using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour {

    public float warpRadius;
    public int warpSegmentCount;
    public float ringDistance; 

    public float minCurveRadius, maxCurveRadius;
    public int minCurveSegmentCount, maxCurveSegmentCount;
    public WarpstoneGenerator[] warpstones;

    private float curveRadius;
    private int curveSegmentCount;

    private float relativeRotation;

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private float curveAngle;

    private Vector2[] uv;

    private void Awake () 
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Warp";
    }

    public void Generate() 
    {
        // Remove warpstones... becuz messy...
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        curveRadius = Random.Range(minCurveRadius, maxCurveRadius);
        curveSegmentCount = Random.Range(minCurveSegmentCount, maxCurveSegmentCount + 1);
        mesh.Clear();
        SetVertices();
        SetUvs();
        SetTriangles();
        mesh.RecalculateNormals();
    }

    public void GenerateVoidStone(int gameMode)
    {
        if(gameMode > 0)
        {
            WarpstoneGenerator wsGen = warpstones[gameMode];
            wsGen.GenerateWarpstones(this);
        }
    }


    private void SetTriangles () 
    {
        triangles = new int[warpSegmentCount * curveSegmentCount * 6];

        var i = 0;
        for(int t = 0; t < triangles.Length; t +=6)
        {
            triangles[t] = i;
            triangles[t + 1] = triangles[t + 4] = i + 2;
            triangles[t + 2] = triangles[t + 3] = i + 1;
            triangles[t + 5] = i + 3;

            i += 4;
        }

        mesh.triangles = triangles;    
    }

    private void SetUvs()
    {
        uv = new Vector2[vertices.Length];
        for(int i = 0; i < vertices.Length; i += 4)
        {
            uv[i] = Vector2.zero;
            uv[i + 1] = Vector2.right;
            uv[i + 2] = Vector2.up;
            uv[i + 3] = Vector2.one;
        }

        mesh.uv = uv;
    }

    private void SetVertices () 
    {
        vertices = new Vector3[warpSegmentCount * curveSegmentCount * 4];
        float uStep = ringDistance / curveSegmentCount; 
        curveAngle = uStep * curveSegmentCount * (360f / (2f * Mathf.PI));
        CreateFirstRing(uStep);
        int iDelta = warpSegmentCount * 4;
        int i = iDelta;
        for(var u = 2; u <= curveSegmentCount; u++) 
        {
            CreateQuadRing(u * uStep, i);
            i += iDelta;
        }

        mesh.vertices = vertices;
    }

    private void CreateFirstRing(float u) 
    {
        float vStep = (2f * Mathf.PI) / warpSegmentCount;

        Vector3 vertexA = GetPointOnTorus(0f, 0f);
        Vector3 vertexB = GetPointOnTorus(u, 0f);
        var i = 0;
        for(int v = 1; v <= warpSegmentCount; v++) 
        {
            // VertexA
            vertices[i] = vertexA;
            vertices[i + 1] = vertexA = GetPointOnTorus(0f, v * vStep);

            // VertexB
            vertices[i + 2] = vertexB;
            vertices[i + 3] = vertexB = GetPointOnTorus(u, v * vStep);

            // inc i for NextQuad
            i += 4;
        }
    }

    private void CreateQuadRing(float u, int i)
    {
        float vStep = (2f * Mathf.PI) / warpSegmentCount;
        int ringOffset = warpSegmentCount * 4;

        Vector3 vertex = GetPointOnTorus(u, 0f);
        for(int v = 1; v <= warpSegmentCount; v++) 
        {
            vertices[i] = vertices[i - ringOffset + 2];
            vertices[i + 1] = vertices[i - ringOffset + 3];
            vertices[i + 2] = vertex;
            vertices[i + 3] = vertex = GetPointOnTorus(u, v * vStep);
            i += 4;
        }
    }

    private Vector3 GetPointOnTorus (float u, float v) 
    {
        Vector3 p;
        float r = (curveRadius + warpRadius * Mathf.Cos(v));
        p.x = r * Mathf.Sin(u);
        p.y = r * Mathf.Cos(u);
        p.z = warpRadius * Mathf.Sin(v);
        return p;
    }

    public void AlignWith(Warp warp)
    {
        relativeRotation = Random.Range(0, curveSegmentCount) * 360f / warpSegmentCount;

        transform.SetParent(warp.transform, false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0f, 0f, -warp.curveAngle);
        transform.Translate(0f, warp.curveRadius, 0f);
        transform.Rotate(relativeRotation, 0f, 0f);
        transform.Translate(0f, -curveRadius, 0f);
        transform.SetParent(warp.transform.parent);
        transform.localScale = Vector3.one;
    }


    public float CurveRadius {
        get {
            return curveRadius;
        }
    }

    public float CurveAngle {
        get {
            return curveAngle;
        }
    }

    public int CurveSegmentCount {
        get {
            return curveSegmentCount;
        }
    }

    public float RelativeRotation {
        get {
            return relativeRotation;
        }
    }
}
