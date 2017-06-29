using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpSystem : MonoBehaviour {

    public Warp warpPrefab;
    public WarpStone warpStonePrefab;
    public Warper warper;
    public int warpCount;


    private Warp[] warps;

    void Awake()
    {
        warps = new Warp[warpCount];

        for(int i = 0 ; i < warps.Length; i++)
        {
            Warp warp = warps[i] = Instantiate<Warp>(warpPrefab);
            warp.transform.SetParent(transform, false);
        }
    }

    public void UpdateWarpPrefab()
    {
        Debug.Log(warpPrefab.warpRadius);
    }

    public int GameMode { get; set; }

    public Warp SetupFirstWarp()
    {
        for(int i = 0; i < warps.Length; i++)
        {
            Warp warp = warps[i];
            warp.Generate();
            if(i > 0) {
                warp.AlignWith(warps[i - 1]);
            }

            if(i > 1 && !warper.faded)
            {
                warp.GenerateVoidStone(GameMode);
            }
        }

        AlignNextWarpWithOrigin();
        transform.localPosition = new Vector3(0f, -warps[1].CurveRadius);
        return warps[1];
    }

    public Warp SetupNextWarp()
    {
        WarpShift();
        AlignNextWarpWithOrigin();
        SyncWarpWithPrefab(warps.Length - 1);
        warps[warps.Length - 1].Generate();
        if (!warper.faded)
        {
            warps[warps.Length - 1].GenerateVoidStone(GameMode);
        }
        
        warps[warps.Length - 1].AlignWith(warps[warps.Length - 2]);
        transform.localPosition = new Vector3(0f, -warps[1].CurveRadius);
        return warps[1];
    }

    private void SyncWarpWithPrefab(int warpIndex)
    {
        warps[warpIndex].warpRadius = warpPrefab.warpRadius;
        warps[warpIndex].warpSegmentCount = warpPrefab.warpSegmentCount;
        warps[warpIndex].ringDistance = warpPrefab.ringDistance;
        warps[warpIndex].minCurveRadius = warpPrefab.minCurveRadius;
        warps[warpIndex].maxCurveRadius = warpPrefab.maxCurveRadius;
        warps[warpIndex].minCurveSegmentCount = warpPrefab.minCurveSegmentCount;
        warps[warpIndex].maxCurveSegmentCount = warpPrefab.maxCurveSegmentCount;
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

    // {}
}
