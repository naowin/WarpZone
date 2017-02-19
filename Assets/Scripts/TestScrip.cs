using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScrip : MonoBehaviour {


    public GameObject gameobj;

    void Awake() {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        /*
        var triangs = mesh.triangles;
        for(var i = 0; i < triangs.Length; i++) 
        {
            triangs[i] = triangs[i] +1;
        }
        mesh.triangles = triangs;
        */

        var verts = mesh.vertices;
        Debug.Log("Verts: " + verts.Length);
        //for(var i = 0; i < verts.Length; i++) {
            verts[0].z = -1;
        //}
        mesh.vertices = verts;
    }

    // Use this for initialization
	void Start () {
     
	}
    	
	// Update is called once per frame
	void Update () {
		
	}
}
