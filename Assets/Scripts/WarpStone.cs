using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpStone : MonoBehaviour {

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private HardStone hardStone = new HardStone();
    private SmothStone smothStone = new SmothStone();
    private BlockStone blockStone = new BlockStone();
    private BlockWarpStone smothBlockStone = new BlockWarpStone();
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
                this.hardStone.warpings = warpings;
                this.mesh.name = "WarpStone";
                this.mesh = this.hardStone.Create(mesh);
                break;
            case Warpings.WarpStoneMode.Smoth:
                this.smothStone.warpings = warpings;
                this.mesh.name = "SmoothWarpStone";
                this.mesh = this.smothStone.Create(mesh);
                break;
            case Warpings.WarpStoneMode.WarpBlock:
                this.blockStone.warpings = warpings;
                this.mesh.name = "WarpBlock";
                this.mesh = this.blockStone.Create(mesh);
                break;
            case Warpings.WarpStoneMode.SmothWarpBlock:
                this.smothBlockStone.warpings = warpings;
                this.mesh.name = "SmoothWarpBlock";
                this.mesh = this.smothBlockStone.Create(mesh);
                break;
        }

        this.AddMeshCollider();
        mesh.RecalculateNormals();
    }

    public void AddMeshCollider() 
    {
        MeshCollider meshc = gameObject.AddComponent<MeshCollider>();
        meshc.sharedMesh = mesh;
        meshc.convex = true;
        meshc.isTrigger = true;
    }
}