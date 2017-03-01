using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpSystem : MonoBehaviour {

    public Warp warpPrefab;
    public WarpStone warpStonePrefab;
    public int warpCount;

    private Warp[] warps;
    private Warpings.WarpStoneMode[] warpstone; 
    private WarpStone wrpstn = new WarpStone();

    void Awake()
    {
        warpstone = new Warpings.WarpStoneMode[7];
        warpstone[0] = Warpings.WarpStoneMode.Smoth;
        warpstone[1] = Warpings.WarpStoneMode.Smoth;
        warpstone[3] = Warpings.WarpStoneMode.Smoth;
        warpstone[4] = Warpings.WarpStoneMode.Smoth;
        warpstone[5] = Warpings.WarpStoneMode.Smoth;
        warpstone[6] = Warpings.WarpStoneMode.WarpBlock;

        warps = new Warp[warpCount];

        for(int i = 0 ; i < warps.Length; i++)
        {
            Warp warp = warps[i] = Instantiate<Warp>(warpPrefab);
            warp.transform.SetParent(transform, false);
        }
    }

    public Warp SetupFirstWarp()
    {
        for(int i = 0; i < warps.Length; i++)
        {
            Warp warp = warps[i];
            warp.Generate();
            if(i > 0) {
                warp.AlignWith(warps[i - 1]);
            }

            if(i > 1)
            {
                GenerateVoidStone(warps[i]);
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
        warps[warps.Length - 1].Generate();
        GenerateVoidStone(warps[warps.Length - 1]);
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
        Warp ws = warps[0];
        for(int i = 1; i < warps.Length; i ++)
        {
            warps[i - 1] = warps[i];
        }

        warps[warps.Length - 1] = ws;
    }

    private void GenerateVoidStone(Warp warp)
    {
        switch(Random.Range(0,5))
        {
            case 0:
                GenerateSpiralVoidStone(warp);
                break;
            case 1: 
            case 2:
            case 3:
            case 4:
            case 5:
                GenerateNormalVoidStone(warp);
                break;
        }
    }

    private void GenerateNormalVoidStone(Warp warp) 
    {
        for(int i = 0; i < warp.CurveSegmentCount; i++)
        {
            int[] indexes = new int[warp.warpSegmentCount];
            int numberOfWarpstones = Random.Range(1, 4);
            int depthFactor = 0;
            for(int n = 0; n < numberOfWarpstones; n++)
            {
                int startIndex = Random.Range(0, warp.warpSegmentCount);
                while(indexes[startIndex] > 0)
                {
                    startIndex = Random.Range(0, warp.warpSegmentCount);
                }

                indexes[startIndex] = 1;
                int warpStoneFactor = Random.Range(1, 3);
                if (warpStoneFactor > depthFactor)
                {
                    depthFactor = warpStoneFactor;
                }
                
                WarpStone wStone = Instantiate<WarpStone>(warpStonePrefab);
                wStone.warpings = new Warpings(
                    startIndex, 
                    i,
                    warp.warpRadius, 
                    warp.warpSegmentCount, 
                    warp.ringDistance,
                    warp.CurveRadius, 
                    warp.CurveSegmentCount,
                    warp.RelativeRotation,
                    warpStoneFactor,
                    warpstone[Random.Range(0,6)],
                    false);
                wStone.transform.SetParent(warp.transform, false);
                wStone.Generate();
            }

            i += depthFactor;
        }
    }

    private void GenerateSpiralVoidStone(Warp warp)
    {
        int direction = Random.value < 0.5f ? 1 : -1;
        int startIndex = Random.Range(0, warp.warpSegmentCount);

        for(int i = 0 ; i < warp.CurveSegmentCount; i++)
        {
            int warpStoneFactor = Random.Range(1, 2);

            WarpStone wStone = Instantiate<WarpStone>(warpStonePrefab);
            wStone.warpings = new Warpings(
                startIndex, 
                i,
                warp.warpRadius, 
                warp.warpSegmentCount, 
                warp.ringDistance,
                warp.CurveRadius, 
                warp.CurveSegmentCount,
                warp.RelativeRotation,
                warpStoneFactor,
                warpstone[6],
                false);
            wStone.transform.SetParent(warp.transform, false);
            wStone.Generate();

            if(i % 2 == 0)
            {
                wStone = Instantiate<WarpStone>(warpStonePrefab);
                wStone.warpings = new Warpings(
                    Random.Range(0, warp.warpSegmentCount), 
                    i,
                    warp.warpRadius, 
                    warp.warpSegmentCount, 
                    warp.ringDistance,
                    warp.CurveRadius, 
                    warp.CurveSegmentCount,
                    warp.RelativeRotation,
                    1,
                    warpstone[0],
                    false);
                wStone.transform.SetParent(warp.transform, false);
                wStone.Generate();
            }

            i += warpStoneFactor;
            startIndex += warpStoneFactor * direction;
        }
    }

    // {}
}
