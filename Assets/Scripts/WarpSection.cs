using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpSection : MonoBehaviour {

    public float warpRadius;
    public int warpSegmentCount;
    public float ringDistance; 

    public float minCurveRadius, maxCurveRadius;
    public int minCurveSegmentCount, maxCurveSegmentCount;

    private float curveRadius;
    private int curveSegmentCount;

    private float relativeRotation;

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private float curveAngle;

    private Vector2[] uv;

    // no longer need this :)
    private void OnDrawGizmos () 
    {
        curveRadius = 4;
        curveSegmentCount = 20;
        float uStep = (2f * Mathf.PI) / curveSegmentCount;
        float vStep = (2f * Mathf.PI) / warpSegmentCount;

        for (int u = 0; u < curveSegmentCount; u++) {
            for (int v = 0; v < warpSegmentCount; v++) {
                Vector3 point = GetPointOnTorus(u * uStep, v * vStep);
                Gizmos.DrawSphere(point, 0.1f);
            }
        }

    }

    private void Awake () 
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Warp";
    }

    public void Generate() 
    {
        curveRadius = Random.Range(minCurveRadius, maxCurveRadius);
        curveSegmentCount = Random.Range(minCurveSegmentCount, maxCurveSegmentCount + 1);
        mesh.Clear();
        SetVertices();
        SetUv();
        SetTriangles();
        mesh.RecalculateNormals();

        addTetraHedron();
    }

    private void addTetraHedron()
    {
        vertices = new Vector3[(warpSegmentCount * curveSegmentCount * 4) + 1];
        vertices = mesh.vertices;
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

    private void SetVertices () 
    {
        vertices = new Vector3[warpSegmentCount * curveSegmentCount * 4];

        //float uStep = (2f * Mathf.PI) / curveSegmentCount;
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

    private void SetUv()
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
            vertexA = GetPointOnTorus(0f, v * vStep);
            vertices[i + 1] = vertexA;

            // VertexB
            vertices[i + 2] = vertexB;
            vertexB = GetPointOnTorus(u, v * vStep);
            vertices[i + 3] = vertexB;

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
            vertex = GetPointOnTorus(u, v * vStep);
            vertices[i + 3] = vertex;
            i += 4;
        }
    }

    public void AlignWith(WarpSection ws)
    {
        relativeRotation = Random.Range(0, curveSegmentCount) * 360f / warpSegmentCount;

        transform.SetParent(ws.transform, false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0f, 0f, -ws.curveAngle);
        transform.Translate(0f, ws.curveRadius, 0f);
        transform.Rotate(relativeRotation, 0f, 0f);
        transform.Translate(0f, -curveRadius, 0f);
        transform.SetParent(ws.transform.parent);
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

    public float RelativeRotation {
        get {
            return relativeRotation;
        }
    }
}

/*
private Pipe[] pipes;

private void Awake () {
    pipes = new Pipe[pipeCount];
    for (int i = 0; i < pipes.Length; i++) {
        Pipe pipe = pipes[i] = Instantiate<Pipe>(pipePrefab);
        pipe.transform.SetParent(transform, false);
    }
}

public Pipe SetupFirstPipe () {
    for (int i = 0; i < pipes.Length; i++) {
        Pipe pipe = pipes[i];
        pipe.Generate(i > emptyPipeCount);
        if (i > 0) {
            pipe.AlignWith(pipes[i - 1]);
        }
    }
    AlignNextPipeWithOrigin();
    transform.localPosition = new Vector3(0f, -pipes[1].CurveRadius);
    return pipes[1];
}

public Pipe SetupNextPipe () {
    ShiftPipes();
    AlignNextPipeWithOrigin();
    pipes[pipes.Length - 1].Generate();
    pipes[pipes.Length - 1].AlignWith(pipes[pipes.Length - 2]);
    transform.localPosition = new Vector3(0f, -pipes[1].CurveRadius);
    return pipes[1];
}

private void ShiftPipes () {
    Pipe temp = pipes[0];
    for (int i = 1; i < pipes.Length; i++) {
        pipes[i - 1] = pipes[i];
    }
    pipes[pipes.Length - 1] = temp;
}

private void AlignNextPipeWithOrigin () {
    Transform transformToAlign = pipes[1].transform;
    for (int i = 0; i < pipes.Length; i++) {
        if (i != 1) {
            pipes[i].transform.SetParent(transformToAlign);
        }
    }

    transformToAlign.localPosition = Vector3.zero;
    transformToAlign.localRotation = Quaternion.identity;

    for (int i = 0; i < pipes.Length; i++) {
        if (i != 1) {
            pipes[i].transform.SetParent(transform);
        }
    }
}
}*/