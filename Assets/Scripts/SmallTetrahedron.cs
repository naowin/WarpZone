using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallTetrahedron : MonoBehaviour {

    public float warpRadius = 1;
    public int warpSegmentCount = 24;
    public float curveRadius = 20;
    public int curveSegmentCount = 20;
    public float ringDistance = 0.77f;

    private Mesh mesh;
    private Mesh lem;
    private Vector3[] vertices;
    private int[] triangles;

    private Warp ws;

    void Awake () 
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "SmallTetrahedron";
    }

    public void Generate(Warp ws) 
    {
        lem = ws.GetComponent<MeshFilter>().mesh;
        Debug.Log(ws.CurveAngle + " : " + ws.CurveRadius);
        mesh.Clear();
        SetVertices(1, 1);
        //SetUv();
        SetTriangles();
        mesh.RecalculateNormals();
    }
       
    public void SetVertices(int u, int v) 
    {
        float vStep = (2f * Mathf.PI) / warpSegmentCount;
        float uStep = ringDistance / curveSegmentCount; 

        vertices = new Vector3[5];


        // VertexA
        vertices[0] = lem.vertices[0];
        vertices[1] = lem.vertices[1];

        // VertexB
        vertices[2] = lem.vertices[0];
        vertices[3] = lem.vertices[1];



        //Vector3 endpoint = CreateEndpoint();
        vertices[4] = lem.vertices[96];

        mesh.vertices = vertices;
    }

    public void SetTriangles() 
    {
        triangles = new int[4 * 6];
        triangles[0] = 1;
        triangles[1] = 0;
        triangles[2] = 4;

        triangles[3] = 3;
        triangles[4] = 1;
        triangles[5] = 4;

        triangles[6] = 2;
        triangles[7] = 3;
        triangles[8] = 4;

        triangles[9] = 0;
        triangles[10] = 2;
        triangles[11] = 4;

        mesh.triangles = triangles;
    }

    private Vector3 CreateEndpoint()
    {
        Vector3 endPoint;
        float halfDeepthStep = 0.5f;//(ringDistance / curveSegmentCount) / 9; 
        float halfSideStep = 0.5f;//((2f * Mathf.PI) / warpSegmentCount) / 2;
        float r = (curveRadius + (warpRadius / 2) * Mathf.Cos(halfSideStep));
        float r2 = (curveRadius + (0.7f) * Mathf.Cos(halfSideStep));
        endPoint.x = r * Mathf.Sin(halfDeepthStep);
        endPoint.y = r2 * Mathf.Cos(halfDeepthStep);
        endPoint.z = warpRadius * Mathf.Sin(halfSideStep);

        return endPoint;
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


    // {} []
   
}

/*


    float vStep = (2f * Mathf.PI) / warpSegmentCount;

    Vector3 vertexA = GetPointOnTorus(0f, 0f);
    Vector3 vertexB = GetPointOnTorus(u, 0f);
    var i = 0;
    for(int v = 1; v <= warpSegmentCount; v++) 
    }
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


    private void addTetraHedron()
    {
        /*
        var newvertices = new Vector3[(warpSegmentCount * curveSegmentCount * 4) + 1];

        for(var i = 0 ; i < mesh.vertices.Length; i++){
            newvertices[i] = mesh.vertices[i];
        }
          
        float halfDeepthStep = (ringDistance / curveSegmentCount) / 9; 
        float halfSideStep = ((2f * Mathf.PI) / warpSegmentCount) / 2;
        Vector3 p;
        float r = (curveRadius + (warpRadius / 2) * Mathf.Cos(halfSideStep));
        float r2 = (curveRadius + (0.7f) * Mathf.Cos(halfSideStep));
        p.x = r * Mathf.Sin(halfDeepthStep);
        p.y = r2 * Mathf.Cos(halfDeepthStep);
        p.z = warpRadius * Mathf.Sin(halfSideStep);
 
        Debug.Log("cr: " + curveRadius + " wr: " + warpRadius);
        Debug.Log("vert1: " + mesh.vertices[0]);
        Debug.Log("vert2: " + mesh.vertices[1]);
        Debug.Log("vertlast: " + p);
        */

        /*
        newvertices[newvertices.Length - 1] = p;
        vertices = mesh.vertices = newvertices;

        //SetUv();

        var newtri = new int[(warpSegmentCount * curveSegmentCount * 6) + 12];
        for(var i = 0 ; i < mesh.triangles.Length; i++){
            newtri[i] = mesh.triangles[i];
        }

        newtri[mesh.triangles.Length] = 1;
        newtri[mesh.triangles.Length + 1] = 0;
        newtri[mesh.triangles.Length + 2] = mesh.vertices.Length - 1;


        newtri[mesh.triangles.Length + 3] = 0;
        newtri[mesh.triangles.Length + 4] = 96;
        newtri[mesh.triangles.Length + 5] = mesh.vertices.Length - 1;


        newtri[mesh.triangles.Length + 6] = 97;
        newtri[mesh.triangles.Length + 7] = 1;
        newtri[mesh.triangles.Length + 8] = mesh.vertices.Length - 1;

        newtri[mesh.triangles.Length + 9] = 96;
        newtri[mesh.triangles.Length + 10] = 97;
        newtri[mesh.triangles.Length + 11] = mesh.vertices.Length - 1;


        // mesh.triangles = newtri;

        //mesh.RecalculateNormals();
//{}

}

*/