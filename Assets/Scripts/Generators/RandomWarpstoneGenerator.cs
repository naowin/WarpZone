using UnityEngine;

public class RandomWarpstoneGenerator : WarpstoneGenerator {

    public WarpStone warpStonePrefab;
    public WarpSettings.WarpStoneMode[] warpstone;
   
    public override void GenerateWarpstones(Warp warp)
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
                    warpstone[Random.Range(0, 6)],
                    false);
                wStone.transform.SetParent(warp.transform, false);
                wStone.Generate();
            }

            i += depthFactor;
        }
    }
}
