using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpSystem : MonoBehaviour {

    public WarpSection warpSecPrefab;
    public int warpCount;

    private WarpSection[] warps;

    void Awake()
    {
        warps = new WarpSection[warpCount];

        for(int i = 0 ; i < warps.Length; i++)
        {
            WarpSection ws = warps[i] = Instantiate<WarpSection>(warpSecPrefab);
            ws.transform.SetParent(transform, false);
            ws.Generate();
            if(i > 0) {
                ws.AlignWith(warps[i - 1]);
            }
        }

        AlignNextWarpWithOrigin();
    }

    public WarpSection SetupFirstWarp()
    {
        transform.localPosition = new Vector3(0f, -warps[1].CurveRadius);
        return warps[1];
    }

    public WarpSection SetupNextWarp()
    {
        WarpShift();
        AlignNextWarpWithOrigin();
        warps[warps.Length - 1].Generate();
        warps[warps.Length - 1].AlignWith(warps[warps.Length - 2]);
        transform.localPosition = new Vector3(0f, -warps[1].CurveRadius);
        return warps[1];
    }

    private void AlignNextWarpWithOrigin()
    {
        Transform transformToAlign = warps[1].transform;
        for(int i = 0; i < warps.Length; i ++)
        {
            if(i != 1)
            {
                warps[i].transform.SetParent(transformToAlign);
            }
        }

        transformToAlign.localPosition = Vector3.zero;
        transformToAlign.localRotation = Quaternion.identity;

        for(int i = 0; i < warps.Length; i++) {
            if(i != 1)
            {
                warps[i].transform.SetParent(transform);
            }
        }
    }

    private void WarpShift()
    {
        WarpSection ws = warps[0];
        for(int i = 1; i < warps.Length; i ++)
        {
            warps[i - 1] = warps[i];
        }

        warps[warps.Length - 1] = ws;
    }

	// Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
