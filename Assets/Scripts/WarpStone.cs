using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpStone : MonoBehaviour {

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private HardWarpStone hardStone = new HardWarpStone();
    private SmothWarpStone smothStone = new SmothWarpStone();
    private BlockWarpStone blockStone = new BlockWarpStone();
    private HardBlockWarpStone hardBlockStone = new HardBlockWarpStone();
    public WarpSettings warpSettings { get; set;}

    void Awake() 
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "WarpStone";
    }

    public void Generate() 
    {        
        mesh.Clear();
        // depending on mode
        switch(warpSettings.mode) {
            case WarpSettings.WarpStoneMode.TetraHedron:
                this.hardStone.warpSettings = warpSettings;
                this.mesh.name = "WarpStone";
                this.mesh = this.hardStone.Create(mesh);
                break;
            case WarpSettings.WarpStoneMode.Smoth:
                this.smothStone.warpSettings = warpSettings;
                this.mesh.name = "SmoothWarpStone";
                this.mesh = this.smothStone.Create(mesh);
                break;
            case WarpSettings.WarpStoneMode.SmothWarpBlock:
                this.blockStone.warpSettings = warpSettings;
                this.mesh.name = "WarpBlock";
                this.mesh = this.blockStone.Create(mesh);
                break;
            case WarpSettings.WarpStoneMode.WarpBlock:
                this.hardBlockStone .warpSettings = warpSettings;
                this.mesh.name = "SmoothWarpBlock";
                this.mesh = this.hardBlockStone .Create(mesh);
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