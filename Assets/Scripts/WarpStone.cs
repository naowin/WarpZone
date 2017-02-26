using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpStone : MonoBehaviour {

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private HardStone hardStone = new HardStone();
    private SmothStone smothStone  = new SmothStone();
    private int depthFactor = 1;

    public Warpings warpings { get; set;}

    void Awake() 
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "WarpStone";
    }

    public void Generate() 
    {        
        mesh.Clear();
        // depending on mode
        switch(warpings.mode) {
            case Warpings.WarpStoneMode.TetraHedron:
                hardStone.warpings = warpings;
                mesh = hardStone.Create(mesh);
                break;
            case Warpings.WarpStoneMode.Smoth:
                smothStone.warpings = warpings;
                mesh = smothStone.Create(mesh);
                break;
        }

        mesh.RecalculateNormals();
    }

    // {}
        
}