using System.Collections.Generic;
using UnityEngine;

public class PyramidGenerator: WarpstoneGenerator {

    public WarpStone warpStonePrefab;
    public WarpSettings.WarpStoneMode[] warpstone;
    private List<int> warpStoneFactors = new List<int>() {1,1,1,1,2};

    public override void GenerateWarpstones(Warp warp)
    {
        for(int i = 0; i < warp.CurveSegmentCount; i++)
        {
            int[] indexes = new int[warp.warpSegmentCount];

            int numberOfWarpstones = Random.Range(1, warp.maxNumberOfWarpStones);
            for(int n = 0; n < numberOfWarpstones; n++)
            {
                int startIndex = Random.Range(0, warp.warpSegmentCount);
                while(indexes[startIndex] > 0)
                {
                    startIndex = Random.Range(0, warp.warpSegmentCount);
                }

                indexes[startIndex] = 1;
                int warpStoneFactor = warpStoneFactors[Random.Range(0, 5)];
                if(warp.warpSegmentCount < 20)
                {
                    warpStoneFactor = 1;
                }

                WarpStone wStone = Instantiate<WarpStone>(warpStonePrefab);
                wStone.warpSettings = new WarpSettings(
                    startIndex, 
                    i,
                    warp.warpRadius, 
                    warp.warpSegmentCount, 
                    warp.ringDistance,
                    warp.CurveRadius, 
                    warp.CurveSegmentCount,
                    warp.RelativeRotation,
                    warpStoneFactor,
                    WarpSettings.WarpStoneMode.TetraHedron,
                    false);
                wStone.transform.SetParent(warp.transform, false);
                wStone.Generate();
            }

            i += warp.warpStoneDistance;
        }
    }
}
