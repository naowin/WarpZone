using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpSystem : MonoBehaviour {

    public Warp warpPrefab;
    public WarpStone warpStonePrefab;
    public int warpCount;

    private Warp[] warps;

    void Awake()
    {
        warps = new Warp[warpCount];

        for(int i = 0 ; i < warps.Length; i++)
        {
            Warp ws = warps[i] = Instantiate<Warp>(warpPrefab);
            ws.transform.SetParent(transform, false);
            ws.Generate();
            if(i > 0) {
                ws.AlignWith(warps[i - 1]);
            }

            GenerateVoidStone(warps[i]);
        }

      AlignNextWarpWithOrigin();
    }

    public Warp SetupFirstWarp()
    {
        transform.localPosition = new Vector3(0f, -warps[1].CurveRadius);
        return warps[1];
    }

    public Warp SetupNextWarp()
    {
        WarpShift();
        AlignNextWarpWithOrigin();
        warps[warps.Length - 1].Generate();
        GenerateVoidStone(warps[warps.Length - 1]);
        warps[warps.Length - 1].AlignWith(warps[warps.Length - 2]);
        transform.localPosition = new Vector3(0f, -warps[1].CurveRadius);
        return warps[1];
    }

    private void GenerateVoidStone(Warp warp) 
    {
        int nrOfTags = Random.Range(8, warp.CurveSegmentCount);
        for(int n = 0; n < nrOfTags; n++)
        {
            int startIndex = Random.Range(0, warp.warpSegmentCount);
            int depthIndex = Random.Range(0, warp.CurveSegmentCount);
            WarpStone wStone = Instantiate<WarpStone>(warpStonePrefab);
            wStone.warpings = new Warpings(
                startIndex, 
                depthIndex,
                warp.warpRadius, 
                warp.warpSegmentCount, 
                warp.ringDistance,
                warp.CurveRadius, 
                warp.CurveSegmentCount,
                warp.RelativeRotation);
            wStone.transform.SetParent(warp.transform, false);
            wStone.Generate();
        }

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
        Warp ws = warps[0];
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
